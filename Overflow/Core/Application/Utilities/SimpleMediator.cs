using System.Collections.Concurrent;
using System.Reflection;
using Application.Exceptions;

namespace Application.Utilities;

public class SimpleMediator(IServiceProvider serviceProvider) : IMediator
{
    private static readonly ConcurrentDictionary<(Type, Type), Type> HandlerTypeCache = new();
    private static readonly ConcurrentDictionary<Type, MethodInfo> MethodCache = new();

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        var handlerType = HandlerTypeCache.GetOrAdd(
            (requestType, responseType),
            key => typeof(IRequestHandler<,>).MakeGenericType(key.Item1, key.Item2)
        );

        var handler =
            serviceProvider.GetService(handlerType)
            ?? throw new MediatorException($"未找到处理程序: {handlerType.Name}");

        var method = MethodCache.GetOrAdd(
            handlerType,
            t =>
                t.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle))
                ?? throw new MediatorException($"未找到Handle方法: {t.Name}")
        );

        var result = await (Task<TResponse>)method.Invoke(handler, [request])!;
        return result;
    }
}

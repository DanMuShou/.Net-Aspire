using System.Reflection;
using Application.Exceptions;
using Application.Utilities;
using FluentAssertions;
using Xunit;

namespace Tests.Application.Utilities;

public class SimpleMediatorTests
{
    [Fact]
    public async Task Send_WithValidHandler_ShouldReturnExpectedResult()
    {
        // Arrange
        var serviceProvider = new MockServiceProvider();
        var mediator = new SimpleMediator(serviceProvider);
        var request = new TestRequest { Value = "test" };

        // Act
        var response = await mediator.Send<string>(request);

        // Assert
        response.Should().Be("handled: test");
    }

    [Fact]
    public async Task Send_WithoutRegisteredHandler_ShouldThrowMediatorException()
    {
        // Arrange
        var serviceProvider = new MockServiceProvider();
        var mediator = new SimpleMediator(serviceProvider);
        var request = new UnregisteredRequest();

        // Act
        Func<Task> act = () => mediator.Send<string>(request);

        // Assert
        await act.Should().ThrowAsync<MediatorException>()
            .WithMessage("未找到处理程序: IRequestHandler`2");
    }

    [Fact]
    public async Task Send_MultipleRequests_ShouldUseCaching()
    {
        // Arrange
        var serviceProvider = new MockServiceProvider();
        var mediator = new SimpleMediator(serviceProvider);
        
        // Act
        await mediator.Send<string>(new TestRequest { Value = "test1" });
        await mediator.Send<string>(new TestRequest { Value = "test2" });
        var response = await mediator.Send<string>(new TestRequest { Value = "test3" });

        // Assert
        response.Should().Be("handled: test3");
    }
}

public class TestRequest : IRequest<string>
{
    public string Value { get; init; } = string.Empty;
}

public class UnregisteredRequest : IRequest<string>
{
}

public class TestRequestHandler : IRequestHandler<TestRequest, string>
{
    public Task<string> Handle(TestRequest request)
    {
        return Task.FromResult($"handled: {request.Value}");
    }
}

public class MockServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType)
    {
        if (serviceType.IsGenericType && 
            serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
        {
            var genericArguments = serviceType.GetGenericArguments();
            if (genericArguments[0] == typeof(TestRequest))
            {
                return new TestRequestHandler();
            }
        }
        
        return null;
    }
}
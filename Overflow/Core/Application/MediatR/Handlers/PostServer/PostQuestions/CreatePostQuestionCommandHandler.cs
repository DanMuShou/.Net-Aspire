using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

/// <summary>
/// 处理创建帖子问题命令的处理器类
/// </summary>
/// <param name="repository">帖子问题仓储接口，用于数据持久化操作</param>
/// <param name="validator">命令验证器，用于验证创建帖子问题命令的有效性</param>
/// <param name="unitOfWork">工作单元模式接口，用于管理事务操作</param>
public class CreatePostQuestionCommandHandler(
    IPostQuestionRepository repository,
    IPostTagRepository postTagRepository,
    IValidator<CreatePostQuestionCommand> validator,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreatePostQuestionCommand, Guid>
{
    /// <summary>
    /// 异步处理创建帖子问题命令
    /// </summary>
    /// <param name="request">创建帖子问题命令对象，包含帖子的标题、内容、用户信息等</param>
    /// <param name="cancellationToken">取消令牌，用于取消异步操作</param>
    /// <returns>返回新创建的帖子问题的唯一标识符(Guid)</returns>
    /// <exception cref="CustomValidationException">当命令验证失败时抛出</exception>
    public async Task<Guid> Handle(
        CreatePostQuestionCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        if (!await postTagRepository.AreTagListValidAsync(request.TagSlugs))
            throw new CustomValidationException("标签列表无效");

        var postQuestion = new Domain.Entity.PostServer.Post.PostQuestion(
            request.Title,
            request.Content,
            request.AskedByUserId,
            request.AskedByUserDisplayName,
            request.TagSlugs
        );

        try
        {
            var result = await repository.AddAsync(postQuestion);
            await unitOfWork.CommitAsync();
            return result.Id;
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}

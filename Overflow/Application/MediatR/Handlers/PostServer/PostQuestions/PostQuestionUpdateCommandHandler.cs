using Application.Common.Queues.Post;
using Application.Common.Response;
using Application.Contracts.Persistence;
using Application.Contracts.Repositories.PostServer;
using Application.DTO.PostServer.PostQuestions;
using Application.Exceptions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using FluentValidation;
using Mapster;
using MediatR;
using Wolverine;

namespace Application.MediatR.Handlers.PostServer.PostQuestions;

/// <summary>
/// 处理更新问题的命令处理器
/// </summary>
/// <param name="repository">问题仓储接口</param>
/// <param name="postTagRepository">标签仓储接口</param>
/// <param name="validator">更新命令验证器</param>
/// <param name="messageBus">消息总线接口</param>
/// <param name="unitOfWork">工作单元接口</param>
public class PostQuestionUpdateCommandHandler(
    IPostQuestionRepository repository,
    IPostTagRepository postTagRepository,
    IValidator<PostQuestionUpdateCommand> validator,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork
) : IRequestHandler<PostQuestionUpdateCommand, ServiceResponse<PostQuestionDto>>
{
    /// <summary>
    /// 处理更新问题命令
    /// </summary>
    /// <param name="request">更新问题命令请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>包含更新后问题数据的服务响应</returns>
    public async Task<ServiceResponse<PostQuestionDto>> Handle(
        PostQuestionUpdateCommand request,
        CancellationToken cancellationToken
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new CustomValidationException(validationResult);

        var missTags = await postTagRepository.AreTagListValidWithMissingTagsAsync(request.Tags);
        if (missTags.Count != 0)
            throw new DataMismatchException(
                $"标签列表中存在不存在的标签：{string.Join(", ", missTags)}"
            );

        var postQuestion =
            await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException();

        postQuestion.UpdateTitle(request.Title);
        postQuestion.UpdateContent(request.Content);
        postQuestion.UpdateTagSlugs(request.Tags);
        postQuestion.SetHasAcceptedAnswer(request.HasAcceptedAnswer);
        postQuestion.UpdateTime();

        try
        {
            await repository.UpdateAsync(postQuestion);
            await unitOfWork.CommitAsync();
            await messageBus.PublishAsync(postQuestion.Adapt<PostQuestionMqUpdated>());
            return ServiceResponse<PostQuestionDto>.ReturnResultWith200(
                postQuestion.Adapt<PostQuestionDto>()
            );
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}

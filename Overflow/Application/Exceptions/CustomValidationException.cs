using FluentValidation.Results;

namespace Application.Exceptions;

/// <summary>
/// 自定义验证异常类，用于处理验证错误
/// </summary>
/// <param name="validationErrors">验证错误消息列表</param>
public class CustomValidationException(List<string> validationErrors) : Exception
{
    /// <summary>
    /// 使用单个错误消息初始化自定义验证异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public CustomValidationException(string message)
        : this([message]) { }

    /// <summary>
    /// 使用FluentValidation的ValidationResult初始化自定义验证异常
    /// </summary>
    /// <param name="validationResult">验证结果对象</param>
    public CustomValidationException(ValidationResult validationResult)
        : this([.. validationResult.Errors.Select(x => x.ErrorMessage)]) { }

    /// <summary>
    /// 获取验证错误消息列表
    /// </summary>
    public List<string> ValidationErrors { get; private set; } = validationErrors;
}

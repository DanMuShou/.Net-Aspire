namespace Application.Exceptions;

/// <summary>
/// 表示在应用程序中找不到指定资源时抛出的异常
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// 初始化 NotFoundException 类的新实例
    /// </summary>
    public NotFoundException() { }

    /// <summary>
    /// 使用指定的错误消息初始化 NotFoundException 类的新实例
    /// </summary>
    /// <param name="message">描述错误的错误消息</param>
    public NotFoundException(string message)
        : base(message) { }
}

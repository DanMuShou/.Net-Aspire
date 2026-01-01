namespace Application.Exceptions;

/// <summary>
/// 表示数据不匹配异常的自定义异常类
/// </summary>
public class DataMismatchException : Exception
{
    /// <summary>
    /// 初始化 DataMismatchException 类的新实例
    /// </summary>
    public DataMismatchException() { }

    /// <summary>
    /// 用指定的错误消息初始化 DataMismatchException 类的新实例
    /// </summary>
    /// <param name="message">描述错误的错误消息</param>
    public DataMismatchException(string message)
        : base(message) { }
}

using System.Net;

namespace Application.Common.Response;

/// <summary>
/// 通用服务响应类，用于封装 API 响应数据
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
public class ServiceResponse<T>
{
    /// <summary>
    /// HTTP 状态码
    /// </summary>
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    /// <summary>
    /// 响应数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 错误信息列表
    /// </summary>
    public IList<string> Errors { get; set; } = [];

    /// <summary>
    /// 请求是否成功
    /// </summary>
    public bool Success => Errors == null || Errors.Count == 0;

    /// <summary>
    /// 初始化 <see cref="ServiceResponse{T}"/> 类的新实例
    /// </summary>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="data">响应数据</param>
    private ServiceResponse(int statusCode, T data)
    {
        StatusCode = statusCode;
        Data = data;
    }

    /// <summary>
    /// 初始化包含错误信息的 <see cref="ServiceResponse{T}"/> 类的新实例
    /// </summary>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="errors">错误信息列表</param>
    private ServiceResponse(int statusCode, List<string>? errors)
    {
        StatusCode = statusCode;
        Errors = errors ?? [];
    }

    /// <summary>
    /// 获取返回成功响应的实例
    /// </summary>
    public static ServiceResponse<T> ReturnSuccess => new((int)HttpStatusCode.OK, null);

    /// <summary>
    /// 返回带有指定状态码和错误信息列表的失败响应
    /// </summary>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="errors">错误信息列表</param>
    /// <returns>失败的响应实例</returns>
    public static ServiceResponse<T> ReturnFailed(int statusCode, List<string> errors) =>
        new(statusCode, errors);

    /// <summary>
    /// 返回带有指定状态码和单个错误信息的失败响应
    /// </summary>
    /// <param name="statusCode">HTTP 状态码</param>
    /// <param name="errorMessage">错误信息</param>
    /// <returns>失败的响应实例</returns>
    public static ServiceResponse<T> ReturnFailed(int statusCode, string errorMessage) =>
        new(statusCode, [errorMessage]);

    /// <summary>
    /// 返回带有数据的成功响应 (HTTP 状态码 200)
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>成功响应实例</returns>
    public static ServiceResponse<T> ReturnResultWith200(T data) =>
        new((int)HttpStatusCode.OK, data);

    /// <summary>
    /// 返回带有数据的成功响应 (HTTP 状态码 201)
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>成功响应实例</returns>
    public static ServiceResponse<T> ReturnResultWith201(T data) =>
        new((int)HttpStatusCode.Created, data);

    /// <summary>
    /// 返回带有数据的成功响应 (HTTP 状态码 204)
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>成功响应实例</returns>
    public static ServiceResponse<T> ReturnResultWith204(T data) =>
        new((int)HttpStatusCode.NoContent, data);

    /// <summary>
    /// 返回 400 错误响应 (Bad Request)
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return400(string message) =>
        new((int)HttpStatusCode.BadRequest, [message]);

    /// <summary>
    /// 返回 400 错误响应 (Bad Request)
    /// </summary>
    /// <param name="messages">错误信息列表</param>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return400(List<string> messages) =>
        new((int)HttpStatusCode.BadRequest, messages);

    /// <summary>
    /// 返回 404 错误响应 (Not Found)
    /// </summary>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return404() => new((int)HttpStatusCode.NotFound, ["未找到"]);

    /// <summary>
    /// 返回 404 错误响应 (Not Found)
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return404(string message) =>
        new((int)HttpStatusCode.NotFound, [message]);

    /// <summary>
    /// 返回 409 错误响应 (Conflict)
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return409(string message) =>
        new((int)HttpStatusCode.Conflict, [message]);

    /// <summary>
    /// 返回 422 错误响应 (Unprocessable Content)
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return422(string message) =>
        new((int)HttpStatusCode.UnprocessableContent, [message]);

    /// <summary>
    /// 返回 500 错误响应 (Internal Server Error)
    /// </summary>
    /// <returns>错误响应实例</returns>
    public static ServiceResponse<T> Return500() =>
        new((int)HttpStatusCode.InternalServerError, ["服务器发生了错误, 请稍后再试."]);
}

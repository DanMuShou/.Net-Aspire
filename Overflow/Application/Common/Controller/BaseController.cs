/// <summary>
/// 基础控制器类，提供通用的API响应格式化功能
/// </summary>
using Application.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Controller;

/// <summary>
/// 基础控制器，继承自ControllerBase
/// 提供通用的响应处理方法
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// 根据ServiceResponse的成功状态返回格式化的响应
    /// 如果响应成功返回Ok结果，否则返回BadRequest结果
    /// </summary>
    /// <typeparam name="T">响应数据的类型</typeparam>
    /// <param name="response">服务响应对象，包含操作结果和数据</param>
    /// <returns>如果响应成功返回OkObjectResult，否则返回BadRequestObjectResult</returns>
    public IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> response) =>
        response.Success ? Ok(response) : BadRequest(response);
}

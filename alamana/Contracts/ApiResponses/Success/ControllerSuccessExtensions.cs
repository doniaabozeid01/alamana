using Microsoft.AspNetCore.Mvc;

namespace alamana.Contracts.ApiResponses.Success
{
    public static class ControllerSuccessExtensions
    {
        // بدون Data
        public static IActionResult OkSuccess(this ControllerBase c, string en, string ar)
            => c.Ok(ApiSuccess.Ok(en, ar));

        // مع Data
        public static IActionResult OkSuccess<T>(this ControllerBase c, string en, string ar, T data)
            => c.Ok(ApiSuccess.Ok(en, ar, data));

        // Created
        public static IActionResult CreatedSuccess(this ControllerBase c, string en, string ar)
            => c.StatusCode(201, ApiSuccess.Created(en, ar));

        public static IActionResult CreatedSuccess<T>(this ControllerBase c, string en, string ar, T data)
            => c.StatusCode(201, ApiSuccess.Created(en, ar, data));

    }
}

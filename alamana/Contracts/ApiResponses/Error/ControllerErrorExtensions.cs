using Microsoft.AspNetCore.Mvc;

namespace alamana.Contracts.ApiResponses.Error
{
    public static class ControllerErrorExtensions
    {
        public static IActionResult BadRequestError(this ControllerBase c, string en, string ar)
        => c.BadRequest(ApiError.BadRequest(en, ar));

        public static IActionResult NotFoundError(this ControllerBase c, string en, string ar)
            => c.NotFound(ApiError.NotFound(en, ar));

        public static IActionResult ConflictError(this ControllerBase c, string en, string ar)
            => c.Conflict(ApiError.Conflict(en, ar));

        public static IActionResult InternalError(this ControllerBase c, string en, string ar)
            => c.StatusCode(500, ApiError.Internal(en, ar));
    }
}

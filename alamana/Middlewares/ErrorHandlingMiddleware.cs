//using alamana.Application.Common.Exceptions;
//using System.Net;
//using System.Text.Json;

//namespace alamana.Middlewares
//{
//    public class ErrorHandlingMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<ErrorHandlingMiddleware> _logger;

//        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Unhandled exception");

//                context.Response.ContentType = "application/json";
//                context.Response.StatusCode = ex switch
//                {
//                    NotFoundException => (int)HttpStatusCode.NotFound,
//                    BadRequestException => (int)HttpStatusCode.BadRequest,
//                    ConflictException => (int)HttpStatusCode.Conflict,
//                    _ => (int)HttpStatusCode.InternalServerError
//                };

//                var result = JsonSerializer.Serialize(new
//                {
//                    errorEn = ex.Message,
//                    errorAr = ex.,
//                    status = context.Response.StatusCode
//                });

//                await context.Response.WriteAsync(result);
//            }
//        }
//    }
//}















using System.Net;
using System.Text.Json;
using alamana.Application.Common.Exceptions;
using FluentValidation;

namespace alamana.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException oce) // المستخدم قفل الطلب أو الـCT اتلغى
        {
            _logger.LogWarning(oce, "Request was canceled.");
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = StatusCodes.Status499ClientClosedRequest; // أو 400/204 حسب تفضيلك
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorEn = "Request was canceled.",
                    errorAr = "تم إلغاء الطلب.",
                    status = context.Response.StatusCode
                }));
            }
        }
        catch (ValidationException ve) // FluentValidation
        {
            _logger.LogWarning(ve, "Validation failed.");
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ve.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    messageEn = e.ErrorMessage,
                    messageAr = "خطأ في التحقق من الحقل." // لو عندك رسائل عربية مخصصة، حطّها هنا
                });

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorEn = "Validation failed.",
                    errorAr = "فشل التحقق من البيانات.",
                    status = context.Response.StatusCode,
                    details = errors
                }));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var (status, en, ar) = MapException(ex);

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";

                var payload = new
                {
                    errorEn = en,
                    errorAr = ar,
                    status
                };

                var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });

                await context.Response.WriteAsync(json);
            }
        }
    }

    private static (int status, string en, string ar) MapException(Exception ex)
    {
        // لو استثناءاتك فيها خاصية ArabicMessage؛ كويس نستفيد بها
        // بأن نعمل casting ونقرأها. لو مش موجودة، هنرجّع ترجمة عامة.
        return ex switch
        {
            NotFoundException nf => ((int)HttpStatusCode.NotFound, nf.Message, (nf as dynamic).ArabicMessage ?? "العنصر غير موجود."),
            ConflictException cf => ((int)HttpStatusCode.Conflict, cf.Message, (cf as dynamic).ArabicMessage ?? "تعارض في البيانات."),
            BadRequestException br => ((int)HttpStatusCode.BadRequest, br.Message, (br as dynamic).ArabicMessage ?? "طلب غير صالح."),
            _ => ((int)HttpStatusCode.InternalServerError,
                                       "Unexpected error.",
                                       "خطأ غير متوقع.")
        };
    }
}

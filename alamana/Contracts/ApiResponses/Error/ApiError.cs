namespace alamana.Contracts.ApiResponses.Error
{
    public class ApiError
    {
        public static ErrorResponse BadRequest(string en, string ar)
        => new(en, ar, 400);

        public static ErrorResponse NotFound(string en, string ar)
            => new(en, ar, 404);

        public static ErrorResponse Conflict(string en, string ar)
            => new(en, ar, 409);

        public static ErrorResponse Internal(string en, string ar)
            => new(en, ar, 500);
    }
}

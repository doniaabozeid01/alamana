namespace alamana.Contracts.ApiResponses.Success
{
    public static class ApiSuccess
    {
        public static SuccessResponse<object?> Ok(string en, string ar)
           => new(en, ar, 200, null);

        public static SuccessResponse<T> Ok<T>(string en, string ar, T? data)
            => new(en, ar, 200, data);

        public static SuccessResponse<object?> Created(string en, string ar)
            => new(en, ar, 201, null);

        public static SuccessResponse<T> Created<T>(string en, string ar, T? data)
            => new(en, ar, 201, data);
    }
}

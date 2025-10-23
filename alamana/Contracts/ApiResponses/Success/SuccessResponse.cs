namespace alamana.Contracts.ApiResponses.Success
{
    public record SuccessResponse<T>(
        string SuccessEn,
        string SuccessAr,
        int Status,
        T? Data = default
    );
}

namespace alamana.Contracts.ApiResponses.Error
{
    public record ErrorResponse(
        string ErrorEn,
        string ErrorAr,
        int Status
    );
}

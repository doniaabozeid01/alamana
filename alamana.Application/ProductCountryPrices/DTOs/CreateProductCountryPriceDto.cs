namespace alamana.Application.ProductCountryPrices.DTOs
{
    public class CreateProductCountryPriceDto
    {
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public decimal Amount { get; set; }
    }
}

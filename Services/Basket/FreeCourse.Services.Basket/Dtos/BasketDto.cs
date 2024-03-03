namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);

    }
}

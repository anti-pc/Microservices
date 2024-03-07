namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public int DiscountRate { get; set; } = 0;
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);

    }
}

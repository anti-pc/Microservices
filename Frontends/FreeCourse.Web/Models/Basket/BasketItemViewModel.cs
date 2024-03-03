namespace FreeCourse.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }

        private decimal? DiscoutAppliedPrice { get; set; }
        public decimal GetCurrentPrice { get => DiscoutAppliedPrice != null ? DiscoutAppliedPrice.Value : Price; }
        public void AppliedDiscount(decimal discoutPrice)
        {
            DiscoutAppliedPrice = discoutPrice;
        }
    }
}

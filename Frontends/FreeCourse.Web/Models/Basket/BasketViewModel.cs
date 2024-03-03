namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public int DiscountRate { get; set; } = 0;
        public decimal TotalPrice => _basketItems.Sum(x => x.GetCurrentPrice * x.Quantity);

        public bool HasDiscount { get => !string.IsNullOrEmpty(DiscountCode); }

        private List<BasketItemViewModel> _basketItems { get; set; }

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * (DiscountRate / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }
    }
}

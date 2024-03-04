namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public string UserId { get; set; }
        public string DiscountCode { get; set; } = string.Empty;
        public int DiscountRate { get; set; } = 0;
        public decimal TotalPrice => _basketItems.Sum(x => x.GetCurrentPrice * x.Quantity);

        public bool HasDiscount { get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate > 0; }

        private List<BasketItemViewModel> _basketItems;

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }

        public void CancelDiscount()
        {
            DiscountCode = string.Empty;
            DiscountRate = 0;
        }

        public void ApplyDiscount(string code, int rate)
        {
            DiscountCode = code;
            DiscountRate = rate;
        }
    }
}

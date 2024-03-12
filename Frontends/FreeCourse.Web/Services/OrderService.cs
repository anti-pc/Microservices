using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Models.Order;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = checkoutInfoInput.TotalPrice
            };

            var responsePaymnet = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePaymnet)
                return new OrderCreatedViewModel { Error = "Payment could not be received", IsSuccessful = false };

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput() { ProductId = x.CourseId, Price = x.GetCurrentPrice, ProductName = x.CourseName, PictureUrl = "" };
                orderCreateInput.OrderItems.Add(orderItem);
            });


            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("order", orderCreateInput);
            if (!response.IsSuccessStatusCode)
                return new OrderCreatedViewModel { Error = "Order could not be created", IsSuccessful = false };

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful = true;
            _basketService.Delete();

            return orderCreatedViewModel.Data;

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("order");

            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput() { ProductId = x.CourseId, Price = x.GetCurrentPrice, ProductName = x.CourseName, PictureUrl = "" };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = checkoutInfoInput.TotalPrice,
                Order = orderCreateInput
            };

            var responsePaymnet = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePaymnet)
                return new OrderSuspendViewModel { Error = "Payment could not be received", IsSuccessful = false };


            await _basketService.Delete();
            return new OrderSuspendViewModel() { IsSuccessful = true };

        }
    }
}

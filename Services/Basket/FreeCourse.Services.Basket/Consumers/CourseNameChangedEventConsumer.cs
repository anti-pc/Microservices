using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Messages;
using MassTransit;


namespace FreeCourse.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly IBasketService _basketService;

        public CourseNameChangedEventConsumer(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var response = await _basketService.GetBasket(context.Message.UserId);

            if (response != null)
            {
                var basketItems = response.Data.BasketItems;
                basketItems.ForEach(basketItem =>
                {
                    basketItem.CourseId = context.Message.CourseId;
                    basketItem.CourseName = context.Message.UpdatedCourseName;
                });

                await _basketService.SaveOrUpdate(response.Data);
            }

        }
    }
}

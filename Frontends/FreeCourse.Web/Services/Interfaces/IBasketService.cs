﻿using FreeCourse.Web.Models.Basket;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketViewModel> Get();
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);
        Task<bool> Delete();
        Task AddBasketItem(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItem(string courseId);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();
    }
}

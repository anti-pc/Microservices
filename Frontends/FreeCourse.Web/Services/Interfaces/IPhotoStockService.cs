using FreeCourse.Web.Models.PhotoStock;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhotoAsync(IFormFile photo);
        Task<bool> DeletePhotoAsync(string photoUrl);
    }
}

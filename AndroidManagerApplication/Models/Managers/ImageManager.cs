using System.Data.Entity;
using System.Linq;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Managers
{
    // Provide CRUD operations for Image entities
    public class ImageManager: BaseManager<Image>
    {
        protected override DbSet<Image> GetDbSet()
        {
            return _dataSource.ImageList;
        }

        // The first element of ImageList contains default image from Resources\DefaultImage.jpg
        public Image GetDefaultImage()
        {
            return _dataSource.ImageList.First();
        }
    }
}
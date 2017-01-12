namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Contracts;
    using JustOrderIt.Data.Models.Media;
    using JustOrderIt.Web.Infrastructure.Mapping;
    using Logic.ServiceModels;
    using ServiceModels;

    /// <summary>
    /// Allows extension of the data service for Image entity
    /// </summary>
    public interface IImagesService : IDeletableEntitiesBaseService<Image, int>, IBaseDataService, IFileInfoService<Image>
    {
        IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawFile> rawImages);

        void SaveImages(IEnumerable<ProcessedImage> images);

        void RemoveImages(IEnumerable<int> imageIds);

        ProcessedImage ToProcessedImage(Image image, byte[] smallSizeContent, byte[] thumbnailContent, byte[] highResolutionContent);

        IEnumerable<Image> GetByProductId(int productId);

        IEnumerable<Image> GetImagesByUrls(ICollection<string> imageUrls);
    }
}

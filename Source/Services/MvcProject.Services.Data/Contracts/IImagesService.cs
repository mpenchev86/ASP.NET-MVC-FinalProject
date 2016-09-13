namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Logic.ServiceModels;
    using MvcProject.Data.Models;
    using MvcProject.Data.Models.Contracts;
    using MvcProject.Web.Infrastructure.Mapping;
    using ServiceModels;

    /// <summary>
    /// Allows extension of the data service for Image entity
    /// </summary>
    public interface IImagesService : IDeletableEntitiesBaseService<Image, int>, IBaseDataService, IFileInfoService<Image>
    {
        IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawFile> rawImages);

        void SaveImages(IEnumerable<ProcessedImage> images);

        void RemoveImages(IEnumerable<int> imageIds);

        IEnumerable<Image> ImagesByUrls(ICollection<string> imageUrls);

        ProcessedImage ToProcessedImage(Image image, byte[] smallSizeContent, byte[] thumbnailContent, byte[] highResolutionContent);

        IEnumerable<Image> GetByProductId(int productId);
    }
}

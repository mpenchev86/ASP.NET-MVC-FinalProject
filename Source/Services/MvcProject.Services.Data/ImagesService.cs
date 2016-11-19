namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Common.GlobalConstants;
    using Logic;
    using Logic.ServiceModels;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;
    using ServiceModels;
    using Web;

    public class ImagesService : FileInfoService<Image>, IImagesService
    {
        private readonly IImageProcessorService imageProcessor;
        private IFileSystemService fileSystemService;
        private IMappingService mappingService;

        public ImagesService(
            IIntPKDeletableRepository<Image> images,
            IIdentifierProvider idProvider,
            IFileSystemService fileSystemService,
            IImageProcessorService imageProcessor,
            IMappingService mappingService)
            : base(images, idProvider)
        {
            this.fileSystemService = fileSystemService;
            this.imageProcessor = imageProcessor;
            this.mappingService = mappingService;
        }

        public IEnumerable<Image> GetByProductId(int productId)
        {
            return this.Repository.All().Where(img => img.ProductId == productId);
        }

        public IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawFile> rawImages)
        {
            var processedImages = rawImages.Select(rawImage =>
            {
                var image = this.PersistFileInfo(rawImage);
                var smallSizeContent = this.imageProcessor.Resize(rawImage.Content, ProcessedImage.SmallSizeImageWidth);
                var thumbnailContent = this.imageProcessor.Resize(rawImage.Content, ProcessedImage.ThumbnailImageWidth);
                var highResContent = this.imageProcessor.Resize(rawImage.Content, ProcessedImage.HighResolutionWidth);
                var processedImage = this.ToProcessedImage(image, smallSizeContent, thumbnailContent, highResContent);
                return processedImage;
            }).ToList();

            return processedImages;
        }

        public void SaveImages(IEnumerable<ProcessedImage> images)
        {
            foreach (var image in images)
            {
                if (image != null)
                {
                    this.fileSystemService.SaveFile(
                        image.SmallSizeContent,
                        string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageSmallSizeSuffix, image.FileExtension));

                    this.fileSystemService.SaveFile(
                        image.ThumbnailContent,
                        string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageThumbnailSuffix, image.FileExtension));

                    this.fileSystemService.SaveFile(
                        image.HighResolutionContent,
                        string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageHighResolutionSuffix, image.FileExtension));
                }
            }
        }

        public void RemoveImages(IEnumerable<int> imageIds)
        {
            foreach (var imageId in imageIds)
            {
                var image = this.GetById(imageId);
                image.ProductId = null;
                image.IsMainImage = false;
                this.Update(image);
                this.DeletePermanent(imageId);
                // this.MarkAsDeleted(decodedId);
                this.fileSystemService.DeleteFile(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageSmallSizeSuffix, image.FileExtension));
                this.fileSystemService.DeleteFile(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageThumbnailSuffix, image.FileExtension));
                this.fileSystemService.DeleteFile(string.Format(StaticResourcesUrls.ServerPathDataItemsImages, image.UrlPath, StaticResourcesUrls.ImageHighResolutionSuffix, image.FileExtension));
            }
        }

        public ProcessedImage ToProcessedImage(Image image, byte[] smallSizeContent, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            var result = this.mappingService/*.IMapper*/.Map<ProcessedImage>(image);
            result.SmallSizeContent = smallSizeContent;
            result.ThumbnailContent = thumbnailContent;
            result.HighResolutionContent = highResolutionContent;
            return result;
        }

        // Rename to GetImagesByUrls
        public IEnumerable<Image> ImagesByUrls(ICollection<string> imageUrls)
        {
            return this.ImagesQueryByUrls(imageUrls).ToList();
        }

        // Rename to GetImagesQueryByUrls
        private IQueryable<Image> ImagesQueryByUrls(ICollection<string> imageUrls)
        {
            return this.Repository
                .All()
                .Where(img => imageUrls.Contains(img.UrlPath));
        }
    }
}

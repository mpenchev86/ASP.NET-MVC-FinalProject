namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Logic;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using ServiceModels;
    using Web;

    public class ImagesService : FileInfoService<Image>, /*BaseDataService<Image, int, IIntPKDeletableRepository<Image>>, */IImagesService
    {
        private const string ImagesServerPath = "~/Content/Images/{0}_{1}.{2}";
        private readonly IImageProcessorService imageProcessor;
        private IFileSystemService fileSystemService;

        public ImagesService(
            IIntPKDeletableRepository<Image> images,
            IIdentifierProvider idProvider,
            IFileSystemService fileSystemService,
            IImageProcessorService imageProcessor)
            : base(images, idProvider)
        {
            this.fileSystemService = fileSystemService;
            this.imageProcessor = imageProcessor;
        }

        //// #########################################################################

        public IEnumerable<ProcessedImage> ProcessImages(IEnumerable<RawFile> rawImages)
        {
            var processedImages = rawImages.Select(img =>
            {
                var image = this.SaveFileInfo(img);
                var thumbnailContent = this.imageProcessor.Resize(img.Content, ProcessedImage.ThumbnailImageWidth);
                var highContent = this.imageProcessor.Resize(img.Content, ProcessedImage.HighResolutionWidth);
                return ProcessedImage.FromImage(image, thumbnailContent, highContent);
            });

            return processedImages;
        }

        public void SaveImages(IEnumerable<ProcessedImage> images)
        {
            foreach (var image in images)
            {
                this.fileSystemService.SaveFile(
                    image.ThumbnailContent,
                    string.Format(ImagesServerPath, image.UrlPath, ProcessedImage.ThumbnailImage, image.FileExtension));

                this.fileSystemService.SaveFile(
                    image.HighResolutionContent,
                    string.Format(ImagesServerPath, image.UrlPath, ProcessedImage.HighResolutionImage, image.FileExtension));
            }
        }

        // Rename to GetImagesByUrls
        public IEnumerable<Image> ImagesByUrls(ICollection<string> imageUrls)
        {
            return this.ImagesQueryByUrls(imageUrls).ToList();
        }

        // Rename to QueryImagesByUrls
        private IQueryable<Image> ImagesQueryByUrls(ICollection<string> imageUrls)
        {
            return this.Repository
                .All()
                .Where(img => imageUrls.Contains(img.UrlPath));
        }
    }
}

﻿namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Logic;
    using Logic.ServiceModels;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;
    using ServiceModels;
    using Web;

    public class ImagesService : FileInfoService<Image>, /*BaseDataService<Image, int, IIntPKDeletableRepository<Image>>, */IImagesService
    {
        private const string ImagesServerPath = "~/Content/Images/{0}_{1}{2}";
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
                var thumbnailContent = this.imageProcessor.Resize(rawImage.Content, ProcessedImage.ThumbnailImageWidth);
                var highContent = this.imageProcessor.Resize(rawImage.Content, ProcessedImage.HighResolutionWidth);
                var processedImage = this.ToProcessedImage(image, thumbnailContent, highContent);
                return processedImage;
            }).ToList();

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

        public ProcessedImage ToProcessedImage(Image image, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            var result = this.mappingService.IMapper.Map<ProcessedImage>(image);
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

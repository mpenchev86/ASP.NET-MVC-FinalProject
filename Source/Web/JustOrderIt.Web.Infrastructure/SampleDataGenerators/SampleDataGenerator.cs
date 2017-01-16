namespace JustOrderIt.Web.Infrastructure.SampleDataGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.GlobalConstants;
    using Extensions;
    using HelperModels;
    using Services.Logic;
    using Services.Logic.ServiceModels;

    public class SampleDataGenerator : ISampleDataGenerator
    {
        private const char WhiteSpace = ' ';
        private readonly IFileSystemService fileSystemService;
        private readonly IImageProcessorService imageProcessor;

        public SampleDataGenerator()
            : this(new FileSystemService(), new ImageProcessorService())
        {
        }

        public SampleDataGenerator(IFileSystemService fileSystemService, IImageProcessorService imageProcessor)
        {
            this.fileSystemService = fileSystemService;
            this.imageProcessor = imageProcessor;
        }

        public string GenerateImageFile(int id, string physicalPath, string originalFileName, string fileExtension)
        {
            var processedImage = this.ProcessImage(id, physicalPath, originalFileName, fileExtension);
            this.SaveImageToFileSystem(processedImage);

            return processedImage.UrlPath;
        }

        private ProcessedImage ProcessImage(int id, string physicalPath, string originalFileName, string fileExtension)
        {
            var content = this.fileSystemService.ToByteArray(physicalPath);

            var smallSizeContent = this.imageProcessor.Resize(content, ProcessedImage.SmallSizeImageWidth);
            var thumbnailContent = this.imageProcessor.Resize(content, ProcessedImage.ThumbnailImageWidth);
            var highResContent = this.imageProcessor.Resize(content, ProcessedImage.HighResolutionWidth);

            var processedImage = new ProcessedImage
            {
                OriginalFileName = originalFileName,
                FileExtension = fileExtension,
                SmallSizeContent = smallSizeContent,
                ThumbnailContent = thumbnailContent,
                HighResolutionContent = highResContent,
                UrlPath = this.GetFilePath(id)
            };

            return processedImage;
        }

        private void SaveImageToFileSystem(ProcessedImage image)
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

        //public T PersistFileInfo(RawFile file, bool persistContent = false)
        //{
        //    var processedFileName = string.Join(WhiteSpace.ToString(), file.OriginalFileName.Split(new[] { WhiteSpace }, StringSplitOptions.RemoveEmptyEntries));
        //    var databaseFile = new T
        //    {
        //        OriginalFileName = processedFileName,
        //        FileExtension = file.FileExtension
        //    };

        //    this.Insert(databaseFile);
        //    databaseFile.UrlPath = this.GetFilePath(databaseFile.Id);
        //    this.Update(databaseFile);

        //    return databaseFile;
        //}

        private string GetFilePath(int id)
        {
            return string.Format(
                "{0}/{1}",
                id % FileSystemConstants.SavedFilesSubfoldersCount,
                string.Format("{0}{1}", id.ToMd5Hash().Substring(0, FileSystemConstants.FileHashLength), id));
        }
    }
}

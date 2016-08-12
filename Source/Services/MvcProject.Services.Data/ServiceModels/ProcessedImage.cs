namespace MvcProject.Services.Data.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Mapping;

    public class ProcessedImage : Image
    {
        public const int ThumbnailImageWidth = 260;
        public const string ThumbnailImage = "tmbl";

        public const int HighResolutionWidth = 1360;
        public const string HighResolutionImage = "hi-res";

        // TODO: Activator.CreateInstance is slow
        public static IMappingService MappingService
        {
            get { return Activator.CreateInstance<IMappingService>(); }
        }

        //static ProcessedImage()
        //{
        //    MappingService = ObjectFactory.Get<IMappingService>();
        //}

        public static Func<ProcessedImage, Image> ToImage
        {
            get { return pi => MappingService.Map<Image>(pi); }
        }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }

        public static ProcessedImage FromImage(Image image, byte[] thumbnailContent, byte[] highResolutionContent)
        {
            var result = MappingService.Map<ProcessedImage>(image);
            result.ThumbnailContent = thumbnailContent;
            result.HighResolutionContent = highResolutionContent;
            return result;
        }
    }
}

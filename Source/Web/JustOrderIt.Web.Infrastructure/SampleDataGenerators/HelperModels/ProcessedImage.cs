namespace JustOrderIt.Web.Infrastructure.SampleDataGenerators.HelperModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessedImage/* : IMapTo<Image>*/
    {
        public const int SmallSizeImageWidth = 50;
        public const int ThumbnailImageWidth = 260;
        public const int HighResolutionWidth = 1360;

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public byte[] SmallSizeContent { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }
    }
}

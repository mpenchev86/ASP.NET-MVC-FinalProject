namespace JustOrderIt.Data.DbAccessConfig.SampleDataGenerators.HelperModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.GlobalConstants;

    public class ProcessedImage
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public byte[] SmallSizeContent { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }
    }
}

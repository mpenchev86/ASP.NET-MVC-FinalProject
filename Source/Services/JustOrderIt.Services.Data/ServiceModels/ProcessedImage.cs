namespace JustOrderIt.Services.Data.ServiceModels
{
    using JustOrderIt.Data.Models.Catalog;
    using JustOrderIt.Data.Models.Media;
    using JustOrderIt.Web.Infrastructure.Mapping;

    public class ProcessedImage : IMapTo<Image>, IMapFrom<Image>
    {
        public const int SmallSizeImageWidth = 50;

        public const int ThumbnailImageWidth = 260;

        public const int HighResolutionWidth = 1360;

        public int Id { get; set; }

        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public string UrlPath { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public byte[] SmallSizeContent { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] HighResolutionContent { get; set; }
    }
}

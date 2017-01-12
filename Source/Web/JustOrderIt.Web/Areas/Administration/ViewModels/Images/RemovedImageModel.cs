namespace JustOrderIt.Web.Areas.Administration.ViewModels.Images
{
    public class RemovedImageModel
    {
        /// <summary>
        /// Encoded Image Id
        /// </summary>
        public string ImageId { get; set; }

        public bool IsMainImage { get; set; }

        public int ProductId { get; set; }
    }
}
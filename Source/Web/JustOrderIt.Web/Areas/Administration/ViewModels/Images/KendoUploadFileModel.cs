namespace MvcProject.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Infrastructure.Mapping;

    public class KendoUploadFileModel : IMapFrom<ImageDetailsForProductViewModel>
    {
        public string OriginalFileName { get; set; }
        public string FileExtension { get; set; }
        public int Size { get; set; }
    }
}
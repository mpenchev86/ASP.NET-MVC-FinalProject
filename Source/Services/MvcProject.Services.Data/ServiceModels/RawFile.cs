namespace MvcProject.Services.Data.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RawFile
    {
        public string OriginalFileName { get; set; }

        public string FileExtension { get; set; }

        public byte[] Content { get; set; }
    }
}

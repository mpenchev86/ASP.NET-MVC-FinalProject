namespace JustOrderIt.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using ServiceModels;

    public interface IFileSystemService
    {
        void SaveFile(byte[] content, string path);

        void DeleteFile(string path);

        RawFile ToRawFile(HttpPostedFileBase httpFile);
    }
}

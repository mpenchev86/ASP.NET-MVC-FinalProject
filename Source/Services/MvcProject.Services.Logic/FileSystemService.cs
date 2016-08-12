namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    public class FileSystemService : IFileSystemService
    {
        // From Showcase.Server.Infrastructure.FileSystem.FileSystemService - Non-async version
        public void SaveFile(byte[] content, string path)
        {
            var filePath = HostingEnvironment.MapPath(path);
            //// TODO: filePath can be null
            var fileInfo = new FileInfo(filePath);
            fileInfo.Directory.Create();
            using (var fileWriter = new FileStream(filePath, FileMode.CreateNew))
            {
                fileWriter.WriteAsync(content, 0, content.Length);
            }
        }
    }
}

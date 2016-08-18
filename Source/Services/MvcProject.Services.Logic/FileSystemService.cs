namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    using Common.GlobalConstants;
    using ServiceModels;
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

        public RawFile ToRawFile(HttpPostedFileBase httpFile)
        {
            // http://stackoverflow.com/a/7852256/4491770
            byte[] fileContent;
            using (Stream inputStream = httpFile.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }

                fileContent = memoryStream.ToArray();
            }

            string fileName = string.Empty;
            string fileExtension = string.Empty;

            if (string.IsNullOrWhiteSpace(httpFile.FileName))
            {
                // return null for files without name and extension
                return null;
            }

            if (httpFile.FileName.Length <= ValidationConstants.ImageUrlPathMaxLength &&
                !httpFile.FileName.Any(Path.GetInvalidPathChars().Contains))
            {
                var fname = Path.GetFileNameWithoutExtension(httpFile.FileName);
                if (!string.IsNullOrWhiteSpace(fname) && fname.Length <= ValidationConstants.ImageOriginalFileNameMaxLength)
                {
                    fileName = fname;
                }
            }

            if (Path.HasExtension(httpFile.FileName))
            {
                var extension = Path.GetExtension(httpFile.FileName);
                if (!string.IsNullOrWhiteSpace(extension) &&
                    extension.Length <= ValidationConstants.ImageFileExtensionMaxLength)
                {
                    //// remove the starting '.'
                    //extension = extension.Substring(1);
                    fileExtension = extension;
                }
            }

            return new RawFile
            {
                OriginalFileName = fileName,
                FileExtension = fileExtension,
                Content = fileContent
            };
        }
    }
}

namespace JustOrderIt.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Hosting;

    using Common.GlobalConstants;
    using ServiceModels;

    public class FileSystemService : IFileSystemService
    {
        public void SaveFile(byte[] content, string path)
        {
            var physicalPath = HostingEnvironment.MapPath(path);
            this.ValidateFilePath(physicalPath);

            var fileInfo = new FileInfo(physicalPath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            using (var fileWriter = new FileStream(physicalPath, FileMode.CreateNew, FileSystemRights.WriteData, FileShare.None, 1024, FileOptions.RandomAccess))
            {
                fileWriter.Write(content, 0, content.Length);
            }
        }

        public void DeleteFile(string path)
        {
            //var fileName = Path.GetFileName(path);
            //var physicalPath = Path.Combine(HostingEnvironment.MapPath("~/CategoryImage/"), fileName);
            //var physicalPath = Path.Combine(HostingEnvironment.MapPath(path));
            var physicalPath = HostingEnvironment.MapPath(path);

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }

        public RawFile ToRawFile(HttpPostedFileBase httpFile)
        {
            // Source: http://stackoverflow.com/a/7852256/4491770
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
            this.ValidateFileName(httpFile.FileName);
            fileName = httpFile.FileName;

            string fileExtension = string.Empty;
            if (Path.HasExtension(httpFile.FileName))
            {
                var extension = Path.GetExtension(httpFile.FileName);
                if (!string.IsNullOrWhiteSpace(extension) && extension.Length <= ValidationConstants.ImageFileExtensionMaxLength)
                {
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

        private void ValidateFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FileNameNullOrEmpty));
            }

            if (fileName.Length > ValidationConstants.ImageFullyQaulifiedFileNameMaxLength)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FileNameTooLong));
            }

            if (fileName.Any(Path.GetInvalidFileNameChars().Contains))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FileNameHasInvalidCharacters));
            }
        }

        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FilePathNullOrEmpty));
            }

            if (filePath.Length >= ValidationConstants.ImageUrlPathMaxLength)
            {
                throw new PathTooLongException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FilePathTooLong));
            }

            if (filePath.Any(Path.GetInvalidPathChars().Contains))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CustomExceptionMessage, ExceptionMessages.FileNameHasInvalidCharacters));
            }
        }
    }
}

namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using JustOrderIt.Common.GlobalConstants;
    using JustOrderIt.Data.DbAccessConfig.Repositories;
    using JustOrderIt.Data.Models;
    using JustOrderIt.Data.Models.Media;
    using JustOrderIt.Web.Infrastructure.Extensions;
    using Logic.ServiceModels;
    using ServiceModels;
    using Web;

    public class FileInfoService<T> : BaseDataService<T, int, IIntPKDeletableRepository<T>>, IFileInfoService<T>
        where T : FileInfo, new()
    {
        private const char WhiteSpace = ' ';

        public FileInfoService(IIntPKDeletableRepository<T> repository, IIdentifierProvider idProvider)
            : base(repository, idProvider)
        {
        }

        public override T GetByEncodedId(string id)
        {
            var image = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return image;
        }

        public override T GetByEncodedIdFromNotDeleted(string id)
        {
            var image = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return image;
        }

        public T PersistFileInfo(RawFile file, bool persistContent = false)
        {
            var processedFileName = string.Join(WhiteSpace.ToString(), file.OriginalFileName.Split(new[] { WhiteSpace }, StringSplitOptions.RemoveEmptyEntries));
            var databaseFile = new T
            {
                OriginalFileName = processedFileName,
                FileExtension = file.FileExtension
            };

            this.Insert(databaseFile);
            databaseFile.UrlPath = this.GetFilePath(databaseFile.Id);
            this.Update(databaseFile);

            return databaseFile;
        }

        public string GetFilePath(int id)
        {
            return string.Format(
                "{0}/{1}",
                id % FileSystemConstants.SavedFilesSubfoldersCount,
                string.Format("{0}{1}", id.ToMd5Hash().Substring(0, FileSystemConstants.FileHashLength), id));
        }
    }
}

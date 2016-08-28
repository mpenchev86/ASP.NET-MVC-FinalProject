namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Logic.ServiceModels;
    using MvcProject.Common.GlobalConstants;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using MvcProject.Web.Infrastructure.Extensions;
    using ServiceModels;
    using Web;

    public class FileInfoService<T> : BaseDataService<T, int, IIntPKDeletableRepository<T>>, IFileInfoService<T>
        where T : MvcProject.Data.Models.FileInfo, new()
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
            return string.Format("{0}/{1}", id % AppSpecificConstants.SavedFilesSubfoldersCount, string.Format("{0}{1}", id.ToMd5Hash().Substring(0, AppSpecificConstants.FileHashLength), id));
        }
    }
}

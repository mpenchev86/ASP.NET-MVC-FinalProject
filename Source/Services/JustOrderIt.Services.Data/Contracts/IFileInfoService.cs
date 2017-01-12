namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Logic.ServiceModels;
    using ServiceModels;

    public interface IFileInfoService<T> : IBaseDataService
    {
        T PersistFileInfo(RawFile file, bool persistContent);
    }
}

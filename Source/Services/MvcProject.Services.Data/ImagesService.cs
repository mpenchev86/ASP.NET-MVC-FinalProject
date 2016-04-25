namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models;
    using Web;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> images;
        private IIdentifierProvider idProvider;

        public ImagesService(IRepository<Image> images, IIdentifierProvider idProvider)
        {
            this.images = images;
            this.idProvider = idProvider;
        }

        public IQueryable<Image> GetAll()
        {
            var result = this.images
                             .All()
                             .OrderBy(x => x.UrlPath);
            return result;
        }

        public Image GetById(string id)
        {
            var idAsInt = this.idProvider.DecodeId(id);
            var image = this.images.GetById(idAsInt);
            return image;
        }

        public Image GetById(int id)
        {
            return this.images.GetById(id);
        }
    }
}

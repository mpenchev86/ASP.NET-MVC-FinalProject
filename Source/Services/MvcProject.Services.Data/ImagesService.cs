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

        public IQueryable<Image> GetAllNotDeleted()
        {
            var result = this.images
                .AllNotDeleted()
                .OrderBy(x => x.UrlPath);
            return result;
        }

        public Image GetById(int id)
        {
            return this.images.GetById(id);
        }

        public Image GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var image = this.images.GetById(idAsInt);
            return image;
        }

        public Image GetByIdFromNotDeleted(int id)
        {
            return this.images.GetByIdFromNotDeleted(id);
        }

        public Image GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var image = this.images.GetByIdFromNotDeleted(idAsInt);
            return image;
        }

        public void Insert(Image entity)
        {
            this.images.Add(entity);
            this.images.SaveChanges();
        }

        public void Update(Image entity)
        {
            this.images.Update(entity);
            this.images.SaveChanges();
        }

        public void MarkAsDeleted(int id)
        {
            var entity = this.GetById(id);
            this.MarkAsDeleted(entity);
            this.images.SaveChanges();
        }

        public void MarkAsDeleted(Image entity)
        {
            entity.IsDeleted = true;
            this.images.SaveChanges();
        }

        public void DeletePermanent(int id)
        {
            var entity = this.GetById(id);
            this.DeletePermanent(entity);
            this.images.SaveChanges();
        }

        public void DeletePermanent(Image entity)
        {
            this.images.DeletePermanent(entity);
            this.images.SaveChanges();
        }
    }
}

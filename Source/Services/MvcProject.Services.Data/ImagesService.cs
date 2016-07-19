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

    public class ImagesService : BaseDataService<Image, int, IIntPKDeletableRepository<Image>>, IImagesService
    {
        private readonly IIntPKDeletableRepository<Image> imagesRepository;
        private IIdentifierProvider idProvider;

        public ImagesService(IIntPKDeletableRepository<Image> images, IIdentifierProvider idProvider)
            : base(images, idProvider)
        {
            this.imagesRepository = images;
            this.idProvider = idProvider;
        }

        public override Image GetByEncodedId(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var image = this.imagesRepository.GetById(idAsInt);
            return image;
        }

        public override Image GetByEncodedIdFromNotDeleted(string id)
        {
            var idAsInt = this.idProvider.DecodeIdToInt(id);
            var image = this.imagesRepository.GetByIdFromNotDeleted(idAsInt);
            return image;
        }
    }
}

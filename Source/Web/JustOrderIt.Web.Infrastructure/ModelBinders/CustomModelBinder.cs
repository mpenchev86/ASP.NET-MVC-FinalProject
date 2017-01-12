namespace JustOrderIt.Web.Infrastructure.ModelBinders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    //using Data.DbAccessConfig.Repositories;
    //using Data.Models.Contracts;

    public class CustomModelBinder<TEntity> : DefaultModelBinder
        where TEntity : class/*, IBaseEntityModel<object>*/
    {
        //private readonly IRepository<TEntity, object> repository;

        //public CustomModelBinder(IRepository<TEntity, object> repository)
        //{
        //    this.repository = repository;
        //}

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var value = bindingContext.ValueProvider.GetValue("id");
        //    var id = value.AttemptedValue as string;
        //    TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));

        //    if (entity.Id is int)
        //    {
        //        entity = this.repository.GetById(int.Parse(id));
        //    }

        //    if (entity.Id is string)
        //    {
        //        entity = this.repository.GetById(id);
        //    }

        //    return entity;
        //}
    }
}

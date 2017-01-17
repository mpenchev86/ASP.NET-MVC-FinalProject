namespace JustOrderIt.Web.Infrastructure.ModelBinders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            // if (typeof(IAdministerable).IsAssignableFrom(modelType))
            // {
            //    var modelBinderType = typeof(CustomModelBinder<>).MakeGenericType(modelType);
            //    var modelBinder = Activator.CreateInstance(modelBinderType);
            //    return (IModelBinder)modelBinder;
            // }
            return null;
        }
    }
}

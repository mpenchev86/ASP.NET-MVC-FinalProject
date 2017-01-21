namespace JustOrderIt.Web.Public
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Areas.Public.ViewModels.Votes;

    public class VoteEditorModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(VoteEditorModel))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;

                int productId = Convert.ToInt32(request.Form.Get(request.Form.AllKeys.FirstOrDefault(k => k.Contains("ProductId"))));
                string userId = request.Form.Get(request.Form.AllKeys.FirstOrDefault(k => k.Contains("UserId")));
                double voteValue = Convert.ToDouble(request.Form.Get(request.Form.AllKeys.FirstOrDefault(k => k.Contains("VoteValue"))));

                return new VoteEditorModel
                {
                    ProductId = productId,
                    UserId = userId,
                    VoteValue = voteValue,
                };

                //// call the default model binder this new binding context
                //return base.BindModel(controllerContext, newBindingContext);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}

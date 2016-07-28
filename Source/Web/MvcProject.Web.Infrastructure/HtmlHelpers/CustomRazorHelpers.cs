namespace MvcProject.Web.Infrastructure.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public static class CustomRazorHelpers
    {
        public static MvcHtmlString ToMvcHtmlString(this string helperString)
        {
            return new MvcHtmlString(helperString);
        }

        // From http://stackoverflow.com/a/24772635/4491770
        public static MvcHtmlString UlListFor<TModel, TValue>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression)
        {
            // Get the model metadata
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            IEnumerable<string> listItems = metaData.Model as IEnumerable<string>;

            if (listItems == null)
            {
                throw new ArgumentException("The  collection passed to the custom UlList html helper is invalid.");
            }

            StringBuilder innerHtml = new StringBuilder();
            TagBuilder listElement = new TagBuilder("li");

            foreach (var listItem in listItems)
            {
                listElement.InnerHtml = listItem;
                innerHtml.Append(listItem.ToString());
            }

            TagBuilder listWrapper = new TagBuilder("ul");
            listWrapper.InnerHtml = innerHtml.ToString();

            return MvcHtmlString.Create(listWrapper.ToString());
        }

        public static MvcHtmlString UlList<TModel>(
            this HtmlHelper<TModel> helper,
            IEnumerable<string> unorderedListItems)
        {
            if (unorderedListItems == null)
            {
                throw new ArgumentException("The  collection passed to the custom UlList html helper is invalid.");
            }

            StringBuilder innerHtml = new StringBuilder();
            TagBuilder listElement = new TagBuilder("li");

            foreach (var listItem in unorderedListItems)
            {
                listElement.InnerHtml = listItem;
                innerHtml.Append(listItem.ToString());
            }

            TagBuilder listWrapper = new TagBuilder("ul");
            listWrapper.InnerHtml = innerHtml.ToString();

            return MvcHtmlString.Create(listWrapper.ToString());
        }
    }
}

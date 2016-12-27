namespace MvcProject.Web.Areas.Public.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Categories;

    public class QuerySearchViewModel
    {
        private List<CategoryForQuerySearchViewModel> categoriesData;

        public QuerySearchViewModel()
        {
            this.categoriesData = new List<CategoryForQuerySearchViewModel>();
        }

        public string Query { get; set; }

        public List<CategoryForQuerySearchViewModel> CategoriesData
        {
            get { return this.categoriesData; }
            set { this.categoriesData = value; }
        }
    }
}
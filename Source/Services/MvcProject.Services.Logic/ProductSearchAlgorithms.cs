namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductSearchAlgorithms : IProductSearchAlgorithms
    {
        public List<string> ProcessSearchOptions(string options)
        {
            var optionsSplit = options.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return optionsSplit;
        }
    }
}

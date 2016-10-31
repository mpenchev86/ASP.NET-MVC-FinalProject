namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProductSearchAlgorithms
    {
        List<string> ProcessSearchOptions(string options);
    }
}

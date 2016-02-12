namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SampleService : ISampleService
    {
        public void Work()
        {
            Trace.WriteLine("I am working...");
        }
    }
}

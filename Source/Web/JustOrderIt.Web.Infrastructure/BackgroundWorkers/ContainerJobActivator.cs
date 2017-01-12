namespace JustOrderIt.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hangfire;

    public class ContainerJobActivator : JobActivator
    {
        //private IContainer container;

        //public ContainerJobActivator(IContainer container)
        //{
        //    this.container = container;
        //}

        //public override object ActivateJob(Type jobType)
        //{
        //    return this.container.Resolve(jobType);
        //    //return base.ActivateJob(jobType);
        //}
    }
}

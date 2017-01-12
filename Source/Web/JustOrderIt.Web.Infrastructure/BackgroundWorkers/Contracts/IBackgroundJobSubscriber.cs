namespace JustOrderIt.Web.Infrastructure.BackgroundWorkers
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines methods that will be processed in the background. The types implementing this interface consume background processing
    /// services that handle the execution of these methods in a separate thread pool.
    /// </summary>
    public interface IBackgroundJobSubscriber
    {
        /// <summary>
        /// Used as an proxy for a worker method invoked in a background job.
        /// </summary>
        /// <param name="methodName">The name of the background worker method.</param>
        /// <param name="args">Object array containing the arguments which will be passed to the worker method at the time of the background
        /// job's execution. They must be in the same order and type as defined in the signature of the worker method.</param>
        void BackgroundOperation(string methodName, object[] args);
    }
}

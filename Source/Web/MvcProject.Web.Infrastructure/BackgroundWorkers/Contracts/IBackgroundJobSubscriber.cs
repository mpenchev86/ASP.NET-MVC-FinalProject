namespace MvcProject.Web.Infrastructure.BackgroundWorkers
{
    /// <summary>
    /// Defines methods that will be processed in the background. The types implementing this interface consume background processing
    /// services that handle the execution of these methods in a separate thread pool.
    /// </summary>
    public interface IBackgroundJobSubscriber
    {
        void BackgroundOperation(object[] args);
    }
}

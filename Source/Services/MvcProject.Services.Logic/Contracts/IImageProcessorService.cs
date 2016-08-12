namespace MvcProject.Services.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base interface for image processors
    /// </summary>
    public interface IImageProcessorService
    {
        byte[] Resize(byte[] originalImage, int width);
    }
}

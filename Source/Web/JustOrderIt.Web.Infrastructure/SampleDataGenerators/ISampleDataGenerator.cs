namespace JustOrderIt.Web.Infrastructure.SampleDataGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HelperModels;

    public interface ISampleDataGenerator
    {
        string GenerateImageFile(int id, string physicalPath, string originalFileName, string fileExtension);

        //ProcessedImage ProcessImage(string physicalPath, string originalFileName, string fileExtension);

        //void SaveImageToFileSystem(ProcessedImage image);

        //string GetFilePath(int id);
    }
}

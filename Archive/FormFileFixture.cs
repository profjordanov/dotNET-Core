using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Moq;

namespace 
{
    public static class FormFileFixture
    {
        public static IFormFile CreateFormFileByEmbeddedResource(string resourceName, string fileName)
        {
            var file = new Mock<IFormFile>();

            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);

            streamWriter.Write(resourceStream);
            streamWriter.Flush();

            memoryStream.Position = 0;

            file.Setup(f => f.FileName).Returns(fileName).Verifiable();

            file.Setup(f => f.Length).Returns(memoryStream.Length);

            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => memoryStream.CopyToAsync(stream, token))
                .Verifiable();

            file.Setup(formFile => formFile.OpenReadStream())
                .Returns(resourceStream)
                .Verifiable();

            return file.Object;
        }
    }
}

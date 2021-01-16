using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Recruitment.Functions;

namespace Recuitment.Tests
{
    [TestClass]
    public class HashGeneratorTests
    {
        private Mock<IHashGenerator> _hashGenerator;
        private readonly ILogger logger = TestFactory.CreateLogger();

        [TestInitialize]
        public void Initialize()
        {
            _hashGenerator = new Mock<IHashGenerator>();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async void GenerateAsync_InValidString_ShouldReturnException()
        {
            _hashGenerator.Setup(x => x.GenerateAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<string>);

            var function = new HashFunction(new HttpClient(), _hashGenerator.Object);
            var request = TestFactory.CreateHttpRequest("test", "test");
            var response = (OkObjectResult)await function.Run(request, logger);
        }

        [TestMethod]
        public async void GenerateAsync_InValidString_ShouldReturnMd5String()
        {
            _hashGenerator.Setup(x => x.GenerateAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<string>);

            var function = new HashFunction(new HttpClient(), _hashGenerator.Object);
            var request = TestFactory.CreateHttpRequest("test", "test");
            var response = (OkObjectResult)await function.Run(request, logger);
            Assert.IsNotNull(response.Value);
        }
    }
}
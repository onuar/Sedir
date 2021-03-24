using NUnit.Framework;

namespace Sedir.Tests.Server
{
    [TestFixture]
    public class ServerConfigurationTests
    {
        [Test]
        public void UrlShouldNotBeNullWhenServerConfigurationIsCreated()
        {
            ServerConfiguration configuration = new ServerConfiguration();
            Assert.NotNull(configuration.Urls);
        }
    }
}
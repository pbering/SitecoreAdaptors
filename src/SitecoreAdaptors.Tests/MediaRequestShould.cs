using System.Web;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace SitecoreAdaptors.Tests
{
    public class MediaRequestShould
    {
        [Fact]
        public void do_stuff()
        {
            //// Arrange
            var processor = new TestHttpProcessor();
            var context = Substitute.For<HttpContextBase>();

            context.Request.RawUrl.Returns("/~/media/hello.png");

            var sitecore = Substitute.For<SitecoreContextBase>();

            sitecore.IsNull.Returns(false);
            sitecore.DatabaseName.Returns("web");
            sitecore.LanguageName.Returns("da-DK");
            sitecore.SiteName.Returns("website");

            var mediaRequest = Substitute.For<MediaRequestBase>();

            mediaRequest.DatabaseName.Returns("web");
            mediaRequest.LanguageName.Returns("da-DK");

            var mediaManager = Substitute.For<MediaManagerBase>();

            mediaManager.IsMediaRequest().Returns(true);
            mediaManager.ParseMediaRequest().Returns(mediaRequest);

            //// Act
            processor.Process(context, sitecore, mediaManager);

            //// Assert
            mediaRequest.MediaPath.Should().Be("/sitecore/media library/hello");
        }
    }
}
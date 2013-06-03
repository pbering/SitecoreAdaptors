using System.Web;
using Sitecore.Pipelines.HttpRequest;

namespace SitecoreAdaptors.Tests
{
    public class TestHttpProcessor : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Process(new HttpContextWrapper(args.Context), new SitecoreContextWrapper(), new MediaManagerWrapper(args.Context.Request));
        }

        public void Process(HttpContextBase context, SitecoreContextBase sitecore, MediaManagerBase mediaManager)
        {
            if (context == null || sitecore.IsNull)
            {
                return;
            }

            if (!mediaManager.IsMediaRequest())
            {
                return;
            }

            var mediaRequest = mediaManager.ParseMediaRequest();

            if (mediaRequest == null)
            {
                return;
            }

            // Just do something...
            mediaRequest.MediaPath = "/sitecore/media library/hello";
        }
    }
}
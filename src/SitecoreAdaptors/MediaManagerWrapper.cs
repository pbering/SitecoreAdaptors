using System.Web;
using Sitecore.Resources.Media;

namespace SitecoreAdaptors
{
    public class MediaManagerWrapper : MediaManagerBase
    {
        private readonly HttpRequest _request;

        public MediaManagerWrapper(HttpRequest request)
        {
            _request = request;
        }

        public override bool IsMediaRequest()
        {
            return MediaManager.IsMediaRequest(_request);
        }

        public override MediaRequestBase ParseMediaRequest()
        {
            return new MediaRequestWrapper(MediaManager.ParseMediaRequest(_request));
        }
    }
}
using Sitecore.Resources.Media;

namespace SitecoreAdaptors
{
    public class MediaRequestWrapper : MediaRequestBase
    {
        private string _databaseName;
        private string _languageName;
        private string _mediaPath;

        public MediaRequestWrapper(MediaRequest mediaRequest)
        {
            _databaseName = mediaRequest.MediaUri.Database.Name;
            _languageName = mediaRequest.MediaUri.Language.CultureInfo.Name;
            _mediaPath = mediaRequest.MediaUri.MediaPath;
        }

        public MediaRequestWrapper()
        {
        }

        public override string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        public override string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        public override string MediaPath
        {
            get { return _mediaPath; }
            set { _mediaPath = value; }
        }
    }
}
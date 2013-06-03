using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace SitecoreAdaptors
{
    public class SitecoreContextWrapper : SitecoreContextBase
    {
        private readonly Context.ContextData _data;
        private readonly Item _item;
        private readonly Language _language;

        public SitecoreContextWrapper()
        {
            _data = Context.Data;

            if (_data == null)
            {
                return;
            }

            _language = Context.Language;
            _item = Context.Item;
        }

        public override string SiteName
        {
            get { return _data != null && _data.Site != null ? _data.Site.Name : string.Empty; }
        }

        public override string DatabaseName
        {
            get { return _data != null && _data.Database != null ? _data.Database.Name : string.Empty; }
        }

        public override string LanguageName
        {
            get { return _language != null && _language.CultureInfo != null ? _language.CultureInfo.Name : string.Empty; }
        }

        public override string ItemFullPath
        {
            get { return HasItem ? _item.Paths.FullPath : string.Empty; }
        }

        public override bool IsNull
        {
            get { return _data == null || _data.Database == null || _data.Site == null || _language == null; }
        }

        public override bool HasContentItem
        {
            get { return _item != null && _item.Paths.IsContentItem; }
        }

        public override bool HasItem
        {
            get { return _item != null; }
        }
    }
}
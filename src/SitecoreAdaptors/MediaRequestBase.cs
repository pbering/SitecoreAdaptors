using System;

namespace SitecoreAdaptors
{
    public abstract class MediaRequestBase
    {
        public virtual string LanguageName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual string DatabaseName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual string MediaPath
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
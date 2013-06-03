using System;

namespace SitecoreAdaptors
{
    public abstract class MediaManagerBase
    {
        public virtual bool IsMediaRequest()
        {
            throw new NotImplementedException();
        }

        public virtual MediaRequestBase ParseMediaRequest()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Stream.UmbracoServices.Interfaces
{
    public interface IUmbracoPageManager
    {
        TRoot GetRoot<TRoot>() where TRoot : class, IPublishedContent;
        TWebsitePage GetPage<TWebsitePage>(Func<TWebsitePage, bool> filter = null) where TWebsitePage : class, IPublishedContent;
    }
}

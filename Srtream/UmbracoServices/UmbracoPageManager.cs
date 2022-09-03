using Stream.Extensions;
using Stream.UmbracoServices.Interfaces;
using System;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Stream.UmbracoServices.Implementation
{
    public class UmbracoPageManager : IUmbracoPageManager
    {
        private readonly IUmbracoContextFactory _contextAccessor;

        public UmbracoPageManager(
            IUmbracoContextFactory umbracoContextAccessor
            )
        {
            _contextAccessor = umbracoContextAccessor;
        }

        private IUmbracoContext Context()
        {
            return _contextAccessor.EnsureUmbracoContext().UmbracoContext;
        }

       

        public TPage GetPage<TPage>(Func<TPage, bool> filter = null) where TPage : class, IPublishedContent
        {
            var applicationRoot = GetRoot<Umbraco.Cms.Web.Common.PublishedModels.Home>();
            if (applicationRoot is null) return null;

            var pages = applicationRoot.Children<TPage>().ToList();

            if (filter is null) return pages.FirstOrDefault();
            return pages.FirstOrDefault(filter);
        }

        public TRoot GetRoot<TRoot>() where TRoot : class, IPublishedContent
        {
            var rootType = typeof(TRoot).Name;
            var root = Context().Content.GetAtRoot().FirstOrDefault(rootPage => rootPage.ContentType.Alias == rootType.ToCamelCase());

            if (root is TRoot actualRoot) return actualRoot;

            return null;
        }
    }
}

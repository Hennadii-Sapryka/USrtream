
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Stream.UmbracoServices.Implementation
{
    public class UmbracoPageManager : IUmbracoPageManager
    {
        private readonly IUmbracoContextFactory contextAccessor;

        public UmbracoPageManager(IUmbracoContextFactory umbracoContextAccessor)
        {
            contextAccessor = umbracoContextAccessor;
        }

        private IUmbracoContext Context()
        {
            return contextAccessor.EnsureUmbracoContext().UmbracoContext;
        }

        public TPage GetPage<TPage>(Func<TPage, bool> filter = null) where TPage : class, IPublishedContent
        {
            var applicationRoot = GetRoot<Home>();
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

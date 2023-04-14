using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Stream.Extensions;

namespace Stream.ViewModels
{
    public enum MenuSectionType
    {
        None = -1,
        General = 0,
        Features = 1,
        Account = 2
    }

    public class NavigationSettings
    {
        public bool ShowInMainMenu { get; set; }
        public bool HideFromLanding { get; set; }
        public bool UseAlternateStyle { get; set; }
        public bool RequiresLogin { get; set; }
        public bool RequiresAnonymous { get; set; }
        public bool IsCurrentPage { get; set; }
        public MenuSectionType MenuSection { get; set; }
        public int MenuOrder { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }

        public static IList<NavigationSettings> FromHierarchy(IPublishedContent root, int currentPageId, bool isOnLanding, bool isLoggedIn)
        {
            var navSettings = root.Children.Select(navItem => FromNavItem(navItem, currentPageId)).ToList();
            navSettings.Add(new NavigationSettings
            {
                Label = root.Value<string>("pageTitle"),
                ShowInMainMenu = true,
                IsCurrentPage = root.Id == currentPageId,
                Url = root.Url(),
                MenuSection = Stream.ViewModels.MenuSectionType.General,
                MenuOrder = -1
            });

            return navSettings
                .Where(settings => settings is not null)
                .Where(settings => settings.ShowInMainMenu && settings.MenuSection != MenuSectionType.None)
                .Where(settings => isLoggedIn && !settings.RequiresAnonymous || !isLoggedIn && !settings.RequiresLogin)
                .Where(settings => !isOnLanding || isOnLanding && !settings.HideFromLanding)
                .OrderBy(settings => settings.MenuSection)
                .ThenBy(settings => settings.MenuOrder)
                .ToList();
        }

        private static NavigationSettings FromNavItem(IPublishedContent navItem, int currentPageId)
        {
            var showInMainMenu = navItem.Value("showInMainMenu", defaultValue: false);
            var hideFromLanding = navItem.Value("hideFromLandingPageMenu", defaultValue: false);
            var useAlternateStyle = navItem.Value("useAltStyle", defaultValue: false);
            var requiresLogin = navItem.Value("requiresLogin", defaultValue: false);
            var requiresAnonymous = navItem.Value("requiresAnonymous", defaultValue: false);
            var menuSection = navItem.Value("menuSection", defaultValue: MenuSectionType.None);
            var menuOrder = navItem.Value("menuOrder", defaultValue: int.MaxValue);

            return new NavigationSettings
            {
                ShowInMainMenu = showInMainMenu,
                HideFromLanding = hideFromLanding,
                UseAlternateStyle = useAlternateStyle,
                RequiresLogin = requiresLogin,
                RequiresAnonymous = requiresAnonymous,
                IsCurrentPage = navItem.Id == currentPageId,
                MenuSection = menuSection,
                MenuOrder = menuOrder,
                Label = navItem.Value<string>("pageTitle"),
                Url = navItem.Url()
            };
        }
    }
}

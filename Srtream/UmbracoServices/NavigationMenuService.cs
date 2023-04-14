using Stream.UmbracoServices.Interfaces;
using Stream.ViewModels.Navigation_Menu;
using Microsoft.AspNetCore.Mvc.Routing;
using Stream.UmbracoServices.Interfaces;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Security;
using System.Collections.Generic;
using Umbraco.Cms.Web.Common.PublishedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Stream.UmbracoServices
{
    public class NavigationMenuService : INavigationMenuService
    {
        private readonly IUmbracoPageManager _pageManager;
        private readonly IMemberManager _memberManager;
        private readonly Microsoft.AspNetCore.Mvc.IUrlHelper _urlHelper;

        private IPublishedContent _currentPage = null;
        private bool _isLoggedIn;
        private MemberIdentityUser _member = null;

        public NavigationMenuService(
            IUmbracoPageManager pageManager,
            IMemberManager memberManager,
            IUrlHelperFactory urlFactory,
            IHttpContextAccessor contextAccessor
            )
        {
            _pageManager = pageManager;
            _memberManager = memberManager;
            // _accessManager = accessManager;
            _urlHelper = urlFactory.GetUrlHelper(new ActionContext { HttpContext = contextAccessor.HttpContext });
        }

        public async Task<NavMenuViewModel> GetNavigationMenu(IPublishedContent currentPage)
        {
            await Initialize(currentPage);

            var navigationMenu = _pageManager.GetRoot<NavigationMenu>();
            if (navigationMenu is null) return null;

            var navMenu = new NavMenuViewModel();
            foreach (var item in navigationMenu.Children())
            {
                if (item is NavLink navLink)
                {
                    var navLinkViewModel = await GetNavLinkViewModel(navLink);
                    if (navLinkViewModel is null) continue;

                    navMenu.Items.Add(navLinkViewModel);
                    continue;
                }

                if (item is NavSection navSection)
                {
                    var navSectionViewModel = await GetNavSectionViewModel(navSection);
                    if (navSectionViewModel is null) continue;

                    navMenu.Items.Add(navSectionViewModel);
                }
            }

            //if (_isLoggedIn) navMenu.Items.Add(GetAccountSection());
            //else navMenu.Items.Add(GetLoginLink());

            return navMenu;
        }

        public async Task<NavMenuViewModel> GetFooterNavigationMenu()
        {
            await Initialize(null);

            var navigationMenu = _pageManager.GetRoot<NavigationMenu>();
            if (navigationMenu is null) return null;

            var navMenu = new NavMenuViewModel();
            foreach (var item in navigationMenu.Children())
            {
                if (item is NavSection navSection)
                {
                    var navSectionViewModel = await GetNavSectionViewModel(navSection);
                    if (navSectionViewModel is null) continue;

                    navMenu.Items.Add(navSectionViewModel);
                }
            }

            return navMenu;
        }

        private async Task Initialize(IPublishedContent currentPage)
        {
            _currentPage = currentPage;

            _isLoggedIn = _memberManager.IsLoggedIn();
            if (!_isLoggedIn) return;

            _member = await _memberManager.GetCurrentMemberAsync();
        }

        private async Task<NavSectionViewModel> GetNavSectionViewModel(NavSection navSection)
        {
            if (navSection is null) return null;

            var navLinks = new List<NavLinkViewModel>();

            foreach (var navLink in navSection.Children<NavLink>())
            {
                var viewModel = await GetNavLinkViewModel(navLink);
                if (viewModel is null) continue;

                navLinks.Add(viewModel);
            }

            if (!navLinks.Any()) return null;

            return new()
            {
                Name = navSection.Name,
                Links = navLinks
            };
        }

        //private NavSectionViewModel GetAccountSection()
        //{
        //    return new()
        //    {
        //        Name = _member.Name.GetAbbreviatedFullName(),
        //        Links = new List<NavLinkViewModel>
        //            {
        //                new NavLinkViewModel
        //                {
        //                    Label = "Profile",
        //                    Url = _pageManager.GetPage<Profile>()?.Url(),
        //                    Style = "alternate"
        //                },
        //                new NavLinkViewModel
        //                {
        //                    Label = "Log Out",
        //                    Url = _urlHelper.Action("Logout", "MemberSurface"),
        //                    Style = "default"
        //                }
        //            }
        //    };
        //}

        //private NavLinkViewModel GetLoginLink()
        //{
        //    return new()
        //    {
        //        Label = "Log In",
        //        Url = _pageManager.GetPage<Login>()?.Url() ?? "/",
        //        Style = "default"
        //    };
        //}

        private async Task<NavLinkViewModel> GetNavLinkViewModel(NavLink navLink)
        {
            if (navLink is null) return null;

            if (navLink.LinkedPage is null) return null;

            if (navLink.HideOnLandingPage && _currentPage is Home) return null;

            //if (navLink.ShowOnlyWithRequiredFeatures)
            //{
            //    var userHasAccess = await _accessManager.UserHasAccess(navLink.LinkedPage);
            //    if (!userHasAccess) return null;
            //}

            return new()
            {
                Label = navLink.Name,
                Url = navLink.LinkedPage.Url(),
                //Style = navLink.LinkStyle.ToLowerInvariant(),
                IsCurrent = navLink.LinkedPage.Key == _currentPage?.Key
            };
        }
    }
}



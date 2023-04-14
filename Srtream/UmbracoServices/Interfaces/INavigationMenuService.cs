using Stream.ViewModels.Navigation_Menu;
using System.Threading.Tasks;

namespace Stream.UmbracoServices.Interfaces
{
    public interface INavigationMenuService
    {
        Task<NavMenuViewModel> GetNavigationMenu(IPublishedContent currentPage);
        Task<NavMenuViewModel> GetFooterNavigationMenu();
    }
}

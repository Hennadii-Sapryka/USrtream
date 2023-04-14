using System.Collections.Generic;

namespace Stream.ViewModels.Navigation_Menu
{
    public class NavSectionViewModel : INavMenuItem
    {
        public string Name { get; set; }
        public List<NavLinkViewModel> Links { get; set; }
    }
}

using System.Collections.Generic;

namespace Stream.ViewModels.Navigation_Menu
{
    public class NavMenuViewModel
    {
        public List<INavMenuItem> Items { get; set; } = new();
    }
}

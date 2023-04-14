namespace Stream.ViewModels.Navigation_Menu
{
    public class NavLinkViewModel : INavMenuItem
    {
        public string Label { get; set; }
        public string Url { get; set; }
        public string Style { get; set; }
        public bool IsCurrent { get; set; }
    }
}

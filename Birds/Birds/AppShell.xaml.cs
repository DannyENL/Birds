using Birds.Views;
using Xamarin.Forms;

namespace Birds
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SightingEntryPage), typeof(SightingEntryPage));
        }
    }
}
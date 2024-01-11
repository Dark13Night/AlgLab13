using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CSlab13
{
    public partial class App : Application
    {
        public const string Dbfilename = "5.db";
        public App()
        {
            InitializeComponent();
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(Dbfilename);
            MainPage = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
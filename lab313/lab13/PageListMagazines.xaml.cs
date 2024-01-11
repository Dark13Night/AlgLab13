using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSlab13
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListMagazines : ContentPage
    {
        public PageListMagazines()
        {
            InitializeComponent();
        }
        // При открытии этой страницы инициализизуется список сборок из базы данных
        protected override void OnAppearing()
        {
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.Dbfilename);
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                ListMedecines.ItemsSource = db.Ыефешу.ToList(); //!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            base.OnAppearing();
        }
        // Обработка кнопки добавления сборки
        private async void CreateAuditorium(object sender, EventArgs e)
        {
            Statie medecine = new Statie();
            PageStatie pageMedecine = new PageStatie();
            pageMedecine.BindingContext = medecine;
            await Navigation.PushAsync(pageMedecine);
        }
        // Обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Statie selectedMedecine = (Statie)e.SelectedItem;
            PageStatie pageMedecine = new PageStatie();
            pageMedecine.BindingContext = selectedMedecine;
            await Navigation.PushAsync(pageMedecine);
        }
    }
}
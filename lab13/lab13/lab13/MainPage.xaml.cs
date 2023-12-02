using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore;

namespace lab13
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            using (AppDbContext db = new AppDbContext())
            {
                MagazinesList.ItemsSource = db.Magazines.ToList();
            }
            base.OnAppearing();
        }

        // обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Magazine selectedMagazine = (Magazine)e.SelectedItem;
            MagazinesPage magazinePage = new MagazinesPage();
            magazinePage.BindingContext = selectedMagazine;
            await Navigation.PushAsync(magazinePage);
        }
        // обработка нажатия кнопки добавления
        private async void CreateMagazine(object sender, EventArgs e)
        {
            Magazine magazine = new Magazine();
            MagazinesPage magazinesPage = new MagazinesPage();
            magazinesPage.BindingContext = magazine;
            await Navigation.PushAsync(magazinesPage);
        }
    }
}


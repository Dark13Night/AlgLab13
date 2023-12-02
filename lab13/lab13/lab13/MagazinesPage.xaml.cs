using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lab13
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MagazinesPage : ContentPage
    {

        public MagazinesPage()
        {
            InitializeComponent();
        }

        private void SaveMagazine(object sender, EventArgs e)
        {
            var magazine = (Magazine)BindingContext;
            if (!String.IsNullOrEmpty(magazine.Name))
            {
                using (AppDbContext db = new AppDbContext())
                {

                    if (magazine.MagazineId == 0)
                        db.Magazines.Add(magazine);
                    else
                    {
                        db.Magazines.Update(magazine);
                    }
                    db.SaveChanges();
                }
            }
            this.Navigation.PopAsync();
        }
        private void DeleteMagazine(object sender, EventArgs e)
        {
            var magazine = (Magazine)BindingContext;
            using (AppDbContext db = new AppDbContext())
            {
                db.Magazines.Remove(magazine);
                db.SaveChanges();
            }
            this.Navigation.PopAsync();
        }
    }
}


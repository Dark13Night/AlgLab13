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
    public partial class PageStatie : ContentPage
    {
        string dbPath;
        
        public PageStatie()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.Dbfilename);
        }
        
        private void SaveAuditorium(object sender, EventArgs e)
        {
            var medecine = (Statie)BindingContext;
            // if (!String.IsNullOrEmpty(detail.Name))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    if (medecine.Id == 0)
                        db.Ыефешу.Add(medecine);
                    else
                    {
                        db.Ыефешу.Update(medecine);
                    }
                    db.SaveChanges();
                }
            }
            this.Navigation.PopAsync();
        }
        private void DeleteAuditorium(object sender, EventArgs e)
        {
            var medecine = (Statie)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.Ыефешу.Remove(medecine);
                db.SaveChanges();
            }
            this.Navigation.PopAsync();
        }
    }
}
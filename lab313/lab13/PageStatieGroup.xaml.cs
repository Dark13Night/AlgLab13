#nullable enable
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
    public partial class PageStatieGroups : ContentPage
    {
        string dbPath;

        public PageStatieGroups()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.Dbfilename);
        }

        private async void SavePart(object sender, EventArgs e)
        {
            var medecineGroup = (StatieGroup)BindingContext;
            var medecineName = Entry.Text;
            if (!string.IsNullOrEmpty(medecineName))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    // Проверка есть ли деталь с которой пытаемся создать часть сборки
                    Statie? medecine = db.Ыефешу.FirstOrDefault(x => x.AuthorId == medecineName);
                    if (medecine == null)
                    {
                        bool flag = await DisplayAlert(
                            "Ошибочка",
                            "Похоже, такого нет(\nХотите создать? (да, хотите)",
                            "Создать",
                            "Отмена");
                        if (flag)
                        {
                            PageStatie pageMedecine = new PageStatie();
                            pageMedecine.EntryNameDetail.Text = medecineName;
                            Navigation.PushAsync(pageMedecine);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }

                    medecineGroup.StatieId = medecine.Id;
                    medecineGroup.Statie = medecine;
                    if (medecineGroup.Id == 0)
                        db.StatieGroups.Add(medecineGroup);
                    else
                    {
                        db.StatieGroups.Update(medecineGroup);
                    }

                    db.SaveChanges();
                }
            }

            Navigation.PopAsync();
        }

        private void DeletePart(object sender, EventArgs e)
        {
            var part = (StatieGroup)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.StatieGroups.Remove(part);
                db.SaveChanges();
            }

            Navigation.PopAsync();
        }
    }
}

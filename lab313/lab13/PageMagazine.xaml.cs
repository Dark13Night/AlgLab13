#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSlab13
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePharmacy : ContentPage
    {
        string dbPath;

        public PagePharmacy()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.Dbfilename);
            Console.WriteLine(dbPath);
        }

        protected override void OnAppearing()
        {
            Magazine v = (Magazine)this.BindingContext;
            if (v.Id != 0)
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    var assembly =
                        db.StatieGroups.Include(x => x.Statie)
                            .Where(x => x.MagazineId == v.Id);
                    ListPartsInAssembly.ItemsSource = assembly;
                }
            }

            base.OnAppearing();
        }

        private void SaveAssembly(object sender, EventArgs e)
        {
            var assembly = (Magazine)BindingContext;
            if (!String.IsNullOrEmpty(assembly.Name))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    if (assembly.Id == 0)
                        db.Magazine.Add(assembly);
                    else
                    {
                        db.Magazine.Update(assembly);
                    }

                    db.SaveChanges();
                }
            }

            this.Navigation.PopAsync();
        }

        private void DeleteAssembly(object sender, EventArgs e)
        {
            var assembly = (Magazine)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.Magazine.Remove(assembly);
                db.SaveChanges();
            }

            this.Navigation.PopAsync();
        }

        private async void AddPart(object sender, EventArgs e)
        {
            var pharmacy = (Magazine)BindingContext;
            var medecineName = await DisplayPromptAsync("Добавление лекарства",
                "Введите его секретное название",
                keyboard: Keyboard.Text);
            Console.WriteLine(medecineName);

            if (medecineName == "" || medecineName == null)
                return;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                var partL = db.StatieGroups.FirstOrDefault(x =>
                    (x.Statie.AuthorId == medecineName) && (x.MagazineId == pharmacy.Id));
                if (partL != null)
                {
                    await DisplayAlert("Внимание", "Лекарсто уже существует", "Хорошо");
                    return;
                }

                var medecine = db.Ыефешу.FirstOrDefault(x => x.AuthorId == medecineName);
                if (medecine == null)
                {
                    bool flag = await DisplayAlert(
                        "Ошибочка",
                        "Похоже, нет такого lecarstva(\nХотите создать?",
                        "Создать or create",
                        "Отмена");
                    // Если хотим то можно сразу создать ее
                    if (flag)
                    {
                        PageStatie pageMedecine = new PageStatie();
                        Statie medecineNew = new Statie { AuthorId = medecineName };
                        pageMedecine.BindingContext = medecineNew;
                        await Navigation.PushAsync(pageMedecine);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }

                string quantity = await DisplayPromptAsync("Добавление лекарства",
                    $"Введите небходимое количество \"{medecineName}\"",
                    keyboard: Keyboard.Numeric);
                if (quantity == "0" || quantity == "" || !int.TryParse(quantity, out var numericValue))
                    return;
                StatieGroup temp = new StatieGroup
                {
                    AuthorId = Int32.Parse(quantity),
                    Statie = medecine,
                    StatieId = medecine.Id
                };
                pharmacy.StatieGroup.Add(temp);
                ListPartsInAssembly.ItemsSource = pharmacy.StatieGroup;

                foreach (var VARIABLE in pharmacy.StatieGroup)
                {
                    Console.WriteLine(VARIABLE.Statie.AuthorId);
                }

                db.Magazine.Update(pharmacy);
                db.StatieGroups.Add(temp);
                db.SaveChanges();
                OnAppearing();
                await DisplayAlert("Внимание", "Лекарство добавлено", "Хорошо");
            }
        }

        private async void EditPart(object sender, EventArgs e)
        {
            var detailName = ((MenuItem)sender).CommandParameter.ToString();
            string quantityNew = await DisplayPromptAsync("Редактирование",
                $"Введите новое количество \"{detailName}\"",
                keyboard: Keyboard.Numeric);
            if (quantityNew == "0" || quantityNew == "" || !int.TryParse(quantityNew, out var numericValue))
                return;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                var part = db.StatieGroups.FirstOrDefault(x => x.Statie.AuthorId == detailName);
                part.AuthorId = int.Parse(quantityNew);
                db.StatieGroups.Update(part);
                db.SaveChanges();
            }

            OnAppearing();
        }

        private void DeletePart(object sender, EventArgs e)
        {
            var detailName = ((MenuItem)sender).CommandParameter.ToString();
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                var part = db.StatieGroups.FirstOrDefault(x => x.Statie.AuthorId == detailName);
                db.StatieGroups.Remove(part);
                db.SaveChanges();
            }

            OnAppearing();
        }
    }
}
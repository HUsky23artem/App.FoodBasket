using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodBasket
{
    public partial class MainPage : ContentPage
    {
        // Логика для основного интерфейса

        private bool isDrawerOpen = false;
        public MainPage()
        {
            InitializeComponent();
            image_fon.Source=ImageSource.FromResource("FoodBasket.image.fon.jpg"); // путь к картинку
            image_fon.Aspect = Aspect.Fill;
            Icon_setting.Source = ImageSource.FromResource("C:\\Users\\Пользователь\\source\\repos\\FoodBasket\\FoodBasket\\FoodBasket\\Setting_icon\\Icon_Setting.png");
        }
        private List<decimal> prices = new List<decimal>(); // список нужен для сохранения суммы
        private decimal totalSum = 0;
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string productName = entryName.Text;
            string productPrice = entryPrice.Text;
            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(productPrice)) // Проверяем, что поля не пустые
            {
                var newRow = new TextCell { Text = productName + " - " + productPrice + "руб." }; // Создаем новую ячейку для строки с данными продукта
                ((TableSection)tableView.Root[0]).Add(newRow); // Добавляем новую строку в таблицу
            }
            if (decimal.TryParse(entryPrice.Text, out decimal price))
            {
                prices.Add(price);
                totalSum += price;
                sum.Text = $"Сумма: {totalSum}";
                entryName.Text = string.Empty;
                entryPrice.Text = string.Empty; // Очищаем поле ввода
            }
        }

        private async void Icon_setting_onClicked(object sender, EventArgs e)
        {
            if (isDrawerOpen) //
            {
                CloseIcon_setting();
            }
            else
            {
                OpenIcon_setting();
            }

            isDrawerOpen = !isDrawerOpen; //
        }

        private async void OpenIcon_setting()
        {
            drawer_setting.IsVisible = true; //
            await drawer_setting.FadeTo(1, 750, Easing.SinInOut); //
        }

        private async void CloseIcon_setting()
        {
            await drawer_setting.FadeTo(0, 750, Easing.SinInOut);
            drawer_setting.IsVisible = false;
        }
        private async void HiddenMenu_onClicked(object sender, EventArgs e)
        {
            if (isDrawerOpen)
            {
                CloseDrawer();
            }
            else
            {
                OpenDrawer();
            }

            isDrawerOpen = !isDrawerOpen;
        }
        private async void OpenDrawer()
        {
            drawer.IsVisible = true;
            await drawer.FadeTo(1, 750, Easing.SinInOut);
        }

        private async void CloseDrawer()
        {
            await drawer.FadeTo(0, 750, Easing.SinInOut);
            drawer.IsVisible = false;
        }
        private void OnSaveDelClicked(object sender, EventArgs e)
        {
            ((TableSection)tableView.Root[0]).Clear(); //
            if (decimal.TryParse("0", out decimal price)) //
            {
                prices.Clear();
                prices.Add(price);
                totalSum = price;
                sum.Text = $"Сумма: {totalSum}";
                //entryPrice.Text = string.Empty; // Очищаем поле ввода
            }
        }

        // Логика для Setting
        void OnToggled(object sender, ToggledEventArgs e)
        {
            var state = e.Value;
            image_fon.IsVisible = state;
        }

        void OnTogg_Color_Fon(object sender, ToggledEventArgs e)
        {
            if (e.Value) // пример условия, можете задать свое
            {
                Fon_Background.BackgroundColor = Color.Black; // пример установки красного цвета, используйте нужный вам цвет
            }
            else
            {
                Fon_Background.BackgroundColor = Color.Azure; // пример установки синего цвета, используйте нужный вам цвет
            };
        }
        private async void Add_Fon_onClicked(object sender, EventArgs e)
        {
            try
            {
                var pickerResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Выберите изображение"
                });

                if (pickerResult != null)
                {
                    Stream stream = await pickerResult.OpenReadAsync();
                    Add_Image.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Обработка исключения, если функция не поддерживается на устройстве
                await DisplayAlert("Ошибка", "Функция выбора изображения не поддерживается", "OK");
            }
            catch (PermissionException)
            {
                // Обработка исключения, если отсутствуют необходимые разрешения
                await DisplayAlert("Ошибка", "Необходимы разрешения для доступа к галерее", "OK");
            }
            catch (Exception)
            {
                // Обработка других исключений
                await DisplayAlert("Ошибка", "Произошла ошибка при выборе изображения", "OK");
            }
        }
        private async void Print_Fon_onClicked(object sender, EventArgs e)
        {
            var pickerResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { });
          Stream stream = await pickerResult.OpenReadAsync();
            image_fon.Source = ImageSource.FromStream(() => stream);
        }
    }
}

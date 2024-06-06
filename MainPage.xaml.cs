using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Ev2
{
    public partial class MainPage : ContentPage
    {
        private string selectedAmount;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var rb = sender as RadioButton;
                selectedAmount = rb.Content.ToString();
                DisplayAlert("Recarga Seleccionada", $"Ha seleccionado una recarga de {selectedAmount}.", "OK");
            }
        }

        private async void OnRechargeClicked(object sender, EventArgs e)
        {
            var phoneNumber = XX_entryPhoneNumber.Text;
            var operatorSelected = XX_pickerOperator.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(phoneNumber) || operatorSelected == null || string.IsNullOrEmpty(selectedAmount))
            {
                await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
                return;
            }

            var confirmation = await DisplayAlert("Confirmación", $"¿Desea recargar {selectedAmount} a {phoneNumber} con el operador {operatorSelected}?", "Sí", "No");
            if (confirmation)
            {
                await PerformRecharge(phoneNumber, selectedAmount);
            }
        }

        private async Task PerformRecharge(string phoneNumber, string amount)
        {
            await Task.Delay(1000);

            var fileName = $"{phoneNumber}.txt";
            var content = $"Se hizo una recarga de {amount} en la siguiente fecha; {DateTime.Now:dd/MM/yyyy}";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            File.WriteAllText(filePath, content);

            await DisplayAlert("Recarga Exitosa", $"Su recarga de {amount} fue exitosa.", "OK");
        }
    }
}

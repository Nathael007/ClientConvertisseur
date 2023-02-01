using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.Input;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel : ObservableObject
    {
        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { devises = value; OnPropertyChanged(); }
        }

        private string txtBoxMontantEuro;

        public string TxtBoxMontantEuro
        {
            get => txtBoxMontantEuro;
            set => SetProperty(ref txtBoxMontantEuro, value);
        }

        private string txtBoxMontantdevise;

        public string TxtBoxMontantdevise
        {
            get => txtBoxMontantdevise;
            set => SetProperty(ref txtBoxMontantdevise, value);
        }

        private int laCombo;

        public int LaCombo
        {
            get => laCombo;
            set => SetProperty(ref laCombo, value);
        }



        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();

            //bouttons
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44359/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            Devises = new ObservableCollection<Devise>(result);
        }

        public IRelayCommand BtnSetConversion { get; }


        private void ActionSetConversion()
        {
            //Code du calcul à écrire
            try 
            {
                foreach (Devise laDevise in devises)
                {
                    if (laDevise.Id == LaCombo + 1)
                    {
                        TxtBoxMontantdevise = (Convert.ToDouble(TxtBoxMontantEuro) * Convert.ToDouble(laDevise.Taux)).ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageAsync("Impossible de calculer le montant !", "Erreur");
            }
        }

        private async void MessageAsync(string message, string title)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = App.MainRoot.XamlRoot;
            await contentDialog.ShowAsync();
        }
    }
}

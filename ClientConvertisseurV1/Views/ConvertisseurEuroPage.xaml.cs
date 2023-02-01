// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.


using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
        public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {       
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Devise> devises;
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { devises = value; OnPropertyChanged(nameof(Devises)); }
        }

        private double montantEuro;

        public double MontantEuro
        {
            get { return montantEuro; }
            set { montantEuro = value; }
        }

        public ConvertisseurEuroPage()
        {
            string lienApi = "https://localhost:44359/api/";
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync(lienApi);
        }

        private async void DisplayNoWifiDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "No wifi connection",
                Content = "Check connection and try again.",
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
        }

        private async void MessageAsync(string message, string title)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = this.Content.XamlRoot;
            await contentDialog.ShowAsync();
        }

        private async void GetDataOnLoadAsync(string lien)
        {
            WSService service = new WSService(lien);
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                MessageAsync("API non disponible !", "Erreur");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        private void button_Convertir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBox.SelectedIndex == -1)
                {
                    MessageAsync("Vous devez sélectionner une devise !", "Erreur");
                }
                foreach (Devise laDevise in devises)
                {
                    if (laDevise.Id == comboBox.SelectedIndex + 1)
                    {
                        textBoxDevises.Text = (Convert.ToDouble(textBoxConvertisseur.Text) * Convert.ToDouble(laDevise.Taux)).ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageAsync("Impossible de calculer le montant !", "Erreur");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
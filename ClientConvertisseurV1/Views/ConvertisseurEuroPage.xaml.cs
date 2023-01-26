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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.WebRequestMethods;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page
    {

        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises { get => devises; set => devises = value; }


        public ConvertisseurEuroPage()
        {
            string lienApi = "https://localhost:44359/api/";
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync(lienApi);
        }


        private async void GetDataOnLoadAsync(string lien)
        {
            WSService service = new WSService(lien);
            List<Devise> result = await service.GetDevisesAsync("devises");
            if(result == null)
            {
                MessageAsync("API non disponible !", "Erreur");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        private async void MessageAsync(string texte, string titre)
        {
            ContentDialog contentDialog = new ContentDialog
            {
                Title = titre,
                Content = texte,
                CloseButtonText = "Ok"
            };
            await contentDialog.ShowAsync();
        }
    }
}

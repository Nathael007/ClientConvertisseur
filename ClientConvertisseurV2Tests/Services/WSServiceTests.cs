using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Windows.Media.Protection.PlayReady;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.ViewModels;
using System.Collections.ObjectModel;

namespace ClientConvertisseurV2.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void WSServiceTest()
        {
            WSService service = new WSService("https://localhost:44359/api/");
            
            Assert.IsNotNull(service);
        }

        [TestMethod()]
        public void GetDevisesAsyncTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.GetDataOnLoadAsync();

            WSService service = new WSService("https://localhost:44359/api/");

            var result = service.GetDevisesAsync("devises").Result;

            Assert.IsNotNull(result);
        }
    }
}
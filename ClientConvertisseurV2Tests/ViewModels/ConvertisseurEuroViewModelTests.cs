using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using System.Collections.ObjectModel;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        /// <summary>
        /// Test constructeur.
        /// </summary>
        [TestMethod()]
        public void GetDataOnLoadAsyncTest()
        {

            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);

        }

        /// <summary>
        /// Test GetDataOnLoadAsyncTest OK
        /// </summary>
        [TestMethod()]
        public void GetDataOnLoadAsyncTest_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);
            //Assert
            Assert.IsNotNull(convertisseurEuro.Devises);
        }

        [TestMethod()]
        public void GetDataOnLoadAsyncTest_Trois_Devises_OK()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            //Assert
            Devise dev1 = new Devise(1, "Dollar", 1.08);
            Assert.AreEqual(convertisseurEuro.Devises[0], dev1);
        }

        /// Test conversion OK
        /// </summary>
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            //Création d'un objet de type ConvertisseurEuroViewModel
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            //Property MontantEuro = 100 (par exemple)
            convertisseurEuro.TxtBoxMontantEuro = "100";

            //Création d'un objet Devise, dont Taux=1.07
            //Property DeviseSelectionnee = objet Devise instancié

            convertisseurEuro.LaCombo = 1;

            //Act
            //Appel de la méthode ActionSetConversion
            convertisseurEuro.ActionSetConversion();

            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 107
            Assert.AreEqual(convertisseurEuro.TxtBoxMontantdevise, "107");
        }

        [TestMethod()]
        public void GetDataOnLoadAsync_Test_NonOK_WSnondemarre()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);

            Assert.IsNull(convertisseurEuro.Devises);
        }
    }
}
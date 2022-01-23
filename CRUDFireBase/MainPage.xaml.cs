using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using CRUDFireBase.Helper;
using CRUDFireBase.Model;

namespace CRUDFireBase
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();

              lstPersons.ItemsSource = allPersons;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddPerson(Convert.ToInt32(txtId.Text), txtName.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Éxito", "Usuario añadido correctamente", "Aceptar");
            var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text) && !String.IsNullOrEmpty(txtId.Text))
            {
                var person = await firebaseHelper.GetPerson(Convert.ToInt32(txtId.Text));
                if (person != null)
                {
                    txtId.Text = person.PersonId.ToString();
                    txtName.Text = person.Name;
                    await DisplayAlert("Éxito", "Usuario recuperado correctamente", "Aceptar");

                }
                else
                {
                    await DisplayAlert("Alerta", "No se encontró el usuario", "Aceptar");
                }
            }
            else
            {
                DisplayAlert("ERROR", "Introduzca un nombre de usuario y un ID", "Aceptar");
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdatePerson(Convert.ToInt32(txtId.Text), txtName.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Éxito", "Usuario actualizado correctamente", "Aceptar");
            var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeletePerson(Convert.ToInt32(txtId.Text));
            await DisplayAlert("Éxito", "Usuario eliminado correctamente", "OK");
            var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
        }
    }
}

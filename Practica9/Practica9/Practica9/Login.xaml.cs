using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica9
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public ObservableCollection<todoitem> Items { get; set; }
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<_13090352> Tabla;
        
        public static MobileServiceUser usuario;
        public Login()
        {
            InitializeComponent();
            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<_13090352>();

        }

        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //usuario = await App.Autenticator.Authenticate();
            //if (App.Autenticator != null)
            //{
            //    if (usuario != null)
            //    {
            //        await DisplayAlert("", "usuario autenticado" , "OK");
            //        await Navigation.PushModalAsync(new MainPage());
            //    }
            //    if (usuario == null)
            //    {
            //        //await DisplayAlert("", "usuario autenticado" + usuario.UserId, "OK");
            //        await DisplayAlert("", "usuario vacio", "OK");
            //        await Navigation.PushModalAsync(new MenuDatos());
            //    }
            //}

            try
            {
                if (App.Autenticator != null)
                {
                    usuario = await App.Autenticator.Authenticate();

                    if (usuario != null)
                    {
                        await DisplayAlert("Usuario Autenticado", usuario.UserId, "ok");
                        await Navigation.PushModalAsync(new MenuDatos());
                    }
                    if (usuario == null)
                    {
                        await DisplayAlert("","Usuario No Autenticado", "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ok");
            }
        }
       
        
    }
}
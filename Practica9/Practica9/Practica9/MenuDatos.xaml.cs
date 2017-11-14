using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Practica9
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDatos : ContentPage
    {
        public ObservableCollection<_13090352> Items { get; set; }
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<_13090352> Tabla;
        public static MobileServiceUser usuario;

        //SQLiteConnection database;

        public MenuDatos()
        {
            InitializeComponent();
            //Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            //Tabla = Cliente.GetTable<todoitem>();
            //Items = new ObservableCollection<todoitem>(database.Table<todoitem>());
            //BindingContext = this;

            //    Login.Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            //  Login.Tabla = Login.Cliente.GetTable<todoitem>();

            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<_13090352>();
            //Lista.ItemsSource = Items;
            //LeerTabla();            
        }

        private async void LeerTabla()
        {
            //comvierte la lista de datos a una lista enumerable
            IEnumerable<_13090352> elementos = await Login.Tabla.ToEnumerableAsync();
            Items = new ObservableCollection<_13090352>(elementos);
            
            //Tabla.CreateQuery("select*from toditem where deleted=true;");                      
            BindingContext = this;
            Lista.ItemsSource = Items;
        }

        private void InsertarRegistros_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegistrosIUD());
        }
        private async void RecuperarRegistros_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new unDelete());
            IEnumerable<_13090352> elementos = await Login.Tabla.IncludeDeleted().ToEnumerableAsync();
            Items = new ObservableCollection<_13090352>(elementos);
            BindingContext = this;
            Lista.ItemsSource = Items.Where(dato => dato.Deleted == true);
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            await Navigation.PushModalAsync(new SelectPage(e.SelectedItem as _13090352));


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Login.usuario != null)
            {
                LeerTabla();
            }
        }
    }
}
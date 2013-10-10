using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
// librerias a agregar
using Windows.Devices.Geolocation;

namespace Monitor_de_tu_app_en_el_background
{
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (App.Geolocator == null)
            {
                //si el elemento geolocator no tiene nada, se recupera una nueva ubicación
                App.Geolocator = new Geolocator();
                //se define que tanto exacta queremos ubicacion. a mayor exactitud, mayor consumo de bateria
                App.Geolocator.DesiredAccuracy = PositionAccuracy.High;
                App.Geolocator.MovementThreshold = 100; // Las unidades son en metro
                App.Geolocator.PositionChanged += geolocator_PositionChanged;
            }
        }

        // Si la aplicación cierra totalmente, se destruye el elemento "geolocator"
        // Si la app se vuelve a iniciar alguna vez, se creara un nuevo "geolocator" OnNavigatedTo
        protected override void OnRemovedFromJournal(System.Windows.Navigation.JournalEntryRemovedEventArgs e)
        {
            //se ejecuta cuando la posición ha cambiado y llama a la función que actualiza la ui
            App.Geolocator.PositionChanged -= geolocator_PositionChanged;
            App.Geolocator = null;
        }
        // funcion que se ejecuta si el dispositivo cambia de posición 
        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (!App.RunningInBackground)
            {
                //Codigo que se ejecuta cuando no se esta corriendo la app en background
                //nuevamente el dispatcher refresca los elementos de la ui que esten dentro de la función
                Dispatcher.BeginInvoke(() =>
                {
                    LatitudeTextBlock.Text = args.Position.Coordinate.Latitude.ToString("0.00");
                    LongitudeTextBlock.Text = args.Position.Coordinate.Longitude.ToString("0.00");
                });
            }
            else
            {

                // Mostrar notificacion SI se esta corriendo en background
                //NO se recomienda implementarlo en una app ya que puede ser molesto al usuario
                // se crea el toast, en su propiedad content se le asigna la latitud, cuando el usuario le da clic lo manda a la pagina 2 
                Microsoft.Phone.Shell.ShellToast toast = new Microsoft.Phone.Shell.ShellToast();
                toast.Content = args.Position.Coordinate.Latitude.ToString("0.00");
                toast.Title = "Location: ";
                toast.NavigationUri = new Uri("/Page2.xaml", UriKind.Relative);
                toast.Show();

            }
        }
    }
}
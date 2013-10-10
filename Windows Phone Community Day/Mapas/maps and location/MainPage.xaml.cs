using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using maps_and_location.Resources;
//agregar las siguientes librerias
using Windows.Devices.Geolocation;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Tasks;

namespace maps_and_location
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
        }

       
        // en la declaración del manejador de evento tenemos que cambiar a async para poder ejecutar la recuperacion de nuestra ubicación.

        private async void LocateMeButton_Click_1(object sender, EventArgs e)
        {
            //lineas de codigo para poder recuperar nuestra ubicación

            Geolocator MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracyInMeters = 5;
            Geoposition myGeoPosition = null;

            // se intenta ubicar la posición de nuestro telefono
            //si por algun motivo no se logra se lanza el catch que nos advierte sobre la posibilidad de tener los servicios de ubicacion apagados
            try
            {
                myGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Ubicación esta desactivada en tu dispositivo");
            }

            //Se mueve el centro del mapa hacia nuestra ubicación
            this.MyMap.Center = new GeoCoordinate(myGeoPosition.Coordinate.Latitude, myGeoPosition.Coordinate.Longitude);
            this.MyMap.ZoomLevel = 15;
        }

        // las siguientes lineas de codigo responden a los elementos en el app bar
        // cada funcion tiene un modo cartografico diferente
        private void mnuRoad_Click(object sender, EventArgs e)
        {
            MyMap.CartographicMode = MapCartographicMode.Road;
        }

        private void mnuAerial_Click(object sender, EventArgs e)
        {
            MyMap.CartographicMode = MapCartographicMode.Aerial;
        }

        private void mnuHybrid_Click(object sender, EventArgs e)
        {
            MyMap.CartographicMode = MapCartographicMode.Hybrid;
        }

        private void mnuTerrain_Click(object sender, EventArgs e)
        {
            MyMap.CartographicMode = MapCartographicMode.Terrain;
        }

        private void mnuLight_Click(object sender, EventArgs e)
        {
            MyMap.ColorMode = MapColorMode.Light;
        }

        private void mnuDark_Click(object sender, EventArgs e)
        {
            MyMap.ColorMode = MapColorMode.Dark;
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            MyMap.ZoomLevel = MyMap.ZoomLevel + 1;
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            MyMap.ZoomLevel = MyMap.ZoomLevel - 1;
        }

       

       
    }
}
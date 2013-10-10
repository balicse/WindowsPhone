using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Monitor_de_Ubicación.Resources;
//librerias a agregar
using Windows.Devices.Geolocation;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;


namespace Monitor_de_Ubicación
{
    public partial class MainPage : PhoneApplicationPage
    {
        // variables a utilizar
        Geolocator geolocator = null;
        bool tracking = false;
        ProgressIndicator pi;
        MapLayer PushpinMapLayer;



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Creando un MapLayer y agregandole la forma al maplayer
            PushpinMapLayer = new MapLayer();
            MyMap.Layers.Add(PushpinMapLayer);

            base.OnNavigatedTo(e);
        }
        public MainPage()
        {
            InitializeComponent();
            // Animación que indica que la aplicación esta "pensando" para recuperar su ubicación continuamente (loading)
            pi = new ProgressIndicator();
            pi.IsIndeterminate = true;
            pi.IsVisible = false;

        }
        private void TrackLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!tracking)
            {
                //si esta monitoreando nuuestra ubicación entra a la condición
                geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 50; // Por default las unidades son metros.

                //Se activa cuando el usuario se ha movido de ubicación
                geolocator.StatusChanged += geolocator_StatusChanged;
                geolocator.PositionChanged += geolocator_PositionChanged;

                tracking = true;
                TrackLocationButton.Content = "detener monitoreo";

                this.MyMap.ResolveCompleted += MapResolveCompleted;
            }
            else
            {
                // si no esta recuperando ubicación se detienen los servicios
                geolocator.PositionChanged -= geolocator_PositionChanged;
                geolocator.StatusChanged -= geolocator_StatusChanged;
                geolocator = null;

                tracking = false;
                TrackLocationButton.Content = "monitor de ubicación";
                StatusTextBlock.Text = "detenido";

                this.MyMap.ResolveCompleted -= MapResolveCompleted;
            }
        }

        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";

            // Hay distintos motivos por los cuales no podemos recuperar la ubicación, las siguientes lineas de codigo
            // le avisan al usuario sobre el status de los servicios de localización
            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    // no tenemos los suficientes permisos
                    status = "ubicación esta desactivada en configuración";
                    break;
                case PositionStatus.Initializing:
                    // el geolocalizador a comenzado a recuperar la ubicación
                    status = "iniciando";
                    break;
                case PositionStatus.NoData:
                    // no tenemos datos para recuperar ubicacion (señal)
                    status = "no hay datos ";
                    break;
                case PositionStatus.Ready:
                    // El geolocalizador esta listo para recuperar coordenadas
                    status = "listo";
                    break;
                case PositionStatus.NotInitialized:
                    //Asi comienza por default y se activa una vez que ya no esta funcionando el geolocalizador
                    break;
            }

            Dispatcher.BeginInvoke(() =>
            {
                StatusTextBlock.Text = status;
            });
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            //El dispatcher refresca nuestra UI con nuevos elementos
            Dispatcher.BeginInvoke(() =>
            {
                LatitudeTextBlock.Text = args.Position.Coordinate.Latitude.ToString("0.00");
                LongitudeTextBlock.Text = args.Position.Coordinate.Longitude.ToString("0.00");

                //El API de geolocalización trabaja con Windows.Devices.Geolocation.Geocoordinate
                //Los controles de mapas ocupan System.Device.Location.GeoCoordinate
                // por lo tanto tenemos que convertir los objetos antes de cualquier cosa

                GeoCoordinate positionCoord = new GeoCoordinate()
                {
                    Altitude = args.Position.Coordinate.Altitude.HasValue ? args.Position.Coordinate.Altitude.Value : 0.0,
                    Course = args.Position.Coordinate.Heading.HasValue ? args.Position.Coordinate.Heading.Value : 0.0,
                    HorizontalAccuracy = args.Position.Coordinate.Accuracy,
                    Latitude = args.Position.Coordinate.Latitude,
                    Longitude = args.Position.Coordinate.Longitude,
                    Speed = args.Position.Coordinate.Speed.HasValue ? args.Position.Coordinate.Speed.Value : 0.0,
                    VerticalAccuracy = args.Position.Coordinate.AltitudeAccuracy.HasValue ? args.Position.Coordinate.AltitudeAccuracy.Value : 0.0
                };

                // Centramos el mapa en la nueva ubicación
                this.MyMap.Center = positionCoord;

                this.MyMap.ZoomLevel = 15;

                // Llamamos a la funcion de crear el pushpin y le mandamos la coordenada de nuestra posición
                DrawPushpin(positionCoord);

                //Muestra el indicador de progreso mientras el mapa se mueve
                pi.IsVisible = true;
                pi.IsIndeterminate = true;
                pi.Text = "Resolviendo...";
                SystemTray.SetProgressIndicator(this, pi);
            });
        }

        private void MapResolveCompleted(object sender, MapResolveCompletedEventArgs e)
        {
            // Acultar el indicador de progreso (loading)
            pi.IsVisible = false;
            pi.IsIndeterminate = false;
            SystemTray.SetProgressIndicator(this, null);
        }

        //Dibujar pushpin en la coordenada seleccionada y agregandolo a la vista
        private void DrawPushpin(GeoCoordinate coord)
        {
            //creamos una variable y llamamos a la funcion que genera todo el gráfico
            var aPushpin = CreatePushpinObject();

            //Cremos un MapOverlay y agregamos el Pushpin con sus respectivas propiedades como coordenadas
            MapOverlay MyOverlay = new MapOverlay();
            MyOverlay.Content = aPushpin;
            MyOverlay.GeoCoordinate = coord;
            MyOverlay.PositionOrigin = new Point(0, 0.5);

            //Agregamos al mapa, el pushpin
            this.PushpinMapLayer.Add(MyOverlay);
        }

        //A continuación se realiza todo el proceso para generar un pushpin
        // el pushpin que se crea es similar al que nos daba Bing Maps en WP7
        private Grid CreatePushpinObject()
        {
            //Creando un elemento de malla
            Grid MyGrid = new Grid();
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.Background = new SolidColorBrush(Colors.Transparent);

            //Creando un rectangulo
            Rectangle MyRectangle = new Rectangle();
            MyRectangle.Fill = new SolidColorBrush(Colors.Black);
            MyRectangle.Height = 20;
            MyRectangle.Width = 20;
            MyRectangle.SetValue(Grid.RowProperty, 0);
            MyRectangle.SetValue(Grid.ColumnProperty, 0);

            //Agregando un rectangulo a la malla
            MyGrid.Children.Add(MyRectangle);

            //Creando un poligono
            Polygon MyPolygon = new Polygon();
            MyPolygon.Points.Add(new Point(2, 0));
            MyPolygon.Points.Add(new Point(22, 0));
            MyPolygon.Points.Add(new Point(2, 40));
            MyPolygon.Stroke = new SolidColorBrush(Colors.Black);
            MyPolygon.Fill = new SolidColorBrush(Colors.Black);
            MyPolygon.SetValue(Grid.RowProperty, 1);
            MyPolygon.SetValue(Grid.ColumnProperty, 0);

            //Agregando el poligono a la malla o grid
            MyGrid.Children.Add(MyPolygon);
            return MyGrid;
        }

      
      
       

    }
}
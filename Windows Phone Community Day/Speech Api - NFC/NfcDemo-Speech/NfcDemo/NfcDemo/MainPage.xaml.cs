using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NfcDemo.Resources;
using Windows.Networking.Proximity;

namespace NfcDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            ProximityDevice pd = ProximityDevice.GetDefault();

            if (pd != null)
            {
                pd.PublishMessage("Prueba", "Hola mundo!");
            }
        }

        private void btnRecibir_Click(object sender, RoutedEventArgs e)
        {
            ProximityDevice pd = ProximityDevice.GetDefault();

            if (pd != null)
            {
                long Id = pd.SubscribeForMessage("Prueba",  NfcReceive);
            }
        }

        private void NfcReceive(ProximityDevice sender, ProximityMessage message)
        {
            var m = message.DataAsString;
            MessageBox.Show("Mensaje recibido: " + m);
        }
    }

}
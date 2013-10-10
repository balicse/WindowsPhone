using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LensDemo.Resources;
using Microsoft.Devices;
using Microsoft.Xna.Framework.Media;

namespace LensDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCamera myCamera;

        MediaLibrary mediaLibrary;

        int startVal = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            mediaLibrary = new MediaLibrary();
            myCamera = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
            myCamera.CaptureImageAvailable += myCamera_CaptureImageAvailable;
            viewfinderBrush.SetSource(myCamera);
        }

        void myCamera_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            startVal++;
            string filename = startVal.ToString();
            mediaLibrary.SavePictureToCameraRoll(filename, e.ImageStream);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (myCamera != null)
            {
                myCamera.CaptureImage();
            }
        }
    }
}
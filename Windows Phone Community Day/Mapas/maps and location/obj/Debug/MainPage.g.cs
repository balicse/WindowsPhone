﻿#pragma checksum "C:\Users\SoreyBibiana\Downloads\maps and location\maps and location\maps and location\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "526FFBCABAED7462D0CB80FF4BF15C9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace maps_and_location {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuRoad;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuAerial;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuHybrid;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuTerrain;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuLight;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem mnuDark;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton LocateMeButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton ZoomIn;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton ZoomOut;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Maps.Controls.Map MyMap;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/maps%20and%20location;component/MainPage.xaml", System.UriKind.Relative));
            this.mnuRoad = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuRoad")));
            this.mnuAerial = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuAerial")));
            this.mnuHybrid = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuHybrid")));
            this.mnuTerrain = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuTerrain")));
            this.mnuLight = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuLight")));
            this.mnuDark = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("mnuDark")));
            this.LocateMeButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("LocateMeButton")));
            this.ZoomIn = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("ZoomIn")));
            this.ZoomOut = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("ZoomOut")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.MyMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("MyMap")));
        }
    }
}


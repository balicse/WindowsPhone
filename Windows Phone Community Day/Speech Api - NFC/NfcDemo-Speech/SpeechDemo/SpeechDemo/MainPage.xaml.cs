using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechDemo.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.Recognition;

namespace SpeechDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnTextoVoz_Click(object sender, RoutedEventArgs e)
        {
            ConvertirTextoVoz();
        }

        private void btnVozTexto_Click(object sender, RoutedEventArgs e)
        {
            ConvertirVozTexto();
        }

        async private void ConvertirTextoVoz()
        {
            string texto = txtTexto.Text;

            if (!string.IsNullOrEmpty(texto))
            {
                SpeechSynthesizer speech = new SpeechSynthesizer();
                await speech.SpeakTextAsync(texto);
            }
        }

        async private void ConvertirVozTexto()
        {
            SpeechRecognizerUI recoWithUI = new SpeechRecognizerUI();

            SpeechRecognitionUIResult recoResult = await recoWithUI.RecognizeWithUIAsync();

            if (recoResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
            {
                MessageBox.Show(recoResult.RecognitionResult.Text);
            }
        }
    }
}
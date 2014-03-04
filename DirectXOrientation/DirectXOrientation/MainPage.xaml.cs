using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PhoneDirect3DXamlAppComponent;
using Windows.Graphics.Display;

namespace PhoneDirect3DXamlAppInterop
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Direct3DBackground m_d3dBackground = null;

        // Constructeur
        public MainPage()
        {
            InitializeComponent();
        }

        private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
        {
            if (m_d3dBackground == null)
            {
                m_d3dBackground = new Direct3DBackground();

                // Définir les limites de la fenêtre en pixels indépendants du périphérique
                m_d3dBackground.WindowBounds = new Windows.Foundation.Size(
                    (float)Application.Current.Host.Content.ActualWidth,
                    (float)Application.Current.Host.Content.ActualHeight
                    );

                // Définir la résolution native en pixels
                m_d3dBackground.NativeResolution = new Windows.Foundation.Size(
                    (float)Math.Floor(Application.Current.Host.Content.ActualWidth * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f),
                    (float)Math.Floor(Application.Current.Host.Content.ActualHeight * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f)
                    );

                // Définir la résolution de rendu sur la résolution native complète
                m_d3dBackground.RenderResolution = m_d3dBackground.NativeResolution;

                // Raccorder le composant natif à DrawingSurfaceBackgroundGrid
                DrawingSurfaceBackground.SetBackgroundContentProvider(m_d3dBackground.CreateContentProvider());
                DrawingSurfaceBackground.SetBackgroundManipulationHandler(m_d3dBackground);
            }
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            m_d3dBackground.SetOrientation(ConvertOrientations(e.Orientation));
        }

        /// <summary>
        /// Fonction helper pour convertir une PageOrientation en DisplayOrientations
        /// </summary>
        /// <param name="pageOrientation">Orientation de la page dans le type PageOrientation</param>
        /// <returns>Equivalent de l'orientation dans le type DisplayOrientations</returns>
        private DisplayOrientations ConvertOrientations(PageOrientation pageOrientation)
        {
            switch (pageOrientation)
            {
                case PageOrientation.LandscapeLeft:
                    return DisplayOrientations.Landscape;
                case PageOrientation.LandscapeRight:
                    return DisplayOrientations.LandscapeFlipped;
                case PageOrientation.PortraitUp:
                    return DisplayOrientations.Portrait;
                case PageOrientation.PortraitDown:
                    return DisplayOrientations.PortraitFlipped;
                default:
                    return DisplayOrientations.None;
            }
        }
    }
}
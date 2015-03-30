using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RouteDisplayer;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using RouteDisplayer.Models;
using System.Threading.Tasks;
using System.Text;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace RouteDisplayer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            this.InitializeComponent();
            MapView myView = (MapView)this.FindName("MyMapView");
            if (Routes.Instance.RouteElements != null && Routes.Instance.RouteElements.FeatureSet.Features.Count > 0)
            {
                addRoute();
            }
            else
            {
                myView.ZoomToScaleAsync(10000);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListPage));
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage));
        }

        private void GetRouteButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GetRoutePage));
        }

        private async void addRoute()
        {
            await TryLoadLayers();
            MapView myView = (MapView)this.FindName("MyMapView");


            var graphicsLayer = MyMap.Layers["MyGraphics"] as Esri.ArcGISRuntime.Layers.GraphicsLayer;
            if (graphicsLayer == null)
            {
                graphicsLayer = new Esri.ArcGISRuntime.Layers.GraphicsLayer();
                graphicsLayer.ID = "MyGraphics";
                MyMap.Layers.Add(graphicsLayer);
            }
            Graphic routePoly = getRouteGraphic();
            graphicsLayer.Graphics.Add(routePoly);
            await myView.SetViewAsync(routePoly.Geometry, new Thickness(20));
        }

        private Graphic getRouteGraphic()
        {
            Polyline routePoly = (Polyline)Routes.Instance.Route.FeatureSet.Features[0].Geometry;
            if (!MyMapView.SpatialReference.Equals(routePoly.SpatialReference))
            {
                routePoly = (Polyline)GeometryEngine.Project(routePoly, MyMapView.SpatialReference);
            }
            Color orange = ((SolidColorBrush)Application.Current.Resources["OrangeBrush"]).Color;
            var orangeSymbol = new SimpleLineSymbol()
            {
                Color = orange,
                Width = 4,
                Style = SimpleLineStyle.Solid
            };

            return new Graphic() { Geometry = routePoly, Symbol = orangeSymbol };
        }

        private async Task TryLoadLayers()
        {
            var builder = new StringBuilder();
            await this.MyMapView.LayersLoadedAsync();
            foreach (var layer in MyMapView.Map.Layers)
            {
                if (layer.InitializationException != null)
                {
                    builder.AppendLine(layer.InitializationException.Message);
                }
            }

            // report layer initialization exception messages here ...
            System.Diagnostics.Debug.WriteLine("initialization exception " + builder.ToString() );
        }
    }
}

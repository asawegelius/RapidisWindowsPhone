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
using Windows.UI.Xaml.Media.Imaging;
using Esri.ArcGISRuntime.Tasks.Geoprocessing;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Http;
using RouteDisplayer.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace RouteDisplayer.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetRoutePage : Page
    {
        //private string PORTAL_SERVER_URL = "http://logistics-test.rapidis.com:6080/arcgis/rest/services/SecureRlpAppGetRoute/GPServer/RlpAppGetRoute";
        private string PORTAL_SERVER_URL = "http://logistics-test.rapidis.com:6080/arcgis/rest/services/RlpAppGetRoute/GPServer/RlpAppGetRoute";
        public GetRoutePage()
        {
            this.InitializeComponent();
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


        private Dictionary<string, string> GetInputFromUI()
        {
            Dictionary<string, string> input = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(userName.Text))
                {
                    userNameImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/NameIconGreen-40.png"));
                    input.Add("User", userName.Text);

                    if (!string.IsNullOrEmpty(password.Password))
                    {
                        passwordImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/PasswordIconGreen-40.png"));
                        input.Add("Password", password.Password);

                        if (!string.IsNullOrEmpty(id.Text))
                        {
                            int i = -1;
                            if (int.TryParse(id.Text, out i) && i >= 0)
                            {
                                idImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/IdIconGreen-40.png"));
                                input.Add("Id", id.Text);
                            }
                            else
                            {
                                idImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/IdIconOrange-40.png"));
                                Brush orange = (Application.Current.Resources["OrangeBrush"] as Brush);
                                statusLabel.Foreground = orange;
                                statusLabel.Text = "Id must be a positive number";
                            }
                        }
                        else
                        {
                            idImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/IdIconOrange-40.png")); 
                            Brush orange = (Application.Current.Resources["OrangeBrush"] as Brush);
                            statusLabel.Foreground = orange;
                            statusLabel.Text = "Id must be a positive number";
                        }
                    }
                    else
                    {
                        passwordImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/PasswordIconOrange-40.png"));
                        Brush orange = (Application.Current.Resources["OrangeBrush"] as Brush);
                        statusLabel.Foreground = orange;
                        statusLabel.Text = "Password is required";
                    }
                }
                else
                {
                    userNameImg.Source = new BitmapImage(new Uri(base.BaseUri, "/Assets/NameIconOrange-40.png"));
                    Brush orange = (Application.Current.Resources["OrangeBrush"] as Brush);
                    statusLabel.Foreground = orange;
                    statusLabel.Text = "Name is required";
                    
                }

            return input;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> input = GetInputFromUI();
            if (input != null && input.Count == 3)
            {
                Brush blue = (Application.Current.Resources["BlueBrush"] as Brush);
                statusLabel.Foreground = blue;
                statusLabel.Text = "Fetching route...";
                submitJob(input);
            }
        }


        private void textBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = false;
                Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.IsInputEnabled = true;
            }
        }

        public async void submitJob(Dictionary<string, string> param){
            try
            {
                var gp = new Geoprocessor(new Uri(PORTAL_SERVER_URL));
                long id;
                long.TryParse(param["Id"], out id);
                System.Diagnostics.Debug.WriteLine("id: " + id);
                var parameter = new GPInputParameter();
                parameter.GPParameters.Add(new GPLong("VehicleID", id));
                parameter.GPParameters.Add(new GPLong("CalculationID", 0));
                foreach (var p in parameter.GPParameters)
                {

                    System.Diagnostics.Debug.WriteLine(((GPLong)p).Value + " " + p.Name);
                }
                var result = await gp.SubmitJobAsync(parameter);
                progress.IsActive = true;
                // keep checking the job as long as the status indicates it's still running
                while (result.JobStatus != GPJobStatus.Cancelled && result.JobStatus != GPJobStatus.Deleted
                    && result.JobStatus != GPJobStatus.Succeeded && result.JobStatus != GPJobStatus.TimedOut)
                {
                    // pass the unique job id to the server to check the job
                    result = await gp.CheckJobStatusAsync(result.JobID);
                    // wait 2 seconds before checking again
                    await Task.Delay(2000);
                }
                if (result.JobStatus == GPJobStatus.Succeeded)
                {
                    GPFeatureRecordSetLayer route = (GPFeatureRecordSetLayer)await gp.GetResultDataAsync(result.JobID, "Route");
                    GPFeatureRecordSetLayer routeElements = (GPFeatureRecordSetLayer)await gp.GetResultDataAsync(result.JobID, "RouteElements");
                    // if the output is valid, download the data
                    if (routeElements != null && routeElements.FeatureSet.Features.Count > 0 )
                    {
                        var features = routeElements.FeatureSet.Features;
                        foreach (var feature in features)
                        {
                            System.Diagnostics.Debug.WriteLine("no of attributes: " + feature.Attributes.Count + " " + feature.Attributes["Description"]);

                        }
                        statusLabel.Text = "Added the route";
                        System.Diagnostics.Debug.WriteLine("no of routeElements: " + routeElements.FeatureSet.Features.Count);
                        Routes.Instance.RouteElements = routeElements;
                        Routes.Instance.Route = route;
                        progress.IsActive = false;
                    }
                    else
                    {
                        statusLabel.Text = "No route with that id";
                        progress.IsActive = false;
                    }
                }
                else
                { // job was not successful
                    var messageText = string.Empty;

                    // loop thru all messages that were generated during execution
                    foreach (var msg in result.Messages)
                    {
                        // concatenate the message descriptions
                        messageText += msg.Description + "\n";
                    }

                    // show the messages (last of which may be an error, time out, or cancellation)
                    statusLabel.Text = messageText;
                }
            }
            catch (ArcGISWebException webExp)
            {
                var msg = "Could not log in. Please check credentials. Error code: " + webExp.Code;
                System.Diagnostics.Debug.WriteLine(webExp.Message);
                statusLabel.Text = msg;
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
    }
}

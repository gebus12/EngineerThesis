using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DriverApplication.Resources;
using DriverApplication.ViewModels;
using Newtonsoft.Json;
using DriverApplication.Models;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace DriverApplication
{
    public partial class MainPage : PhoneApplicationPage
    {

        string apiUrl = @"http://192.168.1.112:14215/Buses/gettnij/";
        BusModel busik;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            //DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            //if (MainLongListSelector.SelectedItem == null)
            //    return;

            //// Navigate to the new page
            //NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            //// Reset selected item to null (no selection)
            //MainLongListSelector.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int desiredID = Int32.Parse(txtID.Text);
            String getUrl = apiUrl + desiredID;
            LoadData();
            WebClient webClient = new WebClient();

            //if (this.IsDataLoaded == false)
            //{
            webClient.Headers["Accept"] = "application/json";
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);
            webClient.DownloadStringAsync(new Uri(getUrl));
            //}
            //this.IsDataLoaded = true;
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);

        }



        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {

        }

        private void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null)
                {
                    busik = JsonConvert.DeserializeObject<BusModel>(e.Result);
                    
                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {

            }
            mapka = new Map();
            var longi = double.Parse(busik.Longitude);
            var latit = double.Parse(busik.Latitude);
            mapka.Center = new GeoCoordinate(latit, longi);
            mapka.ZoomLevel = 18;
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = new GeoCoordinate(latit, longi);

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            mapka.Layers.Add(myLocationLayer);
            mapka.Width = 500;
            mapka.Height = 500;
            ContentPanel.Children.Add(mapka);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sendRequest();
        }

        void sendRequest()
        {
            Uri myUri = new Uri("http://192.168.1.112:14215/Buses/Post");
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(myUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            //myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);
        }

        void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;

            // End the stream request operation
            Stream postStream = myRequest.EndGetRequestStream(callbackResult);

            // Create the post data
            string postData = "INSERT";
            BusModel busik = new BusModel();
            busik.BusID = 10;
            busik.Status = "OFF";
            busik.RegNum = "SZY95XH";


            postData = JsonConvert.SerializeObject(busik);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            myRequest.BeginGetResponse(new AsyncCallback(GetResponsetStreamCallback), myRequest);
        }

        void GetResponsetStreamCallback(IAsyncResult callbackResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
                string result = "";
                using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = httpWebStreamReader.ReadToEnd();
                }

                string APIResult = result;
            }
            catch (Exception e)
            {
            }

        }
    }
   }
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Spatial;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ThroneRooms
{
	public partial class MainPage : ContentPage
	{
	    private readonly DocumentClient _client;
	    private readonly Uri _collectionLink;
	    public MainPage()
	    {
	        InitializeComponent();

	        Map.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(-34.923647, 138.589312), 0.12, 0.1));

	        _client = new DocumentClient(new Uri(Constants.EndpointUri), Constants.PrimaryKey);
	        _collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, Constants.CollectionName);
	    }

	    private async void MainPage_OnAppearing(object sender, EventArgs e)
	    {
	        var locator = CrossGeolocator.Current;
	        locator.DesiredAccuracy = 50;

	        var position = await locator.GetPositionAsync(10000);

	        Map.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Map.VisibleRegion.LatitudeDegrees, Map.VisibleRegion.LongitudeDegrees));

	        Map.IsShowingUser = true;
	    }

        private async void Button_OnClicked(object sender, EventArgs args)
	    {
	        Map.Pins.Clear();
	        try
	        {
	            var longitude = Map.VisibleRegion.Center.Longitude;
	            var latitude = Map.VisibleRegion.Center.Latitude;
	            var distance = Map.VisibleRegion.Radius.Meters;

	            if (distance > 10000)
	            {
	                await DisplayAlert("Zoom", "Please zoom in and search again.", "OK");
	                return;
	            }

	            var query = _client.CreateDocumentQuery<Toilet>(_collectionLink).Where(t => t.Location.Distance(new Microsoft.Azure.Documents.Spatial.Point(longitude, latitude)) < distance)
	                .Select(t => new
	                {
	                    t.Id,
	                    t.Name,
	                    t.Location
	                })
	                .AsDocumentQuery();
	            while (query.HasMoreResults)
	            {
	                var response = await query.ExecuteNextAsync<SimpleToilet>();

	                foreach (var t in response)
	                {
	                    var pin = new Pin()
	                    {
	                        Label = t.Name,
	                        Position = new Xamarin.Forms.Maps.Position(t.Location.Position.Latitude,
	                            t.Location.Position.Longitude)
	                    };
	                    Map.Pins.Add(pin);
	                }
	                Debug.WriteLine("Request Charge:" + response.RequestCharge);
	            }
	        }
	        catch (Exception e)
	        {
	            await DisplayAlert("Error", e.Message, "OK");
	        }
	    }

	    private async void Info_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new InfoPage(), true);
	    }
    }
}

using BruTile.Cache;
using BruTile.Predefined;
using FinalCapProject.Class;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using System.Net.Http.Json;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Maui;

namespace FinalCapProject
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private bool isMapOpen = false;

        public MainPage()
        {
            InitializeComponent();
            this.Check.Clicked += OnSearchButtonClicked;

            // Aggiungi il gestore dell'evento di selezione della riga
            ResultsListView.ItemSelected += OnItemSelected;
        }

        // Metodo per il click sul pulsante di ricerca
        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string country = this.CountryEntry.Text?.Trim().ToLower();
            string codicePostale = this.PostalCodeEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(country))
            {
                await DisplayAlert("Errore", "Per favore, inserisci un'abbreviazione per il paese.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(codicePostale))
            {
                await DisplayAlert("Errore", "Per favore, inserisci un codice postale.", "OK");
                return;
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Errore di Connessione", "Connessione a Internet non disponibile. Controlla la tua connessione e riprova.", "OK");
                return;
            }

            string apiUrl = $"https://api.zippopotam.us/{country}/{codicePostale}";

            try
            {
                var locationList = await _httpClient.GetFromJsonAsync<PlaceList>(apiUrl);

                if (locationList != null && locationList.Places != null && locationList.Places.Count > 0)
                {
                    ResultsListView.ItemsSource = locationList.Places;
                }
                else
                {
                    await DisplayAlert("Risultato", "Non è stato trovato nessun risultato per il codice postale e il paese inseriti.", "OK");
                    ResultsListView.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore", $"Si è verificato un errore durante la connessione all'API: {ex.Message}", "OK");
            }
        }

        // Metodo che viene chiamato quando una riga della ListView viene selezionata
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (isMapOpen) return;
            
            if (e.SelectedItem is Place selectedPlace)
            {
                double longitude = Convert.ToDouble(selectedPlace.Longitude);
                double latitude = Convert.ToDouble(selectedPlace.Latitude);

                // Crea la mappa con la posizione selezionata
                var mapControl = new Mapsui.UI.Maui.MapControl();
                mapControl.Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());

                // Crea il punto della mappa
                var mPoint = new MPoint(longitude, latitude);
                var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(mPoint.X, mPoint.Y).ToMPoint();

                // Impostiamo la visualizzazione della mappa
                mapControl.Map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[11]);

                // Segna che la mappa è aperta
                isMapOpen = true;

                // Mostra la mappa in un nuovo contenuto
                await Navigation.PushAsync(new ContentPage
                {
                    Content = mapControl
                });

                // Deseleziona l'elemento dopo il click
                ResultsListView.SelectedItem = null;
            }
        }
    }
}

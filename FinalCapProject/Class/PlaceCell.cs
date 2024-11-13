using Microsoft.Maui.Controls;

namespace FinalCapProject.Class
{
    internal class PlaceCell : ViewCell
    {
        public PlaceCell()
        {
            // Creazione di un layout verticale per visualizzare i dati in modo ordinato
            var verticalStackLayout = new VerticalStackLayout
            {
                Padding = new Thickness(10),
                Spacing = 5
            };

            // Crea la label per il nome del posto
            var labelPlaceName = new Label
            {
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            };
            labelPlaceName.SetBinding(Label.TextProperty, "PlaceName");

            // Crea la label per lo stato
            var labelState = new Label
            {
                FontSize = 14
            };
            labelState.SetBinding(Label.TextProperty, "State");

            // Crea la label per la latitudine
            var labelLatitude = new Label
            {
                FontSize = 14
            };
            labelLatitude.SetBinding(Label.TextProperty, "Latitude");

            // Crea la label per la longitudine
            var labelLongitude = new Label
            {
                FontSize = 14
            };
            labelLongitude.SetBinding(Label.TextProperty, "Longitude");

            // Aggiungi le label al layout verticale
            verticalStackLayout.Children.Add(labelPlaceName);
            verticalStackLayout.Children.Add(labelState);
            verticalStackLayout.Children.Add(labelLatitude);
            verticalStackLayout.Children.Add(labelLongitude);

            // Impostiamo il layout verticale come la View della cella
            View = verticalStackLayout;
        }
    }
}

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using static Android.Icu.Text.Transliterator;

namespace PM02P012024.Views;

public partial class PageMap : ContentPage
{
	public PageMap()
	{
		InitializeComponent();
        
      
    }

    private void btnProcesar_Clicked(object sender, EventArgs e)
    {

        //Si el Campo es Nulo o esta en blanco
        if (string.IsNullOrWhiteSpace(Latitud.Text) || string.IsNullOrWhiteSpace(Longitud.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese valores para la latitud y la longitud.", "OK");
            return;
        }

        //Si hay alguna letras 
        if (!double.TryParse(Latitud.Text, out double latitud) || !double.TryParse(Longitud.Text, out double longitud))
        {
            DisplayAlert("Error", "Los valores ingresados para la latitud y la longitud deben ser numéricos.", "OK");
            return;
        }

        // Si todo esta bien guarda los valores y los muestra en un pin
        var location = new Location(latitud, longitud);

        // Mover el mapa a la ubicación especificada
        Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));

        // Agregar un pin en la ubicación especificada
        var pin = new Pin
        {
            Label = "Mi Marcador",
            Location = location
        };
        Mapa.Pins.Clear(); // Limpiar los pines existentes
        Mapa.Pins.Add(pin);
    }
}



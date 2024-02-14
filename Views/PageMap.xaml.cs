using AndroidX.Lifecycle;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using static Android.Icu.Text.Transliterator;

namespace PM02P012024.Views;

public partial class PageMap : ContentPage
{
    public PinViewModel ViewModel { get; set; }

    public PageMap()
	{
		InitializeComponent();
        ViewModel = new PinViewModel();
        BindingContext = ViewModel;

    }

    private void btnProcesar_Clicked(object sender, EventArgs e)
    {
        // Verificar si hay campos nulos o en blancos
        if (string.IsNullOrWhiteSpace(Latitud.Text) || string.IsNullOrWhiteSpace(Longitud.Text))
        {
            DisplayAlert("Error", "Por favor, ingrese valores para la latitud y la longitud.", "OK");
            return;
        }

        // Aviso De que solo acepta datos numericos
        if (!double.TryParse(Latitud.Text, out double latitud) || !double.TryParse(Longitud.Text, out double longitud))
        {
            DisplayAlert("Error", "Los valores ingresados para la latitud y la longitud deben ser numéricos.", "OK");
            return;
        }

        // Actualizar la ubicación del pin en el modelo de vista
        ViewModel.UpdateLocation(latitud, longitud);

        // Mover el mapa a la ubicación especificada
        Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(ViewModel.Location, Distance.FromKilometers(1)));

        // Limpiar los pines existentes y agregar el nuevo pin
        Mapa.Pins.Clear();
        Mapa.Pins.Add(new Pin
        {
            Label = "Mi Marcador",
            Location = ViewModel.Location
        });
    }
}

public class PinViewModel : BindableObject
{
    private double _latitude;
    public double Latitude
    {
        get { return _latitude; }
        set
        {
            if (_latitude != value)
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }
    }

    private double _longitude;
    public double Longitude
    {
        get { return _longitude; }
        set
        {
            if (_longitude != value)
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }
    }

    public Location Location => new Location(Latitude, Longitude);

    public void UpdateLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}




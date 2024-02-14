namespace PM02P012024;

public partial class PageInit : ContentPage
{
	readonly Controllers.PersonasControllers PersonDB;
    FileResult photo;

    public PageInit()
    {
        InitializeComponent();
        
    }

    public String GetImage64()
    {
        if(photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                String Base64 = Convert.ToBase64String(data);

                return Base64;
            }
         }
                return null;
    }

    public byte[] GetImageArray()
    {
        if (photo == null)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();               
                return data;
            }
        }
        return null;
    }

    private async void btnprocesar_Clicked(object sender, EventArgs e)
    {
        var person = new Models.Personas
        {
            Nombres = nombres.Text,
            Apellidos = apellidos.Text,
            FechaNac = FechaNac.Date,
            telefono = telefono.Text,
            foto = GetImage64()
        };

        if (await App.Database.StorePerson(person) > 0) 
		{
			await DisplayAlert("Aviso", "Registro ingresado con exito!!", "OK");
		}
    }

    private async void btnfoto_Clicked(object sender, EventArgs e)
    {
        photo = await MediaPicker.CapturePhotoAsync();

        if (photo != null)
        {
            string path = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourcephoto = await photo.OpenReadAsync();
            using FileStream Streamlocal = File.OpenWrite(path);

            foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

            await sourcephoto.CopyToAsync(Streamlocal);

        }
    }
}
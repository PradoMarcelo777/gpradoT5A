using gpradoT5A.Models;

namespace gpradoT5A.Views;

public partial class ActualizarPersona : ContentPage
{
    int idActualizarPersona;
    public ActualizarPersona()
    {
        InitializeComponent();
    }

    public ActualizarPersona(int idPersona)
    {
        InitializeComponent();
        idActualizarPersona = idPersona;
        CargarDatosPersona(idPersona);

    }
    //Cargar el nombre de la persona en la vista
    private void CargarDatosPersona(int idPersona)
    {
        Persona persona = App.personRepository.GetPersonaById(idPersona);

        if (persona != null)
        {
            txtNombre.Text = persona.NombrePersona; // Asigna el nombre actual
        }
    }

    private  async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        // Llama al método que actualiza la persona
        App.personRepository.ActualizarPersonaPorId(idActualizarPersona, txtNombre.Text);

        // Muestra el mensaje de estado
        lblStatus.Text = App.personRepository.StatusMessage;


        // Muestra un DisplayAlert
        await DisplayAlert("Información", App.personRepository.StatusMessage, "OK");

        // Crea una nueva instancia de Principal y establece el mensaje en EstadoMensaje
        var paginaPrincipal = new Principal();
        //paginaPrincipal.EstadoMensajeActualizacion = App.personRepository.StatusMessage;

        // InsertPageBefore: Este método inserta una nueva instancia de Principal justo antes de la página actual en la pila de navegación. 
        // Esto asegura que, al regresar, la nueva página principal se muestre en lugar de la anterior.

        // PopAsync: Este método elimina la página actual de la pila de navegación, dejando solo la nueva instancia de la página principal.

        // Regresa a la página principal y quita la página actual del stack
        //Navigation.InsertPageBefore(new Principal(), this); // Crea una nueva instancia de Principal
        //Navigation.PopToRootAsync(); // Elimina la página actual de la pila

        //Application.Current.MainPage = new NavigationPage(new Principal());

        Application.Current.MainPage = new NavigationPage(paginaPrincipal);
    }
}
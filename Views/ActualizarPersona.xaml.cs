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
        // Llama al m�todo que actualiza la persona
        App.personRepository.ActualizarPersonaPorId(idActualizarPersona, txtNombre.Text);

        // Muestra el mensaje de estado
        lblStatus.Text = App.personRepository.StatusMessage;


        // Muestra un DisplayAlert
        await DisplayAlert("Informaci�n", App.personRepository.StatusMessage, "OK");

        // Crea una nueva instancia de Principal y establece el mensaje en EstadoMensaje
        var paginaPrincipal = new Principal();
        //paginaPrincipal.EstadoMensajeActualizacion = App.personRepository.StatusMessage;

        // InsertPageBefore: Este m�todo inserta una nueva instancia de Principal justo antes de la p�gina actual en la pila de navegaci�n. 
        // Esto asegura que, al regresar, la nueva p�gina principal se muestre en lugar de la anterior.

        // PopAsync: Este m�todo elimina la p�gina actual de la pila de navegaci�n, dejando solo la nueva instancia de la p�gina principal.

        // Regresa a la p�gina principal y quita la p�gina actual del stack
        //Navigation.InsertPageBefore(new Principal(), this); // Crea una nueva instancia de Principal
        //Navigation.PopToRootAsync(); // Elimina la p�gina actual de la pila

        //Application.Current.MainPage = new NavigationPage(new Principal());

        Application.Current.MainPage = new NavigationPage(paginaPrincipal);
    }
}
using gpradoT5A.Models;

namespace gpradoT5A.Views;

public partial class Principal : ContentPage
{
   
    public Principal()
	{
		InitializeComponent();
	}
    //Pasar el emnsaje de actualizacion de GuardarActualizacion a la Principal
    public string EstadoMensajeActualizacion
    {
        set { lblStatus.Text = value; }
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepository.AddNewPerson(txtNombre.Text);
        lblStatus.Text = App.personRepository.StatusMessage;
        // Limpia el Entry despu�s de guardar
        txtNombre.Text = string.Empty;
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> personas = App.personRepository.GetAllpeople();
        listaPersona.ItemsSource = personas;
    }

    private void btnActualizarPersonaId_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        int idPersona = (int)button.CommandParameter;

        // Navega a la p�gina de actualizar persona
        Navigation.PushAsync(new ActualizarPersona(idPersona));

    }

    private async void btnEliminarPersonaId_Clicked(object sender, EventArgs e)
    {
        // Obtiene el bot�n que dispar� el evento
        Button button = (Button)sender;

        // Obt�n el ID de la persona desde el CommandParameter
        int idPersona = (int)button.CommandParameter;

        bool aceptaEliminacion = await DisplayAlert("Confirmaci�n", "�Est�s seguro de que deseas eliminar esta persona?", "Aceptar", "Cancelar");

        // Si el usuario confirma, procede con la eliminaci�n
        if (aceptaEliminacion)
        {
            // Llama al m�todo que maneja la eliminaci�n
            bool resultadoDespuesEliminar = App.personRepository.EliminarPersonaPorId(idPersona);
            listaPersona.ItemsSource = App.personRepository.GetAllpeople();

            // Muestra el mensaje de estado
            lblStatus.Text = App.personRepository.StatusMessage;

            // Opcional: puedes mostrar un mensaje adicional si la eliminaci�n fue exitosa o no
            if (resultadoDespuesEliminar == true)
            {
                DisplayAlert("�xito", $"Persona eliminada correctamente.", "OK");
            }
            else
            {
                DisplayAlert("Error", $"No se pudo eliminar la persona ", "OK");
            }

        }
       
    }


}
using gpradoT5A.Utils;

namespace gpradoT5A
{
    public partial class App : Application
    {
        public static PersonaRepository personRepository { get; set; } //Esto es para inyectar los repositorios dond quiera

        public App(PersonaRepository personRepo)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.Principal());
            personRepository = personRepo;
        }


    }
}

using gpradoT5A.Utils;
using Microsoft.Extensions.Logging;

namespace gpradoT5A
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //
            string dbPath = FileAccessHelper.GetLocalFilePath("personas.db");
            var personRepository = new PersonaRepository(dbPath);
            builder.Services.AddSingleton(personRepository);//Aqui registro con el builder la dependencia de personRepository


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

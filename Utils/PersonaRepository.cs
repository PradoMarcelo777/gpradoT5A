using gpradoT5A.Models;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace gpradoT5A.Utils
{
    public class PersonaRepository
    {
        string dbPath;

        private SQLiteConnection conn;

        public string StatusMessage { get; set; }


        public PersonaRepository(string pathDataBase)
        {
            dbPath = pathDataBase;
        }

        private void Init()
        {
            //esto verifica que la conexión a la base de datos ya ha sido inicializada y existe
            //Si la condición es verdadera (es decir, conn ya tiene un valor asignado y no es null), se ejecuta la línea return;. Esto significa que se sale del método Init y no se ejecutarán las siguientes líneas de código dentro de este método.
            //Si la condición es falsa(es decir, conn es null), el código no ejecuta el return y continúa con las siguientes líneas.
            if (conn is not null) // Verifica si conn NO es nulo, conn is not null es una forma más legible de escribir conn != null
            {
                return; // Si conn NO es nulo, salimos del método
            }

            conn = new(dbPath); // Si conn es nulo, se inicializa
            conn.CreateTable<Persona>(); // Crea la tabla Persona en la base de datos
        }

        //METODOS


        public void AddNewPerson(string nombre)
        {
            int result = 0;// si al final del ytry no  se guarda nada el result sigue en 0 este es un verificador parfa ver sis se inserto o no el dato

            try
            {
                Init();// Inicializa la conexión a la base de datos. Llamo asi por que estoy dentro de la clase

                //Validar que se ingreso el nombre que el entry no este vacio
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new Exception("EL Nombre es requerido");// con el trhrow salta al cactch
                }


                // Crear una nueva instancia de Persona
                Persona persona = new() { NombrePersona = nombre };

                //La instancia de arriba Es lo mismo que la instancia
                //Persona persona = new Persona();
                //persona.Nombre = name;

                // Insertar la nueva persona en la base de datos
                //Si se quiere validar el éxito de la inserción, entonces se necesitas la variable result.
                //Si solo deseas registrar un mensaje de éxito sin validar el resultado, puedes omitir result y solo iria conn.Insert(persona);.
                result = conn.Insert(persona);

                StatusMessage = string.Format("Dato persona insertado, total filas insertadas" + result);

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al insertar persona" + ex.Message);

            }
        }

        public List<Persona> GetAllpeople()
        {
            try
            {
                Init();

                List<Persona>  obtenerTodasPersonas = conn.Table<Persona>().ToList();
                return obtenerTodasPersonas;
            }
            catch (Exception ex)
            {
                //Después de capturar el error, el método devuelve una nueva instancia de List<Persona>, que está vacía salta afuera del catch
                StatusMessage = string.Format("Error al obtener todas las personas" + ex.Message);

                // Asigna una lista vacía a la variable y luego retorna
                List<Persona> listaVacia = new List<Persona>();

                return listaVacia;
            }

            
        }

        // Método para obtener una persona por su ID para obtener el nombre  a la vista Actualizacion
        public Persona GetPersonaById(int idPersona)
        {
            Init();
            return conn.Find<Persona>(idPersona);

            //List<Persona> todasLasPersonas = GetAllpeople();

            //// Variable para almacenar la persona encontrada
            //Persona personaEncontrada = null;

            //for (int i = 0; i < todasLasPersonas.Count; i++)
            //{
            //    if (todasLasPersonas[i].IdPersona == idPersona)
            //    {
            //        personaEncontrada = todasLasPersonas[i];
            //        return personaEncontrada; // Retorna la persona si se encuentra
            //    }
            //}
            //return null; // Retorna null si no se encuentra la persona
        }


        public void ActualizarPersonaPorId(int idPersona, string nombreActualizado)
        {
            try
            {
                Init();
                //var lo define el lenguaje y Persona yo
                Persona personaObtenidaPorId = conn.Find<Persona>(idPersona);

                if (personaObtenidaPorId != null)
                {
                    personaObtenidaPorId.NombrePersona = nombreActualizado; // Actualiza la propiedad
                    conn.Update(personaObtenidaPorId);
                    StatusMessage = "Persona actualizada correctamente."; // Mensaje de éxito
                }
                else
                {
                    StatusMessage = "Persona no encontrada."; // Mensaje en caso de que no se encuentre la persona
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar la persona: {ex.Message}"; // Mensaje de error
            }
        }

        public bool EliminarPersonaPorId(int id)
        {
            try
            {
                Init();

                var obtenerPersonaPorId = conn.Find<Persona>(id);

                if (obtenerPersonaPorId != null)
                {
                    conn.Delete(obtenerPersonaPorId);
                    StatusMessage = $"Persona eliminada correctamente.";
                    return true;
                }

            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al eliminar la persona" + ex.Message);
            }

            return false;
        }

    }
}


using System;
using System.Collections.Generic;

namespace Application
{
    public class Usuario
    {
        private string _id;
        private string firstName;
        private string lastName;
        private int age;
        private Ubicacion _ubicacionActual;
        private List<Ubicacion> _lugarFrecuentes;
        private List<ContactoEmergencia> _contactoEmergencia;

        public List<Ubicacion> LugaresFrecuentes
        {
            get { return _lugarFrecuentes; }
            set { _lugarFrecuentes = value; }        }

        public List<ContactoEmergencia> ContactoEmergencia
        {
            get { return _contactoEmergencia; }
            set { _contactoEmergencia = value; }
        }


        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public Ubicacion UbicacionActual { get => _ubicacionActual; set => _ubicacionActual = value; }

        public Usuario(string id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Usuario(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Usuario(string firstName, int age)
        {
            FirstName = firstName;
            Age = age;
        }

        public Usuario()
        {
        }

        public static void AgregarUsuario(Usuario usuario)
        {
            // Agrega el usuario a una base de datos o lista
            Console.WriteLine("Usuario agregado con éxito.");
        }

        public static void VerUsuarios(List<Usuario> database)
        {
            if (database.Count > 0)
            {
                Console.WriteLine("Lista de Usuarios:");
                foreach (var usuario in database)
                {
                    Console.WriteLine($"Nombre: {usuario.FirstName} {usuario.LastName}, Edad: {usuario.Age}");
                }
            }
            else
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
        }

        public static void BuscarUsuario(List<Usuario> database, string nombre)
        {
            var usuarioEncontrado = database.Find(u => u.FirstName == nombre);
            if (usuarioEncontrado != null)
            {
                Console.WriteLine($"Usuario encontrado - Nombre: {usuarioEncontrado.FirstName} {usuarioEncontrado.LastName}, Edad: {usuarioEncontrado.Age}");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }

        public static void EliminarUsuario(List<Usuario> database, string nombre)
        {
            var usuarioAEliminar = database.Find(u => u.FirstName == nombre);
            if (usuarioAEliminar != null)
            {
                database.Remove(usuarioAEliminar);
                Console.WriteLine("Usuario eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado, no se puede eliminar.");
            }
        }

        public string ObtenerTipoUsuario()
        {
            return Age >= 18 ? "Mayor de Edad" : "Menor de Edad";
        }

    }
}

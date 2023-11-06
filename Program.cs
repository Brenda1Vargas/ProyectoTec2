using System;
using System.Collections.Generic;

namespace Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Usuario> usuarios = new List<Usuario>();
            List<Menor> menores = new List<Menor>();

            while (true)
            {
                Console.WriteLine("************ Menú Principal **********");
                Console.WriteLine("1. Gestionar Usuario");
                Console.WriteLine("2. Línea de Emergencia");
                Console.WriteLine("3. Gestionar Lugares Frecuentes");
                Console.WriteLine("4. Enviar Alerta");
                Console.WriteLine("5. Agregar Persona");
                Console.WriteLine("6. Agregar Menor");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // Agregar un nuevo usuario
                        Console.Write("Ingrese el nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Ingrese el apellido: ");
                        string apellido = Console.ReadLine();
                        Console.Write("Ingrese la edad: ");
                        int edad;
                        if (int.TryParse(Console.ReadLine(), out edad))
                        {
                            Usuario nuevoUsuario = new Usuario
                            {
                                FirstName = nombre,
                                LastName = apellido,
                                Age = edad
                            };
                            usuarios.Add(nuevoUsuario);
                            Console.WriteLine("Usuario agregado con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("La edad ingresada no es válida.");
                        }
                        break;

                    case "2":
                        // Manejar la línea de emergencia
                        Console.Write("Ingrese el número de emergencia: ");
                        if (int.TryParse(Console.ReadLine(), out int numeroEmergencia))
                        {
                            lineaEmergencia.NumeroEmergencia = numeroEmergencia;

                            Console.Write("Ingrese la ubicación de emergencia: ");
                            string ubicacionEmergencia = Console.ReadLine();
                            lineaEmergencia.UbicacionEmergencia = ubicacionEmergencia;

                            // Realizar la llamada de emergencia
                            lineaEmergencia.RealizarLlamadaEmergencia();
                        }
                        else
                        {
                            Console.WriteLine("Número de emergencia no válido.");
                        }
                        break;

                    case "3":
                        // Gestionar lugares frecuentes
                        Console.Write("Ingrese un lugar frecuente: ");
                        string lugarFrecuente = Console.ReadLine();
                        Console.Write("Ingrese la latitud: ");
                        if (double.TryParse(Console.ReadLine(), out double latitud))
                        {
                            Console.Write("Ingrese la longitud: ");
                            if (double.TryParse(Console.ReadLine(), out double longitud))
                            {
                                lugaresFrecuentes.AgregarLugar(lugarFrecuente, latitud, longitud);
                                Console.WriteLine($"Lugar frecuente '{lugarFrecuente}' agregado con éxito.");
                            }
                            else
                            {
                                Console.WriteLine("Longitud no válida.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Latitud no válida.");
                        }
                        break;

                    case "4":
                        // Enviar alerta
                        if (usuarios.Count == 0)
                        {
                            Console.WriteLine("No se pueden enviar alertas, no hay usuarios registrados.");
                        }
                        else
                        {
                            Console.WriteLine("Enviando alerta a los contactos de emergencia...");

                            // Crear una nueva alerta
                            Alerta nuevaAlerta = new Alerta
                            {
                                Ubicacion = "Ubicación de la alerta",
                                Fecha = DateTime.Now,
                                Mensaje = "AYUDA!!! ESTOY EN PELIGRO"
                            };

                            nuevaAlerta.ContactosEmergencia = usuarios.Select(p => new ContactoEmergencia
                            {
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                Parentezco = "Familiar",
                                TelefonoContacto = "Número de Teléfono"
                            }).ToList();

                            nuevaAlerta.EnviarAlerta();
                            Console.WriteLine("Alerta enviada con éxito.");
                        }
                        break;

                    case "5":
                        // Agregar una persona
                        Console.Write("Ingrese el nombre de la persona: ");
                        string nombrePersona = Console.ReadLine();
                        Console.Write("Ingrese el apellido de la persona: ");
                        string apellidoPersona = Console.ReadLine();
                        Console.Write("Ingrese la edad de la persona: ");
                        int edadPersona;
                        if (int.TryParse(Console.ReadLine(), out edadPersona))
                        {
                            Usuario nuevaPersona = new Usuario
                            {
                                FirstName = nombrePersona,
                                LastName = apellidoPersona,
                                Age = edadPersona
                            };
                            usuarios.Add(nuevaPersona);
                            Console.WriteLine("Persona agregada con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("La edad ingresada no es válida.");
                        }
                        break;

case "6":
    // Agregar un menor
    Console.Write("Ingrese el nombre del menor: ");
    string nombreMenor = Console.ReadLine();
    Console.Write("Ingrese el apellido del menor: ");
    string apellidoMenor = Console.ReadLine();
    Console.Write("Ingrese la edad del menor: ");
    int edadMenor;
    if (int.TryParse(Console.ReadLine(), out edadMenor))
    {
        Menor nuevoMenor = new Menor(nombreMenor, apellidoMenor, edadMenor);
        menores.Add(nuevoMenor);
        Console.WriteLine("Menor agregado con éxito.");
    }
    else
    {
        Console.WriteLine("La edad ingresada no es válida.");
    }
    break;


                    case "7":
                        // Salir del programa
                        Console.WriteLine("Saliendo del programa.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
        }
    }
}

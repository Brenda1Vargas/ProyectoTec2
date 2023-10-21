using System.Reflection.Metadata;
//Brenda Vargas Parra - 614222713 & Viviana Garcia Valderrama - 506222720

using System;

namespace Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            Usuario firstUser = new Usuario();
            LineaEmergencia lineaEmergencia = new LineaEmergencia(123);
            LugaresFrecuentes lugaresFrecuentes = new LugaresFrecuentes();
            Alerta alerta = new Alerta();

            while (true)
            {
                Console.WriteLine("************ Menú Principal **********");
                Console.WriteLine("1. Usuario");
                Console.WriteLine("2. Línea de Emergencia");
                Console.WriteLine("3. Lugares Frecuentes");
                Console.WriteLine("4. Alerta");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Ingrese el apellido: ");
                        string apellido = Console.ReadLine();
                        Console.Write("Ingrese la edad: ");
                        int edad;
                        if (int.TryParse(Console.ReadLine(), out edad))
                        {
                            // Crear un nuevo usuario con los datos ingresados
                            Usuario nuevoUsuario = new Usuario();
                            nuevoUsuario.FirstName = nombre;
                            nuevoUsuario.LastName = apellido;
                            nuevoUsuario.Age = edad;
                        }
                        else
                        {
                            Console.WriteLine("La edad ingresada no es válida.");
                        }
                        break;

                    case "2":
                        lineaEmergencia.RealizarLlamadaEmergencia();
                        break;

                    case "3":
                        Console.Write("Ingrese el nombre del lugar frecuente: ");
                        string nombreLugar = Console.ReadLine();
                        Console.Write("Ingrese la latitud: ");
                        double latitud = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Ingrese la longitud: ");
                        double longitud = Convert.ToDouble(Console.ReadLine());

                        // Agregar el lugar frecuente a la lista de lugares frecuentes
                        lugaresFrecuentes.AgregarLugar(nombreLugar, latitud, longitud);
                        break;

                    case "4":
                        alerta.EnviarAlerta();
                        break;

                    case "5":
                        return; // Salir de la aplicación

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }
    }
}
using Application.Data;
using Application.Data.FirestoreModels;
using Application.Data.Respositories;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace Application
{
    public class Program
    {
        static void Main(string[] args)

        {

            Connection dbConn = new Connection();
            LineaEmergenciaRepository lineaEmergenciaRepo = new LineaEmergenciaRepository(dbConn);
            ContactoEmergenciaRepository contactoEmergenciaRepo = new ContactoEmergenciaRepository(dbConn);
            UbicacionRepository ubicacionRepo = new UbicacionRepository(dbConn);
            AlertaRepository alertaRepo = new AlertaRepository(dbConn);
            MenorRepository menorRepo = new MenorRepository(dbConn);
            MenorRepository meayorRepo = new MenorRepository(dbConn);

            while (true)
            {
                Console.WriteLine("************ Menú Principal **********");
                Console.WriteLine("1. Operaciones Mayor");
                Console.WriteLine("2. Operaciones Menor");
                Console.WriteLine("3. Operaciones Contacto de Emergencia");
                Console.WriteLine("4. Operaciones Alerta");
                Console.WriteLine("5. Operaciones Linea Emergencia Operations");
                Console.WriteLine("6. Operaciones Ubicacion");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        PerformMayorOperations();
                        break;
                    case "2":
                        PerformMenorOperations();
                        break;
                    case "3":
                        OperacionesContactoEmergencia(contactoEmergenciaRepo);
                        break;
                    case "4":
                        PerformAlertaOperations();
                        break;
                    case "5":
                        PerformLineaEmergenciaOperations();
                        break;
                    case "6":
                        PerformUbicacionOperations();
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

        #region Linea Emergencia Operations
        private static void PerformLineaEmergenciaOperations()
        {
            Connection dbConn = new Connection();
            LineaEmergenciaRepository lineaEmergenciaRepo = new LineaEmergenciaRepository(dbConn);

            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones de Linea de Emergencia -----------");
                Console.WriteLine("1. Crear Linea de Emergencia");
                Console.WriteLine("2. Mostrar Todas las Lineas de Emergencia");
                Console.WriteLine("3. Buscar Linea de Emergencia por ID");
                Console.WriteLine("4. Actualizar Linea de Emergencia");
                Console.WriteLine("5. Eliminar Linea de Emergencia");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Linea de Emergencia ------");
                        CreateLineaEmergencia(lineaEmergenciaRepo);
                        Console.WriteLine("-----------------------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todas las Lineas de Emergencia ------");
                        FindAllLineasEmergencia(lineaEmergenciaRepo);
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Linea de Emergencia por ID ------");
                        FindByIdLineaEmergencia(lineaEmergenciaRepo);
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Linea de Emergencia ------");
                        UpdateLineaEmergencia(lineaEmergenciaRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Linea de Emergencia ------");
                        DeleteLineaEmergencia(lineaEmergenciaRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones de Linea de Emergencia...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateLineaEmergencia(LineaEmergenciaRepository lineaEmergenciaRepo)
        {
            Console.WriteLine("------ Crear Linea de Emergencia ------");

            Console.Write("ID: ");
            string id = "1234567";

            Console.Write("Ingrese el número de emergencia al que desea comunicarse: ");
            string numero = Console.ReadLine();

            Console.Write("Ingrese su ubicación: ");
            string ubicacion = Console.ReadLine();

            LineaEmergencia lineaEmergencia = new LineaEmergencia(id, numero, ubicacion);
            lineaEmergenciaRepo.Insert(lineaEmergencia);

        }
        public static void FindAllLineasEmergencia(LineaEmergenciaRepository lineaEmergenciaRepo)
        {
            Console.WriteLine("------ Mostrar Todas las Lineas de Emergencia ------");

            var allLineaEmergencia = lineaEmergenciaRepo.FindAll();

            foreach (var item in allLineaEmergencia)
            {
                Console.WriteLine($"{item.Id}{item.NumeroEmergencia} {item.UbicacionEmergencia}");
            }

            if (allLineaEmergencia.Count == 0)
            {
                Console.WriteLine("No se encontraron registros de Lineas de Emergencia.");
            }
        }
        public static void FindByIdLineaEmergencia(LineaEmergenciaRepository lineaEmergenciaRepo)
        {
            Console.WriteLine("------ Buscar Linea de Emergencia por ID ------");

            var allLineaEmergencia = lineaEmergenciaRepo.FindAll();

            var oneLineaEmergencia = lineaEmergenciaRepo.FindById(allLineaEmergencia.First().Id);
            Console.WriteLine($"{oneLineaEmergencia.Id} {oneLineaEmergencia.NumeroEmergencia} {oneLineaEmergencia.UbicacionEmergencia} {oneLineaEmergencia.RealizarLlamadaEmergencia}");

        }
        public static void UpdateLineaEmergencia(LineaEmergenciaRepository lineaEmergenciaRepo)
        {
            Console.WriteLine("------ Actualizar Linea de Emergencia ------");

            var allLineaEmergencia = lineaEmergenciaRepo.FindAll();

            LineaEmergencia updatedLineaEmergencia = allLineaEmergencia.Last();


            Console.Write(" ID: ");
            var id = "1234567";

            Console.Write("Ingrese el número de emergencia al que desea comunicarse: ");

            updatedLineaEmergencia.NumeroEmergencia = Console.ReadLine();

            Console.Write("Ingrese su ubicación: ");
            updatedLineaEmergencia.UbicacionEmergencia = Console.ReadLine();

            lineaEmergenciaRepo.Update(updatedLineaEmergencia);
        }
        public static void DeleteLineaEmergencia(LineaEmergenciaRepository lineaEmergenciaRepo)
        {
            Console.WriteLine("------ Eliminar Linea de Emergencia ------");

            var allLineaEmergencia = lineaEmergenciaRepo.FindAll();
            lineaEmergenciaRepo.Delete(allLineaEmergencia.First().Id);
        }
        #endregion

        #region Operaciones Alerta
        private static void PerformAlertaOperations()
        {
            Connection dbConn = new Connection();
            AlertaRepository alertaRepo = new AlertaRepository(dbConn);

            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones de Alerta -----------");
                Console.WriteLine("1. Crear Alerta");
                Console.WriteLine("2. Mostrar Todas las Alertas");
                Console.WriteLine("3. Buscar Alerta por ID");
                Console.WriteLine("4. Actualizar Alerta");
                Console.WriteLine("5. Eliminar Alerta");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Alerta ------");
                        CreateAlerta(alertaRepo);
                        Console.WriteLine("-----------------------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todas las Alertas ------");
                        FindAllAlertas(alertaRepo);
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Alerta por ID ------");
                        FindAlertaById(alertaRepo);
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Alerta ------");
                        UpdateAlerta(alertaRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Alerta ------");
                        DeleteAlerta(alertaRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones de Alerta...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateAlerta(AlertaRepository alertaRepo)
        {
            Console.WriteLine("------ Crear Alerta...");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Alerta alertaOne = new Alerta();

            Console.Write("Ingrese la ubicación: ");
            alertaOne.Ubicacion = Console.ReadLine();

            Console.Write("Fecha actual: ");
            alertaOne.Fecha = DateTime.Now;

            Console.Write("Hora actual: ");
            alertaOne.Hora = DateTime.Now.Hour;

            Console.Write("Ingrese el mensaje: ");
            alertaOne.Mensaje = Console.ReadLine();

            Console.Write("Número de la alerta: ");
            alertaOne.Numero = 1234567;

            Console.Write("Ingrese el teléfono de contacto actual: ");
            alertaOne.TelefonoContacto = Console.ReadLine();

            // Ingresa los contactos de emergencia
            alertaOne.ContactosEmergencia = new List<ContactoEmergencia>();

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Datos del contacto de emergencia {i + 1}:");
                ContactoEmergencia contacto = new ContactoEmergencia();

                Console.Write("Ingrese el nombre completo: ");
                contacto.FullName = Console.ReadLine();

                Console.Write("Ingrese el teléfono de contacto: ");
                contacto.TelefonoContacto = Console.ReadLine();

                alertaOne.ContactosEmergencia.Add(contacto);
            }

            alertaRepo.Insert(alertaOne);
        }
        private static void FindAllAlertas(AlertaRepository alertaRepo)
        {
            Console.WriteLine("------ Mostrar Todas las Alertas...");

            var allAlertas = alertaRepo.FindAll();

            foreach (var item in allAlertas)
            {
                Console.WriteLine($"{item.Id} {item.Ubicacion}{item.Fecha} {item.Hora}{item.Mensaje}{item.Numero}{item.TelefonoContacto}{item.ContactosEmergencia}");
            }

        }
        private static void FindAlertaById(AlertaRepository alertaRepo)
        {
            Console.WriteLine("------ Buscar Alerta por ID");

            var allAlertas = alertaRepo.FindAll();
            var OneAlerta = alertaRepo.FindById(allAlertas.First().Id);
            Console.WriteLine($"{OneAlerta.Id} {OneAlerta.Ubicacion}{OneAlerta.Fecha}{OneAlerta.Mensaje}{OneAlerta.Mensaje} {OneAlerta.Numero} {OneAlerta.TelefonoContacto}{OneAlerta.ContactosEmergencia}");
        }
        private static void UpdateAlerta(AlertaRepository alertaRepo)
        {
            Console.WriteLine("------ Update Alerta");


            var allAlertas = alertaRepo.FindAll();
            Alerta updatedAlerta = allAlertas.Last();

            Console.Write($" Ingrese la nueva ubicacion (actual: {updatedAlerta.Ubicacion}): ");
            updatedAlerta.Ubicacion = Console.ReadLine();

            Console.Write("Fecha actual: ");
            updatedAlerta.Fecha = DateTime.Now;

            Console.Write("Hora actual: ");
            updatedAlerta.Hora = DateTime.Now.Hour;

            Console.Write($"Ingrese el nuevo Mensaje (actual: {updatedAlerta.Mensaje}): ");
            updatedAlerta.Mensaje = Console.ReadLine();

            Console.Write("Número de la alerta: ");
            updatedAlerta.Numero = 1234567;

            Console.Write($"Ingrese el nuevo telefono de contacto (actual: {updatedAlerta.TelefonoContacto}): ");
            updatedAlerta.TelefonoContacto = Console.ReadLine();

            alertaRepo.Update(updatedAlerta);
        }
        private static void DeleteAlerta(AlertaRepository alertaRepo)
        {
            Console.WriteLine("------ Delete Alerta");

            var allAlertas = alertaRepo.FindAll();
            alertaRepo.Delete(allAlertas.First().Id);
        }

        #endregion

        #region Operaciones ContactoEmergencia
        private static void OperacionesContactoEmergencia(ContactoEmergenciaRepository repo)
        {
            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones de Contacto de Emergencia -----------");
                Console.WriteLine("1. Crear Contacto de Emergencia");
                Console.WriteLine("2. Mostrar Todos los Contactos de Emergencia");
                Console.WriteLine("3. Buscar Contacto de Emergencia por ID");
                Console.WriteLine("4. Actualizar Contacto de Emergencia");
                Console.WriteLine("5. Eliminar Contacto de Emergencia");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Contacto de Emergencia ------");
                        CreateContactoEmergencia(repo);
                        Console.WriteLine("-----------------------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todos los Contactos de Emergencia ------");
                        FindAllContactosEmergencia(repo);
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Contacto de Emergencia por ID ------");
                        FindContactoEmergenciaById(repo);
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Contacto de Emergencia ------");
                        UpdateContactoEmergencia(repo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Contacto de Emergencia ------");
                        DeleteContactoEmergencia(repo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones de Contacto de Emergencia...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateContactoEmergencia(ContactoEmergenciaRepository repo)
        {
            Console.WriteLine("------ Crear Contacto de Emergencia...");

            var allContactosEmergencia = repo.FindAll();

            ContactoEmergencia oneContactoEmergencia = new ContactoEmergencia();


            Console.WriteLine("Ingrese la edad del contacto de emergencia:");
            oneContactoEmergencia.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre del contacto de emergencia:");
            oneContactoEmergencia.FirstName = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido del contacto de emergencia:");
            oneContactoEmergencia.LastName = Console.ReadLine();

            Console.WriteLine("Ingrese el correo electrónico del contacto de emergencia:");
            oneContactoEmergencia.Email = Console.ReadLine();

            Console.WriteLine("Ingrese la relación con el contacto de emergencia:");
            oneContactoEmergencia.Parentezco = Console.ReadLine();

            Console.WriteLine("Ingrese el número de teléfono del contacto de emergencia:");
            oneContactoEmergencia.TelefonoContacto = Console.ReadLine();

            Console.WriteLine("Nombre completo del contacto de emergencia:");
            oneContactoEmergencia.FullName = oneContactoEmergencia.FirstName + " " + oneContactoEmergencia.LastName;

            repo.Insert(oneContactoEmergencia);
        }
        private static void FindAllContactosEmergencia(ContactoEmergenciaRepository repo)
        {
            Console.WriteLine("------ Mostrar Todos los Contactos de Emergencia...");

            var allContactosEmergencia = repo.FindAll();

            foreach (var contacto in allContactosEmergencia)
            {
                Console.WriteLine($"{contacto.Id} {contacto.FirstName} {contacto.LastName} {contacto.Email} {contacto.Parentezco} {contacto.TelefonoContacto} {contacto.FullName}");
            }

            if (allContactosEmergencia.Count == 0)
            {
                Console.WriteLine("No se encontraron registros de Contactos de Emergencia.");
            }
        }
        private static void FindContactoEmergenciaById(ContactoEmergenciaRepository repo)
        {
            Console.WriteLine("------ Buscar Contacto de Emergencia por ID");

            var allContactos = repo.FindAll();

            var oneContacto = repo.FindById(allContactos.First().Id);
            Console.WriteLine($"{oneContacto.Id} {oneContacto.FirstName} {oneContacto.LastName} {oneContacto.Email} {oneContacto.Parentezco} {oneContacto.TelefonoContacto} {oneContacto.FullName}");

        }
        public static void UpdateContactoEmergencia(ContactoEmergenciaRepository repo)
        {
            Console.WriteLine("------ Actualizar Contacto de Emergencia ------");

            Console.WriteLine("------Update");

            var allContactosEmergencia = repo.FindAll();

            ContactoEmergencia updatedContactoEmergencia = allContactosEmergencia.Last();

            Console.WriteLine("Ingrese la nueva edad del contacto de emergencia:");
            updatedContactoEmergencia.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo nombre del contacto de emergencia:");
            updatedContactoEmergencia.FirstName = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo apellido del contacto de emergencia:");
            updatedContactoEmergencia.LastName = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo correo electrónico del contacto de emergencia:");
            updatedContactoEmergencia.Email = Console.ReadLine();

            Console.WriteLine("Ingrese la nueva relación con el contacto de emergencia:");
            updatedContactoEmergencia.Parentezco = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo número de teléfono del contacto de emergencia:");
            updatedContactoEmergencia.TelefonoContacto = Console.ReadLine();

            Console.WriteLine("Nuevo nombre completo del contacto de emergencia:");
            updatedContactoEmergencia.FullName = updatedContactoEmergencia.FirstName + " " + updatedContactoEmergencia.LastName;

            repo.Update(updatedContactoEmergencia);

        }
        public static void DeleteContactoEmergencia(ContactoEmergenciaRepository repo)
        {
            Console.WriteLine("------ Eliminar Contacto de Emergencia ------");

            var allContactosEmergencia = repo.FindAll();
            Console.WriteLine("------Delete");
            repo.Delete(allContactosEmergencia.First().Id);

        }
        #endregion

        #region Mayor Operations
        private static void PerformMayorOperations()
        {
            Connection dbConn = new Connection();
            MayorRepository mayorRepo = new MayorRepository(dbConn);

            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones  -----------");
                Console.WriteLine("1. Crear Mayor");
                Console.WriteLine("2. Mostrar Todos los Mayores");
                Console.WriteLine("3. Buscar Mayor por ID");
                Console.WriteLine("4. Actualizar Mayor");
                Console.WriteLine("5. Eliminar Mayor");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Mayor ------");
                        CreateMayor(mayorRepo);
                        Console.WriteLine("-------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todos los Mayores ------");
                        FindAllMayors(mayorRepo);
                        Console.WriteLine("---------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Mayor por ID ------");
                        FindMayorById(mayorRepo);
                        Console.WriteLine("---------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Mayor ------");
                        UpdateMayor(mayorRepo);
                        Console.WriteLine("------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Mayor ------");
                        DeleteMayor(mayorRepo);
                        Console.WriteLine("----------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones CRUD...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateMayor(MayorRepository mayorRepo)
        {
            Console.WriteLine("------ Create Mayor...");

            Mayor mayor = new Mayor();

            mayor.ContactoEmergencia = new List<ContactoEmergencia>();


            Console.Write(" Location ");
            mayor.LocationUrl = "6.803543797836747, -76.24792559070313";

            mayor.ImageUrl = "https://i0.wp.com/www.haciendadelasflores.gt/wp-content/uploads/2017/03/Reglas-para-tener-una-familia-feliz.png?w=1200&ssl=1";


            Console.Write("Ingrese el Primer Nombre: ");
            mayor.FirstName = Console.ReadLine();

            Console.Write("Ingrese el Apellido: ");
            mayor.LastName = Console.ReadLine();


            int age = 0;
            while (age <= 17)
            {
                Console.Write("Ingrese la Edad: ");

                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age <= 17)
                    {
                        Console.WriteLine("Esta en la seccion mayor de edad, debe ingresar una edad mayor a 18 años.");
                    }
                    else
                    {
                        mayor.Age = age;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de edad inválido. Debe ingresar un número entero.");
                }
            }


            Console.Write("Ingrese la Latitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double latitudHogar))
            {
                mayor.LatitudHogar = latitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de latitud inválido. Se establecerá en 0.");
                mayor.LatitudHogar = 0;
            }



            Console.Write("Ingrese la Longitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double longitudHogar))
            {
                mayor.LongitudHogar = longitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de longitud inválido. Se establecerá en 0.");
                mayor.LongitudHogar = 0;
            }

            Console.WriteLine("Ubicación Actual:");
            Console.WriteLine($"Latitud: {mayor.LatitudHogar}, Longitud: {mayor.LongitudHogar}");


            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in mayor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FirstName}, Teléfono: {contacto.TelefonoContacto}");
            }



            Console.WriteLine("Ingrese los lugares frecuentes:");

            List<Ubicacion> lugaresFrecuentes = new List<Ubicacion>();

            while (true)
            {
                Console.Write("Nombre (o escriba 'salir' para finalizar): ");
                string nombre = Console.ReadLine();

                if (nombre.ToLower() == "salir")
                    break;

                Console.Write("Latitud: ");
                double latitud = Convert.ToDouble(Console.ReadLine());

                Console.Write("Longitud: ");
                double longitud = Convert.ToDouble(Console.ReadLine());

                lugaresFrecuentes.Add(new Ubicacion(nombre, latitud, longitud));
            }

            Console.WriteLine("Lugares frecuentes ingresados:");
            foreach (var place in lugaresFrecuentes)
            {
                Console.WriteLine($"Nombre: {place.Nombre}, Latitud: {place.Latitud}, Longitud: {place.Longitud}");
            }


            Console.Write("¿Desea agregar información de Alarma de Emergencia? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                mayor.AlarmaEmergencia = new Alerta();


                Console.Write("Fecha: ");
                mayor.AlarmaEmergencia.Fecha = DateTime.UtcNow;

                Console.Write("Hora: ");
                int num;
                bool exit = int.TryParse(Console.ReadLine(), out num);
                if (exit)
                {
                    mayor.AlarmaEmergencia.Hora = num;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }


                Console.Write("Ingrese el Mensaje de la Alarma: ");
                mayor.AlarmaEmergencia.Mensaje = Console.ReadLine();


                Console.Write("Ingrese el numero de telefono: ");
                mayor.AlarmaEmergencia.TelefonoContacto = Console.ReadLine();


                Console.Write("Numero de la alarma: ");
                int numero;
                bool exito = int.TryParse(Console.ReadLine(), out numero);
                if (exito)
                {
                    mayor.AlarmaEmergencia.Numero = numero;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese la ubicacion: ");
                mayor.AlarmaEmergencia.Ubicacion = Console.ReadLine();
            }


            mayor.LugaresFrecuentes = lugaresFrecuentes;
            mayorRepo.Insert(mayor);



            Console.WriteLine("Mayor creado exitosamente.");
        }
        private static void FindAllMayors(MayorRepository mayorRepo)

        {
            Console.WriteLine("------ FindAll Mayor...");

            List<Mayor> mayors = mayorRepo.FindAll();

            if (mayors.Count == 0)
            {
                Console.WriteLine("No se encontraron registros de Mayores.");
            }
            else
            {
                var allMayor = mayorRepo.FindAll();

                foreach (var item in allMayor)
                {
                    Console.WriteLine($"{item.Id} {item.LatitudHogar} {item.LongitudHogar} {item.UbicacionActual}");

                    Console.WriteLine("Contactos de emergencia:");
                    foreach (var contacto in item.ContactoEmergencia)
                    {
                        Console.WriteLine($" Nombre: {contacto.FirstName}, Apellido: {contacto.LastName}, Nombre completo: {contacto.FullName},Id: {contacto.Id}, Edad: {contacto.Age}, Email; {contacto.Email} Teléfono: {contacto.TelefonoContacto}");
                    }
                }
            }
        }
        private static void FindMayorById(MayorRepository mayorRepo)
        {

            Console.WriteLine("------ FindById Mayor");

            var allMayor = mayorRepo.FindAll();


            var oneMayor = mayorRepo.FindById(allMayor.First().Id);
            Console.WriteLine($"{oneMayor.Id} {oneMayor.LatitudHogar} {oneMayor.LongitudHogar} {oneMayor.UbicacionActual}");

            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in oneMayor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FullName}, Teléfono: {contacto.TelefonoContacto}");
            }
        }
        private static void UpdateMayor(MayorRepository mayorRepo)
        {
            Console.WriteLine("------ Update Mayor");

            var allMayor = mayorRepo.FindAll();
            var oneMayor = mayorRepo.FindById(allMayor.First().Id);

            Console.Write("Ingrese el ID del Mayor que desea actualizar: ");
            var id = Console.ReadLine();

            var updatedMayor = mayorRepo.FindById(id);
            if (updatedMayor == null)
            {
                Console.WriteLine("Mayor no encontrado.");
                return;
            }

            Console.Write("Location: ");
            updatedMayor.LocationUrl = "6.803543797836747, -76.24792559070313";

            Console.Write("URL imagen: ");
            updatedMayor.ImageUrl = "https://i0.wp.com/www.haciendadelasflores.gt/wp-content/uploads/2017/03/Reglas-para-tener-una-familia-feliz.png?w=1200&ssl=1";

            Console.Write("Ingrese el nuevo Primer Nombre: ");
            updatedMayor.FirstName = Console.ReadLine();

            Console.Write("Ingrese el nuevo Apellido: ");
            updatedMayor.LastName = Console.ReadLine();

            int age = 0;
            while (age >= 18)
            {
                Console.Write("Ingrese la nueva Edad: ");

                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age >= 18)
                    {
                        Console.WriteLine("Esta en la sección de mayores de edad, debe ingresar una edad mayor o igual a 18 años.");
                    }
                    else
                    {
                        updatedMayor.Age = age;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de edad inválido. Debe ingresar un número entero.");
                }
            }

            Console.Write("Ingrese la nueva Latitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLatitudHogar))
            {
                updatedMayor.LatitudHogar = nuevaLatitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de latitud inválido. No se actualizará la latitud.");
            }

            Console.Write("Ingrese la nueva Longitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLongitudHogar))
            {
                updatedMayor.LongitudHogar = nuevaLongitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de longitud inválido. No se actualizará la longitud.");
            }

            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in updatedMayor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FirstName}, Teléfono: {contacto.TelefonoContacto}");
            }


            Console.WriteLine("Ingrese los lugares frecuentes:");

            List<Ubicacion> lugaresFrecuentes = new List<Ubicacion>();

            while (true)
            {
                Console.Write("Nombre (o escriba 'salir' para finalizar): ");
                string nombre = Console.ReadLine();

                if (nombre.ToLower() == "salir")
                    break;

                Console.Write("Latitud: ");
                double latitud = Convert.ToDouble(Console.ReadLine());

                Console.Write("Longitud: ");
                double longitud = Convert.ToDouble(Console.ReadLine());

                lugaresFrecuentes.Add(new Ubicacion(nombre, latitud, longitud));
            }

            Console.WriteLine("Lugares frecuentes ingresados:");
            foreach (var place in lugaresFrecuentes)
            {
                Console.WriteLine($"Nombre: {place.Nombre}, Latitud: {place.Latitud}, Longitud: {place.Longitud}");
            }

            Console.WriteLine("------ Actualizar Alarma de Emergencia ------");
            Console.Write("¿Desea actualizar la información de Alarma de Emergencia? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                if (updatedMayor.AlarmaEmergencia == null)
                {
                    updatedMayor.AlarmaEmergencia = new Alerta();
                }

                Console.Write("Fecha: ");
                updatedMayor.AlarmaEmergencia.Fecha = DateTime.UtcNow;

                Console.Write("Hora: ");
                int num;
                bool exit = int.TryParse(Console.ReadLine(), out num);
                if (exit)
                {
                    updatedMayor.AlarmaEmergencia.Hora = num;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese el Mensaje de la Alarma: ");
                updatedMayor.AlarmaEmergencia.Mensaje = Console.ReadLine();

                Console.Write("Ingrese el número de teléfono: ");
                updatedMayor.AlarmaEmergencia.TelefonoContacto = Console.ReadLine();

                Console.Write("Número de la alarma: ");
                int numero;
                bool exito = int.TryParse(Console.ReadLine(), out numero);
                if (exito)
                {
                    updatedMayor.AlarmaEmergencia.Numero = numero;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese la ubicación: ");
                updatedMayor.AlarmaEmergencia.Ubicacion = Console.ReadLine();
            }

            mayorRepo.Update(updatedMayor);

            Console.WriteLine("Mayor actualizado exitosamente.");
        }
        private static void DeleteMayor(MayorRepository mayorRepo)
        {
            Console.WriteLine("------ Delete Mayor");

            var allMayor = mayorRepo.FindAll();
            mayorRepo.Delete(allMayor.First().Id);
        }
        #endregion

        #region Menor Operations
        private static void PerformMenorOperations()
        {
            Connection dbConn = new Connection();
            MenorRepository menorRepo = new MenorRepository(dbConn);

            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones para Menores -----------");
                Console.WriteLine("1. Crear Menor");
                Console.WriteLine("2. Mostrar Todos los Menores");
                Console.WriteLine("3. Buscar Menor por ID");
                Console.WriteLine("4. Actualizar Menor");
                Console.WriteLine("5. Eliminar Menor");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Menor ------");
                        CreateMenor(menorRepo);
                        Console.WriteLine("-------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todos los Menores ------");
                        FindAllMenores(menorRepo);
                        Console.WriteLine("---------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Menor por ID ------");
                        FindMenorById(menorRepo);
                        Console.WriteLine("---------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Menor ------");
                        UpdateMenor(menorRepo);
                        Console.WriteLine("------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Menor ------");
                        DeleteMenor(menorRepo);
                        Console.WriteLine("----------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones CRUD para Menores...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateMenor(MenorRepository menorRepo)
        {
            Console.WriteLine("------ Create Menor...");
            Menor menor = new Menor();
            menor.ContactoEmergencia = new List<ContactoEmergencia>();

            Console.Write(" Location ");
            menor.LocationUrl = "6.803543797836747, -76.24792559070313";

            Console.Write("URL imagen: ");
            menor.ImageUrl = "https://i0.wp.com/www.haciendadelasflores.gt/wp-content/uploads/2017/03/Reglas-para-tener-una-familia-feliz.png?w=1200&ssl=1";

            Console.Write("Ingrese el Primer Nombre: ");
            menor.FirstName = Console.ReadLine();

            Console.Write("Ingrese el Apellido: ");
            menor.LastName = Console.ReadLine();

            int age = 0;
            while (age >= 17)
            {
                Console.Write("Ingrese la Edad: ");

                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age >= 17)
                    {
                        Console.WriteLine("Esta en la sección de menores de edad, debe ingresar una edad menor a 18 años.");
                    }
                    else
                    {
                        menor.Age = age;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de edad inválido. Debe ingresar un número entero.");
                }
            }

            Console.Write("Ingrese la Latitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double latitudHogar))
            {
                menor.LatitudHogar = latitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de latitud inválido. Se establecerá en 0.");
                menor.LatitudHogar = 0;
            }

            Console.Write("Ingrese la Longitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double longitudHogar))
            {
                menor.LongitudHogar = longitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de longitud inválido. Se establecerá en 0.");
                menor.LongitudHogar = 0;
            }

            Console.WriteLine("Ubicación Actual:");
            Console.WriteLine($"Latitud: {menor.LatitudHogar}, Longitud: {menor.LongitudHogar}");

            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in menor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FirstName}, Teléfono: {contacto.TelefonoContacto}");
            }
      
            
            Console.WriteLine("Ingrese los lugares frecuentes:");

            List<Ubicacion> lugaresFrecuentes = new List<Ubicacion>();

            while (true)
            {
                Console.Write("Nombre (o escriba 'salir' para finalizar): ");
                string nombre = Console.ReadLine();

                if (nombre.ToLower() == "salir")
                    break;

                Console.Write("Latitud: ");
                double latitud = Convert.ToDouble(Console.ReadLine());

                Console.Write("Longitud: ");
                double longitud = Convert.ToDouble(Console.ReadLine());

                lugaresFrecuentes.Add(new Ubicacion(nombre, latitud, longitud));
            }

            Console.WriteLine("Lugares frecuentes ingresados:");
            foreach (var place in lugaresFrecuentes)
            {
                Console.WriteLine($"Nombre: {place.Nombre}, Latitud: {place.Latitud}, Longitud: {place.Longitud}");
            }

            Console.Write("¿Desea agregar información de Alarma de Emergencia? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                menor.AlarmaEmergencia = new Alerta();

                Console.Write("Fecha: ");
                menor.AlarmaEmergencia.Fecha = DateTime.UtcNow;

                Console.Write("Hora: ");
                int num;
                bool exit = int.TryParse(Console.ReadLine(), out num);
                if (exit)
                {
                    menor.AlarmaEmergencia.Hora = num;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese el Mensaje de la Alarma: ");
                menor.AlarmaEmergencia.Mensaje = Console.ReadLine();

                Console.Write("Ingrese el numero de telefono: ");
                menor.AlarmaEmergencia.TelefonoContacto = Console.ReadLine();

                Console.Write("Numero de la alarma: ");
                int numero;
                bool exito = int.TryParse(Console.ReadLine(), out numero);
                if (exito)
                {
                    menor.AlarmaEmergencia.Numero = numero;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese la ubicacion: ");
                menor.AlarmaEmergencia.Ubicacion = Console.ReadLine();
            }

            menorRepo.Insert(menor);
            menor.LugaresFrecuentes = lugaresFrecuentes;

            Console.WriteLine("Menor creado exitosamente.");
        }
        private static void FindAllMenores(MenorRepository menorRepo)
        {
            Console.WriteLine("------ FindAll Menor...");

            List<Menor> menores = menorRepo.FindAll();

            if (menores.Count == 0)
            {
                Console.WriteLine("No se encontraron registros de Menores.");
            }
            else
            {
                var allMenor = menorRepo.FindAll();

                foreach (var item in allMenor)
                {
                    Console.WriteLine($"{item.Id} {item.LatitudHogar} {item.LongitudHogar} {item.UbicacionActual}");

                    Console.WriteLine("Contactos de emergencia:");
                    foreach (var contacto in item.ContactoEmergencia)
                    {
                        Console.WriteLine($" Nombre: {contacto.FirstName}, Apellido: {contacto.LastName}, Nombre completo: {contacto.FullName},Id: {contacto.Id}, Edad: {contacto.Age}, Email; {contacto.Email} Teléfono: {contacto.TelefonoContacto}");
                    }
                }
            }
        }
        private static void FindMenorById(MenorRepository menorRepo)
        {
            Console.WriteLine("------ FindById Menor");

            var allMenor = menorRepo.FindAll();

            var oneMenor = menorRepo.FindById(allMenor.First().Id);
            Console.WriteLine($"{oneMenor.Id} {oneMenor.LatitudHogar} {oneMenor.LongitudHogar} {oneMenor.UbicacionActual}");

            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in oneMenor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FullName}, Teléfono: {contacto.TelefonoContacto}");
            }
        }
        private static void UpdateMenor(MenorRepository menorRepo)
        {
            Console.WriteLine("------ Update Menor");

            var allMenor = menorRepo.FindAll();
            var oneMenor = menorRepo.FindById(allMenor.First().Id);

            Console.Write("Ingrese el ID del Menor que desea actualizar: ");
            var id = Console.ReadLine();

            var updatedMenor = menorRepo.FindById(id);
            if (updatedMenor == null)
            {
                Console.WriteLine("Menor no encontrado.");
                return;
            }

            Console.Write("Location: ");
            updatedMenor.LocationUrl = "6.803543797836747, -76.24792559070313";

            Console.Write("URL imagen: ");
            updatedMenor.ImageUrl = "https://i0.wp.com/www.haciendadelasflores.gt/wp-content/uploads/2017/03/Reglas-para-tener-una-familia-feliz.png?w=1200&ssl=1";

            Console.Write("Ingrese el nuevo Primer Nombre: ");
            updatedMenor.FirstName = Console.ReadLine();

            Console.Write("Ingrese el nuevo Apellido: ");
            updatedMenor.LastName = Console.ReadLine();

            int age = 0;
            while (age >= 17)
            {
                Console.Write("Ingrese la nueva Edad: ");

                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age >= 17)
                    {
                        Console.WriteLine("Esta en la sección de menores de edad, debe ingresar una edad menor a 18 años.");
                    }
                    else
                    {
                        updatedMenor.Age = age;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de edad inválido. Debe ingresar un número entero.");
                }
            }

            Console.Write("Ingrese la nueva Latitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLatitudHogar))
            {
                updatedMenor.LatitudHogar = nuevaLatitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de latitud inválido. No se actualizará la latitud.");
            }

            Console.Write("Ingrese la nueva Longitud del Hogar: ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLongitudHogar))
            {
                updatedMenor.LongitudHogar = nuevaLongitudHogar;
            }
            else
            {
                Console.WriteLine("Formato de longitud inválido. No se actualizará la longitud.");
            }

            Console.WriteLine("Contactos de emergencia:");
            foreach (var contacto in updatedMenor.ContactoEmergencia)
            {
                Console.WriteLine($"Nombre: {contacto.FirstName}, Teléfono: {contacto.TelefonoContacto}");
            }
            Console.WriteLine("Ingrese los lugares frecuentes:");



            Console.WriteLine("Ingrese los lugares frecuentes:");

            List<Ubicacion> lugaresFrecuentes = new List<Ubicacion>();

            while (true)
            {
                Console.Write("Nombre (o escriba 'salir' para finalizar): ");
                string nombre = Console.ReadLine();

                if (nombre.ToLower() == "salir")
                    break;

                Console.Write("Latitud: ");
                double latitud = Convert.ToDouble(Console.ReadLine());

                Console.Write("Longitud: ");
                double longitud = Convert.ToDouble(Console.ReadLine());

                lugaresFrecuentes.Add(new Ubicacion(nombre, latitud, longitud));
            }

            Console.WriteLine("Lugares frecuentes ingresados:");
            foreach (var place in lugaresFrecuentes)
            {
                Console.WriteLine($"Nombre: {place.Nombre}, Latitud: {place.Latitud}, Longitud: {place.Longitud}");
            }


            Console.WriteLine("------ Actualizar Alarma de Emergencia ------");
            Console.Write("¿Desea actualizar la información de Alarma de Emergencia? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                if (updatedMenor.AlarmaEmergencia == null)
                {
                    updatedMenor.AlarmaEmergencia = new Alerta();
                }

                Console.Write("Fecha: ");
                updatedMenor.AlarmaEmergencia.Fecha = DateTime.UtcNow;

                Console.Write("Hora: ");
                int num;
                bool exit = int.TryParse(Console.ReadLine(), out num);
                if (exit)
                {
                    updatedMenor.AlarmaEmergencia.Hora = num;
                }

                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese el Mensaje de la Alarma: ");
                updatedMenor.AlarmaEmergencia.Mensaje = Console.ReadLine();

                Console.Write("Ingrese el número de teléfono: ");
                updatedMenor.AlarmaEmergencia.TelefonoContacto = Console.ReadLine();

                Console.Write("Número de la alarma: ");
                int numero;
                bool exito = int.TryParse(Console.ReadLine(), out numero);
                if (exito)
                {
                    updatedMenor.AlarmaEmergencia.Numero = numero;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es un número válido.");
                }

                Console.Write("Ingrese la ubicación: ");
                updatedMenor.AlarmaEmergencia.Ubicacion = Console.ReadLine();
            }

            menorRepo.Update(updatedMenor);

            Console.WriteLine("Menor actualizado exitosamente.");
        }
        private static void DeleteMenor(MenorRepository menorRepo)
        {
            Console.WriteLine("------ Delete Menor");

            var allMenor = menorRepo.FindAll();
            menorRepo.Delete(allMenor.First().Id);
        }

        #endregion

        #region Ubicacion Operations
        private static void PerformUbicacionOperations()
        {
            Connection dbConn = new Connection();
            UbicacionRepository ubicacionRepo = new UbicacionRepository(dbConn);

            while (true)
            {
                Console.WriteLine("------- Submenú de Operaciones de Ubicación -----------");
                Console.WriteLine("1. Crear Ubicación");
                Console.WriteLine("2. Mostrar Todas las Ubicaciones");
                Console.WriteLine("3. Buscar Ubicación por ID");
                Console.WriteLine("4. Actualizar Ubicación");
                Console.WriteLine("5. Eliminar Ubicación");
                Console.WriteLine("6. Salir");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Ingrese el número de la operación que desea realizar: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("------ Crear Ubicación ------");
                        CreateUbicacion(ubicacionRepo);
                        Console.WriteLine("-----------------------------------------");
                        break;
                    case "2":
                        Console.WriteLine("------ Mostrar Todas las Ubicaciones ------");
                        FindAllUbicaciones(ubicacionRepo);
                        Console.WriteLine("----------------------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("------ Buscar Ubicación por ID ------");
                        FindUbicacionById(ubicacionRepo);
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    case "4":
                        Console.WriteLine("------ Actualizar Ubicación ------");
                        UpdateUbicacion(ubicacionRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "5":
                        Console.WriteLine("------ Eliminar Ubicación ------");
                        DeleteUbicacion(ubicacionRepo);
                        Console.WriteLine("--------------------------------------------");
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del Submenú de Operaciones de Ubicación...");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione un número válido.");
                        break;
                }

                Console.WriteLine();
            }
        }
        private static void CreateUbicacion(UbicacionRepository ubicacionRepo)
        {
            Console.Write("Ingrese el Nombre de su ubicacion: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese el ID: ");
            string id = "1234567";

            Console.Write("Ingrese la Longitud: ");
            if (double.TryParse(Console.ReadLine(), out double longitud))
            {
                Console.Write("Ingrese la Latitud: ");
                if (double.TryParse(Console.ReadLine(), out double latitud))
                {
                    // Crear la instancia de Ubicacion con los datos proporcionados por el usuario
                    Ubicacion ubicacion = new Ubicacion(id, longitud, nombre, latitud);


                    ubicacionRepo.Insert(ubicacion);

                    Console.WriteLine($"Succes Insert... {ubicacion.Nombre}");
                }
                else
                {
                    Console.WriteLine("Formato de latitud inválido.");
                }
            }
            else
            {
                Console.WriteLine("Formato de longitud inválido.");
            }
        }
        private static void FindAllUbicaciones(UbicacionRepository ubicacionRepo)
        {
            var allUbicacion = ubicacionRepo.FindAll();

            foreach (var item in allUbicacion)
            {
                Console.WriteLine($"{item.Latitud}{item.Id} {item.Nombre} {item.Longitud}");
            }
        }
        private static void FindUbicacionById(UbicacionRepository ubicacionRepo)
        {
            Console.WriteLine("------ Buscar Ubicación por ID");

            var allUbicacion = ubicacionRepo.FindAll();

            var oneMenor = ubicacionRepo.FindById(allUbicacion.First().Id);
            Console.WriteLine($"{oneMenor.Id} {oneMenor.Nombre} {oneMenor.Longitud} {oneMenor.Latitud}");
        }
        private static void UpdateUbicacion(UbicacionRepository ubicacionRepo)
        {
            Console.WriteLine("------ Actualizar Ubicación");

            var allUbicaciones = ubicacionRepo.FindAll();
            Console.Write("Ingrese el ID de la ubicación que desea actualizar: ");
            string id = Console.ReadLine();

            // Buscar la ubicación por ID
            var updateUbicacion = ubicacionRepo.FindById(id);
            if (updateUbicacion == null)
            {
                Console.WriteLine("No se encontró ninguna ubicación con el ID proporcionado.");
                return;
            }

            Console.Write($"Ingrese el nuevo Nombre de su ubicación (actual: {updateUbicacion.Nombre}): ");
            updateUbicacion.Nombre = Console.ReadLine();

            Console.Write($"Ingrese la nueva Latitud (actual: {updateUbicacion.Latitud}): ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLatitud))
            {
                updateUbicacion.Latitud = nuevaLatitud;
            }

            Console.Write($"Ingrese la nueva Longitud (actual: {updateUbicacion.Longitud}): ");
            if (double.TryParse(Console.ReadLine(), out double nuevaLongitud))
            {
                updateUbicacion.Longitud = nuevaLongitud;
            }

            ubicacionRepo.Update(updateUbicacion);

            Console.WriteLine($"Actualización exitosa. Ubicación actualizada: {updateUbicacion.Nombre}");
        }
        private static void DeleteUbicacion(UbicacionRepository ubicacionRepo)
        {
            Console.WriteLine("------ Eliminar Ubicación");

            var allUbicaciones = ubicacionRepo.FindAll();
            ubicacionRepo.Delete(allUbicaciones.First().Id);
            Console.WriteLine("Ubicación eliminada exitosamente.");
        }
    }
    #endregion
}


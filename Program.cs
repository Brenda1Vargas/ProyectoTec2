using Application.Data;
using Application.Data.FirestoreModels;
using Application.Data.Respositories;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
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


            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Create Linea Emergencia...");

            Console.Write("ID: ");
            string id = "1234567";

            Console.Write("Ingrese el número de emergencia al que desea comunicarse: ");
            string numero = Console.ReadLine();

            Console.Write("Ingrese su ubicación: ");
            string ubicacion = Console.ReadLine();

            LineaEmergencia lineaEmergencia = new LineaEmergencia(id, numero, ubicacion);
            lineaEmergenciaRepo.Insert(lineaEmergencia);

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ FindAll Linea Emergencia...");

            var allLineaEmergencia = lineaEmergenciaRepo.FindAll();

            foreach (var item in allLineaEmergencia)
            {
                Console.WriteLine($"{item.Id}{item.NumeroEmergencia} {item.UbicacionEmergencia}");
            }

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ FindById Linea Emergencia");

            var oneLineaEmergencia = lineaEmergenciaRepo.FindById(allLineaEmergencia.First().Id);
            Console.WriteLine($"{oneLineaEmergencia.Id} {oneLineaEmergencia.NumeroEmergencia} {oneLineaEmergencia.UbicacionEmergencia} {oneLineaEmergencia.RealizarLlamadaEmergencia}");


            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Delete Linea Emergencia");
            lineaEmergenciaRepo.Delete(allLineaEmergencia.First().Id);

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Update Linea Emergencia");

            LineaEmergencia updatedLineaEmergencia = allLineaEmergencia.Last();


            Console.Write("Ingrese su ID: ");
            id = "1234567";

            Console.Write("Ingrese el número de emergencia al que desea comunicarse: ");

            updatedLineaEmergencia.NumeroEmergencia = Console.ReadLine();

            Console.Write("Ingrese su ubicación: ");
            updatedLineaEmergencia.UbicacionEmergencia = Console.ReadLine();

            lineaEmergenciaRepo.Update(updatedLineaEmergencia);
        }

        #endregion

        #region Alerta Operations

        private static void PerformAlertaOperations()
        {
            Connection dbConn = new Connection();
            AlertaRepository alertaRepo = new AlertaRepository(dbConn);

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ Create ---");

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

            for (int i = 0; i < 2; i++) // Puedes ajustar según la cantidad de contactos que desees ingresar
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

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ FindAll Alertas");

            var allAlertas = alertaRepo.FindAll();

            foreach (var item in allAlertas)
            {
                Console.WriteLine($"{item.Id} {item.Ubicacion}{item.Fecha} {item.Hora}{item.Mensaje}{item.Numero}{item.TelefonoContacto}{item.ContactosEmergencia}");
            }

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ FindById Alerta");
            var OneAlerta = alertaRepo.FindById(allAlertas.First().Id);
            Console.WriteLine($"{OneAlerta.Id} {OneAlerta.Ubicacion}{OneAlerta.Fecha}{OneAlerta.Mensaje}{OneAlerta.Mensaje} {OneAlerta.Numero} {OneAlerta.TelefonoContacto}{OneAlerta.ContactosEmergencia}");

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ Delete Alerta");
            alertaRepo.Delete(allAlertas.First().Id);

            //-----------------------------------------------------------------------------------//


            Console.WriteLine("------ Update Alarta");
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
        #endregion

        #region Operaciones ContactoEmergencia

        private static void OperacionesContactoEmergencia(ContactoEmergenciaRepository repo)
        {

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------create...");

            Console.WriteLine("Ingrese la edad del contacto de emergencia:");
            int edad = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el nombre del contacto de emergencia:");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido del contacto de emergencia:");
            string apellido = Console.ReadLine();

            Console.WriteLine("Ingrese el correo electrónico del contacto de emergencia:");
            string correo = Console.ReadLine();

            Console.WriteLine("Ingrese la relación con el contacto de emergencia:");
            string relacion = Console.ReadLine();

            Console.WriteLine("Ingrese el número de teléfono del contacto de emergencia:");
            string telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el nombre completo del contacto de emergencia:");
            string nombreCompleto = Console.ReadLine();

            ContactoEmergencia contactoEmergencia = new ContactoEmergencia(string.Empty, edad, nombre, apellido, correo, relacion, telefono, nombreCompleto);
            repo.Insert(contactoEmergencia);

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------findAll...");

            var allContactos = repo.FindAll();

            foreach (var item in allContactos)
            {
                Console.WriteLine($"{item.Id} {item.FirstName} {item.LastName} {item.Email} {item.Parentezco} {item.TelefonoContacto} {item.FullName}");
            }

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------Find by id");

            var oneContacto = repo.FindById(allContactos.First().Id);
            Console.WriteLine($"{oneContacto.Id} {oneContacto.FirstName} {oneContacto.LastName} {oneContacto.Email} {oneContacto.Parentezco} {oneContacto.TelefonoContacto} {oneContacto.FullName}");

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------Delete");
            repo.Delete(allContactos.First().Id);

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------Update");

            ContactoEmergencia updatedContactoEmergencia = allContactos.Last();

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

            Console.WriteLine("Ingrese el nuevo nombre completo del contacto de emergencia:");
            updatedContactoEmergencia.FullName = Console.ReadLine();

            repo.Update(updatedContactoEmergencia);
        }

        #endregion

        #region Mayor Operations
        private static void PerformMayorOperations()
        {
            Connection dbConn = new Connection();
            MayorRepository mayorRepo = new MayorRepository(dbConn);



            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Create Mayor...");
            Mayor mayor = new Mayor();
            mayor.ContactoEmergencia = new List<ContactoEmergencia>();

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

            Console.WriteLine("Ingrese la Ubicación Actual:");

            Console.Write("Latitud: ");
            if (double.TryParse(Console.ReadLine(), out double latitudActual))
            {
                Console.Write("Longitud: ");
                if (double.TryParse(Console.ReadLine(), out double longitudActual))
                {
                    mayor.UbicacionActual = new Ubicacion(latitudActual, longitudActual);
                }
                else
                {
                    Console.WriteLine("Formato de longitud inválido. La ubicación actual no se establecerá.");
                }
            }
            else
            {
                Console.WriteLine("Formato de latitud inválido. La ubicación actual no se establecerá.");
            }


            Console.WriteLine("Contactos de emergencia:");

            if (mayor?.ContactoEmergencia != null)
            {
                foreach (var contactoEmergencia in mayor.ContactoEmergencia)
                {
                    Console.WriteLine($"Nombre: {contactoEmergencia.FirstName}");
                    Console.WriteLine($"Apellido: {contactoEmergencia.LastName}");
                    Console.WriteLine($"Nombre completo: {contactoEmergencia.FullName}");
                    Console.WriteLine($"ID: {contactoEmergencia.Id}");
                    Console.WriteLine($"Teléfono: {contactoEmergencia.TelefonoContacto}");
                    Console.WriteLine($"Edad: {contactoEmergencia.Age}");
                    Console.WriteLine($"Email: {contactoEmergencia.Email}");
                    Console.WriteLine($"Parentezco: {contactoEmergencia.Parentezco}");
                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No hay contactos de emergencia registrados.");
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
            mayorRepo.Insert(mayor);

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ FindAll Mayor...");

            var allMayor = mayorRepo.FindAll();

            foreach (var item in allMayor)
            {
                Console.WriteLine($"{item.Id}{item.LatitudHogar} {item.LongitudHogar} {item.AlarmaEmergencia}");
            }

            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ FindById Mayor");

            var oneMayor = mayorRepo.FindById(allMayor.First().Id);
            Console.WriteLine($"{oneMayor.Id} {oneMayor.LatitudHogar} {oneMayor.LongitudHogar} {oneMayor.AlarmaEmergencia}");


            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ Delete Mayor");
            mayorRepo.Delete(allMayor.First().Id);


            //-----------------------------------------------------------------------------------//

            Console.WriteLine("------ Update Mayor");

            Mayor updatedMayor = allMayor.Last();

            Console.Write("Ingrese el nuevo ID: ");
            updatedMayor.Id = Console.ReadLine();

            int nuevaEdad = updatedMayor.Age;
            while (nuevaEdad <= 17)
            {
                Console.Write("Ingrese la nueva Edad: ");

                if (int.TryParse(Console.ReadLine(), out nuevaEdad))
                {
                    if (nuevaEdad <= 17)
                    {
                        Console.WriteLine("Esta en la seccion mayor de edad. Debe ingresar una edad mayor a 18 años.");
                    }
                    else
                    {
                        updatedMayor.Age = nuevaEdad;
                    }
                }
                else
                {
                    Console.WriteLine("Formato de edad inválido. Debe ingresar un número entero.");
                }
            }

            Console.Write("Ingrese el nuevo número de emergencia al que desea comunicarse: ");
            if (double.TryParse(Console.ReadLine(), out double nuevoNumeroEmergencia))
            {
                updatedMayor.LatitudHogar = nuevoNumeroEmergencia;
            }
            else
            {
                Console.WriteLine("Formato de número de emergencia inválido. Se mantendrá el valor anterior.");
            }

            Console.Write("Ingrese su nueva ubicación: ");
            if (double.TryParse(Console.ReadLine(), out double NUbicacion))
            {
                updatedMayor.LongitudHogar = NUbicacion;
            }
            else
            {
                Console.WriteLine("Formato de ubicación inválido. Se mantendrá el valor anterior.");
            }

            mayorRepo.Update(updatedMayor);
        }
        #endregion

        #region Menor Operations

        private static void PerformMenorOperations()
        {
            Connection dbConn = new Connection();
            MenorRepository menorRepo = new MenorRepository(dbConn);

            Console.WriteLine("------ Create Menor...");

            Menor menor = new Menor
            {

                LatitudHogar = 222222,
                LongitudHogar = 333333,
                AlarmaEmergencia = null



            };


            menorRepo.Insert(menor);

            Console.WriteLine("------ FindAll menor...");

            var allMenor = menorRepo.FindAll();

            foreach (var item in allMenor)
            {
                Console.WriteLine($"{item.Id}{item.LatitudHogar} {item.LongitudHogar} {item.AlarmaEmergencia}");
            }

            Console.WriteLine("------ FindById Menor");

            var oneMenor = menorRepo.FindById(allMenor.First().Id);
            Console.WriteLine($"{oneMenor.Id} {oneMenor.LatitudHogar} {oneMenor.LongitudHogar} {oneMenor.AlarmaEmergencia}");

            Console.WriteLine("------ Delete Menor");
            menorRepo.Delete(allMenor.First().Id);

            Console.WriteLine("------ Update Menor");

            Menor updatedMenor = allMenor.Last();
            updatedMenor.Id = "324";
            oneMenor.AlarmaEmergencia = null;
            oneMenor.LatitudHogar = 0;
            oneMenor.LongitudHogar = 0;

            menorRepo.Update(updatedMenor);
        }

        #endregion

        #region Ubicacion Operations

        private static void PerformUbicacionOperations()
        {
            Connection dbConn = new Connection();
            UbicacionRepository ubicacionRepo = new UbicacionRepository(dbConn);

            //-----------------------------------------------------------------------------------//


            Console.WriteLine("------ Create Ubicacion...");

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

                    // Insertar la ubicación en el repositorio
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

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ FindAll Ubicacion...");

            var allUbicacion = ubicacionRepo.FindAll();

            foreach (var item in allUbicacion)
            {
                Console.WriteLine($"{item.Latitud}{item.Id} {item.Nombre} {item.Longitud}");
            }

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ FindById Ubicacion");

            var oneMenor = ubicacionRepo.FindById(allUbicacion.First().Id);
            Console.WriteLine($"{oneMenor.Id} {oneMenor.Nombre} {oneMenor.Longitud} {oneMenor.Latitud}");


            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Delete Ubicacion");
            ubicacionRepo.Delete(allUbicacion.First().Id);

            //-----------------------------------------------------------------------------------//
            Console.WriteLine("------ Update Ubicacion");

            Ubicacion updateUbicacion = allUbicacion.Last();

            Console.WriteLine(" ID ");
            updateUbicacion.Id = "1234567";

            Console.Write($"Ingrese el nuevo Nombre de su ubicacion (actual: {updateUbicacion.Nombre}): ");
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

            Console.WriteLine($"Succes Update... {updateUbicacion.Nombre}");
        }

        #endregion


        /*LineaEmergencia lineaEmergencia = new LineaEmergencia(string.Empty, "1234", "Bogotaa");
        lineaEmergenciaRepo.Insert(lineaEmergencia);

        Console.WriteLine("------findAll...");

        var all = lineaEmergenciaRepo.FindAll();

        foreach (var item in all)
        {
            Console.WriteLine($"{item.Id}{item.NumeroEmergencia} {item.UbicacionEmergencia}");
        }

        Console.WriteLine("------Find by id");



        var oneEntity = lineaEmergenciaRepo.FindById(all.First().Id);
        Console.WriteLine($"{oneEntity.Id} {oneEntity.NumeroEmergencia} {oneEntity.UbicacionEmergencia} {oneEntity.RealizarLlamadaEmergencia}");


        Console.ForegroundColor = ConsoleColor.Cyan;
        Alerta alertaOne = new Alerta
        {
            Id = "",
            Ubicacion = "Bogota",
            Fecha = DateTime.Now,
            Hora = DateTime.Now.Hour,
            Mensaje = "Ayuda, estoy en peligro",
            Numero = 1234567,  
            TelefonoContacto = "3156712473",
            ContactosEmergencia = new List<ContactoEmergencia>
            {
                new ContactoEmergencia { FullName = "Nombre1", TelefonoContacto = "3107568976" },
                new ContactoEmergencia { FullName = "Nombre2", TelefonoContacto = "3209876458" }

            }

        };

        alertaRepo.Insert(alertaOne);

        Console.WriteLine("------findAll Alertas");

        var allAlertas = alertaRepo.FindAll();

        foreach (var item in allAlertas)
        {
            Console.WriteLine($"{item.Id} {item.Ubicacion}{item.Fecha} {item.Hora}{item.Mensaje}{item.Numero}{item.TelefonoContacto}{item.ContactosEmergencia}");
        }

        Console.WriteLine("------find by id");
        var OneAlerta = alertaRepo.FindById(allAlertas.First().Id);
        Console.WriteLine($"{OneAlerta.Id} {OneAlerta.Ubicacion}{OneAlerta.Fecha}{OneAlerta.Mensaje}{OneAlerta.Mensaje} {OneAlerta.Numero} {OneAlerta.TelefonoContacto}{OneAlerta.ContactosEmergencia}");



        Console.WriteLine("------Delete");
        lineaEmergenciaRepo.Delete(all.First().Id);


        Console.WriteLine("------Update");

        LineaEmergencia updatedLineaEmergencia = all.Last();
        updatedLineaEmergencia.NumeroEmergencia = "123";
        updatedLineaEmergencia.UbicacionEmergencia = "Cali";

        lineaEmergenciaRepo.Update(updatedLineaEmergencia);

        Console.ReadLine();





        Console.WriteLine("------Create...");
          Ubicacion ubicacion = new Ubicacion(double.Empty, "1234", "Bogotaa");
          lineaEmergenciaRepo.Insert(lineaEmergencia);*/


        /* LineaEmergencia lineaEmergencia = new LineaEmergencia(123);
         Ubicacion lugaresFrecuentes = new Ubicacion();
         Alerta alerta = new Alerta();
         List<Usuario> usuarios = new List<Usuario>();
         List<Menor> menores = new List<Menor>();
         List<Mayor> mayores = new List<Mayor>();

         while (true)
         {
             Console.WriteLine("************ Menú Principal **********");
             Console.WriteLine("1. Gestionar Usuario");
             Console.WriteLine("2. Línea de Emergencia");
             Console.WriteLine("3. Gestionar Lugares Frecuentes");
             Console.WriteLine("4. Enviar Alerta");
             Console.WriteLine("5. Agregar Mayor");
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
                             //lugaresFrecuentes.AgregarLugar(lugarFrecuente, latitud, longitud);
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
                     // Agregar un mayor
                     Console.Write("Ingrese el nombre de la persona: ");
                     string nombreMayor = Console.ReadLine();
                     Console.Write("Ingrese el apellido de la persona: ");
                     string apellidoMayor = Console.ReadLine();
                     Console.Write("Ingrese la edad de la persona: ");
                     int edadMayor;
                     if (int.TryParse(Console.ReadLine(), out edadMayor))
                     {
                         Mayor nuevoMayor = new Mayor
                         {
                             FirstName = nombreMayor,
                             LastName = apellidoMayor,
                             Age = edadMayor
                         };
                         mayores.Add(nuevoMayor);
                         Console.WriteLine("Mayor agregado con éxito.");
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
         }*/

    }
}

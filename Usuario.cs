using System;
namespace Application 
{ 
    public class Usuario
    {
      
        private string firstName;
        private string lastName;
        private int age;
        private int _id;

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

        public string FullName
        {
            get { return $"{firstName} {lastName}"; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }

        }


        public int Age
        {
            get { return age; }
            set
            {
                if (value < 18)
                {
                    Console.WriteLine("La persona es menor de edad.");
                }
                else
                {
                    Console.WriteLine("La persona es mayor de edad.");
                }
                age = value;
            }
        }

    }



    public class Alarma
    {
        public void DetectarPeligro(Usuario usuario, string mensajePeligro)
        {

            if (mensajePeligro == "Eln")
            {
                Console.WriteLine($"¡Peligro para el usuario {usuario.FirstName}! Eln detectado.");
            
            }
            else if (mensajePeligro == "Farc")
            {
                Console.WriteLine($"¡Peligro para el usuario {usuario.FirstName}! Farc detectada.");
            }
       
        }
    }
}

using System;

namespace Application
{
    public class ContactoEmergencia
    {
        private int age;
        private int _id;
        private string firstName;
        private string lastName;
        private string email;
        private string parentezco;
        private string telefonoContacto;

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
                    throw new Exception("Invalid Age");
                }
                else
                {
                    age = value;
                }
            }
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

        public string FullName
        {
            get { return $"{firstName} {lastName}"; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Parentezco
        {
            get { return parentezco; }
            set { parentezco = value; }
        }

        public string TelefonoContacto
        {
            get { return telefonoContacto; }
            set { telefonoContacto = value; }
        }

    }
}

using System;

namespace Application
{
    public class ContactoEmergencia
    {
        private int age;
        private string _id;
        private string firstName;
        private string lastName;
        private string email;
        private string parentezco;
        private string telefonoContacto;
        private string fullName;

        public ContactoEmergencia()
        {

        }
        public ContactoEmergencia(string id, int age, string firstName, string lastName, string fullName, string email, string parentezco, string telefonoContacto)
        {
            Id = id;
            this.age = age;
            this.firstName = firstName;
            this.lastName = lastName;
            this.FullName = fullName;
            this.email = email;
            this.parentezco = parentezco;
            this.telefonoContacto = telefonoContacto;
        }

        public string Id
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

        public string FullName
        {
           get => fullName;
           set => fullName = value;
        }
    }
}

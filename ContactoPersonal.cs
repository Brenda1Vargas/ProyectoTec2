using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ContactoPersonal
    {
        private int age;
        private int _id;
        private string firstName;
        private string lastName;
        private string email;
        private string parentezco;

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
                if (age < 18)
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
    }


}

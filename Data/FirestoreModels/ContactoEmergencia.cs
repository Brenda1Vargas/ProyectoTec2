using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{
    [FirestoreData]

    public class ContactoEmergencia
    {
        [FirestoreDocumentId]
        public int Id { get; set; }
        [FirestoreProperty]

        public int Age { get; set; }
        [FirestoreProperty]

        public string FirstName { get; set; }
        [FirestoreProperty]

        public string LastName { get; set; }
        [FirestoreProperty]

        public string FullName { get; set; }
        [FirestoreProperty]

        public string Email { get; set; }
        [FirestoreProperty]

        public string Parentezco { get; set; }
        [FirestoreProperty]

        public string TelefonoContacto { get; set; }
    }
}

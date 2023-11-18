using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{

    public class Usuario
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public List<Ubicacion> LugaresFrecuentes { get; set; }
        [FirestoreProperty]
        public List<ContactoEmergencia> ContactoEmergencia { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public int Age { get; set; }

        [FirestoreProperty]
        public Ubicacion UbicacionActual { get; set; }
    }
}

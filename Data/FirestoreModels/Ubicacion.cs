using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{

    [FirestoreData]
    public class Ubicacion
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Nombre { get; set; }
        [FirestoreProperty]
        public double Latitud { get; set; }
        [FirestoreProperty]
        public double Longitud { get; set; }
    }
}

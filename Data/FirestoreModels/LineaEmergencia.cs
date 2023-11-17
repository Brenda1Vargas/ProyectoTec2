using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{
    [FirestoreData]
    public class LineaEmergencia
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string numeroEmergencia { get; set; }

        [FirestoreProperty]
        public string ubicacionEmergencia { get; set; }

    }
}

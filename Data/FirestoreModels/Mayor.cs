using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
 {
    [FirestoreData]
    public class Mayor : Usuario
    {

        [FirestoreProperty]
        public Alerta AlarmaEmergencia { get; set; }

        [FirestoreProperty]
        public double LatitudHogar { get; set; }

        [FirestoreProperty]
        public double LongitudHogar { get; set; }
    }
}

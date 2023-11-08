﻿using Google.Cloud.Firestore;
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
        public string id { get; set; }

        [FirestoreProperty]
        public int numeroEmergencia { get; set; }

        [FirestoreProperty]
        public string ubicacionEmergencia { get; set; }

        }
}

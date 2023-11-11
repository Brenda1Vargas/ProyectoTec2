using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Connection
    {
        public FirestoreDb FirestoreDb { get; set; }

        public Connection()
        {
            var filePath = @"Data\ejemploescuela-57cc9-firebase-adminsdk-wmst1-0016870e86.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            FirestoreDb = FirestoreDb.Create("ejemploescuela-57cc9");

        }
    }   

        
}

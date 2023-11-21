using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{
    [FirestoreData]
    public class Alerta
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Mensaje { get; set; }
        [FirestoreProperty]
        public string Ubicacion { get; set; }
        [FirestoreProperty]
        public int Hora { get; set; }
        [FirestoreProperty]
        public DateTime Fecha { get; set; }
        [FirestoreProperty]
        public int Numero { get; set; }
        [FirestoreProperty]
        public string TelefonoContacto { get; set; }
        public List<ContactoEmergencia> ContactosEmergencia { get => ContactosEmergencia; set => ContactosEmergencia = value; }
    }
}

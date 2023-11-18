using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.FirestoreModels
{
    public class Alerta
    {
        public string Mensaje { get; set; }

        public string Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
        public int Numero { get; set; }
        public string TelefonoContacto { get; set; }
        public List<ContactoEmergencia> ContactosEmergencia { get; set; }
    }
}

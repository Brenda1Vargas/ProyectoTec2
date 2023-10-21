using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Application
{
    public class Alerta
    {
        private string ubicacion;
        private DateTime fecha;
        private int hora;
        private int numero;
        private List<ContactoEmergencia> contactosEmergencia;


        public string Ubicacion
        {
            get { return ubicacion; }
            set { ubicacion = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public int Hora
        {
            get { return hora; }
            set { hora = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public List<ContactoEmergencia> ContactosEmergencia
        {
            get { return contactosEmergencia; }
            set { contactosEmergencia = value; }
        }

        public void EnviarAlerta()
        {
            foreach (var contacto in contactosEmergencia)
            {
                // Simulación de envío de alertas (puedes imprimir mensajes en la consola)
                Console.WriteLine($"Alerta enviada a {contacto.Nombre} ({contacto.Relacion}) en el número {contacto.Telefono}");
            }
        }
        public int GenerarID()
        {
            // Implementa la lógica para generar un ID único aquí
            return 0; // Por ahora, retorna un valor constante
        }

        public void ActualizarFecha(DateTime nuevaFecha)
        {
            Fecha = nuevaFecha;
        }
    }
}
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

        // Método para enviar la alerta a las autoridades o contactos personales.
        public void EnviarAlerta()
        {
            // notificar a las autoridades o contactos personales.
            Console.WriteLine($"Alerta enviada desde {Ubicacion} a las {Hora} el {Fecha}.");
        }

        // Generar un ID único para la alerta 
        public int GenerarID()
        {

            return 0;
        }


        // Actualizar la fecha de la alerta
        public void ActualizarFecha(DateTime nuevaFecha)
        {

            Fecha = nuevaFecha;
        }
    }
}
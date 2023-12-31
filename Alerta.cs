﻿using System;
using System.Collections.Generic;

namespace Application
{
    public class Alerta
    {
        private string mensaje;
        private string id;
        private string ubicacion;
        private DateTime fecha;
        private int hora;
        private int numero;
        private string telefonoContacto;
        private List<ContactoEmergencia> contactosEmergencia;

        public Alerta()
        {

        }

        public Alerta(string mensaje, string ubicacion, DateTime fecha, int hora, int numero, string telefonoContacto, List<ContactoEmergencia> contactosEmergencia)
        {
            this.mensaje = mensaje;
            this.ubicacion = ubicacion;
            this.fecha = fecha;
            this.hora = hora;
            this.numero = numero;
            this.telefonoContacto = telefonoContacto;
            this.contactosEmergencia = contactosEmergencia;
        }

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

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

        public string TelefonoContacto
        {
            get { return telefonoContacto; }
            set { telefonoContacto = value; }
        }

        public List<ContactoEmergencia> ContactosEmergencia
        {
            get { return contactosEmergencia; }
            set { contactosEmergencia = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }


    /*  public void EnviarAlerta()
      {
          foreach (var contacto in contactosEmergencia)
          {
              Console.WriteLine($"Alerta enviada a {contacto.FirstName} ({contacto.Parentezco}) en el número {contacto.TelefonoContacto}");

              // Comprobar si hay un mensaje y mostrarlo
              if (!string.IsNullOrEmpty(Mensaje))
              {
                  Console.WriteLine($"Mensaje: {Mensaje}");
              }
          }
      }

      public int GenerarID()
      {
          // Obtener la fecha y hora actual.
          DateTime ahora = DateTime.Now;

          // Generar un valor único.
          int valorUnico = ObtenerValorUnico();

          // Combinar la fecha y el valor único para generar un ID único.
          string idUnico = ahora.ToString("yyyyMMddHHmmssfff") + valorUnico;

          // Convierte el ID único a un entero.
          if (int.TryParse(idUnico, out int idEntero))
          {
              return idEntero;
          }

          // Si no se puede convertir a entero, retorna un valor predeterminado.
          return -1;
      }

      private int ObtenerValorUnico()
      {

          Random random = new Random();
          return random.Next(1000, 9999); // Número aleatorio de 4 dígitos como ejemplo.
      }


      public void ActualizarFecha(DateTime nuevaFecha)
      {
          Fecha = nuevaFecha;
      }*/
}


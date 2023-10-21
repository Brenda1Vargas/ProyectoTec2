using System;
using System.Collections.Generic;

namespace Application
{

    public class LugarFrecuente
    {
        private string nombre;
        private double latitud;
        private double longitud;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public double Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }

        public double Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }

        public LugarFrecuente(string nombre, double latitud, double longitud)
        {
            Nombre = nombre;
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}
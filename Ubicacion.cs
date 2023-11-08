using System;
using System.Collections.Generic;

namespace Application
{

    public class Ubicacion
    {
        private double longitud;
        private string nombre;
        private double latitud;


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

        public Ubicacion()
        {
                
        }
    }
}
using System;
using System.Collections.Generic;

namespace Application
{

    public class LugaresFrecuentes
    {
        private string nombre;
        private double latitud;
        private double longitud;
        private List<LugarFrecuente> lugares = new List<LugarFrecuente>();

        public LugaresFrecuentes()
        {
        }

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

        public void AgregarLugar(string nombreLugar, double latitud, double longitud)
        {
            LugarFrecuente lugar = new LugarFrecuente
            {
                Nombre = nombreLugar,
                Latitud = latitud,
                Longitud = longitud
            };

            lugares.Add(lugar);
        }
    }

    public class LugarFrecuente
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
    }
}
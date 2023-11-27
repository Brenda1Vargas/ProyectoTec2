using System;
using System.Collections.Generic;

namespace Application
{

    public class Ubicacion
    {
        private string id;
        private double longitud;
        private string nombre;
        private double latitud;
        private double latitudActual;
        private double longitudActual;

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

        public string Id { get => id; set => id = value; }
        public int V1 { get; }
        public int V2 { get; }
        public string V3 { get; }
        public int V4 { get; }


        public Ubicacion()
        {

        }

        public Ubicacion(string id)
        {
            this.id = id;
        }

        public Ubicacion(string id, double longitud, string nombre, double latitud) : this(id)
        {
            this.longitud = longitud;
            this.nombre = nombre;
            this.latitud = latitud;
        }
        public Ubicacion(string id, int v1, int v2, string v3, int v4) : this(id)
        {
        }

        public Ubicacion(double latitudActual, double longitudActual)
        {
            this.latitudActual = latitudActual;
            this.longitudActual = longitudActual;
        }
    }
}
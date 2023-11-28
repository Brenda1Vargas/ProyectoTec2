using System;

namespace Application
{
    public class Mayor : Usuario
    {
        private string nombreMayor;
        private int edadMayor;
        private Alerta alerta;

        public Alerta AlarmaEmergencia { get; set; }
        public double LatitudHogar { get; set; }
        public double LongitudHogar { get; set; }


        public Mayor()
        {
        }

        public Mayor(string nombre, int edad, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
            FirstName = nombre;
            Age = edad;
            AlarmaEmergencia = alarmaEmergencia;
        }

        public Mayor(string? nombreMayor)
        {
            this.nombreMayor = nombreMayor;
        }

        public Mayor(Alerta alarmaEmergencia, string nombreMayor)
        {
            this.nombreMayor = nombreMayor;
        }

        public Mayor(string? nombreMayor, int edadMayor, Alerta alerta)
        {
            this.nombreMayor = nombreMayor;
            this.edadMayor = edadMayor;
            this.alerta = alerta;
        }

        public Mayor(string nombre, int edad, double latitudHogar, double longitudHogar)
        {
            FirstName = nombre;
            Age = edad;
            AlarmaEmergencia = new Alerta(); 
            LatitudHogar = latitudHogar;
            LongitudHogar = longitudHogar;
        }
    }
}
using System;

namespace Application
{
    public class Menor : Usuario
    {
        private string nombreMenor;
        private int edadMenor;
        private Alerta alerta;

        public Alerta AlarmaEmergencia { get; set; }
        public double LatitudHogar { get; set; }
        public double LongitudHogar { get; set; }


        public Menor(string? nombreMenor, string? apellidoMenor, int edadMenor) : this(nombreMenor)
        {
        }

        public Menor(string nombre, int edad, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
            FirstName = nombre;
            Age = edad;
            AlarmaEmergencia = alarmaEmergencia;
        }

        public Menor(string nombreMenor, int edadMenor, Alerta alerta)
        {
            this.nombreMenor = nombreMenor;
            this.edadMenor = edadMenor;
            this.alerta = alerta;
        }

        public Menor(string? nombreMenor)
        {
            this.nombreMenor = nombreMenor;
        }

        public Menor( Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
      
            AlarmaEmergencia = alarmaEmergencia;
            LatitudHogar = latitudHogar;
            LongitudHogar = longitudHogar;
        }



    /*    public void DetectarPeligro(string mensajePeligro)
        {

            double distanciaHogar = CalcularDistancia(UbicacionActual.Latitud, UbicacionActual.Longitud, LatitudHogar, LongitudHogar);

            // Umbral de distancia de peligro (ajusta este valor según tus necesidades)
            double umbralDistanciaPeligro = 10.0; // Por ejemplo, 10 kilómetros


            if (distanciaHogar > umbralDistanciaPeligro)
            {
                AlarmaEmergencia.EnviarAlerta();
            }
        }

        private double CalcularDistancia(double latitud1, double longitud1, double latitud2, double longitud2)
        {
            // Esta función debe calcular la distancia entre dos coordenadas geográficas (latitud y longitud).
            // Debes implementar la fórmula de cálculo de distancia geodésica que se ajuste a tus necesidades.
            // Aquí hay un ejemplo simple usando la distancia euclidiana (no adecuada para distancias reales):
            double distancia = Math.Sqrt(Math.Pow(latitud1 - latitud2, 2) + Math.Pow(longitud1 - longitud2, 2));
            return distancia;
        }*/
    }
}
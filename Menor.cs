using System;

namespace Application
{
    public class Menor : Usuario
    {
        public double LatitudHogar { get; set; }
        public double LongitudHogar { get; set; }


        public Menor(string nombre, int edad, LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
        }
        public void DetectarPeligro(string mensajePeligro)
        {

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
        }
    }
}

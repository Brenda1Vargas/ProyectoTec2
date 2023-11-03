using System;

namespace Application
{
    public class Menor : Usuario
    {
        private string? nombreMenor;
        private int edadMenor;
        private LugaresFrecuentes lugarMenor;
        private Alerta alerta;

        public LugaresFrecuentes UbicacionActual { get; set; }
        public Alerta AlarmaEmergencia { get; set; }
        public double LatitudHogar { get; set; }
        public double LongitudHogar { get; set; }


        public Menor(string nombre, int edad, LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
            FirstName = nombre;
            Age = edad;
            UbicacionActual = ubicacionActual;
            AlarmaEmergencia = alarmaEmergencia;
        }

        public Menor(string? nombreMenor, int edadMenor, LugaresFrecuentes lugarMenor, Alerta alerta)
        {
            this.nombreMenor = nombreMenor;
            this.edadMenor = edadMenor;
            this.lugarMenor = lugarMenor;
            this.alerta = alerta;
        }

        public Menor(string? nombreMenor, string? apellidoMenor, int edadMenor) : this(nombreMenor)
        {
        }

        public Menor(string? nombreMenor)
        {
            this.nombreMenor = nombreMenor;
        }

        public void DetectarPeligro(string mensajePeligro)
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
        }
    }
}

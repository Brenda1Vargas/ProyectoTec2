using System;

namespace Application
{
    public class Mayor : Usuario
    {
        private string nombreMayor;
        private int edadMayor;
        private LugaresFrecuentes lugarMayor;
        private Alerta alerta;

        public LugaresFrecuentes UbicacionActual { get; set; }
        public Alerta AlarmaEmergencia { get; set; }
        public double LatitudHogar { get; set; }
        public double LongitudHogar { get; set; }


        public Mayor(string nombre, int edad, LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
            FirstName = nombre;
            Age = edad;
            UbicacionActual = ubicacionActual;
            AlarmaEmergencia = alarmaEmergencia;
        }

        public Mayor(string? nombreMayor, int edadMayor, LugaresFrecuentes lugarMayor, Alerta alerta)
        {
            this.nombreMayor = nombreMayor;
            this.edadMayor = edadMayor;
            this.lugarMayor = lugarMayor;
            this.alerta = alerta;
        }

        public Mayor(string? nombreMayor, string apellidoMayor, int edadMayor) : this(nombreMayor)
        {
        }

        public Mayor(LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia, string nombreMayor)
        {
            this.nombreMayor = nombreMayor;
        }

        public Mayor(LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia, double latitudHogar, double longitudHogar)
        {
            UbicacionActual = ubicacionActual;
            AlarmaEmergencia = alarmaEmergencia;
            LatitudHogar = latitudHogar;
            LongitudHogar = longitudHogar;
        }

        public Mayor(string? nombreMayor)
        {
            this.nombreMayor = nombreMayor;
        }

        public Mayor()
        {
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

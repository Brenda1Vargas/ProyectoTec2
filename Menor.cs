public class Menor : Usuario
{
    public LugaresFrecuentes UbicacionActual { get; set; }
    public Alerta AlarmaEmergencia { get; set; }

   
    public Menor(string nombre, int edad, LugaresFrecuentes ubicacionActual, Alerta alarmaEmergencia)
    {
        FirstName = nombre;
        Age = edad;
        UbicacionActual = ubicacionActual;
        AlarmaEmergencia = alarmaEmergencia;
    }

    public void DetectarPeligro(string mensajePeligro)
    {
     
        double distanciaHogar = CalcularDistancia(UbicacionActual.Latitud, UbicacionActual.Longitud, latitudHogar, longitudHogar);

        // Si la distancia al hogar es mayor que un umbral, consideramos que es peligroso.
        if (distanciaHogar > umbralDistanciaPeligro)
        {
            AlarmaEmergencia.EnviarAlerta();
        }
    }

}
}

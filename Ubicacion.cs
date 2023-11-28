public class Ubicacion
{
    private string _id;
    private double _longitud;
    private string _nombre;
    private double _latitud;
    private double _latitudActual;
    private double _longitudActual;

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public double Longitud
    {
        get { return _longitud; }
        set { _longitud = value; }
    }

    public string Nombre
    {
        get { return _nombre; }
        set { _nombre = value; }
    }

    public double Latitud
    {
        get { return _latitud; }
        set { _latitud = value; }
    }

    public double LatitudActual
    {
        get { return _latitudActual; }
        set { _latitudActual = value; }
    }

    public double LongitudActual
    {
        get { return _longitudActual; }
        set { _longitudActual = value; }
    }

    public Ubicacion()
    {
    }

    public Ubicacion(string nombre, double latitud, double longitud)
    {
    }
    public Ubicacion(string id)
    {
        _id = id;
    }

    public Ubicacion(string id, double longitud, string nombre, double latitud) : this(id)
    {
        _longitud = longitud;
        _nombre = nombre;
        _latitud = latitud;
    }

    public Ubicacion(double latitudActual, double longitudActual)
    {
        _latitudActual = latitudActual;
        _longitudActual = longitudActual;
    }
}
public abstract class Persona
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public abstract void DetectarPeligro(string mensajePeligro);
}

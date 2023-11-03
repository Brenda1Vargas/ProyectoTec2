public class Usuario
{
    private string firstName;
    private string lastName;
    private int age;
    private int _id;

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public string FullName
    {
        get { return $"{firstName} {lastName}"; }
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }
}

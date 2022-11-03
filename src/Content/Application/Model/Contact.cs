namespace ncapp.Model;

public class Contact
{
    public Contact(string name, string email)
    {
        _name = name;
        _email = email;
    }

    private string _name;
    private string _email;
}
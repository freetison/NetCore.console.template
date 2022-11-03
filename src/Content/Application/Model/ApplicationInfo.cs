namespace ncapp.Model;

public class ApplicationInfo : IApplicationInfo
{
    public ApplicationInfo(Contact contact)
    {
        App = string.Empty;
        Version = string.Empty;
        Description = string.Empty;
        Contact = contact;
    }

    public string App { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public Contact Contact { get; set; }
    
}
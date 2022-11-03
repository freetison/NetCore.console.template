namespace ncapp.Model;

public interface IApplicationInfo
{
    string App { get; set; }
    string Version { get; set; }
    string Description { get; set; }
    Contact Contact { get; set; }
}
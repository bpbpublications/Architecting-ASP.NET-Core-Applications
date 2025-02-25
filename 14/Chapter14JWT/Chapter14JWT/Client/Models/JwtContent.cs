namespace Chapter14JWT.Client.Models;

public class JwtContent
{
    public string Role { get; set; } = string.Empty;
    public string UniqueName { get; set; } = string.Empty;
    public bool Inhabitant { get; set; } = false;
    public bool Adjuster { get; set; } = false;
    public bool Calibrator { get; set; } = false;
    public bool Manufacturer { get; set; } = false;
    public bool Developer { get; set; } = false;
    public int Nbf { get; set; } = 0;
    public int Exp { get; set; } = 0;
    public int Iat { get; set; } = 0;
    public string Iss { get; set; } = string.Empty;
}

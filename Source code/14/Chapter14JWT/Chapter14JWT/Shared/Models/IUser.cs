namespace Chapter14JWT.Shared.Models;
public interface IUser
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Token { get; set; }
    List<int> AllowedFeatures { get; set; }
}

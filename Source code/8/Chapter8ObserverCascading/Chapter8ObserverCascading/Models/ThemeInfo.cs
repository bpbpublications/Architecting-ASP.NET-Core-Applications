using Chapter8ObserverCascading.Components.Pages;

namespace Chapter8ObserverCascading.Models;

public class ThemeInfo
{
    public string? MainTheme { get; set; }
    public void ChangeToDarkTheme() 
        => MainTheme = "div-container dark-theme";
    public void ChangeToLiteTheme() 
        => MainTheme = "div-container";
}

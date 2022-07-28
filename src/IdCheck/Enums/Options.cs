namespace IdCheck.Enums;

/// <summary>
/// Two different options for a response
/// <list type="bullet">
/// <item>None: will return a true or false response</item>
/// <item>Detailed: will return a ValidationResponse object that contains details like (Age, Gender, etc.)</item>
/// </list>
/// </summary>
public enum Options
{
    None,   
    Detailed
}
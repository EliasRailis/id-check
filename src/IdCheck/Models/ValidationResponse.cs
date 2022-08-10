namespace IdCheck.Models;

/// <summary>
/// This class will return a more detailed response
/// <list type="bullet">
/// <item>IsValid: will return if the ID number is valid or not</item>
/// <item>Age: will return the age that's been calculated from the ID number</item>
/// <item>DateOfBirth: will return the date of birth calculated from the ID number</item>
/// <item>Gender: will return the gender</item>
/// </list>
/// </summary>
public class ValidationResponse
{
    public bool IsValid { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Citizenship { get; init; }
    public int? Age { get; set; }
}
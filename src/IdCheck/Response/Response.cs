using IdCheck.Models;

namespace IdCheck.Response;

public static class ManageResponse
{
    /// <summary>
    /// This method is used to return a more detailed response object
    /// </summary>
    /// <param name="idNumber"></param>
    /// <param name="isValidResult"></param>
    /// <returns>Returns object</returns>
    public static ValidationResponse DetailedResponse(string idNumber, bool isValidResult)
    {
        var date = GetDateOfBirth(idNumber);
        var age = CalculateAge(date);
        
        var response = new ValidationResponse()
        {
            Gender = GetGender(idNumber),
            Citizenship = GetCitizenship(idNumber),
            IsValid = isValidResult,
            DateOfBirth = date, 
            Age = age 
        };
        
        return response;
    }

    /// <summary>
    /// This method will get the date of birth from the ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns DateTime</returns>
    public static DateOnly GetDateOfBirth(string id)
    {
        string year = id.Substring(0, 2);
        string month = id.Substring(2, 2);
        string day = id.Substring(4, 2);
        
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        string currentDateToString = currentDate.Year.ToString().Substring(2, 2);

        // Checking if the year is in range of 0 to current year
        if (Enumerable.Range(0, int.Parse(currentDateToString)).Contains(int.Parse(year)))
        {
            year = $"20{year}";
        }
        else
        {
            year = $"19{year}";
        }
        
        return DateOnly.Parse($"{month}/{day}/{year}");
    }

    /// <summary>
    /// This method will calculate the age from the date 
    /// </summary>
    /// <param name="date"></param>
    /// <returns>Returns int or null</returns>
    public static int? CalculateAge(DateOnly date)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        int age = currentDate.Year - date.Year;

        return age;
    }
    
    /// <summary>
    /// This method will grab the gender from the ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns string</returns>
    public static string? GetGender(string id)
    {
        string? gender = null;
        int genderValue = int.Parse(id.Substring(6, 1));

        if (genderValue >= 0 && genderValue <= 4)
        {
            gender = "Female";
        }
        else if (genderValue >= 5 && genderValue <= 9)
        {
            gender = "Male";
        }
        
        return gender;
    }

    /// <summary>
    /// This method will get the citizenship from the ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Will return a string</returns>
    public static string GetCitizenship(string id)
    {
        return (id.Substring(10, 1) == "0") ? "SA" : "Other";
    }
}
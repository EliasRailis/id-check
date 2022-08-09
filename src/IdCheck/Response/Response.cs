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
        // TODO Get the date
        // TODO Get the age
        
        var response = new ValidationResponse()
        {
            Gender = GetGender(idNumber),
            Citizenship = GetCitizenship(idNumber),
            IsValid = isValidResult,
            DateOfBirth = GetDateOfBirth(idNumber), 
            Age = 0
        };
        
        return response;
    }

    /// <summary>
    /// This method will get the date of birth from the ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns DateTime</returns>
    private static DateTime GetDateOfBirth(string id)
    {
        /*
        string year = idNumber.Substring(0, 2);
        string month = idNumber.Substring(2, 2);
        string day = idNumber.Substring(4, 2);
        string date = $"{year}/{month}/{day}";
        */
        
        return new DateTime();
    }

    /// <summary>
    /// This method will calculate the age from the date 
    /// </summary>
    /// <param name="date"></param>
    /// <returns>Returns int or null</returns>
    private static int? CalculateAge(DateTime date)
    {
        return 0;
    }
    
    /// <summary>
    /// This method will grab the gender from the ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns string</returns>
    private static string? GetGender(string id)
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
    private static string GetCitizenship(string id)
    {
        return (id.Substring(10, 1) == "0") ? "SA" : "Other";
    }
}
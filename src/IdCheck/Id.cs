using System.Text.RegularExpressions;
using IdCheck.Enums;
using IdCheck.Response;

namespace IdCheck;

/// <summary>
/// Validate ID number
/// </summary>
public static class Id
{
    // TODO: Testing
    // TODO: Comments
    // https://www.codeproject.com/Questions/268255/Validating-South-African-ID 
    
    /// <summary>
    /// Validation of the id number main method 
    /// </summary>
    /// <param name="idNumber">South African ID number</param>
    /// <param name="options">Will return a more detailed response or just (true or false)</param>
    /// <returns></returns>
    public static dynamic Validate(string idNumber, Options options = Options.None)
    {
        bool isValid = false;
        
        // Need to valid the string is numeric
        if (Regex.IsMatch(idNumber, @"^\d+$")) 
        {
            isValid = IsValidSouthAfricanIdNumber(idNumber);
        
            if (options == Options.Detailed && isValid)
            {
                return DetailedResponse(idNumber.Trim(), isValid);
            }
        }
        
        return isValid;
    }

    /// <summary>
    /// Logic behind a South african id number
    /// </summary>
    /// <param name="idNumber"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static bool IsValidSouthAfricanIdNumber(string idNumber)
    {
        if (string.IsNullOrEmpty(idNumber))
        {
            throw new ArgumentException(@"Id number cannot be null or empty", nameof(idNumber));
        }

        // Remove any whitespace from the string
        string idTrimString = idNumber.Trim();
        
        // Grab the control character
        int controlCharacter = int.Parse(idTrimString.Substring(idTrimString.Length - 1));
        
        // Remove last character (control character)
        string id = idTrimString.Remove(idTrimString.Length - 1);

        int oddPositionValuesResult = AddAllDigitsInOddPositions(id);
        int evenPositionValuesResult = MultiplyEvenDigits(id);
        
        // Adding the two results
        int result = oddPositionValuesResult + evenPositionValuesResult;

        // Grabbing the second character of the result
        int secondChar = int.Parse(
            result.ToString().Substring(result.ToString().Length - 1)
        );

        int total = 10 - secondChar;
        
        return (total == controlCharacter);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    private static int AddAllDigitsInOddPositions(string id)
    {
        int total;
        List<int> valuesInOddPosition = new();

        try
        {
            for (int i = 0; i < id.Length; i++)
            {
                if ((i + 1) % 2 == 1)
                {
                    valuesInOddPosition.Add(int.Parse(id[i].ToString()));
                }
            }

            total = (valuesInOddPosition.Count != 0) ? valuesInOddPosition.Sum() : 0;
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException(@"In AddAllDigitsInOddPositions method", ex);
        }
        
        return total;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    private static int MultiplyEvenDigits(string id)
    {
        int total = 0;
        string valuesInEvenPosition = string.Empty;

        try
        {
            for (int i = 0; i < id.Length; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    valuesInEvenPosition += id[i].ToString();
                }
            }
        
            int valuesInEvenPositionTotal = int.Parse(valuesInEvenPosition) * 2;
            string resultToString = valuesInEvenPositionTotal.ToString();

            for (int i = 0; i < resultToString.Length; i++)
            {
                total += int.Parse(resultToString[i].ToString());
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new IndexOutOfRangeException(@"In MultiplyEvenDigits method", ex);
        }

        return total;
    }

    /// <summary>
    /// This method is used to return a more detailed response object
    /// </summary>
    /// <param name="idNumber"></param>
    /// <param name="isValidResult"></param>
    /// <returns></returns>
    private static ValidationResponse DetailedResponse(string idNumber, bool isValidResult)
    {
        string year = idNumber.Substring(0, 2);
        string month = idNumber.Substring(2, 2);
        string day = idNumber.Substring(4, 2);
        string date = $"{year}/{month}/{day}";
        
        string? gender = null;
        int genderValue = int.Parse(idNumber.Substring(6, 1));

        if (genderValue >= 0 && genderValue <= 4)
        {
            gender = "Female";
        }
        else if (genderValue >= 5 && genderValue <= 9)
        {
            gender = "Male";
        }

        string citizenship = (idNumber.Substring(10, 1) == "0") ? "SA" : "Other";

        var response = new ValidationResponse()
        {
            Gender = gender,
            Citizenship = citizenship,
            IsValid = isValidResult,
            DateOfBirth = date
        };
        
        return response;
    }
}
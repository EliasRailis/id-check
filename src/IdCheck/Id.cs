using System.Text.RegularExpressions;
using IdCheck.Enums;
using IdCheck.Response;

namespace IdCheck;

/// <summary>
/// Validate ID number
/// </summary>
public static class Id
{
    /// <summary>
    /// Validation of the ID number main method 
    /// </summary>
    /// <param name="idNumber">South African ID number</param>
    /// <param name="options">Will return a more detailed response or just (true or false)</param>
    /// <returns>Returns true, false or detailed object</returns>
    public static dynamic Validate(string idNumber, Options options = Options.None)
    {
        bool isValid = false;
        
        if (string.IsNullOrEmpty(idNumber))
        {
            return false;
        }
        
        // Remove any whitespace from the string
        idNumber = idNumber.Trim();
        
        // Need to valid the string is numeric
        if (Regex.IsMatch(idNumber, @"^\d+$")) 
        {
            isValid = IsValidSouthAfricanIdNumber(idNumber);
        
            if (options == Options.Detailed && isValid)
            {
                return ManageResponse.DetailedResponse(idNumber.Trim(), isValid);
            }
        }
        
        return isValid;
    }

    /// <summary>
    /// Logic behind a South african id number
    /// </summary>
    /// <param name="idNumber"></param>
    /// <returns>Returns true or false</returns>
    private static bool IsValidSouthAfricanIdNumber(string idNumber)
    {
        bool valid = false;
        
        // Grab the control character
        int controlCharacter = int.Parse(idNumber.Substring(idNumber.Length - 1));
        
        // Remove last character (control character)
        string id = idNumber.Remove(idNumber.Length - 1);

        int? oddPositionValuesResult = AddAllDigitsInOddPositions(id);
        int? evenPositionValuesResult = MultiplyEvenDigits(id);
        
        // Adding the two results
        if (oddPositionValuesResult is not null && evenPositionValuesResult is not null)
        {
            int result = (int) oddPositionValuesResult + (int) evenPositionValuesResult;
            
            // Getting the second character of the result
            int secondChar = int.Parse(
                result.ToString().Substring(result.ToString().Length - 1)
            );
            
            int total = 10 - secondChar;
            valid = (total == controlCharacter);
        }
        
        return valid;
    }
    
    /// <summary>
    /// This method will get the all the values in the odd position of the ID
    /// Store the values into a List and return the sum
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns int or null</returns>
    /// <exception cref="OverflowException"></exception>
    private static int? AddAllDigitsInOddPositions(string id)
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
        catch (OverflowException)
        {
            return null;
        }
        
        return total;
    }

    /// <summary>
    /// This method will get all the value in the even position of the ID number and convert it into a string
    /// Then it will convert the string into a int that will be multiplied by 2
    /// Convert that result into a string and by using the for loop it will add into a total 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return int or null</returns>
    /// <exception cref="OverflowException"></exception>
    private static int? MultiplyEvenDigits(string id)
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
        catch (OverflowException)
        {
            return null;
        }

        return total;
    }
}
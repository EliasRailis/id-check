using IdCheck.Enums;
using IdCheck.Response;

namespace IdCheck;

/// <summary>
/// Validate South African ID number
/// </summary>
public static class Identification
{
    // https://www.codeproject.com/Questions/268255/Validating-South-African-ID

    public static dynamic Validate(string idNumber, Options options = Options.None)
    {
        bool isValid = IsValidIdNumber(idNumber);
        
        if (options == Options.Detailed)
        {
            return new ValidationResponse();
        }
        
        return isValid;
    }

    private static bool IsValidIdNumber(string idNumber)
    {
        // Grab the control character
        int controlCharacter = int.Parse(idNumber.Substring(idNumber.Length - 1));
        
        // Remove last character (control character)
        string id = idNumber.Remove(idNumber.Length - 1);
        
        List<int> oddPositionValues = new();
        string evenPositionValue = string.Empty;

        for (int i = 0; i < id.Length; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                evenPositionValue += id[i].ToString();
            }
            else
            {
                oddPositionValues.Add(int.Parse(id[i].ToString()));
            }
        }

        // Add all the digits of the ID number in the odd positions
        int oddPositionValuesTotal = oddPositionValues.Sum();
        
        // Take all the even digits as one number and multiply that by 2
        int evenPositionValueTotal = int.Parse(evenPositionValue) * 2;
        string evenPositionValueTotalToString = evenPositionValueTotal.ToString();
        int evenNumbersTotal = 0;

        for (int i = 0; i < evenPositionValueTotalToString.Length; i++)
        {
            evenNumbersTotal += int.Parse(evenPositionValueTotalToString[i].ToString());
        }
        
        int addTwoValuesTogether = oddPositionValuesTotal + evenNumbersTotal;
        string addTwoValuesTogetherToString = addTwoValuesTogether.ToString();
        
        int getSecondCharacter = int.Parse(
            addTwoValuesTogetherToString.Substring(addTwoValuesTogetherToString.Length - 1)
        );

        int total = 10 - getSecondCharacter;

        return (total == controlCharacter);
    }
    
    // Step 1: Add all the digits of the ID number in the odd positions
    // (except for the last number, which is the control digit)
    private static void AddAllDigitsInOddPositions()
    {
        
    }

    private static void MultiplyEvenDigits()
    {
        
    }
}
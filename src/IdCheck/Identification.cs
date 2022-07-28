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
        var validationResponse = new ValidationResponse();

        if (options == Options.Detailed)
        {
            return validationResponse;
        }
        
        return true;
    }
}
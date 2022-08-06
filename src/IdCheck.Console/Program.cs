
using IdCheck;
using IdCheck.Enums;

// This ID is used only for testing purposes
var validate = Id.Validate("8605065397083");
var validateWithDetailedOption = Id.Validate("8605065397083", Options.Detailed);

Console.ReadLine();

using IdCheck;
using IdCheck.Enums;

var validate = Identification.Validate("TEST");
var validateWithDetailedOption = Identification.Validate("TEST2", Options.Detailed);

Console.ReadLine();
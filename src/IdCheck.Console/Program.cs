
using IdCheck;
using IdCheck.Enums;

/*
    [InlineData("8605065397083", true)] // is valid
    [InlineData("8605065347083", false)] // is not valid
    [InlineData("", false)] // is not valid
    [InlineData(null, false)] // is not valid
    [InlineData("86050653470838605065347083", false)] // overflow exception
    [InlineData(" 8605065397083 ", true)] // has whitespaces
    [InlineData("860E65397083", false)] // isn't numeric string 
    [InlineData("%&^%#&^@**333", false)] // isn't numeric string 
    [InlineData("8605--=65397083", false)] // has special characters
    [InlineData("86050653", false)] // has less then 13 characters
*/

// Run some test to check the detailed response
var validateWithDetailedOption = Id.Validate("8605044397083", Options.Detailed);

Console.ReadLine();
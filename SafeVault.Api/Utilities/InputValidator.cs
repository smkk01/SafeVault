using System.Net;
using System.Text.RegularExpressions;

namespace SafeVault.Api.Utilities;

public static class InputValidator
{
    public static string SanitizeInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return WebUtility.HtmlEncode(input.Trim());
    }

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
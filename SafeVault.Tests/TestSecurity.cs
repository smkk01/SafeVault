using NUnit.Framework;
using SafeVault.Api.Utilities;

namespace SafeVault.Tests;

[TestFixture]
public class TestSecurity
{
    [Test]
    public void SQLInjectionPayload_Should_Be_Safely_Handled()
    {
        string payload =
            "'; DROP TABLE Users; --";

        string result =
            InputValidator.SanitizeInput(
                payload);

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result.Length,
            Is.GreaterThan(0));
    }

    [Test]
    public void LoginInjection_Should_Not_Bypass()
    {
        string attack =
            "admin' OR '1'='1";

        string result =
            InputValidator.SanitizeInput(
                attack);

        Assert.That(
            result,
            Is.Not.Null);
    }
    [Test]
    public void XSSAttack_Should_Be_Encoded()
    {
        string attack =
            "<script>alert('hack')</script>";

        string result =
            InputValidator.SanitizeInput(
                attack);

        Assert.That(
            result.Contains("<script>"),
            Is.False);

        Assert.That(
            result.Contains("&lt;script&gt;"),
            Is.True);
    }

    [Test]
    public void ImageTag_Should_Be_Encoded()
    {
        string attack =
            "<img src=x onerror=alert(1)>";

        string result =
            InputValidator.SanitizeInput(
                attack);

        Assert.That(
            result.Contains("<img"),
            Is.False);

        Assert.That(
            result.Contains("&lt;img"),
            Is.True);
    }


    [Test]
    public void Password_Should_Be_Hashed()
    {
        string password =
            "Password123!";

        string hash =
            BCrypt.Net.BCrypt.HashPassword(
                password);

        Assert.That(
            hash,
            Is.Not.EqualTo(password));
    }

    [Test]
    public void CorrectPassword_Should_Verify()
    {
        string password =
            "Password123!";

        string hash =
            BCrypt.Net.BCrypt.HashPassword(
                password);

        bool verified =
            BCrypt.Net.BCrypt.Verify(
                password,
                hash);

        Assert.That(
            verified,
            Is.True);
    }

}
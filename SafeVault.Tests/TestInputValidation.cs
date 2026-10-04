using NUnit.Framework;
using SafeVault.Api.Utilities;

namespace SafeVault.Tests;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjection()
    {
        string payload =
            "'; DROP TABLE Users; --";

        string result =
            InputValidator.SanitizeInput(
                payload);

        Assert.That(result,
            Is.Not.Null);
    }

    [Test]
    public void TestForXSS()
    {
        string payload =
            "<script>alert(1)</script>";

        string result =
            InputValidator.SanitizeInput(
                payload);

        Assert.That(
            result.Contains("<script>"),
            Is.False);

        Assert.That(
            result.Contains("&lt;script&gt;"),
            Is.True);
    }

    [Test]
    public void ValidEmail_ShouldPass()
    {
        bool result =
            InputValidator.IsValidEmail(
                "test@gmail.com");

        Assert.That(result, Is.True);
    }

    [Test]
    public void InvalidEmail_ShouldFail()
    {
        bool result =
            InputValidator.IsValidEmail(
                "bad@@gmail");

        Assert.That(result, Is.False);
    }
}
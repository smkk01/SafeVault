using NUnit.Framework;
using SafeVault.Api.Services;

namespace SafeVault.Tests;

public class TestAuthentication
{
    private AuthService _authService;

    [SetUp]
    public void Setup()
    {
        _authService =
            new AuthService();
    }

    [Test]
    public void Password_Should_Be_Hashed()
    {
        string password =
            "Password123!";

        string hash =
            _authService.HashPassword(
                password);

        Assert.That(
            hash,
            Is.Not.EqualTo(password));
    }

    [Test]
    public void Valid_Password_Should_Verify()
    {
        string password =
            "Password123!";

        string hash =
            _authService.HashPassword(
                password);

        bool result =
            _authService.VerifyPassword(
                password,
                hash);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Invalid_Password_Should_Fail()
    {
        string hash =
            _authService.HashPassword(
                "Password123!");

        bool result =
            _authService.VerifyPassword(
                "WrongPassword",
                hash);

        Assert.That(result, Is.False);
    }
}
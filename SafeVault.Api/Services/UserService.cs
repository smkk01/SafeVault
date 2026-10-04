using MySql.Data.MySqlClient;
using SafeVault.Api.Models;

namespace SafeVault.Api.Services;

public class UserService
{
    private readonly string _connectionString;

    public UserService(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")!;
    }

    public void AddUser(string username, string email)
    {
        string sql = @"
            INSERT INTO Users
            (Username, Email)
            VALUES
            (@Username,@Email)";

        using var connection =
            new MySqlConnection(_connectionString);

        using var command =
            new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@Username",
            username);

        command.Parameters.AddWithValue(
            "@Email",
            email);

        connection.Open();

        command.ExecuteNonQuery();
    }

    public User? GetUser(string username)
    {
        string sql = @"
            SELECT *
            FROM Users
            WHERE Username=@Username";

        using var connection =
            new MySqlConnection(_connectionString);

        using var command =
            new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@Username",
            username);

        connection.Open();

        using var reader =
            command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User
        {
            UserID = reader.GetInt32("UserID"),
            Username = reader.GetString("Username"),
            Email = reader.GetString("Email")
        };
    }

    public void RegisterUser(string username, string email,string passwordHash, string role)
    {
        string sql = @"
                        INSERT INTO Users
                        (Username,Email,PasswordHash,Role)
                        VALUES
                        (@Username,@Email,@PasswordHash,@Role)";

        using var connection =
            new MySqlConnection(_connectionString);

        using var command =
            new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@Username", username);

        command.Parameters.AddWithValue(
            "@Email", email);

        command.Parameters.AddWithValue(
            "@PasswordHash", passwordHash);

        command.Parameters.AddWithValue(
            "@Role", role);

        connection.Open();

        command.ExecuteNonQuery();
    }


    public User? GetUserByUsername( string username)
    {
        string sql = @"
        SELECT *
        FROM Users
        WHERE Username=@Username";

        using var connection =
            new MySqlConnection(_connectionString);

        using var command =
            new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            "@Username", username);

        connection.Open();

        using var reader =
            command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User
        {
            UserID = reader.GetInt32("UserID"),
            Username = reader.GetString("Username"),
            Email = reader.GetString("Email"),
            PasswordHash = reader.GetString("PasswordHash"),
            Role = reader.GetString("Role")
        };
    }



}
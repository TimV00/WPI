namespace dal;

using System.Data;

using MySql.Data.MySqlClient;
//using System.Data.SqlClient;

public static class EmployeeDal
{
    //private string ConnectionString { get; } = connectionString;
    private const string connectionString = "server=host.docker.internal;port=3333;uid=root;pwd=pass;database=dev";

    public static DataTable GetAll()
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select * from Employee;", connection))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }
    public static DataTable GetBy(int id)
    {
        var dt = new DataTable();
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var da = new MySqlDataAdapter(@"select * from Employee where employee_id=" + id.ToString() + ";", connection))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }
    public static int Create(
        string firstName,
        string lastName,
        string address,
        string phoneNumber,
        string officeNumber,
        int yearsOfService,
        int departmentId)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand(@"
            INSERT INTO Employee
                (first_name, last_name, address, phone_number, office_number, years_of_service, department_id)
            VALUES
                (@first_name, @last_name, @address, @phone_number, @office_number, @years_of_service, @department_id);
            SELECT LAST_INSERT_ID();
        ", connection);

        cmd.Parameters.AddWithValue("@first_name", firstName);
        cmd.Parameters.AddWithValue("@last_name", lastName);
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@phone_number", phoneNumber);
        cmd.Parameters.AddWithValue("@office_number", officeNumber);
        cmd.Parameters.AddWithValue("@years_of_service", yearsOfService);
        cmd.Parameters.AddWithValue("@department_id", departmentId);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
    public static int Update(
        int employeeId,
        string firstName,
        string lastName,
        string address,
        string phoneNumber,
        string officeNumber,
        int yearsOfService,
        int departmentId)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand(@"
            UPDATE Employee
            SET first_name = @first_name,
                last_name = @last_name,
                address = @address,
                phone_number = @phone_number,
                office_number = @office_number,
                years_of_service = @years_of_service,
                department_id = @department_id
            WHERE employee_id = @employee_id;
        ", connection);

        cmd.Parameters.AddWithValue("@employee_id", employeeId);
        cmd.Parameters.AddWithValue("@first_name", firstName);
        cmd.Parameters.AddWithValue("@last_name", lastName);
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@phone_number", phoneNumber);
        cmd.Parameters.AddWithValue("@office_number", officeNumber);
        cmd.Parameters.AddWithValue("@years_of_service", yearsOfService);
        cmd.Parameters.AddWithValue("@department_id", departmentId);

        return cmd.ExecuteNonQuery();
    }
    public static int DeleteEmployee(int id)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand("delete from Employee where employee_id = @id;", connection);
        cmd.Parameters.AddWithValue("@id", id);

        return cmd.ExecuteNonQuery();
    }

}

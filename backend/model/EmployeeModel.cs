namespace model;

using System.Data.Common;
using dal;
using System.Data;

public class Employee
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? OfficeNumber { get; set; }
    public int? YearsOfService { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? FullName { get; set; }
}

public static class EmployeeModel
{
    // Get all employees
    public static List<Employee> GetAll()
    {
        var employees = new List<Employee>();
        var dt = EmployeeDal.GetAll();
        foreach (DataRow r in dt.Rows)
        {
            employees.Add(new Employee
            {
                Id = (int)r["employee_id"],
                FirstName = (string)r["first_name"],
                LastName = (string)r["last_name"],
                Address = r["address"] == DBNull.Value ? null : r["address"].ToString(),
                PhoneNumber = r["phone_number"] == DBNull.Value ? null : r["phone_number"].ToString(),
                OfficeNumber = r["office_number"] == DBNull.Value ? null : r["office_number"].ToString(),
                YearsOfService = r["years_of_service"] == DBNull.Value ? null : Convert.ToInt32(r["years_of_service"]),
                DepartmentId = (int)r["department_id"],
                DepartmentName = (string)r["department_name"],
                FullName = (string)r["first_name"] + " " + (string)r["last_name"]
            });
        }

        return employees;
    }

    public static Employee GetBy(int id)
    {
        var dt = EmployeeDal.GetBy(id);

        // check if we are trying to get an employee that doesn't exist
        if (dt == null || dt.Rows.Count == 0)
            return null;

        var r = dt.Rows[0];


        var employee = new Employee
        {
            Id = (int)dt.Rows[0]["employee_id"],
            FirstName = (string)dt.Rows[0]["first_name"],
            LastName = (string)dt.Rows[0]["last_name"],
            Address = r["address"] == DBNull.Value ? null : r["address"].ToString(),
            PhoneNumber = r["phone_number"] == DBNull.Value ? null : r["phone_number"].ToString(),
            OfficeNumber = r["office_number"] == DBNull.Value ? null : r["office_number"].ToString(),
            YearsOfService = r["years_of_service"] == DBNull.Value ? null : Convert.ToInt32(r["years_of_service"]),
            DepartmentId = (int)dt.Rows[0]["department_id"],
            DepartmentName = (string)dt.Rows[0]["department_name"],
            FullName = (string)dt.Rows[0]["first_name"] + " " + (string)dt.Rows[0]["last_name"]
        };
        return employee;
    }
    public static int Create(Employee emp)
    {
        var newId = EmployeeDal.Create(
            emp.FirstName!,
            emp.LastName!,
            emp.Address ?? "",
            emp.PhoneNumber ?? "",
            emp.OfficeNumber ?? "",
            emp.YearsOfService.Value,
            emp.DepartmentId.Value
        );

        return newId;
    }
    public static int Update(Employee emp)
    {
        var rows = EmployeeDal.Update(
            emp.Id.Value,
            emp.FirstName!,
            emp.LastName!,
            emp.Address ?? "",
            emp.PhoneNumber ?? "",
            emp.OfficeNumber ?? "",
            emp.YearsOfService.Value,
            emp.DepartmentId.Value
        );

        return rows;
    }
    public static int DeleteEmployee(int id)
    {
        return EmployeeDal.DeleteEmployee(id);
    }

}


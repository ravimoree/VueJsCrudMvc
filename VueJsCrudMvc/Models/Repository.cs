using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VueJsCrudMvc.Models
{
    public class Repository : IRepository
    {
        string _ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private EmployeeContext employeeContext;
        public Repository(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        public List<Employee> GetAllEmployees()
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        List<Employee> empList = new List<Employee>();
                        SqlCommand cmd = new SqlCommand("UspVueJsCRUD", _connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Task", "Select");
                        _connection.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            Employee e = new Employee();
                            e.EmployeeID = Convert.ToInt32(sdr["EmployeeID"].ToString());
                            e.Name = sdr["Name"].ToString();
                            e.Position = sdr["Position"].ToString();
                            e.Age = Convert.ToInt32(sdr["Age"]);
                            e.Email = Convert.ToString(sdr["Email"]);
                            e.Salary = Convert.ToInt32(sdr["Salary"]);
                            e.Gender = sdr["Gender"].ToString();
                            e.IsActive = Convert.ToBoolean(sdr["IsActive"]);
                            empList.Add(e);
                        }
                        _connection.Close();
                        return empList;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool SaveEmployee(Employee obj)
        {
            using (SqlConnection _connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UspVueJsCRUD", _connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Task", "Insert");
                        cmd.Parameters.AddWithValue("@Name", obj.Name);
                        cmd.Parameters.AddWithValue("@PID", obj.PID);
                        cmd.Parameters.AddWithValue("@Age", obj.Age);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);
                        cmd.Parameters.AddWithValue("@Salary", obj.Salary);
                        cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                        cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                        _connection.Open();
                        if (obj.EmployeeID == 0)
                        {
                            cmd.Parameters.AddWithValue("@EmployeeID", null);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                        }
                        int output = cmd.ExecuteNonQuery();
                        //  int output = con.Execute("AngularCRUD", paramater, null, 0, CommandType.StoredProcedure);
                        _connection.Close();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
        public bool DeleteEmployee(int ID)
        {
            using (SqlConnection _connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UspVueJsCRUD", _connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Task", "Delete");
                        cmd.Parameters.AddWithValue("@EmployeeID", ID);
                        _connection.Open();
                        int output = cmd.ExecuteNonQuery();
                        _connection.Close();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
        public Employee GetEmployeesById(int id)
        {
            DataSet ds = null;
            Employee empList = new Employee();

            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        SqlCommand cmd = new SqlCommand("SELECT [EmployeeID],[Name],[PID],[Age],Email,[salary],Gender,IsActive FROM [dbo].[Employee] where EmployeeID=@id", _connection);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            empList.EmployeeID = Convert.ToInt32(ds.Tables[0].Rows[i]["EmployeeID"].ToString());
                            empList.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            empList.PID = Convert.ToInt32(ds.Tables[0].Rows[i]["PID"]);
                            empList.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"]);
                            empList.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                            empList.Salary = Convert.ToInt32(ds.Tables[0].Rows[i]["Salary"]);
                            empList.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                            empList.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]);
                        }
                        _connection.Close();
                        return empList;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        public IEnumerable<Position> GetPositions()
        {
            List<Position> posList = new List<Position>();
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        SqlCommand cmd = new SqlCommand("SELECT [PositionID],[Name] FROM [dbo].[Position]", _connection);
                        cmd.CommandType = CommandType.Text;
                        //                    cmd.Parameters.AddWithValue("@Task", "Select");
                        _connection.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            Position e = new Position();
                            e.PositionID = Convert.ToInt32(sdr["PositionID"].ToString());
                            e.Name = sdr["Name"].ToString();
                            posList.Add(e);
                        }
                        _connection.Close();
                        return posList;
                    }
                    }
                }
            catch (Exception)
            {
                return null;
            }
  
        }
    }
}

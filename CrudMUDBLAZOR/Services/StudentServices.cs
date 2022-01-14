using CrudMUDBLAZOR.Data;
using CrudMUDBLAZOR.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrudMUDBLAZOR.Services
{
    public class StudentServices : IStudentServices
    {
        List<StudentModel> _students = new List<StudentModel>();

        StudentModel _student = new StudentModel();
        public IConfiguration _Configuration { get; }

        public StudentServices(IConfiguration configuration)
        {
            _Configuration = configuration;
            _connectionstring = _Configuration.GetConnectionString("Practical");
            ProviderName = "System.Data.SqlClient";

        }

        public string ProviderName { get; set; }
        public string _connectionstring { get; set; }
        public void Delete(int studentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public StudentModel GetByID(int studentId)
        {
            _student = new StudentModel();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("GetStudentId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _student.Id = Convert.ToInt32(rdr["StudentId"].ToString());
                    _student.Name = rdr["Studentname"].ToString();
                    _student.Emial = rdr["StudentEmail"].ToString();
                }
                rdr.Close();
            }

            return _student;
        }

        public List<StudentModel> GetStudent()
        {
            _students = new List<StudentModel>();
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("GetStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _student = new StudentModel();
                    _student.Id = Convert.ToInt32(rdr["StudentId"].ToString());
                    _student.Name = rdr["StudentName"].ToString();
                    _student.Emial = rdr["StudentEmail"].ToString();
                    _students.Add(_student);
                }
                rdr.Close();
            }

            return _students;
        }

        public StudentModel SaveOrUpdate(StudentModel student)
        {
            if (student.Id == 0)
            {
                using (SqlConnection con = new SqlConnection(_connectionstring))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("AddStudent", con);
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Studentname", student.Name);
                    cmd2.Parameters.AddWithValue("@Email", student.Emial);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }

                return student;
            }
            else 
            {
                using (SqlConnection con = new SqlConnection(_connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("UpdateStudent", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@StudentID", student.Id);
                    cmd.Parameters.AddWithValue("@Studentname", student.Name);
                    cmd.Parameters.AddWithValue("@Email", student.Emial);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                return student;
            }
        }

        public StudentModel Update(StudentModel student)
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                SqlCommand cmd = new SqlCommand("UpdateStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@StudentID", student.Id);
                cmd.Parameters.AddWithValue("@Studentname", student.Name);
                cmd.Parameters.AddWithValue("@Email", student.Emial);
                cmd.ExecuteNonQuery();

                con.Close();
            }

            return student;
        }
    }
}
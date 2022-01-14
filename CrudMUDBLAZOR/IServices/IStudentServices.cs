using CrudMUDBLAZOR.Data;
using System.Collections.Generic;

namespace CrudMUDBLAZOR.IServices
{
    public interface IStudentServices
    {
        List<StudentModel> GetStudent();

        StudentModel GetByID(int studentId);

        StudentModel SaveOrUpdate(StudentModel student);

        StudentModel Update(StudentModel student);

        void Delete(int studentId);
    }
}

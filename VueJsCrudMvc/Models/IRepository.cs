using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueJsCrudMvc.Models
{
    public interface IRepository
    {
        bool SaveEmployee(Employee obj);
        bool DeleteEmployee(int ID);
        Employee GetEmployeesById(int id);
        IEnumerable<Position> GetPositions();
    }
}

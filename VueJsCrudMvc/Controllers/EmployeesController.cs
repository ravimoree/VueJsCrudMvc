using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VueJsCrudMvc.Models;

namespace VueJsCrudMvc.Controllers
{
    public class EmployeesController : Controller
    {
        private Repository _repository;
        public EmployeesController()
        {
            this._repository = new Repository(new EmployeeContext());
        }

        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var emp = _repository.SaveEmployee(employee);
            var result = "";
            if (emp == true)
            {
                result = "Save done";
            }
            else
            {
                result = "Not save";
            }
            return Content(result);
        }
        public JsonResult Edit(int? id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            var employee = _repository.GetEmployeesById(Convert.ToInt32(id));

            string output = jss.Serialize(employee);

            return Json(output, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            var emp = _repository.SaveEmployee(employee);
            var result = "";
            if (emp == true)
            {
                result = "Update done";
            }
            else
            {
                result = "Not update";
            }
            return Content(result);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var emp = _repository.DeleteEmployee(Convert.ToInt32(id));
            var result = "";
            if (emp == true)
            {
                result = "Delete done";
            }
            else
            {
                result = "Not delete";
            }
            return Content(result);
        }
        [HttpGet]
        public JsonResult GetEmployee()
        {

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var empList = _repository.GetAllEmployees().ToList();

            string output = jss.Serialize(empList);

            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPositionList() 
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var plist = _repository.GetPositions().ToList();

            string output = jss.Serialize(plist);

            return Json(output, JsonRequestBehavior.AllowGet);

        }
    }
}

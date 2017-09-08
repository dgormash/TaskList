using System.Web.Mvc;
using TaskList.DataBaseManipulations;
using TaskList.Models;


namespace TaskList.Controllers
{
    public class TaskListsController : Controller
    {
        private readonly DBaseManager _dBaseManager;

        public TaskListsController()
        {
            
            _dBaseManager = new DBaseManager();
        }

        // GET: TaskLists
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckLists()
        {
            return PartialView("_CheckLists", _dBaseManager.GetCheckLists());
        }

        [HttpPost]
        public ActionResult CreateCheckList(string listName)
        {
            CreateNewCheckList(listName);
            return PartialView("_CheckLists", _dBaseManager.GetCheckLists());
        }

        private void CreateNewCheckList(string name)
        {
            _dBaseManager.CreateChekList(name);
        }

        [HttpGet]
        public ActionResult GetTasksFromCheckList(int id)
        {
            ViewBag.CheckListId = id;
            return PartialView("_TaskList", _dBaseManager.GetTaskList(id));
        }

        [HttpPost]
        public ActionResult CreateNewTask(Task task)
        {
            CreateTask(task);
            return RedirectToAction("GetTasksFromCheckList", new { id = task.CheckListId });
        }

        private void CreateTask(Task task)
        {
            _dBaseManager.CreateNewTask(task);
        }
    }
}
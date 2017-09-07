using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return PartialView("_TaskList", _dBaseManager.GetTaskList(id));
        }

        //[HttpPost]
        //public ActionResult CreateNewTask(Task task)
        //{
        //    _dBaseManager.CreateNewTask(task);
        //    return RedirectToAction("GetTasksFromCheckList", new {id = task.ChekListId});
        //}

        [HttpPost]
        public ActionResult CreateNewTask(string taskDescription)
        {
           CreateTask(taskDescription, 0);
            return RedirectToAction("GetTasksFromCheckList", new { id = 0 });
            //return null;
        }

        //public ActionResult NewTask()
        //{
        //    return PartialView("_NewTask",)
        //}

        private void CreateTask(string description, int id)
        {
            var task = new Task
            {
                TaskDescription = description,
                ChekListId = id
            };
            _dBaseManager.CreateNewTask(task);
        }
    }
}
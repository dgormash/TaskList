using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskDescription { get;  set; }
        public bool CompleteStatus { get;  set; }
        public bool IsStoped { get;  set; }
        public DateTime EntryDate { get;  set; }
        public int CheckListId { get;  set; }
    }
}
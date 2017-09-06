using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class CheckList
    {
        public int CheckListId { get;  set; }
        public string ListName { get;  set; }
        public DateTime EntryDate { get;  set; }
    }
}
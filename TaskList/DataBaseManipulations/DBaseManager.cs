using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskList.Models;

namespace TaskList.DataBaseManipulations
{
    public class DBaseManager
    {
        
        private string _commandText;
        private SqlCommand _sqlCommand;
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TaskListDataBase.mdf;Integrated Security = True";
       
        private static SqlConnection GetConnection()
        {
            var connection = new SqlConnection {ConnectionString = ConnectionString};
            connection.Open();
            return connection;
        }

        public List<Task> GetTaskList(int checkListId)
        {
            var result = new List<Task>();
            using (var cn = GetConnection())
            {
                _commandText = @"select * from tasks where CheckListId = @CheckListId";
                _sqlCommand = new SqlCommand(_commandText, cn);
                _sqlCommand.Parameters.Add("@CheckListId", SqlDbType.Int);
                _sqlCommand.Parameters["@CheckListId"].Value = checkListId;
                var sqlReader = _sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    var task = new Task
                    {
                        TaskId = (int)sqlReader["Id"],
                        TaskDescription = sqlReader["Task"].ToString(),
                        CompleteStatus = (bool)sqlReader["CompleteStatus"],
                        EntryDate = (DateTime)sqlReader["EntryDate"],
                        CheckListId = (int)sqlReader["CheckListId"],
                        IsStoped = (bool)sqlReader["IsStoped"]
                    };

                    result.Add(task);
                }
            }
            return result;
        }

        public List<CheckList> GetCheckLists()
        {
            var result = new List<CheckList>();
            using (var cn = GetConnection())
            {
                _commandText = @"select * from CheckLists";
                _sqlCommand = new SqlCommand(_commandText, cn);
                var sqlReader = _sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    var checkList = new CheckList
                    {
                        CheckListId = (int)sqlReader["Id"],
                        ListName = sqlReader["ListName"].ToString(),
                        EntryDate = (DateTime)sqlReader["EntryDate"]
                    };

                    result.Add(checkList);
                }
            }
            return result;
        }

        public void CreateChekList(string name)
        {
            using (var cn = GetConnection())
            {
                _commandText = @"insert into CheckLists (ListName, EntryDate) values (@ListName, @EntryDate)";
                _sqlCommand = new SqlCommand(_commandText, cn);
                _sqlCommand.Parameters.Add("@ListName", SqlDbType.NChar);
                _sqlCommand.Parameters.Add("@EntryDate", SqlDbType.DateTime);
                _sqlCommand.Parameters["@ListName"].Value = name;
                _sqlCommand.Parameters["@EntryDate"].Value = DateTime.Now;
                _sqlCommand.ExecuteNonQuery();
            }
        }

        public void CreateNewTask(Task taskItem)
        {
            using (var cn = GetConnection())
            {
                _commandText = @"insert into Tasks (Task, CompleteStatus, EntryDate, IsStoped, CheckListId)"+ 
                    "values (@Task, @CompleteStatus, @EntryDate, @IsStoped, @CheckListId)";
                _sqlCommand = new SqlCommand(_commandText, cn);
                _sqlCommand.Parameters.Add("@Task", SqlDbType.NChar);
                _sqlCommand.Parameters.Add("@CompleteStatus", SqlDbType.Bit);
                _sqlCommand.Parameters.Add("@EntryDate", SqlDbType.DateTime);
                _sqlCommand.Parameters.Add("@IsStoped", SqlDbType.Bit);
                _sqlCommand.Parameters.Add("@CheckListId", SqlDbType.Int);
                _sqlCommand.Parameters["@Task"].Value = taskItem.TaskDescription;
                _sqlCommand.Parameters["@CompleteStatus"].Value = false;
                _sqlCommand.Parameters["@EntryDate"].Value = DateTime.Now;
                _sqlCommand.Parameters["@IsStoped"].Value = false;
                _sqlCommand.Parameters["@CheckListId"].Value = taskItem.CheckListId;
                _sqlCommand.ExecuteNonQuery();
            }
        }

        public void ChangeCompleteStatus(int id, bool value)
        {
            using (var cn = GetConnection())
            {
                _commandText = @"update tasks set CompleteStatus = @value where id = @id";
                _sqlCommand = new SqlCommand(_commandText, cn);
                _sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                _sqlCommand.Parameters.Add("@value", SqlDbType.Bit);
                _sqlCommand.Parameters["@id"].Value = id;
                _sqlCommand.Parameters["@value"].Value = value;
                _sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
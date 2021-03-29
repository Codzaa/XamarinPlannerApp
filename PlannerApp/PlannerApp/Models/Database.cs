using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Plan>().Wait();
        }

        public Task<List<Plan>> GetPlansAsync()
        {
            return _database.Table<Plan>().ToListAsync();
        }

        public Task<int> SavePlanAsync(Plan plan)
        {
            return _database.InsertAsync(plan);
        }

        public Task<int> DeletePlanAsync(Plan plan)
        {
            return _database.DeleteAsync(plan);
        }

        public Task<Plan> SearchPlanAsync(string planName)
        {
            //return _database.FindWithQueryAsync<Plan>($"SELECT * FROM [Plan] WHERE PlanTitle = {planName}");
            return _database.FindWithQueryAsync<Plan>("SELECT * FROM Plan WHERE PlanTitle = ?",planName);
        }

        public Task<int> EditPlanAsync(Plan plan)
        {

            return _database.UpdateAsync(plan);
        }
    }
}

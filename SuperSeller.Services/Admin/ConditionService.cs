using System.Linq;
using System.Threading.Tasks;
using SuperSeller.Data;
using SuperSeller.Models;
using SuperSeller.Services.Admin.Interfaces;

namespace SuperSeller.Services.Admin
{
    public class ConditionService : IConditionService
    {
        private readonly ApplicationDbContext context;

        public ConditionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool ExistConditions()
        {
            return context.Conditions.Count() != 0;
        }

        public void AddCondition(string name)
        {
            var condition = new Condition()
            {
                Name = name
            };
            context.Conditions.Add(condition);

            context.SaveChanges();
        }
    }
}
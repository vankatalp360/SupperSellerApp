using System.Threading.Tasks;

namespace SuperSeller.Services.Admin.Interfaces
{
    public interface IConditionService
    {
        bool ExistConditions();
        void AddCondition(string name);
    }
}
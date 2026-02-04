using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IRandomStringGenerator
    {
        Task<string> GenerateAsync();
    }
}

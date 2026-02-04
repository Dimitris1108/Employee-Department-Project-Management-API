using System.Threading.Tasks;

namespace EmployeeDepartmentAndProjectManagement.Services.Interfaces
{
    public interface IRandomStringGenerator
    {
        /// <summary>
        /// Generates random string.
        /// </summary>
        /// <returns></returns>
        Task<string> GenerateAsync();
    }
}

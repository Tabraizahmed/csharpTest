using System.Threading.Tasks;

namespace Recruitment.Functions
{
    public interface IHashGenerator
    {
        Task<string> GenerateAsync(string textToHash);
    }
}
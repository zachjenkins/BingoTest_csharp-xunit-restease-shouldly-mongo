using Bingo.RestEase.Models.Request;
using Bingo.RestEase.Models.Response;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.RestEase.Services
{
    [AllowAnyStatusCode]
    public interface IMusclesService
    {
        #region Muscles Controller

        [Get("api/muscles")]
        Task<Response<List<Muscle>>> GetMuscles();

        [Get("api/muscles/{id}")]
        Task<Response<Muscle>> GetMuscleById([Path] string id);

        [Post("api/muscles")]
        Task<Response<Muscle>> PostMuscle([Body] PostMuscleDto postDto);

        [Delete("api/muscles/{id}")]
        Task<Response<string>> DeleteMuscleById([Path] string id);

        #endregion
    }
}

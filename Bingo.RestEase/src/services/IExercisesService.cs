using Bingo.RestEase.Models.Request;
using Bingo.RestEase.Models.Response;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.RestEase.Services
{
    [AllowAnyStatusCode]
    public interface IExercisesService
    {
        #region Exercises

        [Get("api/exercises")]
        Task<Response<List<Exercise>>> GetExercises();

        [Get("api/exercises/{exerciseId}")]
        Task<Response<Exercise>> GetExerciseById([Path] string exerciseId);

        [Get("api/exercises/{exerciseId}/activations")]
        Task<Response<List<Activation>>> GetActivationsForExercise([Path] string exerciseId);

        [Get("api/exercises/{exerciseId}/activations/{activationId}")]
        Task<Response<Activation>> GetActivationForExercise([Path] string exerciseId, [Path] string activationId);

        [Post("api/exercises")]
        Task<Response<Exercise>> PostExercise([Body] PostExerciseDto postDto);

        [Post("api/exercises/{exerciseId}/activations")]
        Task<Response<Activation>> PostActivationToExercise([Path] string exerciseId, [Body] PostActivationDto postDto);

        [Delete("api/exercises/{exerciseId}")]
        Task<Response<string>> DeleteExerciseById([Path] string exerciseId);

        [Delete("api/exercises/{exerciseId}/activations/{activationId}")]
        Task<Response<string>> DeleteActivationFromExercise([Path] string exerciseId, [Path] string activationId);

        #endregion
    }
}

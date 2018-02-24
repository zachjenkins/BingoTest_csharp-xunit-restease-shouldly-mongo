using Bingo.RestEase.Models.Request;
using Bingo.RestEase.Models.Response;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.RestEase.Services
{
    [AllowAnyStatusCode]
    public interface IActivationsService
    {
        #region Activations

        [Get("api/activations")]
        Task<Response<List<Activation>>> GetActivations();

        [Get("api/activations/{id}")]
        Task<Response<Activation>> GetActivationById([Path] string id);

        [Post("api/activations")]
        Task<Response<Activation>> PostActivation([Body] PostActivationDto postDto);

        [Delete("api/activations/{id}")]
        Task<Response<string>> DeleteActivationById([Path] string id);

        #endregion
    }
}

using Quicken.DateFixer.MinApi.EndpointHandlers;

namespace Quicken.DateFixer.MinApi.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterQuickenEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var quickenEndpoints = endpointRouteBuilder.MapGroup("/api/quicken")
                                                        .WithOpenApi()
                                                        .DisableAntiforgery();
            //AddEndpointFilter for validation that produces validation problem

            quickenEndpoints.MapPost(string.Empty, QuickenEndpoints.ProcessQuickenFileAsync);
        }
    }
}

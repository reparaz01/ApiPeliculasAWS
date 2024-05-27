using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiPeliculasAWS.Repositories;
using ApiPeliculasAWS.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiPeliculasAWS;

public class Functions
{

    private RepositoryPeliculas repo;

    public Functions(RepositoryPeliculas repo)
    {
        this.repo = repo;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<IHttpResult> Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
        return HttpResults.Ok(peliculas);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{actor}")]
    public async Task<IHttpResult> Find(string actor, ILambdaContext context)
    {
        context.Logger.LogInformation($"Handling the 'Find' Request for actor: {actor}");
        List<Pelicula> peliculas = await this.repo.FindPeliculasActorAsync(actor);
        return HttpResults.Ok(peliculas);
    }




}
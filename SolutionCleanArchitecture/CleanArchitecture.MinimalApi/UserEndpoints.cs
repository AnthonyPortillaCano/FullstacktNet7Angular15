using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.MinimalApi
{
    public static class UserEndpoints
    {
        public static RouteGroupBuilder MapUsers(this WebApplication app,IConfiguration configuration, LogApi logApi)
        {
            var group = app.MapGroup("Api/Users");
            group.MapPost("/ValidateUser", async (Users user,IUnitOfWork unitOfWork) =>
            {
                try
                {
                    ITokenService tokenService = new TokenService(configuration);
                    if (user == null) Results.BadRequest();
                    var credencial= await unitOfWork.userServices.ValidarUser(user.Email,user.Password);
                    if(credencial !=null)
                    {
                        var token = tokenService.BuildToken(user);
                        user.Token=token;
                        return Results.Ok(user);
                    }
                    else
                    {
                        return Results.NotFound();
                    }
                }
                catch (Exception ex)
                {
                    logApi.GuardarLog(ex.Message);
                    return Results.BadRequest(ex.Message);
                }
            });
            return group;
        }
    }
}

using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArchitecture.MinimalApi
{
    public static class PurchaseEndpoints
    {
         public static  RouteGroupBuilder MapPurchase(this WebApplication app,LogApi logApi)
        {
            var group = app.MapGroup("Api/Purchase");
            group.MapPost("/Save", [Authorize] async (Compra compra, IUnitOfWork unitOfWork) =>
            {
                RespuestaTransaccionDto respuestaTransaccionDto = new ();
                try
                {
                    var id = await unitOfWork.compraServices.Insert(compra);
                    if (id == 0) return Results.BadRequest();
                    respuestaTransaccionDto.Resultado = $"El id o codigo generado es : {id}{" - Codigo Respuesta:"}{Mensajes.CODE_SUCCESS}";
                    respuestaTransaccionDto.Descripcion = Mensajes.TRANSACTION_SUCCESS;
                    return Results.Ok(respuestaTransaccionDto);
                }
                catch (Exception ex)
                {
                    respuestaTransaccionDto.Resultado = Mensajes.CODE_ERROR;
                    respuestaTransaccionDto.Descripcion = Mensajes.ERROR_TRANSACTION + " " + ex.Message;
                    logApi.GuardarLog($"{respuestaTransaccionDto.Resultado}{" "}{ respuestaTransaccionDto.Descripcion }");
                    return Results.BadRequest(respuestaTransaccionDto);
                }
            });
            group.MapGet("/Detail/{id}", [Authorize] async (int id, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    List<DetalleComprasDto> result = await unitOfWork.detalleCompraServices.ObtenerDetalleCompra(id);
                    if (result.Count == 0) return Results.NotFound();
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapGet("/List", [Authorize] async ([FromQuery] string? numeroDocumento, string? razonSocial, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    List<Compra> result = await unitOfWork.compraServices.ListaBusquedaCompra(numeroDocumento, razonSocial);
                    if(result.Count==0) return Results.NotFound();
                    return Results.Ok(result); 
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapDelete("/Delete/{id}", [Authorize] async (int id, IUnitOfWork unitOfWork) =>
            {
                RespuestaTransaccionDto respuestaTransaccionDto = new();
                try
                {
                    await unitOfWork.detalleCompraServices.DeleteList(id);
                    int result = await unitOfWork.CommitAsync();
                    await unitOfWork.compraServices.Delete(id);
                    if (result == 0) return Results.BadRequest();
                    respuestaTransaccionDto.Resultado = Mensajes.CODE_SUCCESS;
                    respuestaTransaccionDto.Descripcion = Mensajes.TRANSACTION_SUCCESS;
                    return Results.Ok(respuestaTransaccionDto);
                }
                catch (Exception ex)
                {
                    respuestaTransaccionDto.Resultado= Mensajes.CODE_ERROR;
                    respuestaTransaccionDto.Descripcion = Mensajes.ERROR_TRANSACTION + ex.Message;
                    logApi.GuardarLog($"{respuestaTransaccionDto.Resultado}{" "}{respuestaTransaccionDto.Descripcion}");
                    return Results.BadRequest(respuestaTransaccionDto);
                }
            });

            group.MapPut("/Update", [Authorize] async (PurchaseDto purchaseDto, IUnitOfWork unitOfWork) =>
            {
                RespuestaTransaccionDto respuestaTransaccionDto = new();
                using var scope = unitOfWork.BeginTransaction();
                try
                {
                    if (purchaseDto == null) return Results.BadRequest();
                    Compra compra = JsonSerializer.Deserialize<Compra>(JsonSerializer.Serialize(purchaseDto));
                    unitOfWork.compraServices.UpdateFieldsSave(compra);
               
                    foreach (var element in compra.DetalleCompras)
                    {
                         if(element.Id==0)
                        {
                            await unitOfWork.detalleCompraServices.Insert(element);
                        }
                    }
                    if(purchaseDto.EliminarDetalleCompra.Count>0)
                    {
                         foreach(var elementDelete in purchaseDto.EliminarDetalleCompra) 
                        {
                             if(elementDelete.Id !=0)
                            {
                                await unitOfWork.detalleCompraServices.Delete(elementDelete.Id);
                            }
                        }
                    }
                    await unitOfWork.CommitAsync();
                    scope.Commit();
                    respuestaTransaccionDto.Resultado = Mensajes.CODE_SUCCESS;
                    respuestaTransaccionDto.Descripcion = Mensajes.TRANSACTION_SUCCESS;
                    return Results.Ok(respuestaTransaccionDto);
                }
                catch (Exception ex)
                {
                    respuestaTransaccionDto.Resultado = Mensajes.CODE_ERROR;
                    respuestaTransaccionDto.Descripcion = Mensajes.ERROR_TRANSACTION + ex.Message;
                    logApi.GuardarLog($"{respuestaTransaccionDto.Resultado}{" "}{respuestaTransaccionDto.Descripcion}");
                    return Results.BadRequest(respuestaTransaccionDto);
                }
            });
            return group;
        }
    }
}

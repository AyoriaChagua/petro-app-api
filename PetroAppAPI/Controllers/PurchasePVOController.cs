using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiTestIIS.Models;
using System.Linq;
using ApiTestIIS.Contexts;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiTestIIS.Controllers
{
    [Route("api/purchase-order-pvo")]
    [ApiController]
    public class PurchasePVOController : ControllerBase
    {
        private readonly ApiDbContext _context;

        private readonly ILogger<PurchasePVOController> _logger;

        public PurchasePVOController(ApiDbContext contexto, ILogger<PurchasePVOController> logger)
        {
            _context = contexto;
            _logger = logger;
        }

        [HttpGet("search/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> SearchPedido(string nroScop)
        {
            try
            {
                var scops = await _context.Set<PurchaseRequest>()
                    .Where(po => po.Nro_scop.Contains(nroScop))
                    .Select(po => po.Nro_scop)
                    .Take(10)
                    .ToListAsync();

                return Ok(scops);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Un error ocurrió mientras se procesaba tu request");
            }
        }




        [HttpGet("pedidos/{idCliente}/{fechaPedido}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseSearchResponse>>> GetPedidos(string idCliente, string fechaPedido)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
                {
                    ParameterName = "@id_cliente",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 12,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = idCliente
                },   
             new SqlParameter
                {
                    ParameterName = "@fecha_pedido",
                    SqlDbType = System.Data.SqlDbType.Date,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = fechaPedido
                }
            };

            string storedProcPurchases = "EXEC sp_m_search_purchases_pvo @id_cliente, @fecha_pedido";
            var purchases = await _context.purchaseSearch
                .FromSqlRaw(storedProcPurchases, param)
                .ToListAsync();

            var purchasesResponses = new List<PurchaseSearchResponse>();

            foreach (var purchase in purchases)
            {
                var paramScop = new SqlParameter("@nro_scop", System.Data.SqlDbType.VarChar, 12) { Value = purchase.Nro_scop };
                string storeProcedureDetails = "EXEC sp_m_search_purchases_detail_pvo @nro_scop";

                var details = await _context.purchaseDetSearch
                    .FromSqlRaw(storeProcedureDetails, paramScop)
                    .ToListAsync();
                purchase.Detalle = details;
                purchasesResponses.Add(new PurchaseSearchResponse
                {
                    Pedido = purchase,
                });
            }

            BreakCycles(purchasesResponses);

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(purchasesResponses, options);

            return Ok(jsonString);
        }

        private void BreakCycles(List<PurchaseSearchResponse> purchasesResponses)
        {
            foreach (var response in purchasesResponses)
            {
                foreach (var detalle in response.Pedido.Detalle)
                {
                    detalle.PurchaseSearch = null;
                }
            }
        }


        [HttpGet("pedido/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseResponse>>> GetInfoPedido(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC sp_m_search_purchase_pvo @nro_scop";

            return await _context.purchaseResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }

        [HttpGet("pedido-detalle/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseDetResponse>>> GetInfoPedidoDetalle(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC sp_m_search_purchase_det_pvo @nro_scop";

            return await _context.purchaseDetResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }


        [HttpGet("pedido-detalle-compartimiento/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseDetailCompartmentResponse>>> GetInfoPedidoDetalleCompartimiento(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC sp_m_search_purchase_det_compartment_pvo @nro_scop";

            return await _context.purchaseDetCompartimentResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }



        [HttpGet("pedido-edit/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseEditResponse>>> GetEditPedido(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC [sp_m_get_purchase_pvo_edit] @nro_scop";

            return await _context.purchaseEditResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }

        [HttpGet("pedido-detalle-edit/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseDetEditResponse>>> GetEditPedidoDetalle(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC [sp_m_get_purchase_det_pvo_edit] @nro_scop";

            return await _context.purchaseDetEditResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }


        [HttpGet("pedido-compartimiento-edit/{nroScop}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseDetCompartmentEditResponse>>> GetEditPedidoDetalleCompartimiento(string nroScop)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@nro_scop",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 12,
                Direction = System.Data.ParameterDirection.Input,
                Value = nroScop
            }
            };

            string storedProc = "EXEC [sp_m_get_purchase_compartment_pvo_edit] @nro_scop";

            return await _context.purchaseDetCompartmentEditResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }




        [HttpGet("pedido-descuento/{cia}/{idCliente}/{idPlanta}/{idArticuloGrupo}/{idAlmacen}/{idCondicionPago}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PurchaseDiscountResponse>>> GetDescuentoPedido(
         string cia,
         string idCliente,
         string idPlanta,
         string idArticuloGrupo,
         string idAlmacen,
         string idCondicionPago)
        {
            var param = new SqlParameter[]
            {
            new SqlParameter
            {
                ParameterName = "@cia",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 4,
                Direction = System.Data.ParameterDirection.Input,
                Value = cia
            },
            new SqlParameter
            {
                ParameterName = "@id_cliente",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 6,
                Direction = System.Data.ParameterDirection.Input,
                Value = idCliente
            },
            new SqlParameter
            {
                ParameterName = "@id_planta",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 4,
                Direction = System.Data.ParameterDirection.Input,
                Value = idPlanta
            },
            new SqlParameter
            {
                ParameterName = "@id_articulo_grupo",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 4,
                Direction = System.Data.ParameterDirection.Input,
                Value = idArticuloGrupo
            },
             new SqlParameter
            {
                ParameterName = "@id_almacen",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 4,
                Direction = System.Data.ParameterDirection.Input,
                Value = idAlmacen
            },
             new SqlParameter
            {
                ParameterName = "@id_condicion_pago",
                SqlDbType = System.Data.SqlDbType.Char,
                Size = 2,
                Direction = System.Data.ParameterDirection.Input,
                Value = idCondicionPago
            }
            };

            string storedProc = "EXEC sp_m_obtener_descuento_pedido2 @cia, @id_cliente, @id_planta, @id_articulo_grupo,  @id_almacen, @id_condicion_pago";

            return await _context.purchaseDiscountResponse
                    .FromSqlRaw(storedProc, param)
                    .ToListAsync();
        }

        [HttpPost("pedido")]
        [Authorize]
        public IActionResult PostPurchase(PurchaseRequest purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchase = _context.purchaseRegister
                    .FirstOrDefault(p => p.Cia == purchase.Cia && p.Nro_scop == purchase.Nro_scop);
                if (existingPurchase != null)
                {
                    return Ok(new { message = $"El pedido con el nro de scop {purchase.Nro_scop} ya existe.", error = "Duplicado" });
                }

                if (purchase.Fecha_pedido.HasValue)
                {
                    purchase.Estado_pvo = "SOLICITADA";
                    purchase.Fecha_pedido = new DateTime(purchase.Fecha_pedido.Value.Year,
                                                         purchase.Fecha_pedido.Value.Month,
                                                         purchase.Fecha_pedido.Value.Day,
                                                         DateTime.Now.Hour,
                                                         DateTime.Now.Minute,
                                                         DateTime.Now.Second);
                }

                _context.purchaseRegister.Add(purchase);
                _context.SaveChanges();

                return Ok(new { message = "Pedido guardado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al guardar el pedido", error = "Error de servidor" });
            }
        }

        [HttpPost("pedido-detalle")]
        [Authorize]
        public IActionResult PostPurchaseDet(PurchaseDetailRequest purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchase = _context.purchaseDetRegister.FirstOrDefault(p => p.Cia == purchase.Cia && p.Nro_scop == purchase.Nro_scop && p.Id_articulo == purchase.Id_articulo);
                if (existingPurchase != null)
                {
                    return Ok(new { message = $"El detalle con SCOP {existingPurchase.Nro_scop} y producto {existingPurchase.Id_articulo} ya está registrado.", error = "Duplicado" });
                }

                _context.purchaseDetRegister.Add(purchase);
                _context.SaveChanges();

                return Ok(new { message = "Detalle guardado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al guardar el producto", error = "Error de servidor" });
            }
        }

        [HttpPost("pedido-detalle-compartimiento")]
        [Authorize]
        public IActionResult PostPurchaseDetCompartment(PurchaseDetailCompartmentRequest purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchase = _context.purchaseDetCompartmentRegister.FirstOrDefault(p =>
                p.Cia == purchase.Cia &&
                p.Nro_scop == purchase.Nro_scop &&
                p.Id_articulo == purchase.Id_articulo &&
                p.Placa_tractor == purchase.Placa_tractor &&
                p.Placa_cisterna == purchase.Placa_cisterna &&
                p.Nro_compartimiento == purchase.Nro_compartimiento);

                if (existingPurchase != null)
                {
                    return Ok(new { message = $"Ya existe el mismo producto en el compartimento ({purchase.Nro_compartimiento})", error = "Duplicado" });
                }

                _context.purchaseDetCompartmentRegister.Add(purchase);
                _context.SaveChanges();

                return Ok(new { message = "Guardado en el compartimento correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al guardar el producto en el compartimiento", error = "Error de servidor" });
            }
        }

        [HttpPut("pedido/validar")]
        [Authorize]
        public IActionResult ValidatePurchase([FromBody] PurchaseValidationRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede ser nulo" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (string.IsNullOrEmpty(request.Cia) || string.IsNullOrEmpty(request.Nro_scop))
                {
                    return BadRequest(new { message = "Datos incompletos" });
                }

                var purchaseToUpdate = _context.purchaseRegister
                    .FirstOrDefault(p => p.Cia == request.Cia && p.Nro_scop == request.Nro_scop);

                if (purchaseToUpdate == null)
                {
                    return NotFound(new { message = "No se encontró el registro para actualizar" });
                }

                if (request.Fecha_despacho != default(DateTime))
                {
                    purchaseToUpdate.Fecha_despacho = request.Fecha_despacho.Date.Add(DateTime.Now.TimeOfDay);
                }

                purchaseToUpdate.Asesor_valida = request.Asesor_valida ?? purchaseToUpdate.Asesor_valida;
                purchaseToUpdate.Flag_validado = "1";

                _context.SaveChanges();

                return Ok(new { message = "El pedido se encuentra validado. En caso sea un error por favor, comuníquese con el área de sistemas" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el registro: {ex}");

                return StatusCode(500, new { message = "Ocurrió un error al actualizar el registro", error = ex.Message });
            }
        }



        [HttpPut("pedido/anular")]
        [Authorize]
        public IActionResult CancelPurchase([FromBody] PurchaseCancelRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede ser nulo" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (string.IsNullOrEmpty(request.Cia) || string.IsNullOrEmpty(request.Nro_scop))
                {
                    return BadRequest(new { message = "Datos incompletos" });
                }

                var purchaseToUpdate = _context.purchaseRegister
                    .FirstOrDefault(p => p.Cia == request.Cia && p.Nro_scop == request.Nro_scop);

                if (purchaseToUpdate == null)
                {
                    return NotFound(new { message = "No se encontró el registro para anular" });
                }

                purchaseToUpdate.Estado_pvo = "ANULADA";
                purchaseToUpdate.Usuario_anula = request.Usuario_anula;

                _context.SaveChanges();

                return Ok(new { message = "El pedido se anuló" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el registro: {ex}");

                return StatusCode(500, new { message = "Ocurrió un error al actualizar el registro", error = ex.Message });
            }
        }


        [HttpPut("pedido/reactivar")]
        [Authorize]
        public IActionResult ReactivatePurchase([FromBody] PurchaseReactivateRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "El cuerpo de la solicitud no puede ser nulo" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (string.IsNullOrEmpty(request.Cia) || string.IsNullOrEmpty(request.Nro_scop))
                {
                    return BadRequest(new { message = "Datos incompletos" });
                }

                var purchaseToUpdate = _context.purchaseRegister
                    .FirstOrDefault(p => p.Cia == request.Cia && p.Nro_scop == request.Nro_scop);

                if (purchaseToUpdate == null)
                {
                    return NotFound(new { message = "No se encontró el registro para anular" });
                }

                purchaseToUpdate.Estado_pvo = "SOLICITADA";
                purchaseToUpdate.Usuario_reactiva = request.Usuario_reactiva;

                _context.SaveChanges();

                return Ok(new { message = "El pedido se reactivó" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al reactivar el pedido", error = ex.Message });
            }
        }


        [HttpPut("pedido/{nroScop}")]
        [Authorize]
        public IActionResult UpdatePurchase(string nroScop, [FromBody] PurchaseEditRequest updatedPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchase = _context.purchaseRegister.FirstOrDefault(p => p.Nro_scop == nroScop);
                if (existingPurchase == null)
                {
                    return NotFound(new { message = $"El pedido con el n° de scop {nroScop} no existe.", error = "No encontrado" });
                }

                existingPurchase.Id_planta = updatedPurchase.Id_planta;
                existingPurchase.Id_cliente = updatedPurchase.Id_cliente;
                existingPurchase.Placa_cisterna = updatedPurchase.Placa_cisterna;
                existingPurchase.Placa_tractor = updatedPurchase.Placa_tractor;
                existingPurchase.Id_chofer = updatedPurchase.Id_chofer;
                existingPurchase.Id_condicion_pago = updatedPurchase.Id_condicion_pago;
                existingPurchase.Usuario_mod = updatedPurchase.Usuario_mod;

                _context.purchaseRegister.Update(existingPurchase);
                _context.SaveChanges();

                return Ok(new { message = "Pedido actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el pedido", error = "Error de servidor" });
            }
        }




        [HttpDelete("pedido/{sCia}/{sNroScop}")]
        [Authorize]
        public IActionResult DeletePurchase(string sCia, string sNroScop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchases = _context.purchaseRegister
                    .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                    .ToList();

                if (!existingPurchases.Any())
                {
                    return NotFound(new { message = "No se encontraron registros.", error = "No encontrado" });
                }

                _context.purchaseRegister.RemoveRange(existingPurchases);
                _context.SaveChanges();

                return Ok(new { message = "Registros eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar los registros", error = "Error de servidor" });
            }
        }


        [HttpDelete("pedido-detalle/{sCia}/{sNroScop}")]
        [Authorize]
        public IActionResult DeletePurchaseDet(string sCia, string sNroScop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchases = _context.purchaseDetRegister
                    .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                    .ToList();

                if (!existingPurchases.Any())
                {
                    return NotFound(new { message = "No se encontraron registros", error = "No encontrado" });
                }

                _context.purchaseDetRegister.RemoveRange(existingPurchases);
                _context.SaveChanges();

                return Ok(new { message = "Registros eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar los registros", error = "Error de servidor" });
            }
        }

        [HttpDelete("pedido-detalle-compartimiento/{sCia}/{sNroScop}")]
        [Authorize]
        public IActionResult DeletePurchaseDetCompartment(string sCia, string sNroScop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchases = _context.purchaseDetCompartmentRegister
                    .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                    .ToList();

                if (!existingPurchases.Any())
                {
                    return NotFound(new { message = "No se encontraron registros con ese Cia y Nro_Scop.", error = "No encontrado" });
                }

                _context.purchaseDetCompartmentRegister.RemoveRange(existingPurchases);
                _context.SaveChanges();

                return Ok(new { message = "Registros eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar los registros", error = "Error de servidor" });
            }
        }

        [HttpDelete("pedido-all/{sCia}/{sNroScop}")]
        [Authorize]
        public IActionResult DeletePurchaseAndRelatedData(string sCia, string sNroScop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var existingPurchases = _context.purchaseRegister
                            .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                            .ToList();
                        _context.purchaseRegister.RemoveRange(existingPurchases);

                        var existingPurchaseDetails = _context.purchaseDetRegister
                            .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                            .ToList();
                        _context.purchaseDetRegister.RemoveRange(existingPurchaseDetails);

                        var existingPurchaseDetCompartments = _context.purchaseDetCompartmentRegister
                            .Where(p => p.Cia == sCia && p.Nro_scop == sNroScop)
                            .ToList();
                        _context.purchaseDetCompartmentRegister.RemoveRange(existingPurchaseDetCompartments);

                        _context.SaveChanges();
                        transaction.Commit();

                        if (!existingPurchases.Any() && !existingPurchaseDetails.Any() && !existingPurchaseDetCompartments.Any())
                        {
                            return NotFound(new { message = "No se encontraron registros para eliminar.", error = "No encontrado" });
                        }

                        return Ok(new { message = "Registros eliminados correctamente" });
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar los registros", error = "Error de servidor" });
            }
        }
    }
}

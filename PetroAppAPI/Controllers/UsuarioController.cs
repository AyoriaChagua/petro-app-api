using ApiTestIIS.Contexts;
using ApiTestIIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public UsuarioController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS CELULARES DE LOS APROBADORES
        [HttpGet("Discount_Approver")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Discount_Approver>>> Get_Approver()
        {
            //           where....    && new[] { "1","2" }.Contains(c.Flag_m_aprueba_dscto)
            return await (from c in _context.Usuario
                          where c.Id_estado == "01" && c.Flag_m_aprueba_dscto == "1"
                          select new Discount_Approver { Id_celular = c.Id_celular })
                         .ToListAsync();
        }


        // RECUPERA LOS CELULARES DE LOS SOLICITANTES
        [HttpGet("Discount_Requester")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Discount_Requester>>> Get_Requester()
        {
            return await (from c in _context.Usuario
                          where c.Id_estado == "01" && c.Flag_m_request_discount == "1"
                          select new Discount_Requester { Id_Requester = c.Id_usuario, Requester = c.Descripcion, Id_celular = c.Id_celular })
                         .ToListAsync();
        }
    }
}

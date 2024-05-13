using ApiTestIIS.Contexts;
using ApiTestIIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApruebaController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public UsuarioApruebaController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // SP: RECUPERA LISTA DE USUARIO QUE APRUEBAN
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Usuario_aprueba_dscto>>> GetListaUsuarios()
        {
            string StoredProc = "exec sp_m_usuario_aprueba";
            return await _context.usuario_aprueba_dscto.FromSqlRaw(StoredProc).ToListAsync();
        }
    }
}

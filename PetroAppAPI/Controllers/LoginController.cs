using ApiTestIIS.Models;
using ApiTestIIS.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // MEDIANTE INYECCIÓN DE DEPENDENCIAS.
        // 1) TRAEMOS EL Contexto
        // 2) TRAEMOS EL OBJETO DE CONFIGURACIÓN (appsettings.json)
        private readonly ApiDbContext _context;
        private readonly IConfiguration configuration;
        public LoginController(ApiDbContext contexto, IConfiguration configuration)
        {
            _context = contexto;
            this.configuration = configuration;
        }

        /*
        [HttpGet]
                    public async Task<ActionResult<IEnumerable<UsuarioInfo>>> Get()
                    {
                        string idLike = "admin";
                        string StoredProc = "exec sp_m_Login3 " +
                                "@user= '" + idLike + "', @pass= 'bi1907'";
                        return await _context.UsuarioInfo.FromSqlRaw(StoredProc).ToListAsync();
                    }
        */

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UsuarioInfo>>> LoginPost(UsuarioLogin usuarioLogin)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@user",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = usuarioLogin.Id_usuario
                        },
                        new SqlParameter() {
                            ParameterName = "@pass",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = usuarioLogin.Pass_word
                        },
                        new SqlParameter() {
                            ParameterName = "@celular",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = usuarioLogin.Id_celular
                        },
                        new SqlParameter() {
                            ParameterName = "@userOut",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Output,
                        },
                        new SqlParameter() {
                            ParameterName = "@descOut",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output,
                        },
                        new SqlParameter() {
                            ParameterName = "@apruebaDscto",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Output,
                        }};
            int affectedRows = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_Login] @user, @pass, @celular, @userOut out, @descOut out, @apruebaDscto out", param);
            string _userInfo1 = Convert.ToString(param[3].Value);
            string _userInfo2 = Convert.ToString(param[4].Value);
            var _userInfo3 = Convert.ToString(param[5].Value);

            if (_userInfo1 != "")
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo1, _userInfo2), aprueba_dscto = _userInfo3 });
            }
            else
            {
                return Unauthorized();
            }
        }
        //var lst = _context.UsuarioInfo.Where(d => d.Id_usuario == usuario && d.Pass_word == password && d.Id_estado == "01").FirstOrDefault();


        // LOGIN FOR TRANSACTION APP - DISCOUNT VERSION
        [HttpPost("Discount")]
        [AllowAnonymous]
        public async Task<ActionResult> Post_Login_Disc(UsuarioLogin usuarioLogin)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@user",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = usuarioLogin.Id_usuario
                },
                new SqlParameter() {
                    ParameterName = "@pass",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = usuarioLogin.Pass_word
                },
                new SqlParameter() {
                    ParameterName = "@celular",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = usuarioLogin.Id_celular
                },
                new SqlParameter() {
                    ParameterName = "@userOut",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Output,
                },
                new SqlParameter() {
                    ParameterName = "@descOut",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output,
                },
                new SqlParameter() {
                    ParameterName = "@DiscApprover",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 1,
                    Direction = System.Data.ParameterDirection.Output,
                },
                new SqlParameter() {
                    ParameterName = "@DiscRequester",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 1,
                    Direction = System.Data.ParameterDirection.Output,
                }
            };
            int affectedRows = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_Login_Disc] @user, @pass, @celular, @userOut out, @descOut out, @DiscApprover out, @DiscRequester out", param);
            string _userInfo1 = Convert.ToString(param[3].Value);
            string _userInfo2 = Convert.ToString(param[4].Value);
            var _DiscApprover = Convert.ToString(param[5].Value);
            var _DiscRequester = Convert.ToString(param[6].Value);

            if (_userInfo1 != "")
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo1, _userInfo2), Discount_Approver = _DiscApprover, Discount_Requester = _DiscRequester });
            }
            else
            {
                return Unauthorized();
            }
        }


        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        //private string GenerarTokenJWT(UsuarioInfo usuarioInfo)
        private string GenerarTokenJWT(string _userInfo1, string _userInfo2)
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, _userInfo1),       //usuarioInfo.Id_usuario.ToString()),
                new Claim("Descripcion", _userInfo2)    //usuarioInfo.Descripcion)
                //new Claim(ClaimTypes.Role, usuarioInfo.Rol)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 12 horas.
                    expires: DateTime.UtcNow.AddHours(12)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

    }
}
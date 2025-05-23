using ApiTestIIS.Models;
using ApiTestIIS.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
      
        private readonly ApiDbContext _context;
        private readonly IConfiguration configuration;
        public LoginController(ApiDbContext contexto, IConfiguration configuration)
        {
            _context = contexto;
            this.configuration = configuration;
        }

        [HttpGet("saludo")]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetSay()
        {
            return Ok(new { message = "Hola, un gusto tenerte por aquí"});
        }

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
                        },
                        new SqlParameter() {
                            ParameterName = "@usuarioExterno",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Output,
                        },
                        new SqlParameter() {
                            ParameterName = "@descripcion",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output,
                        }
        };
                        
            await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_login_pedidos] @user, @pass, @celular, @userOut out, @descOut out, @apruebaDscto out, @usuarioExterno out, @descripcion out", param);
            string _userInfo1 = Convert.ToString(param[3].Value);
            string _userInfo2 = Convert.ToString(param[4].Value);
            var _userInfo3 = Convert.ToString(param[5].Value);
            var _userInfo4 = Convert.ToString(param[6].Value);
            var _userInfo5 = Convert.ToString(param[7].Value);
            if (_userInfo1 != "")
            {
                return Ok(new { 
                    token = GenerarTokenJWT(_userInfo1, _userInfo2), 
                    aprueba_dscto = _userInfo3, 
                    usuario_externo = _userInfo4, 
                    descripcion = _userInfo5 
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerarTokenJWT(string _userInfo1, string _userInfo2)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, _userInfo1),      
                new Claim("Descripcion", _userInfo2)  
            };

            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(12)
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

    }
}
using Api_Fatoracao.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Api_Fatoracao.Controllers
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class FaturacaoController : ControllerBase
    {

        #region Decompor Números
        /// <summary>
        /// Decompõe um número em todos os seus divisores
        /// </summary>
        /// <param name="numero">Número a ser fatorado</param>
        /// <returns>
        /// 200 - Operação realizada!
        /// </returns>
        [HttpGet("{numero:int}")]
        public ActionResult DecomporNumeros(int numero)
        {
            try
            {
                var resultado = Operacoes.MontarTexto(numero);

                return Ok(resultado.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}

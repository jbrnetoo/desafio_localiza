using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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
                var resultado = MontarTexto(numero);

                return Ok(resultado.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Métodos Privados
        private StringBuilder MontarTexto(int numero)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Número de entrada: {numero}");

            sb.Append("\nNúmeros divisores: ");

            foreach (var divisor in ObterDivisores(numero))
            {
                sb.Append($"{divisor} ");
            }

            sb.Append("\nDivisores primos: ");

            foreach (var numeroPrimo in ObterPrimosDeEspecificoNumero(numero))
            {
                sb.Append($"{numeroPrimo} ");
            }

            return sb;
        }

        private List<int> ObterDivisores(int numero)
        {
            List<int> listaDivisores = new List<int>();

            for (int i = 1; i <= numero; i++)
                if (numero % i == 0)
                    listaDivisores.Add(i);
            return listaDivisores;
        }

        private List<int> ObterPrimosDeEspecificoNumero(int numero)
        {
            var listaNumerosPrimos = new List<int>();
            for (var i = 2; i <= numero + 1; i++)
            {
                var flAdicionar = true;
                foreach (var primo in listaNumerosPrimos)
                {
                    if (primo * primo > i)
                        break;
                    if (i % primo == 0)
                    {
                        flAdicionar = false;
                        break;
                    }
                }
                if (flAdicionar)
                    listaNumerosPrimos.Add(i);
            }
            listaNumerosPrimos.Insert(0, 1);

            return listaNumerosPrimos.Where(x => numero % x == 0).ToList();
        }
        #endregion
    }
}

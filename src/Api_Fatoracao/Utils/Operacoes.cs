using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api_Fatoracao.Utils
{
    public static class Operacoes
    {
        public static StringBuilder MontarTexto(int numero)
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

        public static List<int> ObterDivisores(int numero)
        {
            List<int> listaDivisores = new List<int>();

            for (int i = 1; i <= numero; i++)
                if (numero % i == 0)
                    listaDivisores.Add(i);
            return listaDivisores;
        }

        public static List<int> ObterPrimosDeEspecificoNumero(int numero)
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
    }
}

using Api_Fatoracao.Utils;
using System.Collections.Generic;
using Xunit;

namespace Api_Fatoracao_UnitTests.Operacao
{
    public class OperacaoTest
    {
        [Fact]
        public void MontarTexto_DeveRetornarSolucaoDoDesafio()
        {
            //Arrange
            var numero = 68;

            //Act
            var texto = Operacoes.MontarTexto(numero);

            //Assert
            Assert.NotNull(texto);
            Assert.Contains("Número de entrada: 68", texto.ToString());
            Assert.Contains("Números divisores: 1 2 4 17 34 68", texto.ToString());
            Assert.Contains("Divisores primos: 1 2 17", texto.ToString());
        }

        [Fact]
        public void ObterDivisores_DeveRetornarListaDeDivisores()
        {
            //Arrange
            var numero = 68;

            //Act
            var divisores = Operacoes.ObterDivisores(numero);

            //Assert
            Assert.NotEmpty(divisores);
            Assert.Equal(divisores, new List<int>() { 1, 2, 4, 17, 34, 68 });
        }

        [Fact]
        public void ObterPrimosDeEspecificoNumero_DeveRetornarListaNumerosPrimos()
        {
            //Arrange
            var numero = 68;

            //Act
            var primos = Operacoes.ObterPrimosDeEspecificoNumero(numero);

            //Assert
            Assert.NotEmpty(primos);
            Assert.Equal(primos, new List<int>() { 1, 2, 17 });
        }
    }
}

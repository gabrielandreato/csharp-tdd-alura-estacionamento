using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AluraEstacionamentos.Testes
{
    public class VeiculoTeste : IEnumerable<Object[]>, IDisposable
    {

        private Veiculo veiculo = new Veiculo();
        public VeiculoTeste()
        {
            Veiculo veiculo = new Veiculo();
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Veiculo
                {
                    Proprietario = "André Silva",
                    Placa = "ASD-9999",
                    Cor="Verde",
                    Modelo="Fusca"
                }
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
        }
        [Fact]
        public void TestaVeiculoAcelerarComParametro10()
        {
            // Arrange
            //var veiculo = new Veiculo();
            // Act
            veiculo.Acelerar(10);
            // Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste para validar metodo frear")]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrear()
        {
            // Arrange
            // Act
            veiculo.Frear(10);
            // Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }
        

        [Theory]
        [ClassData(typeof(VeiculoTeste))]
        public void TestaVeiculoClass(Veiculo modelo)
        {
            // Arrange
            //Act
            veiculo.Acelerar(10);
            modelo.Acelerar(10);

            //Assert
            Assert.Equal(modelo.VelocidadeAtual, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void FichaDeInformacaoDoVeiculo()
        {
            // Arrange

            veiculo.Proprietario = "Gabriel";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ABC-1234";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";

            // Act 
            string dados = veiculo.ToString();

            // Assert 
            Assert.Contains("Tipo do veiculo: Automovel", dados);
        }

        [Fact]
        public void TestaNomeProprietarioComMenosDeTresCaracteres()
        {
            // Arrange
            string nomeProprietario = "ab";
            // Assert
            Assert.Throws<System.FormatException>(
                // Act
                () => new Veiculo(nomeProprietario)
            );
        }


        [Fact]
        public void TestaMensagemDeExcessaoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placa = "ASDF1234";

            var mensagem = Assert.Throws<System.FormatException>(
                    () => new Veiculo().Placa = placa
                );

            Assert.Equal("O 4 caractere deve ser um hifen", mensagem.Message);
        }
    }
}

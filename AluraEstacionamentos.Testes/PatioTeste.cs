using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AluraEstacionamentos.Testes
{

    public class PatioTeste: IDisposable
    {
        private Veiculo veiculo = new Veiculo();
        public ITestOutputHelper SaidaConsoloTeste;
        public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoloTeste = _saidaConsoleTeste;

            veiculo = new Veiculo();
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            // Arrange
            var estacionamento = new Patio();
            veiculo.Proprietario = "Gabriel";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ABC-1234";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act
            double faturamento = estacionamento.TotalFaturado();

            // Assert 
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André", "ABC-1234", "preto", "Gol")]
        [InlineData("João", "CVH-1224", "branco", "Uno")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario,
                                                        string placa,
                                                        string cor,
                                                        string modelo)
        {
            // Arrange
            var estacionamento = new Patio();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            // Act
            double faturamento = estacionamento.TotalFaturado();

            // Assert
            Assert.Equal(2, faturamento);

        }

        [Theory]
        [InlineData("André", "ABC-1234", "preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string proprietario,
                                                        string placa,
                                                        string cor,
                                                        string modelo)
        {
            // Arrange
            var estacionamento = new Patio();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            Assert.Contains("### Ticket EstacionamentoAlura ###", consultado.Ticket);
        }

        [Fact]
        public void AlteraDadosDoVeiculoDoProprioVeiculo()
        {
            // Arrange
            var estacionamento = new Patio();

            veiculo.Proprietario = "Gabriel";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ABC-1234";

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "Gabriel";
            veiculoAlterado.Tipo = TipoVeiculo.Automovel;
            veiculoAlterado.Cor = "Preto"; //Dado alterado
            veiculoAlterado.Modelo = "Fusca";
            veiculoAlterado.Placa = "ABC-1234";

            // Act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            // Assert
            Assert.Equal(veiculoAlterado.Cor, alterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoloTeste.WriteLine("Construtor invocado!");
        }
    }
}

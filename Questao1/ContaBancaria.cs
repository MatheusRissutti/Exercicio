using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public ContaBancaria(int numero, string nomeTitular)
        {
            Numero = numero;
            NomeTitular = nomeTitular;
        }
        public ContaBancaria(int numero, string nomeTitular, double depositoInicial)
        {
            Numero = numero;
            NomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        private int Numero { get; }
        public string NomeTitular { get; }
        private double Saldo { get; set; }


        public void Deposito(double valor)
        {
            if(valor > 0)
                Saldo += valor;
        }

        public void Saque(double valor)
        {

        }
    }
}

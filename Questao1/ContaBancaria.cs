using System;

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

        protected int Numero { get; }
        protected string NomeTitular { get; set; }
        protected double Saldo { get; set; }


        public void AlteraNomeTitular(string nomeTitular)
        {
            NomeTitular = nomeTitular;
        }

        public void Deposito(double valor)
        {
            if (valor > 0)
                Saldo += valor;
            else
                Console.WriteLine("O valor de depósito deve ser positivo.");
        }

        public void Saque(double valor)
        {
            if (valor > 0)
                Saldo -= (valor + RegraNegocio.TaxaSaque);
            else
                Console.WriteLine("O valor de saque deve ser positivo.");
        }

        public string Detalhes()
        {
            return $"Conta {Numero}, Titular: {NomeTitular}, Saldo: $ {Saldo.ToString("0.00")}";
        }
    }
}

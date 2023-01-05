using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Entities
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public double Saldo { get; set; }

        public Usuario(string nome, string cpf, string password, double saldo)
        {
            Nome = nome;
            Cpf = cpf;
            Password = password;
            Saldo = saldo;
        }
    }
}
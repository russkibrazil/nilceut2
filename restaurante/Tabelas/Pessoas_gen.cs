using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Pessoas_gen
    {
        public string cpf  { get; private set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string telefone { get; set; }

        public void Definir_Cpf(string valor)
        {
            cpf = valor;
        }
    }
}

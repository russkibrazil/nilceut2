using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Cliente : Pessoas_gen
    {
        public List<string> ListarValores()
        {
            return new List<string>
            {
                cpf,
                nome,
                endereco,
                telefone
            };
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "Cpf", "Nome", "Endereco", "Telefone"
            };
        }
        public static List<Cliente> ConverteObject(List<object[]> listaObjs)
        {
            List<Cliente> s = new List<Cliente>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Cliente)data);
            }
            return s;
        }

        public static explicit operator Cliente(object[] entrada)
        {
            Cliente ev = new Cliente();
            int i = 0;
            ev.Definir_Cpf(entrada[i++].ToString());
            ev.nome = entrada[i++].ToString();
            ev.endereco = entrada[i++].ToString();
            ev.telefone = entrada[i++].ToString();
            return ev;
        }
    }
}

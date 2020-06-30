using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Servidor
    {
        public Pessoas_gen p { get; set; }
        public string Departamento { get; set; }

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
                "Cpf", "Nome",
                // "Setor", 
                "Endereco", "CTPS", "DataAdmissao", "Salario", "Telefone"
            };
        }
        public static List<Servidor> ConverteObject(List<object[]> listaObjs)
        {
            List<Servidor> s = new List<Servidor>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Servidor)data);
            }
            return s;
        }

        public static explicit operator Servidor(object[] entrada)
        {
            Servidor ev = new Servidor();
            int i = 0;
            ev.Definir_Cpf(entrada[i++].ToString());
            ev.nome = entrada[i++].ToString();
            ev.setor = entrada[i++].ToString();
            ev.endereco = entrada[i++].ToString();
            ev.ctps = entrada[i++].ToString();
            i++;
            ev.salario = double.Parse(entrada[i++].ToString());
            ev.telefone = entrada[i++].ToString();
            return ev;
        }
    }
}

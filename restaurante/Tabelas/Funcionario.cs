using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Funcionario : Pessoas_gen
    {
        public string ctps { get; set; }
        public string setor { get; set; }
        public DateTime admissao { get; set; }
        public double salario { get; set; }

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
        public static List<Funcionario> ConverteObject(List<object[]> listaObjs)
        {
            List<Funcionario> s = new List<Funcionario>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Funcionario)data);
            }
            return s;
        }

        public static explicit operator Funcionario(object[] entrada)
        {
            Funcionario ev = new Funcionario();
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

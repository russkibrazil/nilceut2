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
        public DateTime dnasc { get; set; }
        public string tipoUsuario { get; set; }

        public void Definir_Cpf(string valor)
        {
            cpf = valor;
        }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                cpf,
                nome,
                dnasc.ToString(),
                tipoUsuario
            };
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "CPF", "Nome", "Dnasc", "TipoUsuario"
            };
        }

        public static List<Pessoas_gen> ConverteObject(List<object[]> listaObjs)
        {
            List<Pessoas_gen> s = new List<Pessoas_gen>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Pessoas_gen)data);
            }
            return s;
        }

        public static explicit operator Pessoas_gen(object[] entrada)
        {
            Pessoas_gen ev = new Pessoas_gen();
            int i = 0;
            ev.Definir_Cpf(entrada[i++].ToString());
            ev.nome = entrada[i++].ToString();
            ev.dnasc = DateTime.Parse(entrada[i++].ToString());
            ev.tipoUsuario = (entrada[i++].ToString());
            return ev;
        }
    }
}

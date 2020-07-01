using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Telefone
    {
        public Pessoas_gen p { get; set; }
        public string telefone { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                p.cpf,
                telefone
            };
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "CPF", "Telefone"
            };
        }
        public static List<Telefone> ConverteObject(List<object[]> listaObjs)
        {
            List<Telefone> s = new List<Telefone>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Telefone)data);
            }
            return s;
        }

        public static explicit operator Telefone(object[] entrada)
        {
            Telefone ev = new Telefone();
            int i = 0;
            ev.p.Definir_Cpf(entrada[i++].ToString());
            ev.telefone = entrada[i++].ToString();
            return ev;
        }
    }
}

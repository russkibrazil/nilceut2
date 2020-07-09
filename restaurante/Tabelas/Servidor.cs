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

        public Servidor()
        {
            p = new Pessoas_gen();
        }
        public List<string> ListarValores()
        {
            return new List<string>
            {
                p.cpf,
                Departamento
            };
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "CPF", "Departamento"
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
            ev.p.Definir_Cpf(entrada[i++].ToString());
            ev.Departamento = entrada[i++].ToString();
            return ev;
        }
    }
}

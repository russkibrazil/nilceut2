using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Compra
    {
        public DateTime dt { get; private set; }
        public Pessoas_gen p { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                dt.ToString(),
                p.cpf
            };
        }

        public void DefinirData(DateTime t)
        {
            dt = t;
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "Dt", "CPF"
            };
        }

        public static List<Compra> ConverteObject(List<object[]> listaObjs)
        {
            List<Compra> s = new List<Compra>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Compra)data);
            }
            return s;
        }

        public static explicit operator Compra(object[] entrada)
        {
            Compra ev = new Compra();
            int i = 0;
            ev.dt = DateTime.Parse(entrada[i++].ToString());
            ev.p.Definir_Cpf(entrada[i++].ToString());
            return ev;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Vendas
    {
        public int nf { get; private set; }
        public string cliente { get; set; }
        public DateTime dtCompra { get; set; }
        public string vendedor { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                nf.ToString(),
                cliente,
                dtCompra.ToShortDateString(),
                vendedor
            };
        }
        public void Definir_nf(string nota)
        {
            nf = int.Parse(nota);
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "NotaFiscal", "ClienteCpf", "DataCompra", "VendedorCpf"
            };
        }

        public static List<Vendas> ConverteObject(List<object[]> listaObjs)
        {
            List<Vendas> s = new List<Vendas>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Vendas)data);
            }
            return s;
        }

        public static explicit operator Vendas(object[] entrada)
        {
            Vendas ev = new Vendas();
            int i = 0;
            ev.Definir_nf(entrada[i++].ToString());
            ev.cliente = entrada[i++].ToString();
            ev.dtCompra = DateTime.Parse(entrada[i++].ToString());
            ev.vendedor = (entrada[i++].ToString());
            return ev;
        }
    }
}

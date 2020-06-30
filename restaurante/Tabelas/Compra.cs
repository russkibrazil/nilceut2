using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Compra
    {
        public int dt { get; private set; }
        public string cpf { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                dt.ToString(),
                cpf
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
            ev.Definir_nf(entrada[i++].ToString());
            ev.cliente = entrada[i++].ToString();
            ev.dtCompra = DateTime.Parse(entrada[i++].ToString());
            ev.vendedor = (entrada[i++].ToString());
            return ev;
        }
    }
}

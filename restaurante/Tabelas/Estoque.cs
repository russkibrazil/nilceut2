using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Estoque
    {
        public string item { get; private set; }
        public string unidade { get; set; }
        public double quantidade { get; set; }
        public DateTime vencimento { get; set; }
        public string estoquista { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                item,
                unidade,
                quantidade.ToString(),
                vencimento.ToString(),
                estoquista

            };
        }
        public void Definir_item(string item)
        {
            this.item = item;
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "Item", "Unidade", "Quantidade", "Data_vencto", "Estoqusita_responsavel"
            };
        }

        public static List<Estoque> ConverteObject(List<object[]> listaObjs)
        {
            List<Estoque> s = new List<Estoque>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Estoque)data);
            }
            return s;
        }

        public static explicit operator Estoque(object[] entrada)
        {
            Estoque ev = new Estoque();
            int i = 0;
            ev.Definir_item(entrada[i++].ToString());
            ev.unidade = entrada[i++].ToString();
            ev.quantidade = double.Parse(entrada[i++].ToString());
            ev.vencimento = DateTime.Parse(entrada[i++].ToString());
            ev.estoquista = entrada[i++].ToString();
            return ev;
        }
    }
}

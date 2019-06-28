using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Cardapio
    {
        public DateTime dtCompra { get; private set; }
        public int refeicao { get; set; }
        public int quantidade { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                dtCompra.ToShortDateString(),
                refeicao.ToString(),
                quantidade.ToString()
            };
        }
        public void Definir_data(string data)
        {
            this.dtCompra = DateTime.Parse(data);
        }
        public void Definir_data(DateTime data)
        {
            this.dtCompra = data;
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "Data", "Refeicao", "Quantidade"
            };
        }

        public static List<Cardapio> ConverteObject(List<object[]> listaObjs)
        {
            List<Cardapio> s = new List<Cardapio>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Cardapio)data);
            }
            return s;
        }

        public static explicit operator Cardapio(object[] entrada)
        {
            Cardapio ev = new Cardapio();
            int i = 0;
            ev.Definir_data(entrada[i++].ToString());
            ev.refeicao = int.Parse(entrada[i++].ToString());
            ev.quantidade = int.Parse(entrada[i++].ToString());
            return ev;
        }
    }
}

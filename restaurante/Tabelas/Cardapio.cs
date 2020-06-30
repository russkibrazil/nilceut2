using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Cardapio
    {
        public DateTime dtPreparo { get; private set; }
        public int refeicao { get; set; }
        public int qtPreparada { get; set; }
        public int qtDisponivel { get; set;  }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                dtPreparo.ToShortDateString(),
                refeicao.ToString(),
                qtPreparada.ToString()
            };
        }
        public void Definir_data(string data)
        {
            this.dtPreparo = DateTime.Parse(data);
        }
        public void Definir_data(DateTime data)
        {
            this.dtPreparo = data;
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "DtPreparo", "QtPreparada", "QtDisponivel","IdRefeicao"
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
            ev.qtPreparada = int.Parse(entrada[i++].ToString());
            ev.qtDisponivel = int.Parse(entrada[i++].ToString());
            ev.refeicao = int.Parse(entrada[i++].ToString());
            return ev;
        }
    }
}

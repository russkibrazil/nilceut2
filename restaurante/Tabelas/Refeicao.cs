using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Refeicao
    {
        public int id { get; private set; }
        public string rbase {get;set;}
        public string rguarnicao { get; set; }
        public string rsalada { get; set; }
        public string rsobremesa { get; set; }
        public string rsuco { get; set; }
        public string nutricionista { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                id.ToString(),
                rbase,
                rguarnicao,
                rsalada,
                rsobremesa,
                rsuco,
                nutricionista
            };
        }
        public void Definir_id(string id)
        {
            this.id = int.Parse(id);
        }

        public static List<string> Campos()
        {
            return new List<string>
            {
                "idRefeicao", "Base", "Guarnicao", "Salada", "Sobremesa", "Suco", "Nutricionista_Resp"
            };
        }

        public static List<Refeicao> ConverteObject(List<object[]> listaObjs)
        {
            List<Refeicao> s = new List<Refeicao>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Refeicao)data);
            }
            return s;
        }

        public static explicit operator Refeicao(object[] entrada)
        {
            Refeicao ev = new Refeicao();
            int i = 0;
            ev.Definir_id(entrada[i++].ToString());
            ev.rbase = entrada[i++].ToString();
            ev.rguarnicao = entrada[i++].ToString();
            ev.rsalada = entrada[i++].ToString();
            ev.rsobremesa = entrada[i++].ToString();
            ev.rsuco = entrada[i++].ToString();
            ev.nutricionista = entrada[i++].ToString();
            return ev;
        }
    }
}

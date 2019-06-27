using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Setor
    {
        public string setor { get; private set; }
        public string supervisor { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                setor,
                supervisor
            };
        }
        public void DefinirSetor(string valor)
        {
           setor = valor;
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "Setor", "Supervisor_Cpf"
            };
        }
        public static List<Setor> ConverteObject(List<object[]> listaObjs)
        {
            List<Setor> s = new List<Setor>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Setor)data);
            }
            return s;
        }

        public static explicit operator Setor(object[] entrada)
        {
            Setor ev = new Setor();
            int i = 0;
            ev.setor = entrada[i++].ToString();
            ev.supervisor= entrada[i++].ToString();
            return ev;
        }
    }
}

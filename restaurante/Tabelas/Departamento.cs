using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Departamento
    {
        public string nome { get; private set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                nome
            };
        }
        public void DefinirSetor(string valor)
        {
           nome = valor;
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "Nome"
            };
        }
        public static List<Departamento> ConverteObject(List<object[]> listaObjs)
        {
            List<Departamento> s = new List<Departamento>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Departamento)data);
            }
            return s;
        }

        public static explicit operator Departamento(object[] entrada)
        {
            Departamento ev = new Departamento();
            ev.DefinirSetor(entrada[0].ToString());
            return ev;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Endereco
    {
        public Pessoas_gen p { get; set; }
        public string logradouro { get; set; }
        public string identificador { get; set; }
        public int numero { get; set; }

        public Endereco()
        {
            p = new Pessoas_gen();
        }
        public List<string> ListarValores()
        {
            return new List<string>
            {
                p.cpf,
                logradouro,
                identificador,
                numero.ToString()
            };
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "CPF", "Logradouro", "Identificador", "Numero"
            };
        }
        public static List<Endereco> ConverteObject(List<object[]> listaObjs)
        {
            List<Endereco> s = new List<Endereco>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Endereco)data);
            }
            return s;
        }

        public static explicit operator Endereco(object[] entrada)
        {
            Endereco ev = new Endereco();
            int i = 0;
            ev.p.Definir_Cpf(entrada[i++].ToString());
            ev.logradouro = entrada[i++].ToString();
            ev.identificador = entrada[i++].ToString();
            ev.numero = int.Parse(entrada[i++].ToString());
            return ev;
        }
    }

}

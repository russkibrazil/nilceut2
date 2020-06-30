﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante.Tabelas
{
    class Aluno
    {
        public Pessoas_gen p { get; set; }
        public int ra { get; set; }

        public List<string> ListarValores()
        {
            return new List<string>
            {
                p.cpf,
                ra.ToString()
            };
        }
        public static List<string> Campos()
        {
            return new List<string>
            {
                "CPF", "RA"
            };
        }
        public static List<Aluno> ConverteObject(List<object[]> listaObjs)
        {
            List<Aluno> s = new List<Aluno>();
            foreach (object[] data in listaObjs)
            {
                s.Add((Aluno)data);
            }
            return s;
        }

        public static explicit operator Aluno(object[] entrada)
        {
            Aluno ev = new Aluno();
            int i = 0;
            ev.p.Definir_Cpf(entrada[i++].ToString());
            ev.ra = int.Parse(entrada[i++].ToString());
            return ev;
        }
    }
}

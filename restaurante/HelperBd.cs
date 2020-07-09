using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante
{
    static class HelperBd
    {
        public static bool VerificaInt (string entrada){
            return int.TryParse(entrada, out int t);
                }
        public static bool VerificaBool (string entrada, out string saida)
        {
            if (Boolean.TryParse(entrada, out bool c)){if (c)
                {
                    saida = "1";
                    return true;
                }
                else
                {
                    saida = "0"; return true;
                }
            }
            saida = null;
            return false;
        }
        public static bool SanitizaString (string s, out string retorno)
        {
            if (s != null)
                if (s.Contains("'"))
                {
                    retorno = s.Replace("'", "*");
                    return true;
                }
            retorno = s;
            return false;
        }
        public static string SanitizaString (string s)
        {
            if (s != null)
                if (s.Contains("'"))
                {
                    return s.Replace("'", "*");
                }
            return s;
        }
        public static DateTime DataBdparaApp(string e)
        {
            string[] vt = e.Split(" ".ToCharArray());
            string[] dt = vt[0].Split("/".ToCharArray());
            return new DateTime(int.Parse(dt[0]), int.Parse(dt[1]), int.Parse(dt[2]));
        }
        public static string DataAppparaBd(DateTime e)
        {
            string[] vt = e.ToString().Split(" ".ToCharArray());
            string[] dt = vt[0].Split("/".ToCharArray());
            return dt[2]+"/"+ dt[1]+"/"+dt[0];
        }
}
}

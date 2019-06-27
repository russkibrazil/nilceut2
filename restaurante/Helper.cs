using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurante
{
    class Helper
    {
         static readonly internal Dictionary<string, bool> equivB = new Dictionary<string, bool>{
            {"verdadeiro",true }, {"sim",true }, {"verdade",true }, {"true", true}, {"1", true},
            {"falso",false }, {"não", false}, {"nao",false}, {"false", false}, {"0", false}
        };
        public static bool ParseBoolean(string valor)
        {
            return equivB.TryGetValue(valor.ToLower(), out bool retorno) ? retorno : false;
        }
    }

   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace restaurante
{
    /*class tabelasBd
    {
        public const string PALAVRA = "palavra";
        public const string RUBRICA = "rubrica";
        public const string REFERENCIAS = "referencias";
        public const string USUARIOS = "usr";
        public const string CONJUGACAOPT = "conjugacao_pt";
        public const string CONJUGACAOEN = "conjugacao_en";
        public const string EQUIVALENTE = "equivalencias";
        public const string FRASEOLOGIA = "fraseologia";
    }*/
    internal class ConectaBanco
    {
        protected string servidor;
        protected string bancoDados;
        protected string usuario;
        protected string senha;
        protected string porta;
        protected MySqlConnection conexao;

        public ConectaBanco(string bd = "nilceut2", string usr = "root", string pss = "gamesjoker", string svr = "localhost", string porta = "3306")
        {
            string connectionString = "Server=" + svr + ";" + "Port=" + porta + ";Database=" + bd + ";" + "User=" + usr + ";" + "pwd=" + pss + ";";
            conexao = new MySqlConnection(connectionString);
            servidor = svr;
            bancoDados = bd;
            usuario = usr;
            senha = pss;
            this.porta = porta;
        }
        public bool AbreConexao()
        {
            try
            {
                conexao.Open();
                if (conexao.ConnectionString.Contains("localhost"))
                    InformaDiag.Erro("Banco de testes ativo.");
                return true;
            }
            catch (MySqlException ex)
            {
                {
                    switch (ex.ErrorCode)
                    {
                        case 0:
                            InformaDiag.Erro("Falha ao conectar no servidor de dados.");
                            break;
                        case 1045:
                            InformaDiag.Erro("A combinação de usuário e senha não existe. Tente novamente.");
                            break;
                        default:
                            InformaDiag.Erro("Erro" + ex.Code.ToString() + ex.Message);
                            break;
                    }
                   
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public bool FechaConexao()
        {
            try
            {
                conexao.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                InformaDiag.Erro(ex.Message);
                return false;
            }
        }
        public MySqlConnection PegaConexao()
        {
            return this.conexao;
        }
    }
    static class CRUD{
        private static ConectaBanco ControllerBanco = new ConectaBanco();
        private static int EnviaComando(string query){
            int impacto = 0;
            if (ControllerBanco.AbreConexao() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, ControllerBanco.PegaConexao());
                try
                {
                    impacto = cmd.ExecuteNonQuery();
                }
                catch(MySqlException mErr)
                {
                    if (mErr.Message.Contains("Duplicate Entry")) impacto = 0;
                }
                ControllerBanco.FechaConexao();
            }
            return impacto;
        }
        /*public static int InsereLinha(string tabela, List<string> campos, List<string> valores)
        {
            string s; 
            string query = "INSERT INTO " + tabela + " (";
            foreach (string item in campos)
            {
                query += item;
                query += ", ";
            }
            query = query.Remove(query.Length - 2);
            query += ") VALUES(";
            foreach (string item in valores)
            {
                if (HelperBd.VerificaInt(item)) { //na verdade eu tenho que verificar o Controller e o tipo do campo atual
                    query += item;
                }
                else {
                    if (HelperBd.VerificaBool(item, out s)){
                            query += s;
                    }
                    else {
                        query += "'";
                        query += HelperBd.SanitizaString(item);
                        query += "'";
                    }
                }
                query += ",";
            }
            query = query.Remove(query.Length - 1);
            query += ")";
            return EnviaComando(query);
        }
        public static int UpdateLine(string tabela, List<string> campos, List<string> valores, string filtro)
        {
            string query = "UPDATE " + tabela + " SET ";
            string temp1, temp2, s;
            while (campos.Count > 0)
            {
                temp1 = campos.First();
                  temp2 = valores.First();
                query += temp1 + "=";
                 if (HelperBd.VerificaInt(temp2)) { //na verdade eu tenho que verificar o Controller e o tipo do campo atual
                    query += temp2;
                }
                else {
                    if (HelperBd.VerificaBool(temp2, out s)){
                            query += s;
                    }
                    else {
                        query += "'";
                        query += HelperBd.SanitizaString(temp2);
                        query += "'";
                    }
                }
                campos.RemoveAt(0);
                valores.RemoveAt(0);
                if (campos.Count > 0)
                    query += ", ";
            }
            if (filtro != "")
                query += " WHERE " + filtro;
            return EnviaComando(query);
        }*/
        public static  int ApagaLinha(string tabela, string filtro)
        {
            string query = "DELETE FROM " + tabela + " WHERE " + filtro;
            return (EnviaComando(query));
        }
        public static List<object[]> SelecionarTabela(string tabela, List<string> campos, string filtro = "", string outrosParam = "")
        {
            string query = "SELECT * FROM " + tabela;
            if (filtro != "")
                query += " WHERE " + filtro;
            if (outrosParam != "")
                query += " " + outrosParam;
            List<object[]> resultados = new List<object[]>();
            if (ControllerBanco.AbreConexao() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, ControllerBanco.PegaConexao());
                MySqlDataReader dataReader = cmd.ExecuteReader();
                 //REF HERE https://dev.mysql.com/doc/dev/connector-net/6.10/html/T_MySql_Data_MySqlClient_MySqlDataReader.htm
                 if (dataReader.HasRows){
                    object[] colunas = new object[campos.Count];
                    while(dataReader.Read()){
                        dataReader.GetValues(colunas);
                        resultados.Add(RetornaCopia(colunas, campos.Count));
                    }
                 }
                 dataReader.Close();
                 ControllerBanco.FechaConexao();
            }
            return resultados;
        }
        private static object[] RetornaCopia (object[] obj, int t)
        {
            object[] o = new object[t];
            obj.CopyTo(o,0);
            return o;
        }

        public static MySqlDataAdapter PegaTabela (string SelectCmd)
        {
            return new MySqlDataAdapter(SelectCmd, ControllerBanco.PegaConexao());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace restaurante
{
    public partial class frm_cliente : Form
    {
        private MySqlDataAdapter adapta = CRUD.PegaTabela("SELECT * FROM Cliente");
        bool novo = false;
        public frm_cliente()
        {
            InitializeComponent();
            adapta.InsertCommand = new MySqlCommand("INSERT INTO Cliente (Cpf, Nome, Endereco, Telefone) VALUES (@cpf, @nome, @end, @tel)");
            adapta.UpdateCommand = new MySqlCommand("UPDATE Cliente SET Nome=@nome, Endereco=@end, Telefone=@tel WHERE Cpf=@cpf");
            adapta.DeleteCommand = new MySqlCommand("DELETE FROM Cliente WHERE Cpf=@cpf");

            adapta.InsertCommand.Parameters.Add("@cpf", MySqlDbType.Int16, 11, "Cpf");
            adapta.UpdateCommand.Parameters.Add("@cpf", MySqlDbType.Int16, 11, "Cpf");
            adapta.DeleteCommand.Parameters.Add("@cpf", MySqlDbType.Int16, 11, "Cpf");

            adapta.InsertCommand.Parameters.Add("@nome", MySqlDbType.VarChar, 50, "Nome");
            adapta.UpdateCommand.Parameters.Add("@nome", MySqlDbType.VarChar, 50, "Nome");

            adapta.InsertCommand.Parameters.Add("@end", MySqlDbType.VarChar, 45, "Endereco");
            adapta.UpdateCommand.Parameters.Add("@end", MySqlDbType.VarChar, 45, "Endereco");

            adapta.InsertCommand.Parameters.Add("@tel", MySqlDbType.VarChar, 45, "Telefone");
            adapta.UpdateCommand.Parameters.Add("@tel", MySqlDbType.VarChar, 45, "Telefone");
            adapta.Fill(dsCliente);
        }

        private void button2_Click(object sender, EventArgs e)
        {
                adapta.Update(dsCliente);
        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {
            
        }
    }
}

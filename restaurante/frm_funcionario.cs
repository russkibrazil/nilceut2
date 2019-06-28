using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using restaurante.Tabelas;

namespace restaurante
{
    public partial class frmFuncionario : Form
    {
        bool novo = true;
        List<Funcionario> resF = new List<Funcionario>();
        Funcionario regAtual = new Funcionario();
        int pos = 0;
        public frmFuncionario()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtCtps.Text = "";
            txtEnd.Text = "";
            txtNome.Text = "";
            txtSalario.Text = "";
            txtTel.Text = "";
            novo = true;
            regAtual.Definir_Cpf("");
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.ctps = txtCtps.Text;
            regAtual.endereco = txtEnd.Text;
            regAtual.nome = txtNome.Text;
            regAtual.salario = double.Parse(txtSalario.Text);
            regAtual.telefone = txtTel.Text;
            if (novo && regAtual.cpf != "")
            {
                string[] cs = txtCpf.Text.Split(".-".ToArray());
                string c = "";
                for (int i = 0; i < 4; i++)
                {
                    c += cs[i];
                }
                regAtual.Definir_Cpf(c);
                if (CRUD.InsereLinha("Funcionario", Funcionario.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Funcionario", Funcionario.Campos(), regAtual.ListarValores(), "Cpf='" + regAtual.cpf + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Funcionario", "Cpf='" + regAtual.cpf + "'");
        }
    }
}

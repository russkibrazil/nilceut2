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
        List<Departamento> resS = new List<Departamento>();
        Departamento SetSelec = new Departamento();
        Servidor regAtual = new Servidor();
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
            regAtual.admissao = txtAdmissao.Value;
            if (novo && regAtual.cpf != "")
            {
                string[] cs = txtCpf.Text.Split(".-".ToArray());
                string c = "";
                for (int i = 0; i < 4; i++)
                {
                    c += cs[i];
                }
                regAtual.Definir_Cpf(c);
                if (CRUD.InsereLinha("Funcionario", Servidor.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Funcionario", Servidor.Campos(), regAtual.ListarValores(), "Cpf='" + regAtual.cpf + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Funcionario", "Cpf='" + regAtual.cpf + "'");
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtCtps.Text = "";
            txtEnd.Text = "";
            txtNome.Text = "";
            txtSalario.Text = "";
            comboSetor.Text = "";
            txtTel.Text = "";
            txtAdmissao.Value = DateTime.Today;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string[] cs = txtCpf.Text.Split(".-".ToArray());
            string c = "";
            for (int i = 0; i < 4; i++)
            {
                c += cs[i];
            }
            regAtual = Servidor.ConverteObject(CRUD.SelecionarTabela("Funcionario", Servidor.Campos(), "Cpf='" + c + "'")).First();
            if (regAtual.nome == "")
                InformaDiag.Erro("Nenhum registro encontrado!");
            else
            {
                txtCpf.Text = regAtual.cpf;
                txtCtps.Text = regAtual.ctps;
                txtEnd.Text = regAtual.endereco;
                txtNome.Text = regAtual.nome;
                txtSalario.Text = regAtual.salario.ToString();
                txtTel.Text = regAtual.telefone;
                comboSetor.Text = regAtual.setor;
                txtAdmissao.Value = regAtual.admissao;
                novo = false;
            }
        }
    }
}

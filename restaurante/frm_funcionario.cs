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
    public partial class frm_funcionario : Form
    {
        bool novo = true;
        List<Departamento> resS = new List<Departamento>();
        Departamento SetSelec = new Departamento();
        Servidor regAtual = new Servidor();

        public frm_funcionario()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtNome.Text = "";
            novo = true;
            regAtual.p.Definir_Cpf("");
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.p.nome = txtNome.Text;
            regAtual.p.dnasc = dtNascto.Value;
            regAtual.p.tipoUsuario = comboTipo.SelectedText;
            if (novo && regAtual.p.cpf != "")
            {
                string[] cs = txtCpf.Text.Split(".-".ToArray());
                string c = "";
                for (int i = 0; i < 4; i++)
                {
                    c += cs[i];
                }
                regAtual.p.Definir_Cpf(c);
                if (CRUD.InsereLinha("pessoa", Pessoas_gen.Campos(), regAtual.p.ListarValores()) > 0)
                {
                    CRUD.InsereLinha("servidor", Servidor.Campos(), regAtual.ListarValores());
                    InformaDiag.InformaSalvo();
                }
            }
            else
            {
                if (CRUD.UpdateLine("pessoa", Pessoas_gen.Campos(), regAtual.p.ListarValores(), "CPF=" + regAtual.p.cpf) > 0)
                {
                    CRUD.UpdateLine("servidor", Servidor.Campos(), regAtual.ListarValores(), "CPF=" + regAtual.p.cpf);
                    InformaDiag.InformaSalvo();
                }
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("servidor", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("telefone", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("endereco", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("pessoa", "CPF=" + regAtual.p.cpf);
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtNome.Text = "";
            comboSetor.Text = "";
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string[] cs = txtCpf.Text.Split(".-".ToArray());
            string c = "";
            for (int i = 0; i < 4; i++)
            {
                c += cs[i];
            }
            regAtual = Servidor.ConverteObject(CRUD.SelecionarTabela("servidor", Servidor.Campos(), "CPF=" + c)).First();
            if (regAtual.Departamento == "")
                InformaDiag.Erro("Nenhum registro encontrado!");
            else
            {
                txtCpf.Text = regAtual.p.cpf;
                txtNome.Text = regAtual.p.nome;
                dtNascto.Value = regAtual.p.dnasc;
                comboTipo.SelectedText = regAtual.p.tipoUsuario;
                comboSetor.Text = regAtual.Departamento;
                novo = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_telefone t = new frm_telefone(regAtual.p.nome, regAtual.p.cpf);
            t.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_endereco ender = new frm_endereco(regAtual.p.nome, regAtual.p.cpf);
            ender.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string pesquisa = comboSetor.Text;
            comboSetor.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resS = Departamento.ConverteObject(CRUD.SelecionarTabela("departamento", Departamento.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Departamento f in resS)
                {
                    comboSetor.Items.Add(f.nome);
                }
            }
            timer1.Enabled = false;
        }

        private void comboSetor_TextChanged(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
            }
        }

        private void comboSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSetor.Text != "")
            {
                regAtual.Departamento = resS.Find(f => f.nome == comboSetor.Text).nome;
            }
            else
            {
                regAtual.Departamento = "";
            }
        }
    }
}

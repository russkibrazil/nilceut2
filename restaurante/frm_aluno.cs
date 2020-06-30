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
    public partial class frm_aluno : Form
    {
        bool novo = true;
        List<Aluno> resC = new List<Aluno>();
        Aluno regAtual = new Aluno();
        public frm_aluno()
        {
            InitializeComponent();
        }

        private string RetornaCpf()
        {
            string[] cs = txtCpf.Text.Split(".-".ToArray());
            string c = "";
            for (int i = 0; i < 4; i++)
            {
                c += cs[i];
            }
            return c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            regAtual.p.nome = txtNome.Text;
            regAtual.p.dnasc = dtNascto.Value;
            regAtual.p.tipoUsuario = "ALUNO";
            regAtual.ra = int.Parse(txtRa.Text);
            if (novo && regAtual.p.cpf == "")
            {
                regAtual.p.Definir_Cpf(RetornaCpf());
                if (CRUD.InsereLinha("pessoa", Pessoas_gen.Campos(), regAtual.p.ListarValores()) > 0)
                {
                    CRUD.InsereLinha("aluno", Aluno.Campos(), regAtual.ListarValores());
                    InformaDiag.InformaSalvo();
                }
            }
            else
            {
                if (CRUD.UpdateLine("pessoa", Pessoas_gen.Campos(), regAtual.p.ListarValores(), "CPF=" + regAtual.p.cpf ) > 0)
                {
                    CRUD.UpdateLine("aluno", Aluno.Campos(), regAtual.ListarValores(), "CPF=" + regAtual.p.cpf);
                    InformaDiag.InformaSalvo();
                }
            }
            novo = false;
        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("aluno", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("telefone", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("endereco", "CPF=" + regAtual.p.cpf);
            CRUD.ApagaLinha("pessoa", "CPF=" + regAtual.p.cpf);
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtNome.Text = "";
            txtRa.Text = "";
            novo = true;
            regAtual.p.Definir_Cpf("");
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string psq = RetornaCpf();
            if (psq.Length == 11)
            {
                resC = Aluno.ConverteObject(CRUD.SelecionarTabela("aluno", Aluno.Campos(), "CPF=" + psq));
                if (resC.Count() > 0)
                {
                    regAtual = resC.First();
                    regAtual.p = Pessoas_gen.ConverteObject(CRUD.SelecionarTabela("pessoa", Pessoas_gen.Campos(), "CPF=" + regAtual.p.cpf)).First();
                    MostraDados();
                    novo = false;
                }
            }
        }

        private void MostraDados()
        {
            txtCpf.Text = regAtual.p.cpf;
            txtNome.Text = regAtual.p.nome;
            dtNascto.Value = regAtual.p.dnasc;
            txtRa.Text = regAtual.ra.ToString();
        }

        private void frm_aluno_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_telefone t = new frm_telefone();
            t.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frm_endereco ender = new frm_endereco();
            ender.ShowDialog();
        }
    }
}

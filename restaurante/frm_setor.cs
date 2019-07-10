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
    public partial class frm_setor : Form
    {
        bool novo = true;
        List<Funcionario> resFuncio = new List<Funcionario>();
        List<Setor> resSetor = new List<Setor>();
        Setor regAtual = new Setor();
        int pos = 0;
        public frm_setor()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtSetor.Text =  "";
            comboSupervisor.Text = "";
            regAtual.DefinirSetor("");
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (novo && regAtual.setor == "")
            {
                regAtual.DefinirSetor(txtSetor.Text);
                if (CRUD.InsereLinha("Setor", Setor.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Setor", Setor.Campos(), regAtual.ListarValores(), "Cpf='" + regAtual.setor + "'") > 0)
                    InformaDiag.InformaSalvo();
                resSetor.RemoveAt(pos);
                resSetor.Insert(pos, regAtual);
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Setor", "Setor='" + regAtual.setor + "'");
            resSetor.RemoveAt(pos);
            btnPrimeiro_Click(sender, e);
        }

        private void MostraDados()
        {
            txtSetor.Text = regAtual.setor;
            comboSupervisor.Text = regAtual.supervisor;
        }

        private void AtivaNavegador()
        {
            btnAnterior.Enabled = true;
            btnPrimeiro.Enabled = true;
            btnProximo.Enabled = true;
            btnUltimo.Enabled = true;
        }
        private void DesativaNavegador()
        {
            btnAnterior.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string psq = txtSetor.Text;
            if (psq.Length > 2)
            {
                resSetor = Setor.ConverteObject(CRUD.SelecionarTabela("Setor", Setor.Campos(), "Setor LIKE '" + psq + "'", "ASC"));
                if (resSetor.Count() > 0)
                {
                    regAtual = resSetor.First();
                    pos = 0;
                    MostraDados();
                    if (resSetor.Count() > 1)
                    {
                        AtivaNavegador();
                    }
                }
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resSetor.First();
                MostraDados();
                pos = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resSetor.ElementAt(--pos);
                MostraDados();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (pos < (resSetor.Count - 1))
            {
                regAtual = resSetor.ElementAt(++pos);
                MostraDados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int mx = (resSetor.Count - 1);
            if (pos < mx)
            {
                regAtual = resSetor.Last();
                pos = mx;
                MostraDados();
            }
        }

        private void txtSupervisor_TextChanged(object sender, EventArgs e)
        {
            if (timerSupervisor.Enabled)
            {
                timerSupervisor.Enabled = false;
                timerSupervisor.Enabled = true;
            }
            else
            {
                timerSupervisor.Enabled = true;
            }
        }

        private void timerSupervisor_Tick(object sender, EventArgs e)
        {
            string pesquisa = comboSupervisor.Text;
            comboSupervisor.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resFuncio = Funcionario.ConverteObject(CRUD.SelecionarTabela("Funcionario", Funcionario.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Funcionario f in resFuncio)
                {
                    comboSupervisor.Items.Add(f.nome);
                }
            }
            timerSupervisor.Enabled = false;
        }

        private void comboSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSupervisor.Text != "")
            {
                regAtual.supervisor = resFuncio.Find(f => f.nome == comboSupervisor.Text).cpf;
            }
            else
            {
                regAtual.supervisor = "";
            }
        }
    }
}

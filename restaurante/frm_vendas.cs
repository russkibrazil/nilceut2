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
    public partial class frm_vendas : Form
    {
        bool novo = true;
        List<Compra> resVendas = new List<Compra>();
        List<Pessoas_gen> resCli = new List<Pessoas_gen>();
        Compra regAtual = new Compra();
        int pos = 0;

        public frm_vendas()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                regAtual.DefinirData(DateTime.Parse(txtData.Text));
                if (CRUD.InsereLinha("compra", Compra.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("compra", Compra.Campos(), regAtual.ListarValores(), "Dt=" + regAtual.dt.ToString() + " AND CPF=" + regAtual.p.cpf) > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void MostraDados()
        {
            txtData.Value = regAtual.dt;
            txtCliente.Text = regAtual.p.nome;
        }

        private void btnProcurarNF_Click(object sender, EventArgs e)
        {
            string psq = txtData.Text;
            if (psq.Length > 2)
            {
                resVendas = Compra.ConverteObject(CRUD.SelecionarTabela("compra", Compra.Campos(), "Dt=" + psq));
                if (resVendas.Count() > 0)
                {
                    regAtual = resVendas.First();
                    regAtual.p = Pessoas_gen.ConverteObject(CRUD.SelecionarTabela("pessoa", Pessoas_gen.Campos(), "CPF=" + regAtual.p.cpf)).First();
                    pos = 0;
                    MostraDados();
                }
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (timerCliente.Enabled)
            {
                timerCliente.Enabled = false;
                timerCliente.Enabled = true;
            }
            else
            {
                timerCliente.Enabled = true;
            }
        }

        private void txtCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCliente.Text != "")
            {
                regAtual.p = resCli.Find(f => f.nome == txtCliente.Text);
            }
            else
            {
                regAtual.p = null;
            }
        }

        private void timerCliente_Tick(object sender, EventArgs e)
        {
            string pesquisa = txtCliente.Text;
            txtCliente.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resCli = Pessoas_gen.ConverteObject(CRUD.SelecionarTabela("pessoa", Pessoas_gen.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Pessoas_gen c in resCli)
                {
                    txtCliente.Items.Add(c.nome);
                }
            }
            timerCliente.Enabled = false;
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

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resVendas.First();
                MostraDados();
                pos = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resVendas.ElementAt(--pos);
                MostraDados();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (pos < (resVendas.Count - 1))
            {
                regAtual = resVendas.ElementAt(++pos);
                MostraDados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int mx = (resVendas.Count - 1);
            if (pos < mx)
            {
                regAtual = resVendas.Last();
                pos = mx;
                MostraDados();
            }
        }
    }
}

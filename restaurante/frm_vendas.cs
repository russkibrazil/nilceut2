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
        List<Cliente> resCli = new List<Cliente>();
        List<Servidor> resFuncio = new List<Servidor>();
        Compra regAtual = new Compra();
        int pos = 0;

        public frm_vendas()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtNota.Text = "";
            txtVendedor.Text = "";
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.dtCompra = DateTime.Parse(txtData.Text);
            if (novo && regAtual.nf <= 0)
            {
                regAtual.Definir_nf(txtNota.Text);
                if (CRUD.InsereLinha("Vendas", Compra.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Vendas", Compra.Campos(), regAtual.ListarValores(), "NotaFiscal=" + regAtual.nf.ToString()) > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void MostraDados()
        {
            txtNota.Text = regAtual.nf.ToString();
            txtData.Value = regAtual.dtCompra;
            //txtCliente
            //txtVendedor
        }

        private void btnProcurarNF_Click(object sender, EventArgs e)
        {
            string psq = txtNota.Text;
            if (psq.Length > 2)
            {
                resVendas = Compra.ConverteObject(CRUD.SelecionarTabela("Vendas", Compra.Campos(), "NotaFiscal=" + psq));
                if (resVendas.Count() > 0)
                {
                    regAtual = resVendas.First();
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
                regAtual.cliente = resCli.Find(f => f.nome == txtCliente.Text).cpf;
            }
            else
            {
                regAtual.cliente = "";
            }
        }

        private void timerCliente_Tick(object sender, EventArgs e)
        {
            string pesquisa = txtCliente.Text;
            txtCliente.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resCli = Cliente.ConverteObject(CRUD.SelecionarTabela("Cliente", Cliente.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Cliente c in resCli)
                {
                    txtCliente.Items.Add(c.nome);
                }
            }
            timerCliente.Enabled = false;
        }

        private void txtVendedor_TextChanged(object sender, EventArgs e)
        {
            if (timerVendedor.Enabled)
            {
                timerVendedor.Enabled = false;
                timerVendedor.Enabled = true;
            }
            else
            {
                timerVendedor.Enabled = true;
            }
        }

        private void txtVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtVendedor.Text != "")
            {
                regAtual.vendedor = resFuncio.Find(f => f.nome == txtVendedor.Text).cpf;
            }
            else
            {
                regAtual.vendedor = "";
            }
        }

        private void timerVendedor_Tick(object sender, EventArgs e)
        {
            string pesquisa = txtVendedor.Text;
            txtVendedor.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resFuncio = Servidor.ConverteObject(CRUD.SelecionarTabela("Funcionario", Servidor.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Servidor f in resFuncio)
                {
                    txtVendedor.Items.Add(f.nome);
                }
            }
            timerVendedor.Enabled = false;
        }
    }
}

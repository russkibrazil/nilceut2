using restaurante.Tabelas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurante
{
    public partial class frm_endereco : Form
    {
        Endereco regAtual = new Endereco();
        List<Endereco> resEndereco = new List<Endereco>();
        int pos = 0;
        bool novo = true;
        string ndc, cpf;
        public frm_endereco(string nomeDoCliente, string cpf)
        {
            InitializeComponent();
            ndc = nomeDoCliente;
            this.cpf = cpf;
            resEndereco = Endereco.ConverteObject(CRUD.SelecionarTabela("endereco", Endereco.Campos(), "CPF=" + cpf));
        }

        private void frm_endereco_Load(object sender, EventArgs e)
        {
            label4.Text = "Informações de endereço para " + ndc;
            if (resEndereco.Count > 0)
            {
                regAtual = resEndereco.First();
                MostraDados();
                if (resEndereco.Count > 1)
                    AtivaNavegador();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtDescricao.Text = "";
            txtNumero.Text= "";
            regAtual.numero = 0;
            regAtual.identificador = "";
            regAtual.p.Definir_Cpf("");
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.logradouro = comboLogra.SelectedText;
            regAtual.identificador = txtDescricao.Text;
            if (int.TryParse(txtNumero.Text, out int v))
            regAtual.numero = v;
            if (novo && regAtual.p.cpf == "")
            {
                regAtual.p.Definir_Cpf(cpf);
                if (CRUD.InsereLinha("endereco", Endereco.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("endereco", Endereco.Campos(), regAtual.ListarValores(), "CPF=" + regAtual.p.cpf + " AND Logradouro='" + regAtual.logradouro + "'") > 0)
                    InformaDiag.InformaSalvo();
                resEndereco.RemoveAt(pos);
                resEndereco.Insert(pos, regAtual);
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("endereco", "CPF=" + regAtual.p.cpf + " AND Logradouro='" + regAtual.logradouro + "'");
            resEndereco.RemoveAt(pos);
            btnPrimeiro_Click(sender, e);
        }

        private void MostraDados() {
            comboLogra.Text = regAtual.logradouro;
            txtDescricao.Text = regAtual.identificador;
            txtNumero.Text = regAtual.numero.ToString();
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
                regAtual = resEndereco.First();
                MostraDados();
                pos = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resEndereco.ElementAt(--pos);
                MostraDados();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (pos < (resEndereco.Count - 1))
            {
                regAtual = resEndereco.ElementAt(++pos);
                MostraDados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int mx = (resEndereco.Count - 1);
            if (pos < mx)
            {
                regAtual = resEndereco.Last();
                pos = mx;
                MostraDados();
            }
        }
    }
}

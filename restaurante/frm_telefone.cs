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
    public partial class frm_telefone : Form
    {
        Telefone regAtual = new Telefone();
        List<Telefone> resTelefone = new List<Telefone>();
        int pos = 0;
        bool novo = true;
        string ndc, ncpf;
        public frm_telefone(string nomeDoCliente, string cpf)
        {
            InitializeComponent();
            ndc = nomeDoCliente;
            ncpf = cpf;
            resTelefone = Telefone.ConverteObject(CRUD.SelecionarTabela("telefone", Telefone.Campos(), "CPF="+ncpf));
        }

        private void frm_endereco_Load(object sender, EventArgs e)
        {
            label4.Text = "Informações de endereço para " + ndc;
            if (resTelefone.Count > 0)
            {
                regAtual = resTelefone.First();
                MostraDados();
                if (resTelefone.Count > 1)
                    AtivaNavegador();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtTelefone.Text = "";
            regAtual.p.Definir_Cpf("");
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.telefone = txtTelefone.Text;
            if (novo && regAtual.p.cpf == "")
            {
                regAtual.p.Definir_Cpf(ncpf);
                if (CRUD.InsereLinha("telefone", Telefone.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("telefone", Telefone.Campos(), regAtual.ListarValores(), "CPF=" + regAtual.p.cpf + " AND Telefone=" + regAtual.telefone ) > 0)
                    InformaDiag.InformaSalvo();
                resTelefone.RemoveAt(pos);
                resTelefone.Insert(pos, regAtual);
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("telefone", "CPF=" + regAtual.p.cpf + " AND Telefone=" + regAtual.telefone);
            resTelefone.RemoveAt(pos);
            btnPrimeiro_Click(sender, e);
        }

        private void MostraDados() {
            txtTelefone.Text = regAtual.telefone;
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
                regAtual = resTelefone.First();
                MostraDados();
                pos = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resTelefone.ElementAt(--pos);
                MostraDados();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (pos < (resTelefone.Count - 1))
            {
                regAtual = resTelefone.ElementAt(++pos);
                MostraDados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int mx = (resTelefone.Count - 1);
            if (pos < mx)
            {
                regAtual = resTelefone.Last();
                pos = mx;
                MostraDados();
            }
        }
    }
}

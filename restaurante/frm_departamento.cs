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
    public partial class frm_departamento : Form
    {
        bool novo = true;
        List<Departamento> resSetor = new List<Departamento>();
        Departamento regAtual = new Departamento();
        int pos = 0;
        public frm_departamento()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtSetor.Text =  "";
            regAtual.DefinirSetor("");
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (novo && regAtual.nome == "")
            {
                regAtual.DefinirSetor(txtSetor.Text);
                if (CRUD.InsereLinha("departamento", Departamento.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("departamento", Departamento.Campos(), regAtual.ListarValores(), "Nome='" + regAtual.nome + "'") > 0)
                    InformaDiag.InformaSalvo();
                resSetor.RemoveAt(pos);
                resSetor.Insert(pos, regAtual);
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("departamento", "Nome='" + regAtual.nome + "'");
            resSetor.RemoveAt(pos);
            btnPrimeiro_Click(sender, e);
        }

        private void MostraDados()
        {
            txtSetor.Text = regAtual.nome;
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
                resSetor = Departamento.ConverteObject(CRUD.SelecionarTabela("departamento", Departamento.Campos(), "Nome LIKE '" + psq + "'", "ASC"));
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
    }
}

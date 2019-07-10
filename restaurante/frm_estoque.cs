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
    public partial class frm_estoque : Form
    {
        bool novo = true;
        List<Estoque> resEst = new List<Estoque>();
        List<Funcionario> resFuncio = new List<Funcionario>();
        Estoque regAtual = new Estoque();
        int pos = 0;

        public frm_estoque()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtItem.Text = "";
            txtQuantidade.Text = "";
            txtResponsavel.Text = "";
            txtUnidade.Text = "";
            novo = true;
            regAtual.Definir_item("");
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.quantidade = double.Parse(txtQuantidade.Text);
            regAtual.unidade = txtUnidade.Text;
            regAtual.vencimento = DateTime.Parse( txtVencimento.Text);
            if (novo && regAtual.item != "")
            {
                regAtual.Definir_item(txtItem.Text);
                if (CRUD.InsereLinha("Estoque", Estoque.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Estoque", Estoque.Campos(), regAtual.ListarValores(), "Item='" + regAtual.item + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApaga_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Estoque", "Item='" + regAtual.item + "'");
        }

        private void MostraDados()
        {
            txtItem.Text = regAtual.item;
            txtQuantidade.Text = regAtual.quantidade.ToString();
            txtUnidade.Text = regAtual.unidade;
            txtVencimento.Value = regAtual.vencimento;
            //txtResponsavel
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resEst.First();
                MostraDados();
                pos = 0;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                regAtual = resEst.ElementAt(--pos);
                MostraDados();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (pos < (resEst.Count - 1))
            {
                regAtual = resEst.ElementAt(++pos);
                MostraDados();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int mx = (resEst.Count - 1);
            if (pos < mx)
            {
                regAtual = resEst.Last();
                pos = mx;
                MostraDados();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string psq = txtItem.Text;
            if (psq.Length > 2)
            {
                resEst = Estoque.ConverteObject(CRUD.SelecionarTabela("Estoque", Estoque.Campos(), "Item LIKE '" + psq + "'", "ASC"));
                if (resEst.Count() > 0)
                {
                    regAtual = resEst.First();
                    pos = 0;
                    MostraDados();
                    if (resEst.Count() > 1)
                    {
                        AtivaNavegador();
                    }
                }
            }
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

        private void txtResponsavel_TextChanged(object sender, EventArgs e)
        {
            if (timerResponsavel.Enabled)
            {
                timerResponsavel.Enabled = false;
                timerResponsavel.Enabled = true;
            }
            else
            {
                timerResponsavel.Enabled = true;
            }
        }

        private void txtResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtResponsavel.Text != "")
            {
                regAtual.estoquista = resFuncio.Find(f => f.nome == txtResponsavel.Text).cpf;
            }
            else
            {
                regAtual.estoquista = "";
            }
        }

        private void timerResponsavel_Tick(object sender, EventArgs e)
        {
            string pesquisa = txtResponsavel.Text;
            txtResponsavel.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resFuncio = Funcionario.ConverteObject(CRUD.SelecionarTabela("Funcionario", Funcionario.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Funcionario f in resFuncio)
                {
                    txtResponsavel.Items.Add(f.nome);
                }
            }
            timerResponsavel.Enabled = false;
        }
    }
}

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
    public partial class frm_refeicao : Form
    {
        bool novo = true;
        List<Refeicao> resRef = new List<Refeicao>();
        List<Servidor> resFuncio = new List<Servidor>();
        Refeicao regAtual = new Refeicao();
        int pos = 0;
        public frm_refeicao()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtBase.Text = "";
            txtGuarnicao.Text = "";
            txtId.Text = "";
            txtNutricionista.Text = "";
            txtSalada.Text = "";
            txtSobremesa.Text = "";
            txtSuco.Text = "";
            novo = true;
            regAtual.Definir_id("0");
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.nutricionista = txtNutricionista.Text;
            regAtual.rbase = txtBase.Text;
            regAtual.rguarnicao = txtGuarnicao.Text;
            regAtual.rsalada = txtSalada.Text;
            regAtual.rsobremesa = txtSobremesa.Text;
            regAtual.rsuco = txtSuco.Text;
            if (novo && regAtual.id <= 0)
            {
                regAtual.Definir_id(txtId.Text);
                if (CRUD.InsereLinha("Refeicao", Refeicao.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Refeicao", Refeicao.Campos(), regAtual.ListarValores(), "idRefeicao='" + regAtual.id.ToString()) > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApaga_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Refeicao", "idRefeicao=" + regAtual.id.ToString());
        }

        private void MostraDados()
        {
            txtId.Text = regAtual.id.ToString();
            txtBase.Text = regAtual.rbase;
            txtGuarnicao.Text = regAtual.rguarnicao;
            //txtNutricionista
            txtSalada.Text = regAtual.rsalada;
            txtSobremesa.Text = regAtual.rsobremesa;
            txtSuco.Text = regAtual.rsuco;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string psq = txtId.Text;
            if (psq != "")
            {
                resRef = Refeicao.ConverteObject(CRUD.SelecionarTabela("Refeicao", Refeicao.Campos(), "Refeicao=" + psq));
                if (resRef.Count() > 0)
                {
                    regAtual = resRef.First();
                    MostraDados();
                }
            }
        }

        private void txtNutricionista_TextChanged(object sender, EventArgs e)
        {
            if (timerNutricionista.Enabled)
            {
                timerNutricionista.Enabled = false;
                timerNutricionista.Enabled = true;
            }
            else
            {
                timerNutricionista.Enabled = true;
            }
        }

        private void timerNutricionista_Tick(object sender, EventArgs e)
        {
            string pesquisa = txtNutricionista.Text;
            txtNutricionista.Items.Clear();
            if (pesquisa.Length > 0)
            {
                resFuncio = Servidor.ConverteObject(CRUD.SelecionarTabela("Funcionario", Servidor.Campos(), "Nome LIKE '%" + pesquisa + "%'", "LIMIT 15"));

                foreach (Servidor f in resFuncio)
                {
                    txtNutricionista.Items.Add(f.nome);
                }
            }
            timerNutricionista.Enabled = false;
        }

        private void txtNutricionista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNutricionista.Text != "")
            {
                regAtual.nutricionista = resFuncio.Find(f => f.nome == txtNutricionista.Text).cpf;
            }
            else
            {
                regAtual.nutricionista = "";
            }
        }
    }
}

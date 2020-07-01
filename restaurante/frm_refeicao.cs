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
        Refeicao regAtual = new Refeicao();

        public frm_refeicao()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtBase.Text = "";
            txtGuarnicao.Text = "";
            txtId.Text = "";
            txtSalada.Text = "";
            txtSobremesa.Text = "";
            txtSuco.Text = "";
            novo = true;
            regAtual.Definir_id("0");
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.rbase = txtBase.Text;
            regAtual.rguarnicao = txtGuarnicao.Text;
            regAtual.rsalada = txtSalada.Text;
            regAtual.rsobremesa = txtSobremesa.Text;
            regAtual.rsuco = txtSuco.Text;
            if (novo && regAtual.id <= 0)
            {
                regAtual.Definir_id(txtId.Text);
                if (CRUD.InsereLinha("refeicao", Refeicao.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("refeicao", Refeicao.Campos(), regAtual.ListarValores(), "Id=" + regAtual.id.ToString()) > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApaga_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("refeicao", "Id=" + regAtual.id.ToString());
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
                resRef = Refeicao.ConverteObject(CRUD.SelecionarTabela("refeicao", Refeicao.Campos(), "Id=" + psq));
                if (resRef.Count() > 0)
                {
                    regAtual = resRef.First();
                    MostraDados();
                }
            }
        }
    }
}

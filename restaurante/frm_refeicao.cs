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
    }
}

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
    }
}

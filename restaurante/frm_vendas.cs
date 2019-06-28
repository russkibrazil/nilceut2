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
        List<Vendas> resVendas = new List<Vendas>();
        Vendas regAtual = new Vendas();
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
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            regAtual.dtCompra = DateTime.Parse(txtData.Text);
            if (novo && regAtual.nf <= 0)
            {
                regAtual.Definir_nf(txtNota.Text);
                if (CRUD.InsereLinha("Vendas", Vendas.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Vendas", Vendas.Campos(), regAtual.ListarValores(), "NotaFiscal=" + regAtual.nf.ToString()) > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }
    }
}

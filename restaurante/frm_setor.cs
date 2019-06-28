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
    public partial class frm_setor : Form
    {
        bool novo = true;
        List<Setor> resSetor = new List<Setor>();
        Setor regAtual = new Setor();
        int pos = 0;
        public frm_setor()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtSetor.Text =  "";
            txtSupervisor.Text = "";
            regAtual.DefinirSetor("");
            novo = true;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            if (novo && regAtual.setor == "")
            {
                regAtual.DefinirSetor(txtSetor.Text);
                if (CRUD.InsereLinha("Setor", Setor.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Setor", Setor.Campos(), regAtual.ListarValores(), "Cpf='" + regAtual.setor + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Setor", "Setor='" + regAtual.setor + "'");
        }
    }
}

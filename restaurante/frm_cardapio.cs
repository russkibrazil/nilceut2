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
    public partial class frm_cardapio : Form
    {
        bool novo = true;
        List<Cardapio> resCardapio = new List<Cardapio>();
        Cardapio regAtual = new Cardapio();
        public frm_cardapio()
        {
            InitializeComponent();
        }

        private void LimpaControles()
        {
            txtData.Value = DateTime.Today;
            txtQtd.Value = 1;
            txtRefeicao.Text = "";
            regAtual.Definir_data(DateTime.Today.ToShortDateString());
            regAtual.quantidade = 1;
            regAtual.refeicao = 0;
        }
        private void MostraDados()
        {
            txtData.Value = regAtual.dtCompra;
            txtQtd.Value = regAtual.quantidade;
            txtRefeicao.Text = regAtual.refeicao.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaControles();
            novo = true;
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            string psq = txtData.Text;
            resCardapio = Cardapio.ConverteObject(CRUD.SelecionarTabela("Cardapio", Cardapio.Campos(), "Data='" + psq + "'", "ASC"));
            if (resCardapio.Count() > 0)
            {
                regAtual = resCardapio.First();
                MostraDados();
            }
            else
            {
                LimpaControles();
            }
            novo = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            regAtual.quantidade = int.Parse(txtQtd.Value.ToString());
            regAtual.refeicao = int.Parse(txtRefeicao.Text);
            if (novo)
            {
                regAtual.Definir_data(txtData.Text);
                if (CRUD.InsereLinha("Cardapio", Cardapio.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Cardapio", Cardapio.Campos(), regAtual.ListarValores(), "Data='" + regAtual.dtCompra + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string psq = txtRefeicao.Text;
            List<Refeicao> resRef = new List<Refeicao>();
            Refeicao cRef = new Refeicao();
            if (psq.Length > 0)
            {
                resRef = Refeicao.ConverteObject(CRUD.SelecionarTabela("Refeicao", Refeicao.Campos(), "idRefeicao=" + psq));
                if (resRef.Count() > 0)
                {
                    cRef = resRef.First();
                    labelRefeicao.Text = cRef.rbase + "\n" + cRef.rguarnicao + "\n" + cRef.rsalada + "\n" + cRef.rsobremesa + "\n" + cRef.rsuco;
                }
            }
        }
    }
}

﻿using System;
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
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_funcionario func = new frm_funcionario();
            func.ShowDialog();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_aluno cli = new frm_aluno();
            cli.ShowDialog();
        }

        private void setorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_departamento setor = new frm_departamento();
            setor.ShowDialog();
        }

        private void refeiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_refeicao refeicao = new frm_refeicao();
            refeicao.ShowDialog();
        }

        private void vendasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_vendas vendas = new frm_vendas();
            vendas.ShowDialog();
        }

        private void cardápioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_cardapio cardapio = new frm_cardapio();
            cardapio.ShowDialog();
        }
    }
}

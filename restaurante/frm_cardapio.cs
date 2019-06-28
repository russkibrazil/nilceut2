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
        int pos = 0;
        public frm_cardapio()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }
    }
}

using System.Windows.Forms;

namespace restaurante
{
    static class InformaDiag
    {
        public static void Informa(string mensagem){
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void InformaSalvo()
        {
            MessageBox.Show("Salvo!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void Erro(string mensagem)
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ConfirmaSN(string mensagem)
        {
            return MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public static DialogResult ConfirmaOkCancel(string mensagem)
        {
            return MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}

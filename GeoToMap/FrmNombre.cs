using System;
using System.Windows.Forms;

namespace GeoToMap
{
    public partial class FrmNombre : Form
    {

        public string Texto { set; get; }

        public FrmNombre()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox1.Text))
            {
                Texto = textBox1.Text;
                this.Close();
            }
            else
            {
                label2.Text = "Escriba un nombre!";
            }
        }

        private void FrmNombre_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(label2.Text))
            {
                label2.Text = "";
            }
        }
    }
}

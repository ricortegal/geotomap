using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GeoToMap
{
    public partial class FrmInicio : Form
    {
        delegate void dlgVoid();

        public FrmInicio()
        {
            InitializeComponent();
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {

        }

        public void Mostrar()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new dlgVoid(Mostrar));
                return;
            }
            this.Show();
            Application.Run(this);
        }

        public void Cerrar()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new dlgVoid(Cerrar));
                return;
            }
            this.Close();
        }




    }
}

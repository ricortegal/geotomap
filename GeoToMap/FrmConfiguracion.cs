using System;
using System.Windows.Forms;
using GeoToMap.Properties;

namespace GeoToMap
{
    public partial class FrmConfiguracion : Form
    {
        public FrmConfiguracion()
        {
            InitializeComponent();
        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            txtProyeccion.Text = Settings.Default.Proyeccion;
            txtBingMaps.Text = Settings.Default.BingMapsKey;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Settings.Default.Proyeccion = txtProyeccion.Text;
            Settings.Default.BingMapsKey = txtBingMaps.Text;
            Settings.Default.Save();
        }
    }
}

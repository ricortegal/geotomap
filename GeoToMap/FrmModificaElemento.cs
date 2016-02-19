using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GeoToMap.Properties;

namespace GeoToMap
{
    public partial class FrmModificaElemento : Form
    {
        private DireccionPuntoSOB _listaObservada;
        private DireccionPunto _dp;
        private IGeocodificador _geocodificador;

        public FrmModificaElemento(DireccionPuntoSOB listaObservada, string key, IGeocodificador geocodificador)
        {
            InitializeComponent();
            _listaObservada = listaObservada;
            _dp = listaObservada[key];
            _geocodificador = geocodificador;
        }


        private void FrmModificaElemento_Load(object sender, EventArgs e)
        {
            txtDireccion.Text = _dp.Direccion;
            txtLat.Text = Convert.ToString(_dp.Latitud);
            txtLon.Text = Convert.ToString(_dp.Longitud);
            txtX.Text = Convert.ToString(_dp.X);
            txtY.Text = Convert.ToString(_dp.Y);
            labGeo.Text = _dp.GeoreferenciadorDeno;
            labPais.Text = _dp.Pais;
            labProvincia.Text = _dp.NivelRegional2;
            labRegion.Text = _dp.NivelRegional1;
            labPais.Text = _dp.Pais;
        }


        private void btnGeoCodifica_Click(object sender, EventArgs e)
        {

            DireccionPunto dp = _geocodificador.GetCoordenadas(txtDireccion.Text);
            if(dp!=null)
            {
                _dp.Direccion = dp.Direccion;
                _dp.Latitud = dp.Latitud;
                _dp.Longitud = dp.Longitud;
                ((GeocodificadorBase) _geocodificador).utm30Ntransformacion(dp);
                _dp.X = dp.X;
                _dp.Y = dp.Y;
                _dp.Pais = dp.Pais;
                _dp.Municipio = dp.Municipio;
                _dp.NivelRegional2 = dp.NivelRegional2;
                _dp.NivelRegional1 = dp.NivelRegional1;
                txtLat.Text = Convert.ToString(_dp.Latitud);
                txtLon.Text = Convert.ToString(_dp.Longitud);
                txtX.Text = Convert.ToString(_dp.X);
                txtY.Text = Convert.ToString(_dp.Y);
                labPais.Text = dp.Pais;
                labProvincia.Text = dp.NivelRegional2;
                labRegion.Text = dp.NivelRegional1;
                _dp.IsGeocodificado = true;
           
            }

        }

        /// <summary>
        /// Modifica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifica_Click(object sender, EventArgs e)
        {
            try
            {

                _dp.Direccion = txtDireccion.Text;
                _dp.Latitud = Convert.ToDouble(txtLat.Text);
                _dp.Longitud = Convert.ToDouble(txtLon.Text);
                _dp.X = Convert.ToDouble(txtX.Text);
                _dp.Y = Convert.ToDouble(txtY.Text);
                _listaObservada.Update(_dp.key,_dp);

            }
            catch(Exception ex)
            {
                MessageBox.Show(Resources.Error, Resources.msgVerifique_los_campos_numéricos___ + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Es llamado poro todos los cuadros de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            _dp.IsGeocodificado = false;
            if(((TextBox)(sender)).Name=="txtLat" || ((TextBox)(sender)).Name=="txtLon")
            {
                double lat = 0, lon = 0;
                bool error = false;
                if (!string.IsNullOrEmpty(txtLon.Text))
                {
                    try
                    {
                        lon = Convert.ToDouble(txtLon.Text);
                        _dp.Longitud = lon;
                        txtLon.BackColor = Color.White;
                        error = false;
                    }
                    catch(FormatException excast)
                    {
                        txtLon.BackColor = Color.OrangeRed;
                        error = true;
                    }
                }
                if (!string.IsNullOrEmpty(txtLat.Text))
                {
                    try
                    {
                        lat = Convert.ToDouble(txtLat.Text);
                        _dp.Latitud = lat;
                        txtLat.BackColor = Color.White;
                        error = false;
                    }
                    catch(FormatException excast)
                    {
                        txtLat.BackColor = Color.OrangeRed;
                        error = true;
                    }
                }
                if (!error)
                {
                    ((GeocodificadorBase) _geocodificador).utm30Ntransformacion(_dp);
                    txtX.Text = _dp.X.ToString(CultureInfo.CurrentCulture);
                    txtY.Text = _dp.Y.ToString(CultureInfo.CurrentCulture);
                }

            }
        }
    }
}

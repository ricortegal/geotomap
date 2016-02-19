using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GeoToMap;

namespace GeoToMap
{
    public partial class FrmAnadeElemento : Form
    {
        private DireccionPuntoSOB _listaObservada;
        private DireccionPunto _dp;
        private IGeocodificador _geocodificador;
        private bool _flagReferenciando = false;

        public FrmAnadeElemento(DireccionPuntoSOB listaObservada,  IGeocodificador geocodificador)
        {
            InitializeComponent();
            _listaObservada = listaObservada;
            _dp = new DireccionPunto();
            _geocodificador = geocodificador;
        }


        private void FrmModificaElemento_Load(object sender, EventArgs e)
        {
            txtDireccion.Text = _dp.Direccion;
            txtLat.Text = Convert.ToString(_dp.Latitud);
            txtLon.Text = Convert.ToString(_dp.Longitud);
            //txtX.Text = Convert.ToString(_dp.X);
            //txtY.Text = Convert.ToString(_dp.Y);
            txtX.Text = "0";
            txtY.Text = "0";
            labGeo.Text = _dp.GeoreferenciadorDeno;
            labPais.Text = _dp.Pais;
            labProvincia.Text = _dp.NivelRegional2;
            labRegion.Text = _dp.NivelRegional1;
            labPais.Text = _dp.Pais;
            labPais.Text = "";
            labProvincia.Text = "";
            labRegion.Text = "";
            labMunicipio.Text = "";
            _geocodificador.MensajeEvent += muestraError;
        }

        private void muestraError(object sender,EventArgsGeocodificador eventArg)
        {
            labError.Text = eventArg.Mensaje;
        }

        private void btnGeoCodifica_Click(object sender, EventArgs e)
        {      
            DireccionPunto dp = _geocodificador.GetCoordenadas(txtDireccion.Text);
            _dp = new DireccionPunto();
            _flagReferenciando = true;
            if(dp!=null)
            {
                _dp.key = dp.Direccion;
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
                labMunicipio.Text = dp.Municipio;
                _dp.IsGeocodificado = true;
                _dp.GeoreferenciadorDeno = _geocodificador.Deno;
                labGeo.Text = _dp.GeoreferenciadorDeno;
            }
            _flagReferenciando = false;
        }

        private void btnAnade_Click(object sender, EventArgs e)
        {
            try
            {
              DireccionPunto ndp = new DireccionPunto(); 
              ndp.Direccion = txtDireccion.Text;
              ndp.Latitud = Convert.ToDouble(txtLat.Text);
              ndp.Longitud = Convert.ToDouble(txtLon.Text);
              ndp.X = Convert.ToDouble(txtX.Text);
              ndp.Y = Convert.ToDouble(txtY.Text);
              ndp.IsGeocodificado = _dp.IsGeocodificado;
              ndp.GeoreferenciadorDeno = _dp.GeoreferenciadorDeno;
              ndp.Municipio = _dp.Municipio;
              ndp.NivelRegional1 = _dp.NivelRegional1;
              ndp.NivelRegional2 = _dp.NivelRegional2;
              ndp.Pais = _dp.Pais;
              if(String.IsNullOrEmpty(_dp.key))
              {
                  _dp.key = txtDireccion.Text;
              }
              ndp.key = _dp.key;
              String key = _dp.key;

                if(_listaObservada.ContainsKey(_dp.key))
                {
                    int i = 1;
                    while(_listaObservada.ContainsKey(ndp.key))
                    {
                        ndp.key = key+"_"+Convert.ToString(i);
                        i++;
                    }
                    _listaObservada.Add(ndp.key,ndp);
                }
                else
                {
                    
                    _listaObservada.Add(ndp.key, ndp);
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error", "Verifique los campos numéricos : " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        ///// <summary>
        ///// Es llamado poro todos los cuadros de texto
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txtDireccion_TextChanged(object sender, EventArgs e)
        //{
        //    if(txtDireccion.Text==String.Empty)
        //    {
        //        txtLat.Text = "0";
        //        txtLon.Text = "0";
        //        txtX.Text = "0";
        //        txtY.Text = "0";

        //    }
        //        _dp.IsGeocodificado = false;
        //}

        /// <summary>
        /// Es llamado poro todos los cuadros de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

            _dp.IsGeocodificado = false;
            if ((((TextBox)(sender)).Name == "txtLat" || ((TextBox)(sender)).Name == "txtLon") && !_flagReferenciando)
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
                    catch (FormatException excast)
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
                    catch (FormatException excast)
                    {
                        txtLat.BackColor = Color.OrangeRed;
                        error = true;
                    }
                }
                if (!error)
                {
                    ((GeocodificadorBase)_geocodificador).utm30Ntransformacion(_dp);
                    txtX.Text = _dp.X.ToString(CultureInfo.CurrentCulture);
                    txtY.Text = _dp.Y.ToString(CultureInfo.CurrentCulture);
                }

                if (txtDireccion.Text == String.Empty)
                {
                    txtLat.Text = "0";
                    txtLon.Text = "0";
                    txtX.Text = "0";
                    txtY.Text = "0";

                }



            }
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged_1(object sender, EventArgs e)
        {
            if (txtDireccion.Text == String.Empty)
            {
                txtLat.Text = "0";
                txtLon.Text = "0";
                txtX.Text = "0";
                txtY.Text = "0";

            }
            _dp.IsGeocodificado = false;
        }
    }
}

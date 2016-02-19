using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EGIS.ShapeFileLib;
using GeoToMap.Properties;
using System.Linq;

namespace GeoToMap
{
    public partial class FrmMain : Form
    {
        #region VARIABLES PRIVADAS
        private readonly DireccionPuntoSOB items;
        private IGeocodificador geocodificador;
        private string nameFile = String.Empty;
        private string pathFile = String.Empty;
        private StringBuilder sbSalida;
        private readonly bool runInmediately = false;
        private readonly bool closeAfterRun = false;
        private List<Covariable> covariables = new List<Covariable>();
        private const char separador = '|';
        private List<RegistroModel> listadoRegistro;
        private int idRegistro = 0;
        private int final = 0;
        private const string _saviso = "Aviso";
        private const string _serror = "Error";
        private const string _sCovariables = "Covariables";
        private const string _smensajeFinProcesoTraduccion =
            "Ha finalizado el proceso de traducción de direcciones a coordenadas";
        private const string _snoTieneDefCovariable = "No tiene definidadas covariables definidas\n¿Quiere definirlas";
        private const string _susrlDocumentacion = "http://geotomap.codeplex.com/documentation";
        private const string _s1TieneDefinidas = "Tiene las siguientes covariables definidas:\n\n{0}";
        private const string _sNoHayUnArchivoAbierto = "No hay un archivo de texto abierto con datos a georreferenciar";
        private readonly FrmInicio _frmInicio = new FrmInicio();
 
        #endregion

        #region DEFINICION DELEGADOS

        private delegate void DlgVoid();
        private delegate void DlgProcesoLoteGeoreferenciacion(IGeocodificador codificador, List<string> direcciones);
        private delegate void DlgMensaje(string s);
        private delegate void AnadeDireccionPunto(DireccionPunto p);

        #endregion

        #region CONSTRUCTORES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="runInmediately"></param>
        /// <param name="closeAfterRun"></param>
        public FrmMain(string filename, bool runInmediately, bool closeAfterRun)
        {
            pathFile = Path.GetDirectoryName(filename);
            nameFile = Path.GetFileName(filename);
            this.closeAfterRun = closeAfterRun;
            this.runInmediately = runInmediately;
            if (String.IsNullOrEmpty(pathFile))
            {
                pathFile = Path.GetDirectoryName(Application.ExecutablePath);
            }
            this.Hide();
            Thread hiloVentanaInicio = new Thread(new ThreadStart(_frmInicio.Mostrar));
            hiloVentanaInicio.IsBackground = true;
            hiloVentanaInicio.Start();
            InitializeComponent();
            items = new DireccionPuntoSOB();
            items.ChangeCollection += manejaCambiosObserver;
            sbSalida = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public FrmMain(string filename)
        {
            pathFile = Path.GetPathRoot(filename);
            nameFile = Path.GetFileName(filename);
            runInmediately = false;
            txtCarpetaSalida.Text = pathFile;
            this.Hide();
            Thread hiloVentanaInicio = new Thread(new ThreadStart(_frmInicio.Mostrar));
            hiloVentanaInicio.IsBackground = true;
            hiloVentanaInicio.Start();
            InitializeComponent();
            items = new DireccionPuntoSOB();
            items.ChangeCollection += manejaCambiosObserver;
            sbSalida = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        public FrmMain()
        {
            this.Hide();
            Thread hiloVentanaInicio = new Thread(new ThreadStart(_frmInicio.Mostrar));
            hiloVentanaInicio.IsBackground = true;
            hiloVentanaInicio.Start();
            InitializeComponent();
            items = new DireccionPuntoSOB();
            items.ChangeCollection += manejaCambiosObserver;
            sbSalida = new StringBuilder();
        }

        #endregion

        #region FORM LOAD Y AJUSTE TAMAÑO

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!runInmediately)
            {
                Thread.Sleep(2500);
            }
            _frmInicio.Cerrar();
            this.Show();
            this.Activate();

            ajustatamanio();

            covariables = new List<Covariable>();

            if (!String.IsNullOrEmpty(pathFile))
                txtCarpetaSalida.Text = pathFile;

            var dict = new Dictionary<ImplementacionGeocodificador, string>
                       {
                           {ImplementacionGeocodificador.GOOGLE_MAPS, "Google MAPS"},
                           {ImplementacionGeocodificador.BING_MAPS, "Bing MAPS"},
                           {ImplementacionGeocodificador.OPEN_STREET_NOMINATIM, "Open Street NOMINATIM"}
                       };
            cboGeocodificador.DataSource = new BindingSource(dict, null);
            cboGeocodificador.DisplayMember = "Value";
            cboGeocodificador.ValueMember = "Key";
            cboGeocodificador.Select(1, 1);
            geocodificador =
                GeocodingFactory.GetGeodificadorInstancia((ImplementacionGeocodificador)cboGeocodificador.SelectedValue);
            geocodificador.MensajeEvent += OnMensajeFromGeoCodificador;
            if (this.runInmediately)
            {
                string file = String.Format("{0}\\{1}", pathFile, nameFile);
                if (File.Exists(file))
                {
                    abrirArchivo();
                }
                else
                {
                    BeginInvoke(new DlgMensaje(escribeConsolaRojo),
                        new object[]
                        {
                            String.Format("No existe el archivo {0}", file)
                        });
                    if (closeAfterRun)
                        Application.Exit();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void ajustatamanio()
        {
            progressBar1.Width = Width - 30;
            tableLayoutPanel1.Width = Width - 25;
            tableLayoutPanel1.Height = Height - 80;
            groupBox1.Width = Width - 30;
            //groupBox1.Width = Width - 10;
            //btnGoogleMaps.Left = Height - 20;
            //btnGrabar.Left = Height - 20;
        }
        #endregion //FORM LOAD 

        #region MANEJADORES DE EVENTOS

        private void manejaCambiosObserver(object sender, DireccionPuntoSOBEventArgs arg)
        {
            if (arg.Type == DireccionPuntoSOBEventArgs.TypeChange.NEW)
            {
                foreach (DireccionPunto p in arg.PuntosChangeNew.Values)
                {
                    anadeElementoListaVista(p);
                }
            }

            if (arg.Type == DireccionPuntoSOBEventArgs.TypeChange.DELETE)
            {
                foreach (DireccionPunto p in arg.PuntosChangeNew.Values)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if ((String) item.Tag == p.key)
                        {
                            listView1.Items.Remove(item);
                        }
                    }
                }
            }

            if (arg.Type == DireccionPuntoSOBEventArgs.TypeChange.UPDATE)
            {
                foreach (DireccionPunto p in arg.PuntosChangeNew.Values)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if ((String) item.Tag == p.key)
                        {
                            item.Text = p.Direccion;
                            item.SubItems[1].Text = (p.Latitud.ToString(CultureInfo.InvariantCulture));
                            item.SubItems[2].Text = (p.Longitud.ToString(CultureInfo.InvariantCulture));
                            item.SubItems[3].Text = (p.X.ToString(CultureInfo.InvariantCulture));
                            item.SubItems[4].Text = (p.Y.ToString(CultureInfo.InvariantCulture));
                            item.SubItems[5].Text = (p.Municipio);
                            item.SubItems[6].Text = (p.NivelRegional2);
                            item.SubItems[7].Text = (p.NivelRegional1);
                            item.SubItems[8].Text = (p.Pais);
                            item.SubItems[9].Text = (p.GeoreferenciadorDeno);
                            item.BackColor = !p.IsGeocodificado ? Color.Yellow : Color.White;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pathFile = Path.GetDirectoryName(openFileDialog1.FileName);
                nameFile = Path.GetFileName(openFileDialog1.FileName);
                abrirArchivo();
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCarpetaSalida.Text))
                {
                    if (!Directory.Exists(txtCarpetaSalida.Text))
                    {
                        MessageBox.Show(Resources.El_directorio_no_existe, _serror, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(
                        Resources.FrmBuscaDireccion_btnGoogleMaps_Click_Tiene_que_seleccionar_una_carpeta_de_salida,
                        _serror, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
                if (items.Count > 0)
                {
                    generaSHPSalida();
                }
                else
                {
                    MessageBox.Show(Resources.No_Existen_Registros, _serror, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                MessageBox.Show(Resources.sFicheros_correctamente);
            }
            catch (Exception ex)
            {
                BeginInvoke(new DlgMensaje(escribeConsolaRojo), new object[] {ex.Message});
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelDirectorio_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            txtCarpetaSalida.Text = folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aCercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frm;
            frm = new FrmAbout();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configuracionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmConfiguracion frm;
            frm = new FrmConfiguracion();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(_susrlDocumentacion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        /// <summary>
        ///Abre el menú de covariables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void covariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreCovariables();
        }

        /// <summary>
        /// Comprobamos si tenemos definidas covariables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCovariables.Checked)
            {
                if (covariables.Count == 0)
                {
                    if (MessageBox.Show( _snoTieneDefCovariable,_saviso,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        checkCovariables.Checked = false;
                    }
                    else
                    {
                        abreCovariables();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    covariables.ForEach(
                        (cov) => sb.AppendLine(String.Format("{0} {1}", cov.Nombre, cov.Tipo.ToString())));
                    String mensaje = String.Format(_s1TieneDefinidas, sb);
                    if (MessageBox.Show(mensaje,
                        _sCovariables,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.Cancel)
                        checkCovariables.Checked = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGeocodificador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeof(ImplementacionGeocodificador) == cboGeocodificador.SelectedValue.GetType())
            {
                geocodificador =
                    GeocodingFactory.GetGeodificadorInstancia(
                        (ImplementacionGeocodificador)cboGeocodificador.SelectedValue);
                geocodificador.MensajeEvent += OnMensajeFromGeoCodificador;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMensajeFromGeoCodificador(object sender, EventArgsGeocodificador e)
        {
            BeginInvoke(new DlgMensaje(escribeConsolaRojo), new object[] { e.Mensaje });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBuscaDireccion_Resize(object sender, EventArgs e)
        {
            ajustatamanio();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var frmModifica = new FrmModificaElemento(items, (String)listView1.SelectedItems[0].Tag, geocodificador);
                frmModifica.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnade_Click(object sender, EventArgs e)
        {
            var frmAnade = new FrmAnadeElemento(items, geocodificador);
            frmAnade.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    DireccionPunto dp = items[(string)listView1.SelectedItems[0].Tag];
                    if (
                        MessageBox.Show(
                            String.Format("Atencion:\n Va a eliminar : {0}, {1} {2} {3} {4}\n Lat : {5} \n Lon : {6}",
                                dp.Direccion, dp.Municipio, dp.NivelRegional2, dp.NivelRegional1, dp.Pais,
                                Convert.ToString(dp.Latitud), Convert.ToString(dp.Longitud)),
                            Resources.msg_Confirmación_de_Borrado, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) ==
                        DialogResult.Yes)
                    {
                        items.Remove(dp.key);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoogleMaps_Click(object sender, EventArgs e)
        {
            generaGoogleMaps();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegeo_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(String.Format("{0}\\{1}", pathFile, nameFile)))
            {
                abrirArchivo();
            }
            else
            {
                MessageBox.Show(_sNoHayUnArchivoAbierto, _saviso,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region LOGICA PRINCIPAL

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dp"></param>
        private void anadeElementoListaVista(DireccionPunto dp)
        {
            if (items.Count == 1)
                listView1.Items.Clear();

            var it = new ListViewItem {Name = dp.Direccion, Text = dp.Direccion, Tag = dp.key};
            it.SubItems.Add(dp.Latitud.ToString(CultureInfo.InvariantCulture));
            it.SubItems.Add(dp.Longitud.ToString(CultureInfo.InvariantCulture));
            it.SubItems.Add(dp.X.ToString(CultureInfo.InvariantCulture));
            it.SubItems.Add(dp.Y.ToString(CultureInfo.InvariantCulture));
            it.SubItems.Add(dp.Municipio);
            it.SubItems.Add(dp.NivelRegional2);
            it.SubItems.Add(dp.NivelRegional1);
            it.SubItems.Add(dp.Pais);
            it.SubItems.Add(dp.GeoreferenciadorDeno);

            it.BackColor = !dp.IsGeocodificado ? Color.Yellow : Color.White;

            listView1.Items.Add(it);
        }

        /// <summary>
        /// abre el archivo de texto que se va a georreferenciar
        /// </summary>
        private void abrirArchivo()
        {
            try
            {
                txtCarpetaSalida.Text = pathFile;
                string archivo = String.Format("{0}\\{1}", pathFile, nameFile);
                items.Clear();
                idRegistro = 0;
                listadoRegistro = new List<RegistroModel>();
                var sr = new StreamReader(archivo, true);
                int numLinea = 0;
                int numCovariables = covariables.Count;
                //Modificaciones para SpainRDR
                while (!sr.EndOfStream)
                {
                    numLinea++;
                    var line = sr.ReadLine();
                    if (line == null) continue;
                    if (checkCovariables.Checked)
                    {
                        int lineCovariables = 0;
                        string[] campos = line.Split(separador);
                        lineCovariables = campos.Length - 1;
                        RegistroModel registro = new RegistroModel();
                        registro.Direccion = campos[0];
                        registro.Covariables = new object[numCovariables];
                        if (numCovariables == lineCovariables)
                        {
                            int i = 1;
                            bool okParseCo = true;
                            foreach (var covariable in covariables)
                            {
                                string errorParse = "";
                                try
                                {
                                    switch (covariable.Tipo)
                                    {
                                        case TiposDatos.CADENA:
                                            registro.Covariables[i - 1] = campos[i];
                                            break;
                                        case TiposDatos.DECIMAL:
                                            errorParse = "se esperaba un decimal";
                                            registro.Covariables[i - 1] = Double.Parse(campos[i],
                                                CultureInfo.InvariantCulture);
                                            break;
                                        case TiposDatos.ENTERO:
                                            errorParse = "se esperaba un entero";
                                            registro.Covariables[i - 1] = Int32.Parse(campos[i]);
                                            break;
                                        case TiposDatos.FECHA:
                                            errorParse = "se esperaba una fecha";
                                            registro.Covariables[i - 1] = DateTime.Parse(campos[i],
                                                CultureInfo.CurrentCulture);
                                            break;
                                    }
                                    i++;
                                }
                                catch (Exception ex)
                                {
                                    BeginInvoke(new DlgMensaje(escribeConsolaRojo),
                                        new object[]
                                        {
                                            String.Format("Error leyendo la línea {0}  {1} en la variable nº{2} : {3}",
                                                numLinea, errorParse, i, line)
                                        });
                                    okParseCo = false;
                                }
                            }
                            if (!okParseCo) continue;
                            registro.Id = idRegistro.ToString(CultureInfo.InvariantCulture);
                            listadoRegistro.Add(registro);
                            idRegistro++;
                        }
                        else
                        {
                            BeginInvoke(new DlgMensaje(escribeConsolaRojo),
                                new object[]
                                {
                                    String.Format(
                                        "Error leyendo la línea {0} se esperaban {1} variable en lugar {2}: {3}",
                                        numLinea,
                                        numCovariables, lineCovariables, line)
                                });
                        }
                    }
                    else
                    {
                        //Caso de no estar marcadas las covariables
                        //Ya está la dirección leída
                        string[] campos = line.Split(separador);
                        RegistroModel registro = new RegistroModel();
                        registro.Direccion = campos[0];
                        registro.Id = idRegistro.ToString();
                        listadoRegistro.Add(registro);
                        idRegistro++;
                    }
                }
                IGeocodificador geodificadorInstancia =
                    GeocodingFactory.GetGeodificadorInstancia(
                        (GeoToMap.ImplementacionGeocodificador) cboGeocodificador.SelectedValue);
                var dlgProcesoGeoRreferenciacion = new DlgVoid(
                    () => ProcesoLoteGeoreferenciacion
                        (
                            geodificadorInstancia,
                            listadoRegistro
                        )
                    );
                dlgProcesoGeoRreferenciacion.BeginInvoke(finProcesoLoteGeoreferenciacion, null);
            }
            catch (Exception ex)
            {
                BeginInvoke(new DlgMensaje(escribeConsolaRojo),
                    new object[]
                    {
                        ex.Message
                    });
            }
        }

        /// <summary>
        /// Aunque List<RegistroModel> es una variable global, en el próximo incremento de geoToMap, el 
        /// proceso de georreferenciación se desagregará.
        /// </summary>
        /// <param name="codificador"></param>
        /// <param name="direcciones"></param>
        private void ProcesoLoteGeoreferenciacion(IGeocodificador codificador, List<RegistroModel> direcciones)
        {
            try
            {
                BeginInvoke(new DlgMensaje(escribeConsolaNegro),
                    new object[]
                    {
                        String.Format("Se han leído {0} direcciones",
                            direcciones.Count.ToString(CultureInfo.InvariantCulture))
                    });
                int i = 0;
                items.Clear();
                BeginInvoke(new DlgVoid(() => listView1.Items.Clear()));
                foreach (RegistroModel registro in direcciones)
                {
                    if (!String.IsNullOrEmpty(registro.Direccion))
                    {
                        int i1 = i;
                        BeginInvoke(
                            new DlgVoid(
                                () => progressBar1.Value = (int) (((double) (i1 + 1)/(double) direcciones.Count)*100)));
                        i++;
                        DireccionPunto dp = codificador.GetCoordenadas(registro.Direccion);
                        if (dp != null)
                        {
                            dp.key = registro.Id;
                            Invoke(new DlgVoid(() => items.Add(dp.key, dp)));
                            //TO DO Posible efecto secundario EVITAR!!!
                            registro.Id = dp.key;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iar"></param>
        private void finProcesoLoteGeoreferenciacion(IAsyncResult iar)
        {
            BeginInvoke(new DlgMensaje(escribeConsolaNegro),
                new object[] {_smensajeFinProcesoTraduccion});
            if (this.closeAfterRun)
            {
                this.BeginInvoke(new DlgVoid(() => btnGrabar.Enabled = true));
                generaSHPSalida();
            }
        }

        /// <summary>
        /// Escribe el la consola (Salida estandar)
        /// </summary>
        /// <param name="s"></param>
        private void escribeConsolaNegro(string s)
        {
            richTextBox1.AppendText(string.Format("{0}\n", s));
            Console.Out.Write("{0}\n", s);
        }

        /// <summary>
        /// Escribe en rojo en la consola salida de error
        /// </summary>
        /// <param name="s"></param>
        private void escribeConsolaRojo(string s)
        {
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText(string.Format("{0}\n", s));
            richTextBox1.SelectionLength = s.Length;
            Console.Error.Write("{0}\n", s);
        }

        /// <summary>
        /// Genera la salida en SHP
        /// </summary>
        private void generaSHPSalida()
        {
            try
            {
                final = 0;
                if (items.Count > 0)
                {
                    if (String.IsNullOrEmpty(pathFile))
                    {
                        pathFile = txtCarpetaSalida.Text;
                    }

                    if (String.IsNullOrEmpty(nameFile))
                    {
                        var frmNombre = new FrmNombre();
                        frmNombre.ShowDialog();
                        nameFile = frmNombre.Texto;
                    }

                    string archivoSalidaDatos = "";
                    String nombreFichero =
                        Path.GetFileNameWithoutExtension(String.Format("{0}\\{1}", pathFile, nameFile));
                    archivoSalidaDatos = String.Format("{0}\\{1}_salida.dat", pathFile, nombreFichero);

                    var fs = new FileStream(archivoSalidaDatos, FileMode.Create);
                    var sw = new StreamWriter(fs);

                    if (checkCovariables.Checked)
                    {
                        sw.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}", "Toponimo", separador, "Latitud", separador, "Longitud",
                            separador, "X", separador, "Y");
                        foreach (var cov in covariables)
                        {
                            sw.Write("{0}{1}", separador, cov.Nombre);
                        }
                        sw.Write("\n");
                    }
                    else
                    {
                        sw.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}", "Toponimo", separador, "Latitud", separador,
                            "Longitud", separador, "X", separador, "Y");
                    }

                    DbfFieldDesc[] camposdb = null;
                    int numDatos = 2;
                    ShapeFileWriter shapefilewrite = null;
                    if (checkCovariables.Checked)
                    {
                        int numCovariables = covariables.Count;
                        numDatos = numCovariables + 2;
                        camposdb = new DbfFieldDesc[numCovariables + 2];
                        camposdb[0].FieldType = DbfFieldType.Character;
                        camposdb[0].FieldLength = 50;
                        camposdb[0].FieldName = "ObjectId";
                        camposdb[1].FieldType = DbfFieldType.Character;
                        camposdb[1].FieldLength = 255;
                        camposdb[1].FieldName = "Toponimo";
                        for (int i = 0; i < numCovariables; i++)
                        {
                            camposdb[i + 2].FieldName = covariables[i].Nombre;
                            switch (covariables[i].Tipo)
                            {
                                case TiposDatos.CADENA:
                                    camposdb[i + 2].FieldType = DbfFieldType.Character;
                                    camposdb[i + 2].FieldLength = 255;
                                    break;
                                case TiposDatos.DECIMAL:
                                    camposdb[i + 2].FieldType = DbfFieldType.FloatingPoint;
                                    camposdb[i + 2].FieldLength = 19;
                                    camposdb[i + 2].DecimalCount = 8;
                                    break;
                                case TiposDatos.ENTERO:
                                    camposdb[i + 2].FieldType = DbfFieldType.Number;
                                    camposdb[i + 2].FieldLength = 15;
                                    break;
                                case TiposDatos.FECHA:
                                    camposdb[i + 2].FieldType = DbfFieldType.Date;
                                    camposdb[i + 2].FieldLength = 8;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        camposdb = new DbfFieldDesc[2];
                        camposdb[0].FieldType = DbfFieldType.Character;
                        camposdb[0].FieldLength = 50;
                        camposdb[0].FieldName = "ObjectId";
                        camposdb[1].FieldType = DbfFieldType.Character;
                        camposdb[1].FieldLength = 255;
                        camposdb[1].FieldName = "Toponimo";
                    }
                    shapefilewrite =
                            ShapeFileWriter.CreateWriter(pathFile, nombreFichero,
                                ShapeType.Point, camposdb);
                    foreach (var kv in items)
                    {
                        try
                        {
                            RegistroModel rm =
                                listadoRegistro.FirstOrDefault(obj => String.Equals(obj.Id, kv.Value.key,
                                    StringComparison.InvariantCultureIgnoreCase));
                            string[] datos = new string[numDatos];
                            datos[0] = kv.Value.key;
                            datos[1] = kv.Value.Direccion;
                            sw.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", kv.Value.Direccion, separador,
                                kv.Value.Latitud.ToString("##########.########", CultureInfo.InvariantCulture),
                                separador,
                                kv.Value.Longitud.ToString("##########.########", CultureInfo.InvariantCulture),
                                separador,
                                kv.Value.X.ToString("##########.##", CultureInfo.InvariantCulture),
                                separador,
                                kv.Value.Y.ToString("##########.##", CultureInfo.InvariantCulture),
                                separador);
                            if (checkCovariables.Checked)
                            {
                                for (int i = 0; i < covariables.Count; i++)
                                {
                                    if (rm == null) continue;
                                    if (rm.Covariables[i] is DateTime)
                                    {
                                        var fecha = (DateTime) rm.Covariables[i];
                                        datos[i + 2] = fecha.ToString("yyyyMMdd");
                                        if (i == covariables.Count - 1)
                                            sw.Write("{0}", fecha.ToShortDateString());
                                        else
                                            sw.Write("{0}{1}", fecha.ToShortDateString(), separador);
                                    }
                                    else if (rm.Covariables[i] is Double)
                                    {
                                        var doble = (Double) rm.Covariables[i];
                                        datos[i + 2] = doble.ToString("##########.########",
                                            CultureInfo.InvariantCulture);
                                        if (i == covariables.Count - 1)
                                            sw.Write("{0}",
                                                doble.ToString("##########.########", 
                                                CultureInfo.InvariantCulture));
                                        else
                                            sw.Write("{0}{1}",
                                                doble.ToString("##########.########", 
                                                CultureInfo.InvariantCulture),
                                                separador);
                                    }
                                    else
                                    {
                                        datos[i + 2] = rm.Covariables[i].ToString();
                                    }
                                    
                                }
                            }
                            sw.Write("\n");
                            if (!double.IsNaN(kv.Value.X) && !double.IsNaN(kv.Value.Y))
                            {
                                if (shapefilewrite != null)
                                    shapefilewrite.AddRecord(
                                        new[] {kv.Value.X, kv.Value.Y},
                                        1,
                                        datos);
                            }
                        }
                        catch (Exception ex)
                        {
                            final = 1;
                        }
                    }//Fin foreach de items georreferenciados

                    if (shapefilewrite != null) shapefilewrite.Close();
                    sw.Flush();
                    sw.Close();

                    StreamWriter spjr = new StreamWriter(String.Format("{0}\\{1}.prj", pathFile, nombreFichero));
                    spjr.Write(Settings.Default.Proyeccion);
                    spjr.Close();

                    StreamWriter scfg = new StreamWriter(String.Format("{0}\\{1}.cfg", pathFile, nombreFichero));
                    scfg.Write("UTF-8");
                    scfg.Close();
                }
                else
                {
                    MessageBox.Show(Resources.No_Existen_Registros, Resources.Error, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                final = -1;
                BeginInvoke(new DlgMensaje(escribeConsolaRojo), new object[] {ex.Message});
            }
        }

        /// <summary>
        /// Genera el mapa de eventos sobre google maps para ver en un navegador web
        /// </summary>
        private void generaGoogleMaps()
        {
            try
            {
                if (items.Count == 0)
                {
                    MessageBox.Show(Resources.No_Existen_Registros, Resources.Error, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }

                if (!String.IsNullOrEmpty(txtCarpetaSalida.Text))
                {
                    if ((Directory.Exists(txtCarpetaSalida.Text)))
                    {
                        var htmlsb = new StringBuilder();

                        LatLon centroLatLong = LatLongCentro();

                        htmlsb.Append(
                            "<!DOCTYPE html>\n<html>\n<head>\n<meta name=\"viewport\" content=\"initial-scale=1.0, user-scalable=no\">\n<meta charset=\"utf-8\">\n<title>Simple markers</title>\n<link href=\"/maps/documentation/javascript/examples/default.css\" rel=\"stylesheet\">\n<script src=\"https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false\">\n</script>\n");

                        htmlsb.AppendLine(
                            "<style>\n\t.todo {    border: 2px solid silver;    bottom: 0;    left: 0;    overflow: auto;    position: absolute;    right: 0;    top: 0;}</style>");

                        htmlsb.AppendLine("<script>\nfunction initialize() {");
                        htmlsb.AppendFormat("var myLatlng = new google.maps.LatLng({0},{1});\n",
                            centroLatLong.Lat.ToString(CultureInfo.InvariantCulture),
                            centroLatLong.Lon.ToString(CultureInfo.InvariantCulture));
                        htmlsb.AppendLine(
                            "var mapOptions = { zoom:8, center:myLatlng, mapTypeId: google.maps.MapTypeId.ROADMAP }");
                        htmlsb.AppendLine(
                            "var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);");

                        int i = 0;

                        foreach (var dp in items)
                        {
                            i++;
                            htmlsb.AppendFormat("var marca{0} = new google.maps.LatLng({1},{2});\n",
                                i.ToString(CultureInfo.InvariantCulture),
                                dp.Value.Latitud.ToString(CultureInfo.InvariantCulture),
                                dp.Value.Longitud.ToString(CultureInfo.InvariantCulture));
                            htmlsb.AppendLine("var marker = new google.maps.Marker({      position: marca" +
                                              i.ToString(CultureInfo.InvariantCulture) +
                                              ",      map: map,      title: '" + dp.Value.Direccion + "'  });\n");
                        }

                        htmlsb.AppendLine("}\ngoogle.maps.event.addDomListener(window, 'load', initialize);");
                        htmlsb.AppendLine("</script>");
                        htmlsb.AppendLine(
                            "</head>\n\t\t<body>\n\t\t\t<div id=\"map-canvas\" class=\"todo\"></div>\n\t\t</body>\n</html>");

                        //richTextBox1.AppendText(htmlsb.ToString());

                        var file =
                            new StreamWriter(txtCarpetaSalida.Text + "\\" + "googlemaps.html");
                        file.Write(htmlsb.ToString());
                        file.Close();

                        Process.Start(txtCarpetaSalida.Text + "\\" + "googlemaps.html");
                    }
                    else
                    {
                        MessageBox.Show(Resources.El_directorio_no_existe, Resources.Error, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(
                        Resources.FrmBuscaDireccion_btnGoogleMaps_Click_Tiene_que_seleccionar_una_carpeta_de_salida,
                        Resources.Error, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LatLon LatLongCentro()
        {
            double maxLat = -1000;
            double minLat = 1000;
            double maxLon = -1000;
            double minLon = 1000;
            foreach (var dp in items)
            {
                if (!((dp.Value.Latitud < 0.0 + 0.0001) && (dp.Value.Longitud < 0.0 + 0.0001)))
                {
                    if (dp.Value.Latitud < minLat)
                    {
                        minLat = dp.Value.Latitud;
                    }

                    if (dp.Value.Latitud > maxLat)
                    {
                        maxLat = dp.Value.Latitud;
                    }

                    if (dp.Value.Longitud < minLon)
                    {
                        minLon = dp.Value.Longitud;
                    }

                    if (dp.Value.Longitud > maxLon)
                    {
                        maxLon = dp.Value.Longitud;
                    }
                }
            }
            var minLatLon = new LatLon {Lat = minLat, Lon = minLon};
            var maxLatLon = new LatLon {Lat = maxLat, Lon = maxLon};
            return new LatLon {Lat = (maxLatLon.Lat + minLatLon.Lat)/2, Lon = (maxLatLon.Lon + minLatLon.Lon)/2};
        }

        /// <summary>
        /// Abre el menú de covariables
        /// </summary>
        private void abreCovariables()
        {
            FrmCovariables frm = new FrmCovariables(covariables);
            frm.ShowDialog();
            List<int> indexNulList =
                covariables.Where(obj => String.IsNullOrEmpty(obj.Nombre))
                    .Select(obj => covariables.IndexOf(obj))
                    .ToList();
            indexNulList.ForEach(i => covariables.RemoveAt(i));
        }

        #endregion
    }
}
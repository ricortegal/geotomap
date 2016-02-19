using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GeoToMap
{
    public partial class FrmCovariables : Form
    {
        private readonly List<TipoCovariable> tipos = new List<TipoCovariable>();
        private List<Covariable> covariables;
        private readonly BindingList<Covariable> bindingCovariables;

        public FrmCovariables(List<Covariable> covariables)
        {
            this.covariables = covariables;
            bindingCovariables = new BindingList<Covariable>(covariables);
            tipos.Add(new TipoCovariable() {Deno = "CADENA", Tipo = TiposDatos.CADENA});
            tipos.Add(new TipoCovariable() {Deno = "ENTERO", Tipo = TiposDatos.ENTERO});
            tipos.Add(new TipoCovariable() {Deno = "DECIMAL", Tipo = TiposDatos.DECIMAL});
            tipos.Add(new TipoCovariable() {Deno = "FECHA", Tipo = TiposDatos.FECHA});
            InitializeComponent();
        }

        private void FrmCoveriables_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn columnaTextoNombreCovariable =
                new DataGridViewTextBoxColumn();
            DataGridViewComboBoxColumn columnaComboTipo = new DataGridViewComboBoxColumn();
            columnaTextoNombreCovariable.Name = "Nombre";
            columnaComboTipo.Name = "Tipo";
            columnaComboTipo.DataSource = tipos;
            columnaComboTipo.DisplayMember = "Deno";
            columnaComboTipo.ValueMember = "Tipo";
            columnaComboTipo.DefaultCellStyle.NullValue = tipos[0].Deno;
            columnaComboTipo.DefaultCellStyle.DataSourceNullValue = tipos[0].Tipo;

            columnaTextoNombreCovariable.DataPropertyName = "Nombre";
            columnaComboTipo.DataPropertyName = "Tipo";

            this.dataGridView1.Columns.Add(columnaTextoNombreCovariable);
            this.dataGridView1.Columns.Add(columnaComboTipo);

            this.dataGridView1.DataSource = bindingCovariables;

            this.FormClosing += OnFormClosing;
            this.dataGridView1.DataError += DataGridView1OnDataError;
            this.dataGridView1.RowValidating += DataGridView1OnRowValidating;
            this.dataGridView1.RowsAdded += DataGridView1OnRowsAdded;
            this.dataGridView1.DefaultValuesNeeded += DataGridView1OnDefaultValuesNeeded;
        }

        private void DataGridView1OnDefaultValuesNeeded(object sender, DataGridViewRowEventArgs dataGridViewRowEventArgs)
        {
            dataGridViewRowEventArgs.Row.Cells[1].Value = TiposDatos.CADENA;
        }


        private void DataGridView1OnRowsAdded(object sender,
            DataGridViewRowsAddedEventArgs dataGridViewRowsAddedEventArgs)
        {
        }

        private void DataGridView1OnRowValidating(object sender,
            DataGridViewCellCancelEventArgs dataGridViewCellCancelEventArgs)
        {
            //var cell = dataGridView1.Rows[dataGridViewCellCancelEventArgs.RowIndex].Cells[0];
            //DataGridViewRow dgr = dataGridView1.Rows[dataGridViewCellCancelEventArgs.RowIndex];
            //Boolean nr = dgr.IsNewRow;
            //if (!nr)
            //{
            //    if (String.IsNullOrEmpty((string) cell.Value))
            //    {
            //        MessageBox.Show("El nombre de la variable no puede estar vacio", "Error", MessageBoxButtons.OK,
            //            MessageBoxIcon.Error);
            //        dataGridViewCellCancelEventArgs.Cancel = true;
            //    }
            //}
        }

        private void DataGridView1OnDataError(object sender,
            DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs)
        {
        }

        private void OnFormClosing(object sender, FormClosingEventArgs formClosingEventArgs)
        {
            covariables = new List<Covariable>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //covariables.Add(item: new Covariable() { Nombre = (String)row.Cells[0].Value, Tipo = (TiposDatos)row.Cells[1].Value });
            }
        }

        private void DataGridView1OnRowsRemoved(object sender,
            DataGridViewRowsRemovedEventArgs dataGridViewRowsRemovedEventArgs)
        {
            covariables = new List<Covariable>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                covariables.Add(item:
                    new Covariable() {Nombre = (String) row.Cells[0].Value, Tipo = (TiposDatos) row.Cells[1].Value});
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                String archivo = saveFileDialog1.FileName;
                XmlSerializer serializer = new XmlSerializer(typeof (List<Covariable>));
                TextWriter tw = new StreamWriter(archivo);
                serializer.Serialize(tw, covariables);
                tw.Flush();
                tw.Close();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(String.Format("Error: {0}", ex.Message));
                Console.Error.Write(ex.StackTrace);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// abrimos un archivo con las covariables serializadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (covariables.Count > 0)
            {
                if (MessageBox.Show("Va a borrar todas las covariables definidas",
                    "Atención",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    abreArchivo(openFileDialog1.FileName);
                }
            }
            else
            {
                abreArchivo(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// leamos el archivo, borramos las covariables y añadimos las del archivo serializado
        /// </summary>
        /// <param name="archivo">archivo con covariables serializadas</param>
        private void abreArchivo(String archivo)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof (List<Covariable>));
                TextReader tr = new StreamReader(archivo);
                List<Covariable> covariablesTemp = (List<Covariable>) serializer.Deserialize(tr);
                tr.Close();
                covariables.Clear();
                dataGridView1.Rows.Clear();
                covariablesTemp.ForEach(co => bindingCovariables.Add(co));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(String.Format("Error: {0}", ex.Message));
                Console.Error.Write(ex.StackTrace);
            }
        }
    }
}

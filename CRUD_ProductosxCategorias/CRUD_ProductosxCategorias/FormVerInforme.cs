using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CRUD_ProductosxCategorias
{
    public partial class FormVerInforme : Form
    {
        private string pdfPath;

        public FormVerInforme(string ruta)
        {
            InitializeComponent();
            pdfPath = ruta;
        }

        private void FormVerInforme_Load(object sender, EventArgs e)
        {
            if (File.Exists(pdfPath))
            {
                // Inicializar WebView2, tuve que usar este pues el 
                // Webrowser daba problemas
                webViewInforme.Source = new Uri(pdfPath);
            }
            else
            {
                MessageBox.Show("El PDF no se encontró.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void webViewInforme_Click(object sender, EventArgs e)
        {

        }
    }
}

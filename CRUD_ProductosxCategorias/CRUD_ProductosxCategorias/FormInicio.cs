using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CRUD_ProductosxCategorias.Conexion;
using CRUD_ProductosxCategorias.DAO;
using CRUD_ProductosxCategorias.Modelo;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CRUD_ProductosxCategorias
{
    public partial class panelPrincipal : Form
    {
        public panelPrincipal()
        {
            InitializeComponent();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormCategorias>();
        }

        private void btnProductos_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<FormProductos>();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        private void GenerarPDF()
        {
            try
            {
                // Obtener la ruta del directorio de la solución
                string solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                String pdfPath = Path.Combine(solutionDirectory, "Reporte_Productos.pdf");

                Document doc = new Document(PageSize.A4);

                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    // Estilos de fuente
                    iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    iTextSharp.text.Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                    // Título
                    doc.Add(new Paragraph("Reporte de Productos", titleFont) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("\n"));

                    // Crear tabla con 6 columnas
                    PdfPTable table = new PdfPTable(6) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 10, 25, 30, 15, 25, 25 });

                    // Encabezados de tabla
                    string[] headers = { "ID", "Nombre", "Descripción", "Precio", "Fecha Alta", "Categoría" };
                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(cell);
                    }

                    // Obtener datos desde la BD usando consultarProductos()
                    ProductoDAO productoDAO = new ProductoDAO();
                    List<Producto> productos = productoDAO.consultarProductos("");

                    foreach (Producto producto in productos)
                    {
                        table.AddCell(new PdfPCell(new Phrase(producto.IdProducto.ToString(), cellFont)));
                        table.AddCell(new PdfPCell(new Phrase(producto.Nombre, cellFont)));
                        table.AddCell(new PdfPCell(new Phrase(producto.Descripcion, cellFont)));
                        table.AddCell(new PdfPCell(new Phrase(producto.Precio.ToString("C2"), cellFont)));
                        table.AddCell(new PdfPCell(new Phrase(producto.FechaAlta.ToShortDateString(), cellFont)));
                        table.AddCell(new PdfPCell(new Phrase(producto.Categoria.Nombre, cellFont)));
                    }

                    // Agregar la tabla al documento
                    doc.Add(table);
                    doc.Close();
                }

                MessageBox.Show($"PDF generado correctamente en: {pdfPath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnVerInforme_Click(object sender, EventArgs e)
        {
            string solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            String pdfPath = Path.Combine(solutionDirectory, "Reporte_Productos.pdf");

            FormVerInforme formVerInforme = new FormVerInforme(pdfPath);
            formVerInforme.ShowDialog();
        }
    }
}

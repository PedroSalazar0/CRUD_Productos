using CRUD_ProductosxCategorias.DAO;
using CRUD_ProductosxCategorias.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_ProductosxCategorias
{
    public partial class FormProductos : Form
    {
        private ProductoDAO pDAO = new ProductoDAO();
        public List<Producto> listaProductos = new List<Producto>();
        int idProducto = 0;

        public FormProductos()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            FormDetProducto DetPro = new FormDetProducto();
            DetPro.idProducto = 0;
            DetPro.ShowDialog();
            //ACTUALIZO DATAGRIDVIEW
            CargarListaProductos();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
            idProducto = Convert.ToInt32(fila.Cells["id"].Value);

            if (dgvProductos.Columns[e.ColumnIndex].Name == "modificar")
            {
                //llamar a la ventana FrmDetCategoria
                FormDetProducto Det = new FormDetProducto();
                Det.idProducto = idProducto;
                Det.ShowDialog();
            }
            if (dgvProductos.Columns[e.ColumnIndex].Name == "eliminar")
            {
                if (idProducto == -1) return;
                if (MessageBox.Show("¿Desea eliminar el producto?", "Eliminar producto", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (pDAO.eliminarProducto(idProducto) != 0)
                    {
                        MessageBox.Show("Producto eliminado con éxito.");

                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }
            }
            //ACTUALIZO DATAGRIDVIEW
            CargarListaProductos();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            CargarListaProductos("");
        }

        private void CargarListaProductos(string filtro = "")
        {
            //1-VACIO DataGridView
            dgvProductos.Rows.Clear();
            dgvProductos.Refresh();

            //Cargo la lista con la select
            listaProductos = pDAO.consultarProductos(filtro);

            for (int i = 0; i < listaProductos.Count(); i++)
            {
                dgvProductos.Rows.Add(
                    listaProductos[i].IdProducto,
                    listaProductos[i].Nombre,
                    listaProductos[i].Descripcion,
                    listaProductos[i].Precio,
                    listaProductos[i].FechaAlta,
                    listaProductos[i].Categoria.Nombre);
            }

        }
    }
}

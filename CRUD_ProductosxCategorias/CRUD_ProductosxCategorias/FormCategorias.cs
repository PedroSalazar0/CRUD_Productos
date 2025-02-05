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
    public partial class FormCategorias : Form
    {
        // Atributos del apartado de CATEGORIAS
        private CategoriaDAO catDAO = new CategoriaDAO();
        public List<Categoria> listaCategorias = new List<Categoria>();
        private int idCategoria;

        public FormCategorias()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            FormDetCategoria DetCategoria = new FormDetCategoria();
            DetCategoria.id = 0;
            DetCategoria.ShowDialog();
            CargarListaCategorias();
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            DataGridViewRow fila = dgvCategorias.Rows[e.RowIndex];
            idCategoria = Convert.ToInt32(fila.Cells["id"].Value);

            if (dgvCategorias.Columns[e.ColumnIndex].Name == "modificar")
            {
                //llamar a la ventana FrmDetCategoria
                FormDetCategoria Det = new FormDetCategoria();
                Det.id = idCategoria;
                Det.ShowDialog();
            }
            if (dgvCategorias.Columns[e.ColumnIndex].Name == "eliminar")
            {
                if (idCategoria == -1) return;
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Eliminar Categoría", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (catDAO.eliminarCategoria(idCategoria) != 0)
                    {
                        MessageBox.Show("Categoria eliminada con éxito.");                   
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la categoria.");
                    }
                }
            }
            CargarListaCategorias();
        }

        private void FormCategorias_Load(object sender, EventArgs e)
        {
            CargarListaCategorias();
        }

        private void CargarListaCategorias()
        {
            //1-VACIO DataGridView
            dgvCategorias.Rows.Clear();
            dgvCategorias.Refresh();

            //Cargo la lista con la select
            listaCategorias = catDAO.consultarCategorias("");

            for (int i = 0; i < listaCategorias.Count(); i++)
            {
                dgvCategorias.Rows.Add(
                    listaCategorias[i].IdCategoria,
                    listaCategorias[i].Nombre,
                    listaCategorias[i].Descripcion,
                    listaCategorias[i].Activo);
            }

        }
    }
}

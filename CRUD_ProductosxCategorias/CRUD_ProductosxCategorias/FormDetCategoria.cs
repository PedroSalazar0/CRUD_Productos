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
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_ProductosxCategorias
{
    public partial class FormDetCategoria : Form
    {
        // 
        private CategoriaDAO catDAO = new CategoriaDAO();
        private Categoria cat;
        public int id;

        public FormDetCategoria()
        {
            InitializeComponent();
        }

        private void FormDetCategoria_Load(object sender, EventArgs e)
        {
            CenterToScreen();

            if (id > 0)
            {
                //CARGAR LA CATEGORIA
                cat = catDAO.leerCategoriaPorId(id);
                //Cargar datos en los controles
                txtId.Text = id.ToString();
                txtNombre.Text = cat.Nombre.ToString();
                txtDescripcion.Text = cat.Descripcion.ToString();
                txtActivo.Text = cat.Activo? "Si":"No";
            }
            else
            {
                txtId.Clear();
                txtNombre.Clear();
                txtDescripcion.Clear();
            }
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            cat = new Categoria();

            cat.Nombre = txtNombre.Text.Trim();
            cat.Descripcion = txtDescripcion.Text.Trim();
            if (txtActivo.Text.Equals("Si"))
            {
                cat.Activo = true;
            } else if (txtActivo.Text.Equals("No"))
            {
                cat.Activo = false;
            } else
            {
                MessageBox.Show("Error, introduca 'Si' o 'No' en activo");
                return;
            }

            if (id > 0)
            {
                //modificacion
                cat.IdCategoria = getIdIfExist();
                if (catDAO.modificarCategoria(cat) != 0)
                {
                    MessageBox.Show("Categoria modificada correctamente");
                }
                else
                {
                    MessageBox.Show("Error al modificar la categoría");
                }
            }
            else
            {
                //alta
                if (catDAO.agregarCategoria(cat) != 0)
                {
                    MessageBox.Show("Categoria alta correcta");
                }
                else
                {
                    MessageBox.Show("Error en el alta de la categoría");
                }
            }

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int getIdIfExist()
        {
            if (!txtId.Text.Trim().Equals(""))
            {
                if (int.TryParse(txtId.Text.Trim(), out int id))
                    return id;
                else
                    return -1;
            }
            else
                return -1;
        }
    }
}

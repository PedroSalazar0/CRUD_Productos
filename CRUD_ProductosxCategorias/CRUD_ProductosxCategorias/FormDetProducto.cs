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
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_ProductosxCategorias
{
    public partial class FormDetProducto : Form
    {
        private CategoriaDAO catDAO = new CategoriaDAO(); 
        // Para buscar el id de la categoria, así como cargar la cbx
        private ProductoDAO proDAO = new ProductoDAO();
        private Producto p;
        public int idProducto;

        public FormDetProducto()
        {
            InitializeComponent();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            String categoria;
            p = new Producto();
            p.Nombre = txtNombre.Text.Trim();
            p.Descripcion = txtDescripcion.Text.Trim();
            p.Precio = float.Parse(txtPrecio.Text.Trim());

            categoria = comboCategoria.SelectedItem.ToString();

            if (string.IsNullOrEmpty(categoria))
            {
                MessageBox.Show("Selecciona una categoría válida.");
                return;
            }

            int idCategoria = catDAO.leerIdCategoriaPorNombre(categoria);

            if (idCategoria <= 0)
            {
                MessageBox.Show("No se encontró la categoría seleccionada.");
                return;
            }

            p.Categoria = new Categoria(idCategoria, categoria);
            p.FechaAlta = dtpProducto.Value;

            if (idProducto > 0)
            {
                p.IdProducto = getIdIfExist();
                MessageBox.Show("Modificando producto con ID: " + p.IdProducto);

                if (proDAO.modificarProducto(p) != 0)
                {
                    MessageBox.Show("Producto modificado correctamente");
                }
                else
                {
                    MessageBox.Show("Error al modificar el producto");
                }
            }
            else
            {
                if (proDAO.agregarProducto(p) != 0)
                {
                    MessageBox.Show("Producto dado de alta correctamente");
                }
                else
                {
                    MessageBox.Show("Error en el alta del producto");
                }
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDetProducto_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            cargarComboCategorias();

            if (idProducto > 0)
            {
                p = proDAO.leerProductoPorId(idProducto);
                if (p != null)
                {
                    txtId.Text = idProducto.ToString();
                    txtNombre.Text = p.Nombre;
                    txtDescripcion.Text = p.Descripcion;
                    txtPrecio.Text = p.Precio.ToString();
                    dtpProducto.Value = p.FechaAlta;

                    if (p.Categoria != null)
                    {
                        comboCategoria.SelectedItem = p.Categoria.Nombre;
                    }
                    else
                    {
                        MessageBox.Show("Categoría no encontrada para el producto.");
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el producto.");
                }
            }
            else
            {
                txtId.Clear();
                txtNombre.Clear();
                txtPrecio.Clear();
                txtDescripcion.Clear();
            }
    }

        public void cargarComboCategorias()
        {
            List<String> listaCategorias;
            listaCategorias = catDAO.consultarCategoriasCombo();
            comboCategoria.DataSource = listaCategorias;
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

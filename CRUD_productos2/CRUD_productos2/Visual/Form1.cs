using CRUD.Objects;
using CRUD_Productos;
using CRUD_productos2.Visual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CRUD_productos2
{
    public partial class Form1 : Form
    {
        // Declaramos la lista que usará el programa y el DAO
        public List<Productos> listaProductos = new List<Productos>();
        private ProductoDAO productoDAO = new ProductoDAO();

        public Form1()
        {
            InitializeComponent();
            cargarListaProductos();
        }

        private void cargarListaProductos()
        {
            dgvProductos.AllowUserToAddRows = false;
            //VACIO DataGridView
            dgvProductos.Rows.Clear();
            dgvProductos.Refresh();
            //VACIO lista
            listaProductos.Clear();
            //Cargo la lista con la select
            listaProductos = productoDAO.consultarProductos();

            for (int i = 0; i < listaProductos.Count(); i++)
            {
                dgvProductos.Rows.Add(
                    listaProductos[i].IdProducto,
                    listaProductos[i].Descripcion,
                    listaProductos[i].Nombre,
                    listaProductos[i].Precio,
                    listaProductos[i].Activo);
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            bool seguir = true;
            int idDado = Convert.ToInt32(numID.Value);

            // Si la descripcion no es valida
            if (!Logica.validarDescripcion(txtDescripcion.Text))
            {
                seguir = false;
                lblAyudaDescripcion.ForeColor = Color.Red;
                lblAyudaDescripcion.Text = "Error: La descripción es de 10 o más caracteres.";
            }

            else
            {
                lblAyudaDescripcion.ForeColor = Color.Green;
                lblAyudaDescripcion.Text = "Correcto...";
            }

            // Si el nombre no es valido
            if (!Logica.validarNombre(txtNombre.Text) || txtNombre.Text == "")
            {
                seguir = false;
                lblAyudaNombre.ForeColor = Color.Red;
                lblAyudaNombre.Text = "Error: El nombre no puede contener números.";
            }

            else
            {
                lblAyudaNombre.ForeColor = Color.Green;
                lblAyudaNombre.Text = "Correcto...";
            }

            // Si el precio no es valido
            if (!Logica.validarPrecio(txtPrecio.Text) || txtPrecio.Text.Equals(""))
            {
                seguir = false;
                lblAyudaPrecio.ForeColor = Color.Red;
                lblAyudaPrecio.Text = "Error: El precio solo debe contener números.";
            }

            else
            {
                lblAyudaPrecio.ForeColor = Color.Green;
                lblAyudaPrecio.Text = "Correcto...";
            }

            // Validar si activo no es válido
            if (!Logica.validarActivo(txtActivo.Text))
            {
                seguir = false;
                lblAyudaActivo.ForeColor = Color.Red;
                lblAyudaActivo.Text = "Error: Introduzca 'Activo' o 'Inactivo'";
            }

            else
            {
                lblAyudaActivo.ForeColor = Color.Green;
                lblAyudaActivo.Text = "Correcto...";
            }

            if (seguir)
            {
                bool activo;
                if (txtActivo.Text.ToLower().Equals("activo"))
                {
                    activo = true;
                }
                else
                {
                    activo = false;
                }

                double precio = Convert.ToDouble(txtPrecio.Text);

                Productos pr = new Productos(idDado, txtDescripcion.Text, txtNombre.Text, precio, activo);
                productoDAO.agregarProducto(pr);
                MessageBox.Show("Producto agregado correctamente");

                cargarListaProductos();
            }
            else
            {
                MessageBox.Show("Error al agregar el producto");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            numID.Value = 1;
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtNombre.Text = "";
            txtActivo.Text = "";
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "Modificar")
            {
                ModificacionProducto Det = new ModificacionProducto();
                Det.id = Convert.ToInt32(numID.Text);
                Det.listaProductos = listaProductos;
                Det.ShowDialog();
                cargarListaProductos();
            }

            if (dgvProductos.Columns[e.ColumnIndex].Name == "Eliminar")
            {

                int id;
                id = getIdIfExist();
                if (id == -1) return;
                if (MessageBox.Show("¿Desea eliminar el producto?", "Eliminar producto", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (productoDAO.eliminarProducto(id) != 0)
                    {
                        MessageBox.Show("producto eliminado con éxito.");
                        cargarListaProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }
            }
        }

        private int getIdIfExist()
        {
            if (!numID.Text.Trim().Equals(""))
            {
                if (int.TryParse(numID.Text.Trim(), out int id))
                    return id;
                else
                    return -1;
            }
            else
                return -1;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el índice de fila es válido
            if (e.RowIndex >= 0 && e.RowIndex < dgvProductos.Rows.Count)
            {
                // Obtener la fila seleccionada
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // Cargar los valores en los controles
                numID.Text = Convert.ToString(fila.Cells["idProducto"].Value);
                txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value);
                txtNombre.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                txtPrecio.Text = Convert.ToString(fila.Cells["Precio"].Value);
                txtActivo.Text = (fila.Cells["Activo"].Value.Equals(true)) ? "Activo" : "Inactivo";
            }
        }
    }
}

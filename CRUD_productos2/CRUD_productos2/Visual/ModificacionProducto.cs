using CRUD.Objects;
using CRUD_Productos;
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

namespace CRUD_productos2.Visual
{
    public partial class ModificacionProducto : Form
    {
        private Productos p;
        public int id;
        public List<Productos> listaProductos = new List<Productos>();
        private ProductoDAO productoDAO = new ProductoDAO();

        public ModificacionProducto()
        {
            InitializeComponent();
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool seguir = true;
            int idDado = Convert.ToInt32(numID.Value);

            // Si la descripcion no es valida
            if (!Logica.validarDescripcion(txtDescripcion.Text))
            {
                seguir = false;
                lblAyudaDescripcion.ForeColor = Color.Red;
                lblAyudaDescripcion.Text = "Error: La descripción debe ser de 10 caracteres mínimo.";
            }

            else
            {
                lblAyudaDescripcion.ForeColor = Color.Green;
                lblAyudaDescripcion.Text = "Correcto...";
            }

            // Si el nombre no es valido
            if (!Logica.validarNombre(txtNombre.Text))
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
            if (!Logica.validarPrecio(txtPrecio.Text))
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
                productoDAO.modificarProducto(pr);

                MessageBox.Show("Exito al modificar el producto");
                Dispose();
            }
            else
            {
                MessageBox.Show("Error al modificar el producto");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void CargarDatosProducto()
        {
            p = new Productos();
            p.IdProducto = getIdIfExist();
            p.Descripcion = txtDescripcion.Text.Trim();
            p.Nombre = txtNombre.Text.Trim();
            p.Precio = Double.Parse(txtPrecio.Text.Trim());
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

        private void ModificacionProducto_Load(object sender, EventArgs e)
        {
            //Select
            Productos prod = productoDAO.leerUno(id);
            //Cargar datos en los controles
            numID.Text = id.ToString();
            txtDescripcion.Text = prod.Descripcion;
            txtNombre.Text = prod.Nombre;
            txtPrecio.Text = prod.Precio.ToString();
            txtActivo.Text = prod.Activo ? "Activo" : "Inactivo";
        }
    }
}

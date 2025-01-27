using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Productos
{
    public class Productos
    {
        // Atributos del objeto producto:
        //IdProducto, Descripcion, Nombre, Precio, Activo(boolean)
        int idProducto;
        String descripcion;
        String nombre;
        Double precio;
        Boolean activo;

        // Constructores
        public Productos()
        {
        }

        public Productos(int idProducto, string descripcion, string nombre, double precio, bool activo)
        {
            this.idProducto = idProducto;
            this.descripcion = descripcion;
            this.nombre = nombre;
            this.precio = precio;
            this.activo = activo;
        }

        // Getters y setters
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public double Precio { get => precio; set => precio = value; }
        public bool Activo { get => activo; set => activo = value; }
    }
}

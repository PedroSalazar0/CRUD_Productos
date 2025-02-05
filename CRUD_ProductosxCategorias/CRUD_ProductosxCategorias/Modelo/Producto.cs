using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ProductosxCategorias.Modelo
{
    public class Producto
    {
        private int idProducto;
        private string nombre;
        private string descripcion;
        private float precio;
        private DateTime fechaAlta;
        private Categoria categoria;

        public Producto()
        {
        }

        public Producto(int idProducto, string nombre, string descripcion, float precio, DateTime fechaAlta, Categoria categoria)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.fechaAlta = fechaAlta;
            this.categoria = categoria;
        }

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public float Precio { get => precio; set => precio = value; }
        public DateTime FechaAlta { get => fechaAlta; set => fechaAlta = value; }
        public Categoria Categoria { get => categoria; set => categoria = value; }
    }
}

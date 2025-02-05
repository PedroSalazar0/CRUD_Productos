using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_ProductosxCategorias.Conexion;
using CRUD_ProductosxCategorias.Modelo;
using CRUD_ProductosxCategorias.Excepcion;

namespace CRUD_ProductosxCategorias.DAO
{
    public class ProductoDAO
    {
        private ConexionBD conexion = new ConexionBD();
        public ProductoDAO()
        {
        }
        public int agregarProducto(Producto prod)
        {
            int resul = 0;
            string strINSERT = "INSERT INTO productos (nombre, descripcion, precio, fechaAlta, idCategoria) values (@nombre, @descripcion, @precio, @fechaAlta, @idCategoria);";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strINSERT, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@nombre", prod.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", prod.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@precio", prod.Precio));
                mCommand.Parameters.Add(new MySqlParameter("@fechaAlta", prod.FechaAlta));
                mCommand.Parameters.Add(new MySqlParameter("@idCategoria", prod.Categoria.IdCategoria));

                resul = mCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ExcepcionProductoCategoria e = new ExcepcionProductoCategoria();
                e.CodigoError = ex.ErrorCode;
                e.MensajeAdministrador = ex.Message;
                e.SentenciaSQL = strINSERT;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return resul;
        }

        public int modificarProducto(Producto prod)
        {
            int resul = 0;
            string strUPDATE = "UPDATE productos SET nombre=@nombre, descripcion=@descripcion, precio=@precio, fechaAlta=@fechaAlta, idCategoria=@idCategoria WHERE idProducto=@idProducto";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strUPDATE, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@nombre", prod.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", prod.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@precio", prod.Precio));
                mCommand.Parameters.Add(new MySqlParameter("@fechaAlta", prod.FechaAlta));
                mCommand.Parameters.Add(new MySqlParameter("@idCategoria", prod.Categoria.IdCategoria));
                mCommand.Parameters.Add(new MySqlParameter("@idProducto", prod.IdProducto));

                resul = mCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ExcepcionProductoCategoria e = new ExcepcionProductoCategoria();
                e.CodigoError = ex.ErrorCode;
                e.MensajeAdministrador = ex.Message;
                e.SentenciaSQL = strUPDATE;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return resul;
        }

        public int eliminarProducto(int id)
        {
            int resul = 0;
            string strDELETE = "DELETE FROM productos WHERE idProducto=@id";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strDELETE, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@id", id));
                resul = mCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ExcepcionProductoCategoria e = new ExcepcionProductoCategoria();
                e.CodigoError = ex.ErrorCode;
                e.MensajeAdministrador = ex.Message;
                e.SentenciaSQL = strDELETE;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return resul;
        }

        public Producto leerProductoPorId(int id)
        {
            MySqlDataReader mReader = null;
            Producto prod = new Producto();
            // Lo usamos para obtener el nombre de la categoria con un INNER JOIN
            string strCONSULTA = "SELECT p.*, c.nombre as nombreCategoria FROM productos p JOIN categorias c ON p.idCategoria = c.idCategoria WHERE idProducto=@id";

            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Parameters.Add(new MySqlParameter("@id", id));
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();
                while (mReader.Read())
                {
                    prod.IdProducto = mReader.GetInt16("idProducto");
                    prod.Nombre = mReader.GetString("nombre");
                    prod.Descripcion = mReader.GetString("descripcion");
                    prod.Precio = mReader.GetFloat("precio");
                    prod.FechaAlta = mReader.GetDateTime("fechaAlta");
                    // hay que inicializar la categoría antes de acceder a IdCategoria
                    prod.Categoria = new Categoria();
                    prod.Categoria.IdCategoria = mReader.GetInt16("idCategoria");
                    prod.Categoria.Nombre = mReader.GetString("nombre");
                }
                mReader.Close();
            }
            catch (MySqlException ex)
            {
                ExcepcionProductoCategoria e = new ExcepcionProductoCategoria();
                e.CodigoError = ex.ErrorCode;
                e.MensajeAdministrador = ex.Message;
                e.SentenciaSQL = strCONSULTA;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return prod;
        }

        public List<Producto> consultarProductos(string filtro)
        {
            List<Producto> lisProductos = new List<Producto>();
            MySqlDataReader mReader = null;
            Producto prod;
            string strCONSULTA = "SELECT p.*, c.nombre as nombreCategoria FROM productos p JOIN categorias c ON p.idCategoria = c.idCategoria";

            if (!string.IsNullOrEmpty(filtro))
            {
                strCONSULTA += " WHERE p.nombre LIKE @filtro OR p.descripcion LIKE @filtro";
            }

            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();

                while (mReader.Read())
                {
                    prod = new Producto();
                    prod.IdProducto = mReader.GetInt16("idProducto");
                    prod.Nombre = mReader.GetString("nombre");
                    prod.Descripcion = mReader.GetString("descripcion");
                    prod.Precio = mReader.GetFloat("precio");
                    prod.FechaAlta = mReader.GetDateTime("fechaAlta");
                    // hay que inicializar la categoría antes de acceder a nombre 
                    prod.Categoria = new Categoria();
                    prod.Categoria.Nombre = mReader.GetString("nombreCategoria");
                    lisProductos.Add(prod);
                }
                mReader.Close();
            }
            catch (MySqlException ex)
            {
                ExcepcionProductoCategoria e = new ExcepcionProductoCategoria();
                e.CodigoError = ex.ErrorCode;
                e.MensajeAdministrador = ex.Message;
                e.SentenciaSQL = strCONSULTA;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return lisProductos;
        }
    }
}

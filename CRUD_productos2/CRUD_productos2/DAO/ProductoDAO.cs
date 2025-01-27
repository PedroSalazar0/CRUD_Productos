using CRUD.BD;
using CRUD_Productos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD.Objects
{
    internal class ProductoDAO
    { 
        //ATRIBUTOS
        private Conexion conexion = new Conexion();
        public ProductoDAO()
        {
        }

        public int agregarProducto(Productos pro)
        {
            int resul=0;
            string strINSERT = "INSERT INTO productos (descripcion, precio, nombre, activo) values (@descripcion, @precio, @nombre, @activo);";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strINSERT, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", pro.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@precio", pro.Precio));
                mCommand.Parameters.Add(new MySqlParameter("@nombre", pro.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@activo", pro.Activo));

                resul=mCommand.ExecuteNonQuery();
                //if (resul!=0)     MessageBox.Show("OK");
                //else              MessageBox.Show("Fallo");
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return resul;
        }


        public int modificarProducto(Productos pro)
        {
            int resul = 0;
            string strUPDATE = " UPDATE productos " +
                   "SET " +
                   "descripcion = @descripcion, " +
                   "nombre = @nombre, " +
                   "precio = @precio, " +
                   "activo = @activo " +
                   "WHERE idProducto = @idProducto";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strUPDATE, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", pro.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@nombre", pro.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@precio", pro.Precio));
                mCommand.Parameters.Add(new MySqlParameter("@activo", pro.Activo));
                mCommand.Parameters.Add(new MySqlParameter("@idProducto", pro.IdProducto));
                resul= mCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Si existe un error aquí muestra el mensaje
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
            string strDELETE = "DELETE FROM productos WHERE idProducto=@IdProducto";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strDELETE, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@idProducto", id));
                resul = mCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return resul;
        }
        public Productos leerUno(int id)
        {
            MySqlDataReader mReader = null;
            Productos p = new Productos();
            string strCONSULTA = "SELECT * FROM productos WHERE IdProducto=@IdProducto";
                
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Parameters.Add(new MySqlParameter("@IdProducto", id));
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();

                while (mReader.Read())
                {
                    p.IdProducto = mReader.GetInt16("idProducto");
                    p.Descripcion = mReader.GetString("descripcion");
                    p.Nombre = mReader.GetString("nombre");
                    p.Precio = mReader.GetFloat("precio");
                    p.Activo = mReader.GetBoolean("activo");  
                }
                mReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return p;
    }
        public List<Productos> consultarProductos()
        {
            List<Productos> lisProductos = new List<Productos>();
            MySqlDataReader mReader = null;
            Productos p = new Productos();

            string strCONSULTA = "SELECT * FROM productos";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();

                while(mReader.Read()) { 
                    // Crear una nueva instancia de Productos para cada fila leída
                    Productos pr = new Productos();
                    pr.IdProducto = mReader.GetInt16("idProducto");
                    pr.Descripcion = mReader.GetString("descripcion");
                    pr.Nombre = mReader.GetString("nombre");
                    pr.Precio = mReader.GetFloat("precio");
                    pr.Activo = mReader.GetBoolean("activo");
                    lisProductos.Add(pr); // Agregar la nueva instancia a la lista
                }
                mReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conexion.cerrarConexion();
            }
            return lisProductos;
        }
    }
}

using CRUD_ProductosxCategorias.Modelo;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_ProductosxCategorias.Conexion;
using CRUD_ProductosxCategorias.Excepcion;

namespace CRUD_ProductosxCategorias.DAO
{
    public class CategoriaDAO
    {
        private ConexionBD conexion = new ConexionBD();
        public CategoriaDAO()
        {
        }
        public int agregarCategoria(Categoria cat)
        {
            int resul = 0;
            string strINSERT = "INSERT INTO categorias (nombre, descripcion, activo) values (@nombre, @descripcion, @activo);";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strINSERT, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@nombre", cat.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", cat.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@activo", cat.Activo));

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

        public int modificarCategoria(Categoria cat)
        {
            int resul = 0;
            string strUPDATE = "UPDATE categorias SET nombre=@nombre, descripcion=@descripcion, activo=@activo WHERE idCategoria=@idCategoria";
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strUPDATE, conexion.abrirConexion());
                mCommand.Parameters.Add(new MySqlParameter("@nombre", cat.Nombre));
                mCommand.Parameters.Add(new MySqlParameter("@descripcion", cat.Descripcion));
                mCommand.Parameters.Add(new MySqlParameter("@activo", cat.Activo));
                mCommand.Parameters.Add(new MySqlParameter("@idCategoria", cat.IdCategoria));

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

        public int eliminarCategoria(int id)
        {
            int resul = 0;
            string strDELETE = "DELETE FROM categorias WHERE idCategoria=@id";
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

        public Categoria leerCategoriaPorId(int id)
        {
            MySqlDataReader mReader = null;
            Categoria cat = new Categoria();
            string strCONSULTA = "SELECT * FROM categorias WHERE idCategoria=@id";

            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Parameters.Add(new MySqlParameter("@id", id));
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();
                while (mReader.Read())
                {
                    cat.IdCategoria = mReader.GetInt16("idCategoria");
                    cat.Nombre = mReader.GetString("nombre");
                    cat.Descripcion = mReader.GetString("descripcion");
                    cat.Activo = mReader.GetBoolean("activo");
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
            return cat;
        }

        public List<Categoria> consultarCategorias(string filtro)
        {
            List<Categoria> lisCategorias = new List<Categoria>();
            MySqlDataReader mReader = null;
            Categoria cat;
            string strCONSULTA = "SELECT * FROM categorias";
            if (filtro != "")
            {
                strCONSULTA += " WHERE nombre LIKE '%" + filtro + "%' OR descripcion LIKE '%" + filtro + "%';";
            }
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();

                while (mReader.Read())
                {
                    cat = new Categoria();
                    cat.IdCategoria = mReader.GetInt16("idCategoria");
                    cat.Nombre = mReader.GetString("nombre");
                    cat.Descripcion = mReader.GetString("descripcion");
                    cat.Activo = mReader.GetBoolean("activo");
                    lisCategorias.Add(cat);
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
            return lisCategorias;
        }

        public List<String> consultarCategoriasCombo()
        {
            List<String> lisCategorias = new List<String>();
            MySqlDataReader mReader = null;
            string strCONSULTA = "SELECT * FROM categorias";

            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();

                while (mReader.Read())
                {
                    lisCategorias.Add(mReader.GetString("nombre"));
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
            return lisCategorias;
        }

        public int leerIdCategoriaPorNombre(String nombre)
        {
            MySqlDataReader mReader = null;
            string strCONSULTA = "SELECT idCategoria FROM categorias WHERE nombre=@nombre";
            int idcategoria = 0;
            try
            {
                MySqlCommand mCommand = new MySqlCommand(strCONSULTA);
                mCommand.Parameters.Add(new MySqlParameter("@nombre", nombre));
                mCommand.Connection = conexion.abrirConexion();
                mReader = mCommand.ExecuteReader();
                while (mReader.Read())
                {
                    idcategoria = mReader.GetInt16("idCategoria");

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
            return idcategoria;
        }
    }
}

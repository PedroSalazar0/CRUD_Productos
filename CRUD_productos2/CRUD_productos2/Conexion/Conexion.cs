﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD.BD
{
    public class Conexion 
    {
        //ATRIBUTOS
        protected string server = "localhost";
        protected string database = "tienda";
        protected string user = "root";
        protected string password = "";
        private MySqlConnection conex;
        private string cadenaConexion;
        public Conexion()
        {
            
        }
        public MySqlConnection abrirConexion()
        {
            cadenaConexion = "Database=" + database +
                "; DataSource=" + server +
                "; User Id= " + user +
                "; Password=" + password;
            try
            {
                conex = new MySqlConnection(cadenaConexion);
                if (conex.State != System.Data.ConnectionState.Open)
                {
                    conex.Open();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("No ha podido conectar con el servidor.  Contacte con el administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Error en el usuario y/o contraseña, por favor intente de nuevo");
                        break;
                }
            }
            return conex;
        }
        

        public void cerrarConexion()
        {
            conex.Close();
        }
    }
}

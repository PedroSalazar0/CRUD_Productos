using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Productos
{
    internal class Logica
    {
        // 1. Validamos ID, buscándolo en la lista y si existe, devuelve false pues no puede repetirse
        public static bool validarID(List<Productos> lista_producto, int idDado)
        {
            bool IDValido = true;

            for (int i = 0; i < lista_producto.Count; i++)
            {
                if (lista_producto[i].IdProducto.Equals(idDado))
                {
                    IDValido = false;
                } 
            }

            return IDValido;
        }

        // 2. Validamos Descripción, tiene que ser más de 10 caracteres
        public static bool validarDescripcion(String descripcionDada)
        {
            bool DescValida = true;

            if (descripcionDada.Length < 10)
            {
                DescValida = false;
            }

            return DescValida;
        }

        // 3. Validamos el nombre, no puede tener numeros
        public static bool validarNombre(string texto)
        {
            bool nombreValido = true;

            foreach (char c in texto)
            {
                if (char.IsDigit(c)) // Verifica si el carácter es un número
                {
                    nombreValido = false;
                }
            }

            return nombreValido;
        }

        // 4. Validamos el precio, no puede tener letras
        public static bool validarPrecio(string texto)
        {
            bool precioValido = true;

            foreach (char c in texto)
            {
                if (!char.IsDigit(c)) // Verifica si el carácter es un número
                {
                    precioValido = false;
                }
            }

            return precioValido;
        }

        // 5. Validamos el activo, si escribo algo distinto a Activo o Inactivo da error
        public static bool validarActivo(string texto)
        {
            bool ActivoValido = true;

            if (!texto.ToLower().Equals("activo") && !texto.ToLower().Equals("inactivo"))
            {
                ActivoValido = false;
            }

            return ActivoValido;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class LibroNegocio
    {
        public List<Libro> listar()
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();  

            try
            {
                datos.setearConsulta("select L.IdLibros, Codigo, C.Categoria, Titulo, Autor, Descripcion, UrlTapaLibro, FechaPublicacion, Stock, L.IdCategoria from Libros L, Categoria C where C.IdCategoria=L.IdCategoria");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Libro aux = new Libro();
                    aux.IdLibros = (int)datos.Lector["IdLibros"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Categoria = new Categorias();
                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Categoria = (string)datos.Lector["Categoria"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Autor = (string)datos.Lector["Autor"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.UrlTapaLibro = (string)datos.Lector["UrlTapaLibro"];
                    aux.FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    lista.Add(aux);
                }   
                

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            
        }
        public void agregar(Libro altaLibro)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert Into Libros (Codigo,IdCategoria,Titulo, Autor, Descripcion, UrlTapaLibro, FechaPublicacion, Stock) values (@codigo, @idCategoria, @titulo, @autor, @descripcion, @urlTapaLibro, @fechaPublicacion, @stock)");
                datos.setearParametros("@codigo", altaLibro.Codigo);
                datos.setearParametros("@idCategoria", altaLibro.Categoria.IdCategoria);
                datos.setearParametros("@titulo", altaLibro.Titulo);
                datos.setearParametros("@autor", altaLibro.Autor);
                datos.setearParametros("@descripcion", altaLibro.Descripcion);
                datos.setearParametros("@urlTapaLibro", altaLibro.UrlTapaLibro);
                datos.setearParametros("@fechaPublicacion", altaLibro.FechaPublicacion);
                datos.setearParametros("@stock", altaLibro.Stock);

                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Libro libroModificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update Libros set Codigo = @codigo, IdCategoria = @idCategoria, Titulo = @titulo, Autor = @autor, Descripcion = @descripcion, UrlTapaLibro = @urlTapaLibro, FechaPublicacion = @fechaPublicacion, Stock = @stock Where IdLibros = @idLibros");
                datos.setearParametros("@codigo", libroModificado.Codigo);
                datos.setearParametros("@idCategoria", libroModificado.Categoria.IdCategoria);
                datos.setearParametros("@titulo", libroModificado.Titulo);
                datos.setearParametros("@autor", libroModificado.Autor);
                datos.setearParametros("@descripcion", libroModificado.Descripcion);
                datos.setearParametros("@urlTapaLibro", libroModificado.UrlTapaLibro);
                datos.setearParametros("@fechaPublicacion", libroModificado.FechaPublicacion);
                datos.setearParametros("@stock", libroModificado.Stock);
                datos.setearParametros("@idLibros", libroModificado.IdLibros);
                
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Libro> filtrar(string campo, string criterio, string filtro)
        {
            List<Libro> lista = new List<Libro>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select L.IdLibros, Codigo, C.Categoria, Titulo, Autor, Descripcion, UrlTapaLibro, FechaPublicacion, Stock, L.IdCategoria from Libros L, Categoria C where C.IdCategoria=L.IdCategoria AND ";
                if (campo == "Título")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "Titulo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Titulo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Titulo like '%" + filtro + "%'";
                            break;
                    }
                }

                else
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "Autor like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Autor like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Autor like '%" + filtro + "%'";
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Libro aux = new Libro();
                    aux.IdLibros = (int)datos.Lector["IdLibros"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Categoria = new Categorias();
                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Categoria = (string)datos.Lector["Categoria"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.Autor = (string)datos.Lector["Autor"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.UrlTapaLibro = (string)datos.Lector["UrlTapaLibro"];
                    aux.FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];
                    aux.Stock = (int)datos.Lector["Stock"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminar(int idLibros)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Libros where IdLibros=@idLibros");
                datos.setearParametros("@idLibros", idLibros);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}

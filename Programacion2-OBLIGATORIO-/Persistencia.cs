using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using biblioteca;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace Programacion2_OBLIGATORIO_
{
    public class Persistencia
    {

        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=analista2022_estudiofotografo;User Id=analista2022_estudiofotografo;Password=123456789;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

        public static int guardarfotografo(Fotografos fot)
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("update Fotografo  set nombre = @nombre , apellido = @apellido, paga = @paga  where idfotografo=" + fot.idFotografo.ToString(), conexion);
            sentencia1.Parameters.AddWithValue("@nombre", fot.nombre);
            sentencia1.Parameters.AddWithValue("@apellido", fot.apellido);
            sentencia1.Parameters.AddWithValue("@paga", fot.precio);
            int cont = sentencia1.ExecuteNonQuery();
            //si contador esta en cero, signfica que no se modificaron registros en la tabla

            if (cont == 0)
            {
                sentencia1 = new SqlCommand("insert into Fotografo (nombre,apellido,paga)  values (@nombre,@apellido,@paga)", conexion);
                sentencia1.Parameters.AddWithValue("@nombre", fot.nombre);
                sentencia1.Parameters.AddWithValue("@apellido", fot.apellido);
                sentencia1.Parameters.AddWithValue("@paga", fot.precio);
                cont = sentencia1.ExecuteNonQuery();
            }
            conexion.Close();
            return cont;
        }

        public static int guardarcliente(Cliente cli)
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("update Cliente  set nombre = @nombre , apellido = @apellido, telefono = @telefono  where idcliente=" + cli.IdCliente.ToString(), conexion);
            sentencia1.Parameters.AddWithValue("@nombre", cli.nombre);
            sentencia1.Parameters.AddWithValue("@apellido", cli.apellido);
            sentencia1.Parameters.AddWithValue("@telefono", cli.telefono);
            int cont = sentencia1.ExecuteNonQuery();
            //si contador esta en cero, signfica que no se modificaron registros en la tabla

            if (cont == 0)
            {
                sentencia1 = new SqlCommand("insert into Cliente (nombre,apellido,telefono)  values (@nombre,@apellido,@telefono)", conexion);
                sentencia1.Parameters.AddWithValue("@nombre", cli.nombre);
                sentencia1.Parameters.AddWithValue("@apellido", cli.apellido);
                sentencia1.Parameters.AddWithValue("@telefono", cli.telefono);
                cont = sentencia1.ExecuteNonQuery();

                int ultimoid = Persistencia.ultimoidcliente();

                //NECESITA EL ID DEL CLIENTE RECIEN REGISTRADO PARA PODER GUARDAR
                if (cli.tipoCliente == "Empresa" && cont != 0)
                {
                    SqlCommand sentencia2 = new SqlCommand("insert into ClienteEmpresa (idcliente,rut,descuento)  values (@id,@rut,@descuento)", conexion);
                    sentencia2.Parameters.AddWithValue("@id", ultimoid);
                    sentencia2.Parameters.AddWithValue("@rut", cli.RUT);
                    sentencia2.Parameters.AddWithValue("@descuento", cli.Descuento);
                    int cont1 = sentencia2.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand sentencia2 = new SqlCommand("insert into ClienteParticular (idcliente,tipo)  values (@id, @tipo)", conexion);
                    sentencia2.Parameters.AddWithValue("@id", ultimoid);
                    sentencia2.Parameters.AddWithValue("@tipo", cli.tipoParticular);
                    int cont1 = sentencia2.ExecuteNonQuery();
                }
            }
            else
            {
                if (cli.tipoCliente == "Empresa")
                {
                    SqlCommand sentencia2 = new SqlCommand("update ClienteEmpresa  set rut = @rut , descuento = @descuento where id=" + cli.IdCliente.ToString(), conexion);
                    sentencia2.Parameters.AddWithValue("@rut", cli.RUT);
                    sentencia2.Parameters.AddWithValue("@descuento", cli.Descuento);
                    int cont1 = sentencia2.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand sentencia2 = new SqlCommand("update ClienteParticular  set tipo = @tipo where id=" + cli.IdCliente.ToString(), conexion);
                    sentencia2.Parameters.AddWithValue("@tipo", cli.tipoParticular);
                    int cont1 = sentencia2.ExecuteNonQuery();
                }
            }
            conexion.Close();
            return cont;
        }

        public static int ultimoidcliente()
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("SELECT MAX(idcliente) FROM Cliente", conexion);
            int valor = (int)sentencia1.ExecuteScalar();

            return valor;
        }

        public static List<Cliente> listaclientes()
        {
            List<Cliente> lista = new List<Cliente>(); ;
            Cliente cliente = null;
            //SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("select *  from cliente", conectar());
            SqlCommand sentencia2 = new SqlCommand("select *  from clienteEmpresa", conectar());
            SqlCommand sentencia3 = new SqlCommand("select *  from clienteParticular", conectar());
            SqlDataReader reader = sentencia1.ExecuteReader();


            while (reader.Read())
            {
                SqlDataReader reader2 = sentencia2.ExecuteReader();
                SqlDataReader reader3 = sentencia3.ExecuteReader();
                cliente = new Cliente();
                cliente.IdCliente = (int)reader["idcliente"];
                cliente.nombre = reader["nombre"].ToString();
                cliente.apellido = reader["apellido"].ToString();
                cliente.nombrecompleto = cliente.nombre + " " + cliente.apellido;

                while (reader2.Read())
                {
                    if (cliente.IdCliente == (int)reader2["idcliente"])
                    {
                        cliente.tipoCliente = "Empresa";
                        cliente.Descuento = (int)reader2["descuento"];
                    }
                }
                while (reader3.Read())
                {
                    if (cliente.IdCliente == (int)reader3["idcliente"])
                    {
                        cliente.tipoCliente = "Particular";
                        cliente.tipoParticular = reader3["tipo"].ToString();
                    }
                }

                lista.Add(cliente);
                reader2.Close();
                reader3.Close();
            }
            reader.Close();

            //conexion.Close();

            return lista;
        }


        //////METODO REGISTRO DE SERVICIOS///////////////
        public static int guardarservicio(Servicio serv)
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("update Servicio set locacion = @localizacion, precio = @precios, duracion = @duracion, idcliente = @idcliente, fecha = @fecha where idservicio=" + serv.IdServicio.ToString(), conexion);
            sentencia1.Parameters.AddWithValue("@Localizacion", serv.Localizacion);
            sentencia1.Parameters.AddWithValue("@precios", serv.precios);
            sentencia1.Parameters.AddWithValue("@duracion", serv.duracion);
            sentencia1.Parameters.AddWithValue("@idcliente", serv.IdCliente);
            sentencia1.Parameters.AddWithValue("@fecha", serv.fecha);
            int cont = sentencia1.ExecuteNonQuery();

            //si contador esta en cero, signfica que no se modificaron registros en la tabla

            if (cont == 0)
            {
                sentencia1 = new SqlCommand("insert into Servicio (idservicio,locacion,precio,duracion,idcliente,fecha)  values (@IdServicio,@Localizacion,@precios,@duracion,@idcliente,@fecha)", conexion);
                sentencia1.Parameters.AddWithValue("@IdServicio", serv.IdServicio);
                sentencia1.Parameters.AddWithValue("@Localizacion", serv.Localizacion);
                sentencia1.Parameters.AddWithValue("@precios", serv.precios);
                sentencia1.Parameters.AddWithValue("@duracion", serv.duracion);
                sentencia1.Parameters.AddWithValue("@idcliente", serv.IdCliente);
                sentencia1.Parameters.AddWithValue("@fecha", serv.fecha);
                cont = sentencia1.ExecuteNonQuery();

                if (cont != 0)
                {
                    for (int i = 0; i < serv.fotografos.Count; i++)
                    {
                        sentencia1 = new SqlCommand("insert into ServicioFotografos (idservicio,idfotografo) values (@idservicio,@idfotografo)", conexion);
                        sentencia1.Parameters.AddWithValue("@idservicio", serv.IdServicio);
                        sentencia1.Parameters.AddWithValue("@idfotografo", serv.fotografos[i].idFotografo);
                        cont = sentencia1.ExecuteNonQuery();
                    }

                }
            }
            //FALTA ELSE PARA UPDATE SERVICIOFOTOGRAFOS
            conexion.Close();
            return cont;
        }

        public static int GuardarPrecio(Fotografos fot)
        {
            SqlConnection conexion = conectar();

            SqlCommand sentencia1 = new SqlCommand("update Fotografo set paga = @paga", conexion);
            sentencia1.Parameters.AddWithValue("@paga", fot.precio);

            int cont = sentencia1.ExecuteNonQuery();

            conexion.Close();
            return cont;
        }

        public static List<Fotografos> listafotografo()
        {
            List<Fotografos> lista = new List<Fotografos>();
            Fotografos fotografo = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("select *  from fotografo", conexion);
            SqlDataReader reader = sentencia1.ExecuteReader();

            while (reader.Read())
            {
                fotografo = new Fotografos();
                fotografo.idFotografo = (int)reader["idfotografo"];
                fotografo.nombre = reader["nombre"].ToString();
                fotografo.apellido = reader["apellido"].ToString();
                fotografo.precio = (int)reader["paga"];

                lista.Add(fotografo);
            }
            reader.Close();
            conexion.Close();

            return lista;
        }

        public static int BorrarFotografo(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("delete from Fotografo where idfotografo=" + id, conexion);
            int cont = sentencia1.ExecuteNonQuery();

            conexion.Close();
            return cont;

        }

        public static string ConsultaPrecio(string precio)
        {
            precio = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("select paga  from fotografo", conexion);
            SqlDataReader reader = sentencia1.ExecuteReader();

            while (reader.Read())
            {
                precio = Convert.ToString((int)reader["paga"]);
            }
            reader.Close();
            conexion.Close();

            return precio;
        }


        public static List<Servicio> BuscarServicio(string fecha)
        {
            List<Servicio> lista = new List<Servicio>();
            Servicio servicio = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("select * from Servicio where fecha='" + fecha + "' order by duracion desc", conexion);
            SqlDataReader reader = sentencia1.ExecuteReader();

            while (reader.Read())
            {
                servicio = new Servicio();
                servicio.IdServicio = (int)reader["idservicio"];
                servicio.duracion = (double)reader["duracion"];
                servicio.Localizacion = reader["locacion"].ToString();
                servicio.precios = (int)reader["precio"];
                servicio.IdCliente = (int)reader["idcliente"];
                servicio.fecha = (DateTime)reader["fecha"];

                lista.Add(servicio);
            }
            reader.Close();
            conexion.Close();

            return lista;
        }

        public static List<ServiciosClientes> OrdenServicio(string seleccion)
        {
           
            List<ServiciosClientes> lista = new List<ServiciosClientes>();
            ServiciosClientes servicio = null;
            SqlConnection conexion = conectar();
            if (seleccion == "-Seleccionar-")
            {
                SqlCommand sentencia1 = new SqlCommand("select Servicio.idservicio,Servicio.fecha,Cliente.nombre, Cliente.apellido from Servicio INNER JOIN Cliente ON Servicio.idcliente=Cliente.idcliente order by Servicio.idservicio", conexion);
                SqlDataReader reader = sentencia1.ExecuteReader();
                while (reader.Read())
                {
                    servicio = new ServiciosClientes();
                    servicio.idservicio = (int)reader["idservicio"];
                    servicio.nombreCli = reader["nombre"].ToString() + " " + reader["apellido"].ToString();
                    servicio.fechaServ = (DateTime)reader["fecha"];

                    lista.Add(servicio);
                }
                reader.Close();
                conexion.Close();
            }
            if (seleccion == "Por Nombre A-Z")
            {
                SqlCommand sentencia1 = new SqlCommand("select Servicio.idservicio,Servicio.fecha,Cliente.nombre, Cliente.apellido from Servicio INNER JOIN Cliente ON Servicio.idcliente=Cliente.idcliente order by Cliente.nombre", conexion);
                SqlDataReader reader = sentencia1.ExecuteReader();
                while (reader.Read())
                {
                    servicio = new ServiciosClientes();
                    servicio.idservicio = (int)reader["idservicio"];
                    servicio.nombreCli = reader["nombre"].ToString() + " " + reader["apellido"].ToString();
                    servicio.fechaServ = (DateTime)reader["fecha"];

                    lista.Add(servicio);
                }
                reader.Close();
                conexion.Close();
            }
            if (seleccion == "Por Fecha Descendente")
            {
                SqlCommand sentencia1 = new SqlCommand("select Servicio.idservicio,Servicio.fecha,Cliente.nombre, Cliente.apellido from Servicio INNER JOIN Cliente ON Servicio.idcliente=Cliente.idcliente order by Servicio.fecha desc", conexion);
                SqlDataReader reader = sentencia1.ExecuteReader();
                while (reader.Read())
                {
                    servicio = new ServiciosClientes();
                    servicio.idservicio = (int)reader["idservicio"];
                    servicio.nombreCli = reader["nombre"].ToString() + " " + reader["apellido"].ToString();
                    servicio.fechaServ = (DateTime)reader["fecha"];

                    lista.Add(servicio);
                }
                reader.Close();
                conexion.Close();
            }

            return lista;
        }
        ///// ELEGIR TIPO DE CLIENTE /////


        public static List<Fotografos> Listafotografosasignado = new List<Fotografos>();

        //METODO QUE USAMOS EN EL VERIFICAR!
        public static String MostrarPrecio(Servicio servicio)
        {
            SqlConnection conexion = conectar();

            SqlCommand sentencia1 = new SqlCommand("select precio from Servicio where idservicio =" + servicio.IdServicio, conexion);
            SqlDataReader reader = sentencia1.ExecuteReader();
            while (reader.Read())
            {
                servicio.precios = (int)reader["precio"];
            }
            reader.Close();
            conexion.Close();
            return servicio.precios.ToString();
        }

    }
    }





    /*        protected void GridTripulantes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        String cedula = e.Values["cedula"].ToString();

        for (int i = 0; i < Global.transitoMaritimo.tripulantes.Count; i++)
        {
            if (Global.transitoMaritimo.tripulantes[i].cedula == Convert.ToInt32(cedula))
            {
                LabelError.Text = "Se borro el tripulante " + Global.transitoMaritimo.tripulantes[i].nombre;
                Global.transitoMaritimo.tripulantes.Remove(Global.transitoMaritimo.tripulantes[i]);                   
                break;
            }
        }

        Persistencia.RegistroCambio(Global.transitoMaritimo.idUsuario, "Borrar tripulantes");
        Persistencia.guardarDatos();
        Persistencia.tipostripulantes();

        GridTripulantes.DataBind();
    }*/


    /*
        public static Persona buscar(int id)
        {
            Persona resultado = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("select *  from personas  where id=" + id.ToString(), conexion);
            SqlDataReader reader = sentencia1.ExecuteReader();

            while (reader.Read())
            {
                resultado = new Persona();
                resultado.id = id;
                resultado.nombre = reader["nombre"].ToString();
                resultado.apellido = reader["apellido"].ToString();
                resultado.email = reader["email"].ToString();
                resultado.sexo = reader["sexo"].ToString();
            }

            reader.Close();
            conexion.Close();

            return resultado;
        }


        public static int guardar(Persona per)
        {
            SqlConnection conexion = conectar();
            SqlCommand sentencia1 = new SqlCommand("update personas  set nombre = @nombre , apellido = @apellido, email=@email, sexo=@sexo  where id=" + per.id.ToString(), conexion);
            sentencia1.Parameters.AddWithValue("@nombre", per.nombre);
            sentencia1.Parameters.AddWithValue("@apellido", per.apellido);
            sentencia1.Parameters.AddWithValue("@email", per.email);
            sentencia1.Parameters.AddWithValue("@sexo", per.sexo);
            int cont = sentencia1.ExecuteNonQuery();
            //si contador esta en cero, signfica que no se modificaron registros en la tabla

            if (cont == 0)
            {

                sentencia1 = new SqlCommand("insert into personas (id,nombre,apellido,email,sexo)  values (@id, @nombre.@apellido,@email,@sexo)", conexion);
                sentencia1.Parameters.AddWithValue("@id", per.id);
                sentencia1.Parameters.AddWithValue("@nombre", per.nombre);
                sentencia1.Parameters.AddWithValue("@apellido", per.apellido);
                sentencia1.Parameters.AddWithValue("@email", per.email);
                sentencia1.Parameters.AddWithValue("@sexo", per.sexo);
                cont = sentencia1.ExecuteNonQuery();

            }
            conexion.Close();
            return cont;

        }
    }
    */
    ///////////////// METODO MOSTRAR PRECIO ////////////777777




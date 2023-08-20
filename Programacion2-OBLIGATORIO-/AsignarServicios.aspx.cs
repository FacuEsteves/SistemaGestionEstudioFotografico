using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca;

namespace Programacion2_OBLIGATORIO_
{
    public partial class AsignarServicios : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_mostrar.Text = "";

            string seleccion = ddOrden.SelectedValue;

            if (!IsPostBack)
            {
                drop_clientes.DataSource = Persistencia.listaclientes();
                drop_clientes.DataTextField = "nombrecompleto";
                drop_clientes.DataValueField = "IdCliente";
                drop_clientes.DataBind();
            }
            
            grid_fotografos.DataSource = Persistencia.listafotografo();
            grid_fotografos.DataBind();

            grid_Fseleccionados.DataSource = Persistencia.Listafotografosasignado;
            grid_Fseleccionados.DataBind();

            gridServicios.DataSource = Persistencia.OrdenServicio(seleccion);
            gridServicios.DataBind();
        }

        protected void btn_ingresar_Click(object sender, EventArgs e)
        {
            lbl_mostrar.Text = "";
            List<Cliente> clientes = Persistencia.listaclientes();
            Servicio servicio = new Servicio();
            DateTime fecha = DateTime.Parse(txt_fecha.Text);
            double preciofotografo = 0;
            double descuentocliente = 0;

            if (txt_codigo_servicio.Text == "")
            {
                lbl_mostrar.Text = "Ingrese codigo de servicio";
                return;
            }
            if(txt_fecha.Text == "")
            {
                lbl_mostrar.Text = "Ingresar fecha";
                return;
            }
            if (Txt_Duracion.Text == "")
            {
                lbl_mostrar.Text = "Ingrese duracion";
                return;
            }
            if (DD_Localidad.SelectedIndex == 0)
            {
                lbl_mostrar.Text = "Seleccione localizacion";
                return;
            }
            if (drop_clientes.SelectedIndex == 0)
            {
                lbl_mostrar.Text = "Seleccione cliente";
                return;
            }
            if (Persistencia.Listafotografosasignado.Count == 0)
            {
                lbl_mostrar.Text = "Falta asignar fotografos a este servicio";
                return;
            }

            String fechasql = fecha.ToString("yyyy-MM-dd"); // fecha al formato de sql 

            servicio.IdServicio = Convert.ToInt32(txt_codigo_servicio.Text);
            servicio.fecha = fecha;
            servicio.Localizacion = DD_Localidad.SelectedValue;
            servicio.duracion = Convert.ToDouble(Txt_Duracion.Text);
            servicio.IdCliente = Convert.ToInt32(drop_clientes.SelectedValue);
            servicio.fotografos = Persistencia.Listafotografosasignado;

            for(int i = 0; i < servicio.fotografos.Count; i++)
            {
                preciofotografo = preciofotografo + servicio.fotografos[i].precio;
            }

            for(int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].IdCliente == servicio.IdCliente)
                {
                    if (clientes[i].tipoCliente == "Empresa")
                    {
                        double descuento = Convert.ToDouble(clientes[i].Descuento);

                        descuentocliente = descuento/100;//no calcula descuento
                    }
                    else
                    {
                        if (clientes[i].tipoParticular == "VIP")
                        {
                            descuentocliente = 0.10;
                        }
                    }
                    break;
                }
                
            }

            if(DD_Localidad.SelectedValue == "Externo")
            {
                double precioservicio = preciofotografo * servicio.duracion;
                double sumaexterior = precioservicio * 0.20;
                double descClienteFinal = precioservicio * descuentocliente;
                servicio.precios = Convert.ToInt32(precioservicio + sumaexterior + descClienteFinal); 
            }
            else
            {
                double precioservicio = preciofotografo * servicio.duracion;
                double descClienteFinal = precioservicio * descuentocliente;
                servicio.precios = Convert.ToInt32(precioservicio + descClienteFinal);
            }
            
            Persistencia.guardarservicio(servicio);
            Persistencia.Listafotografosasignado = new List<Fotografos>();
            grid_fotografos.DataBind();
            grid_Fseleccionados.DataBind();
            gridServicios.DataBind();
        }


        protected void grid_fotografos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = grid_fotografos.SelectedIndex;
            List<Fotografos> Listafotografosparaasignar = new List<Fotografos>();
            Listafotografosparaasignar = Persistencia.listafotografo();

            int idfotografo = Listafotografosparaasignar[index].idFotografo;

            for (int i = 0; i < Persistencia.Listafotografosasignado.Count; i++)
            {
                if (idfotografo == Persistencia.Listafotografosasignado[i].idFotografo)
                {
                    lbl_mostrar.Text = "Este fotografo ya se encuentra asignado";
                    return;
                }
            }
            Persistencia.Listafotografosasignado.Add(Listafotografosparaasignar[index]);
            Listafotografosparaasignar.Remove(Listafotografosparaasignar[index]);

            grid_Fseleccionados.DataSource = Persistencia.Listafotografosasignado;
            grid_Fseleccionados.DataBind();
            grid_fotografos.DataBind();

        }

        protected void grid_Fseleccionados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String id = e.Values["idFotografo"].ToString();
            List<Fotografos> lista = Persistencia.listafotografo();
            Fotografos fotografos = new Fotografos();

            for(int i = 0; i < lista.Count; i++)
            {
                if (lista[i].idFotografo == Convert.ToInt32(id))
                {
                    fotografos = lista[i];
                    break;
                }
            }
            for(int i = 0; i < Persistencia.Listafotografosasignado.Count; i++)
            {
                if (Persistencia.Listafotografosasignado[i].idFotografo == fotografos.idFotografo)
                {
                    Persistencia.Listafotografosasignado.Remove(Persistencia.Listafotografosasignado[i]);
                    break;
                }
            }
            
            grid_Fseleccionados.DataBind();

        }


        protected void ddOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = ddOrden.SelectedValue;

            List<ServiciosClientes> a = new List<ServiciosClientes>();

            a = Persistencia.OrdenServicio(seleccion);

            gridServicios.DataSource = a;
            gridServicios.DataBind();
        }

        protected void btn_verificar_Click(object sender, EventArgs e)
        {
            if (txt_codigo_servicio.Text == "")
            {
                lbl_mostrar.ForeColor = System.Drawing.Color.Red;
                lbl_mostrar.Text = "Ingrese Numero de servicio";
                return;
            }

            Servicio servicio = new Servicio();
        

            servicio.IdServicio = Convert.ToInt32(txt_codigo_servicio.Text);
            String precio = Persistencia.MostrarPrecio(servicio);

            lbl_mostrar.Text = "El valor a pagar es $" + precio;


        }
    }
}
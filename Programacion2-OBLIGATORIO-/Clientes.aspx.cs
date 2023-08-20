using biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Programacion2_OBLIGATORIO_
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void drop_tipo_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_tipo_cliente.SelectedIndex == 0)//nada
            {
                lblTipo.Visible = false;
                lblRut.Visible = false;
                lblDesc.Visible = false;
                drop_cliente_particular.Visible = false;
                txt_rut.Visible = false;
                txt_descuento.Visible = false;
            }
            else if (drop_tipo_cliente.SelectedIndex == 1)//particular
            {
                lblTipo.Visible = true;
                lblRut.Visible = false;
                lblDesc.Visible = false;
                drop_cliente_particular.Visible = true;
                txt_rut.Visible = false;
                txt_descuento.Visible = false;
            }
            else
            {
                lblTipo.Visible = false;
                lblRut.Visible = true;
                lblDesc.Visible = true;
                drop_cliente_particular.Visible = false;
                txt_rut.Visible = true;
                txt_descuento.Visible = true;
            }
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();

            //cli.IdCliente = Convert.ToInt32(txt_id.Text);
            cli.nombre = txt_nombre.Text;
            cli.apellido = txt_apellido.Text;
            cli.telefono = Convert.ToInt32(txt_telefono.Text);
            cli.tipoCliente = drop_tipo_cliente.SelectedValue;

            if (drop_tipo_cliente.SelectedIndex == 2)//OPCION EMPRESA
            {   
                cli.Descuento = Convert.ToInt32(txt_descuento.Text);
                String rut = txt_rut.Text;
                if (rut.Length == 12)
                {
                    cli.RUT = rut;
                }
                else
                {
                    lbl_aviso.Text = "Ingrese un numero de rut valido";
                    return;
                }
                
                Persistencia.guardarcliente(cli);
            }
            else //OPCION particular
            {
                cli.tipoParticular = drop_cliente_particular.SelectedValue;
                Persistencia.guardarcliente(cli);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Programacion2_OBLIGATORIO_
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string a = "";
                txtPrecio.Text = Persistencia.ConsultaPrecio(a);
            }
            labelError.Text = "";
            labelError2.Text = "";
            gridFotos.DataSource = Persistencia.listafotografo();
            gridFotos.DataBind();
        }

        protected void btnGuardarFoto_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtPrecio.Text=="")
            {
                labelError.Text = "Campos faltantes para realizar el ingreso";
                return;
            }
            else 
            {
                Fotografos a = new Fotografos();
                a.nombre = txtNombre.Text;
                a.apellido = txtApellido.Text;
                a.precio = Convert.ToInt32(txtPrecio.Text);

                Persistencia.guardarfotografo(a);
                Persistencia.GuardarPrecio(a);
                gridFotos.DataSource = Persistencia.listafotografo();
                gridFotos.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "")
            {
                labelError2.Text = "Ingrese un valor valido";
                return;
            }
            else
            {
                Fotografos a = new Fotografos();
                a.precio = Convert.ToInt32(txtPrecio.Text);

                Persistencia.GuardarPrecio(a);
                gridFotos.DataSource = Persistencia.listafotografo();
                gridFotos.DataBind();
            }
        }

        protected void gridFotos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Values["idFotografo"]);

            Persistencia.BorrarFotografo(id);
            gridFotos.DataSource = Persistencia.listafotografo();
            gridFotos.DataBind();
        }
    }
}


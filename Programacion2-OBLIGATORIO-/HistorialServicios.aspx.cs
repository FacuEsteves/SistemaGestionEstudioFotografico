using biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Programacion2_OBLIGATORIO_
{
    public partial class CrearServicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            labelError.Text = "";

            if (txtFecha.Text == "")
            {
                labelError.Text = "Ingrese Una Fecha";
                return;
            }

            DateTime fechatexto = DateTime.Parse(txtFecha.Text);

            string año = fechatexto.Year.ToString();
            string dia = fechatexto.Day.ToString();
            string mes = fechatexto.Month.ToString();

            string fecha = (año + "/" + mes + "/" + dia);


            List<Servicio> a = new List<Servicio>();

            a = Persistencia.BuscarServicio(fecha);

            if (a.Count == 0)
            {
                gridHistorial.DataSource = "";
                gridHistorial.DataBind();
                labelError.ForeColor = System.Drawing.Color.Red;
                labelError.Text = "En esa fecha no se encontraron servicios,intente con otra";
                return;
            }
            else
            {
                labelError.ForeColor=System.Drawing.Color.Blue;
                labelError.Text = "Se encontraron los siguientes resultados: ";
                gridHistorial.DataSource = a;
                gridHistorial.DataBind();
            }
        }
    }
}
using Programacion2_OBLIGATORIO_;
using System;
using System.Collections.Generic;

namespace biblioteca
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public DateTime fecha { get; set; }
        public string Localizacion { get; set; }
        public int precios { get; set; }
        public double duracion { get; set; }
        public int IdCliente { get; set; }

        public List<Fotografos> fotografos = new List<Fotografos>();

    }
}

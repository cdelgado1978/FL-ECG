using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ECG.Models
{
    public class NuevoECGModel
    {
        [DisplayName("Paciente ID")]
        public int paciente_id { get; set; }

        [DisplayName("Nombre Paciente")]
        public string paciente_nombre { get; set; }

        [DisplayName("Fecha Nacimiento")]
        public DateTime fecha_nacimiento { get; set; }

        [DisplayName("Fecha Estudio")]
        public DateTime fecha_estudio { get; set; }

        [DisplayName("Motivo del estudio")]
        public int motivo_id { get; set; }

        [DisplayName("Técnico Responsable")]
        public string usuarioResponsable { get; set; }


        
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECG_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public paciente()
        {
            this.ecgs = new HashSet<ecg>();
        }
    
        public decimal paciente_id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public System.DateTime fecha_nacimiento { get; set; }
        public bool activo { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public string creado_por { get; set; }
        public Nullable<System.DateTime> fecha_ultimamodificacion { get; set; }
        public string modificado_por { get; set; }
        public string genero { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ecg> ecgs { get; set; }
    }
}

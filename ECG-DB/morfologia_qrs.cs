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
    
    public partial class morfologia_qrs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public morfologia_qrs()
        {
            this.ecg_detalle = new HashSet<ecg_detalle>();
        }
    
        public decimal morfologiaqrs_id { get; set; }
        public string morfologia_qrs1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ecg_detalle> ecg_detalle { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DecisionSupportSystem.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class criteriaSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public criteriaSet()
        {
            this.crit_scaleSet = new HashSet<crit_scaleSet>();
            this.crit_valueSet = new HashSet<crit_valueSet>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public double rank { get; set; }
        public string relative_criteria { get; set; }
        public string user_mark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crit_scaleSet> crit_scaleSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crit_valueSet> crit_valueSet { get; set; }
        public virtual taskSet taskSet { get; set; }
    }
}

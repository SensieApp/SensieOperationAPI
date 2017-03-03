using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SensieOperationAPI.Models
{
    public partial class ActivationCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActivationCode()
        {
            Modules = new HashSet<Module>();
            Activations = new HashSet<Activation>();
        }

        public int ActivationCodeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Tag { get; set; }

        public int MaxNumInstances { get; set; }

        public int NumInstances { get; set; }

        public int? Duration { get; set; }

        public bool Enabled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Module> Modules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activation> Activations { get; set; }
    }
}

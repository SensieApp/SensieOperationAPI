using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensieOperationAPI.Models
{
    public partial class Activation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActivationCodeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpeditionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }

        [StringLength(300)]
        public string Referencias { get; set; }

        public virtual ActivationCode ActivationCode { get; set; }

        public virtual User User { get; set; }
    }
}

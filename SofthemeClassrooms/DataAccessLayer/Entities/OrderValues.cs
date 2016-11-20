namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sales.OrderValues")]
    public partial class OrderValues
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShipperId { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime OrderDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? val { get; set; }
    }
}

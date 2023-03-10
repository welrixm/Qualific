//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Qualific.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductOrder = new HashSet<ProductOrder>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public byte[] MainImage { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateIfAddition { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> UnitId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual Unit Unit { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JustBakery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Покупатели
    {
        public Покупатели()
        {
            this.Журнал_учета_продукции = new HashSet<Журнал_учета_продукции>();
        }
    
        public System.Guid ID_Покупателя { get; set; }
        public System.Guid ID_Личности { get; set; }
        public int Сумма_на_счете { get; set; }
    
        public virtual Личности Личности { get; set; }
        public virtual ICollection<Журнал_учета_продукции> Журнал_учета_продукции { get; set; }
    }
}

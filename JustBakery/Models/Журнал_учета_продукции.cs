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
    
    public partial class Журнал_учета_продукции
    {
        public Журнал_учета_продукции()
        {
            this.Состав_операций_по_продукции = new HashSet<Состав_операций_по_продукции>();
        }
    
        public System.Guid ID_Записи_журнала_продукции { get; set; }
        public System.Guid ID_Типа_операции { get; set; }
        public System.Guid ID_Склада { get; set; }
        public Nullable<System.Guid> ID_Покупателя { get; set; }
        public Nullable<System.Guid> ID_Сотрудника { get; set; }
        public System.DateTime Дата_операции { get; set; }
        public bool Удалена { get; set; }
    
        public virtual Cклады Cклады { get; set; }
        public virtual Покупатели Покупатели { get; set; }
        public virtual Сотрудники Сотрудники { get; set; }
        public virtual Справочник_типов_операций Справочник_типов_операций { get; set; }
        public virtual ICollection<Состав_операций_по_продукции> Состав_операций_по_продукции { get; set; }
    }
}

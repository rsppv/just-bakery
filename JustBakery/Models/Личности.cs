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
    
    public partial class Личности
    {
        public Личности()
        {
            this.Аккаунты = new HashSet<Аккаунты>();
            this.Покупатели = new HashSet<Покупатели>();
            this.Поставщики = new HashSet<Поставщики>();
            this.Сотрудники = new HashSet<Сотрудники>();
        }
    
        public System.Guid ID_Личности { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime Дата_рождения { get; set; }
        public string Адрес { get; set; }
        public string Телефон { get; set; }
    
        public virtual ICollection<Аккаунты> Аккаунты { get; set; }
        public virtual ICollection<Покупатели> Покупатели { get; set; }
        public virtual ICollection<Поставщики> Поставщики { get; set; }
        public virtual ICollection<Сотрудники> Сотрудники { get; set; }
    }
}

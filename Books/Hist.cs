//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Books
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hist
    {
        public int HistId { get; set; }
        public int HistBookId { get; set; }
        public int HistSubsId { get; set; }
        public System.DateTime HistDate { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Sub Sub { get; set; }
    }
}

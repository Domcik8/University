//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Muzika
{
    using System;
    using System.Collections.Generic;
    
    public partial class Priklauso
    {
        public short PriklausoNr { get; set; }
        public string Albumo_Pavadinimas { get; set; }
        public string Dainos_Pavadinimas { get; set; }
    
        public virtual Albumas OAlbumas { get; set; }
        public virtual Daina ODaina { get; set; }
    }
}

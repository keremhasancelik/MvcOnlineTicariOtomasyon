using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Mesajlar
    {
        [Key]
        public int MesajID { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(50)]
        public string Gonderen { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Alan { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(3000)]
        public string İcerik { get; set; }

        [Column(TypeName = "Smalldatetime")]
        public DateTime Tarih { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string MesajGorsel { get; set; }


    }
}
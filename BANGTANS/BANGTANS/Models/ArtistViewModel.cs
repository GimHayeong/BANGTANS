using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BANGTANS.Models
{
    public class ArtistViewModel
    {
        public int Id { get; set; }

       // 아티스트명
       [Required(AllowEmptyStrings = false)]
       [Display(Name = "아티스트명")]
        public string Name { get; set; }

        // 데뷔일
        [Required]
        [Display(Name = "데뷔일")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DebutDate { get; set; }

        // 설명
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "설명")]
        public string Description { get; set; }

        // 멤버목록
        public virtual ICollection<MemberViewModel> Members { get; set; }

        // 앨범목록
        public virtual ICollection<AlbumViewModel> Albums { get; set; }
    }

}
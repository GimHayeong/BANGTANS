using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BANGTANS.Models
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        // 예명
        [Display(Name = "예명")]
        public string StageName { get; set; }

        // 본명
        [Required]
        [Display(Name = "본명")]
        public string Name { get; set; }

        // 역할
        [Required]
        [Display(Name = "역할")]
        public string Role { get; set; }

        // 앨범 이미지 URL
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "멤버이미지")]
        public string ImageUrl { get; set; }

        // 생년월일
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "생년월일")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirtyDay { get; set; }

        // 키
        [Display(Name = "키")]
        public int Height { get; set; }

        // 몸무게
        [Display(Name = "몸무게")]
        public int Weight { get; set; }

        // 아티스트(그룹)아이디
        [Display(Name = "소속")]
        public int ArtistId  { get; set; }

        // 아티스트(그룹)정보
        public virtual ArtistViewModel Artist { get; set; }
    }
}
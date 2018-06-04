using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BANGTANS.Models
{
    public class AlbumViewModel
    {
        // 앨범 아이디
        public int Id { get; set; }

        // 앨범 타이틀
        [Required]
        [Display(Name = "타이틀")]
        public string Title { get; set; }

        // 앨범 서브타이틀
        [Display(Name = "부제")]
        [DisplayFormat(NullDisplayText = "")]
        public string Subtitle { get; set; }

        // 앨범 발행일
        [Required]
        [Display(Name = "발행일")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleasedDate { get; set; }

        // 앨범 이미지 URL
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "앨범이미지")]
        public string ImageUrl { get; set; }

        // 앨범 설명
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "설명")]
        public string Description { get; set; }

        // 아티스트(그룹)아이디
        [Required]
        public int ArtistId { get; set; }

        // 아티스트(그룹)정보
        public virtual ArtistViewModel Artist { get; set; }

        // 수록곡목록
        public virtual ICollection<ArtistViewModel> Tracks { get; set; }
    }
}
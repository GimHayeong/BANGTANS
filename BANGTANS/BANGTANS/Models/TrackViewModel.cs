using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BANGTANS.Models
{
    public class TrackViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 곡제목
        /// </summary>
        [Required]
        [Display(Name = "제목")]
        public string Title { get; set; }

        /// <summary>
        /// 작사가
        /// </summary>
        [Required]
        [Display(Name = "작사가")]
        public string Writer { get; set; }

        /// <summary>
        /// 작곡가
        /// </summary>
        [Required]
        [Display(Name = "작곡가")]
        public string Composition { get; set; }

        /// <summary>
        /// 곡설명
        /// </summary>
        [Display(Name = "설명")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // 동영상 URL
        [Display(Name = "뮤직비디오")]
        public string VideoUrl { get; set; }
        
        public int AlbumId { get; set; }

        public virtual AlbumViewModel Album { get; set; }
    }
}
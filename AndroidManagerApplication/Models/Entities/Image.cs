using System;
using System.Collections.Generic;

namespace AndroidManagerApplication.Models.Entities
{
    public class Image: BaseEntity
    {
        public byte[] ImageData { get; set; }
        public virtual ICollection<Android> Androids { get; set; }

        public string Base64
        {
            get { return Convert.ToBase64String(ImageData); }
        }

        public Image()
        {
            Androids = new List<Android>();
            ImageData = new byte[] { };
        }
    }
}
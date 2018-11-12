using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoshuaWebAPI.Models
{
    public class InfoToAdd
    {
        /// <summary>
        /// Url where data is to be stored.
        /// </summary>
        public string URL { get; set; } = "";

        /// <summary>
        /// The root string list.
        /// </summary>
        public List<string> Root { get; set; } = null;

        /// <summary>
        /// The child string list.
        /// </summary>
        public List<string> Child { get; set; } = null;

        /// <summary>
        /// The node string list.
        /// </summary>
        public List<string> Node { get; set; } = null;
    }
}
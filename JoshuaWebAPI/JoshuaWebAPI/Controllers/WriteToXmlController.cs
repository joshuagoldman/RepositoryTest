using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JoshuaWebAPI.Models;

namespace JoshuaWebAPI.Controllers
{
    public class WriteToXmlController : ApiController
    {

        List<InfoToAdd> WriteToXml = new List<InfoToAdd>();

        /// <summary>
        /// Adding Data to write to xml to be saved in a database.
        /// </summary>
        public WriteToXmlController()
        {
            WriteToXml.Add(new InfoToAdd
            {
                URL = "C:/Users/jogo/Documents/git_Test/Countries.xml",
                Root = new List<string> { "Document", "Value", "Document" },
                Child = new List<string> { "Countries", "Value", "Countries" },
                Node = new List<string> { "Country", "Language", "Swedish" }
            });
        }

        /// <summary>
        /// Writing to Xml.
        /// </summary>
        /// <returns></returns>
        public List<InfoToAdd> Get()
        {
            return WriteToXml;
        }

        // GET: api/WriteToXml/5 

        // POST: api/WriteToXml
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WriteToXml/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WriteToXml/5
        public void Delete(int id)
        {
        }
    }
}

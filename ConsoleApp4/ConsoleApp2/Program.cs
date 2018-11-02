using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.XmlDoc;
using System.Xml.Linq;

namespace Main.InformationGather
{
    public class Program
    {
        static void Main(string[] args)
        {
            ExEmEl xml = new ExEmEl("C:/Users/jogo/Documents/git_Test/Countries.xml",
                                   ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(new List<string> { "Country", "Value", "Sweden" },
                              new List<string> { "Language", "Value", "Swedish" },
                              ExEmEl.Replicate.No);
            xml.WriteNodeToXml();
            //act
            //Assert

        }
    }
}

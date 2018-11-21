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
            xml.XmlSearchInfo("Country,Sweden",
                              "Language,Swedish",
                              ExEmEl.ReplicateOrNewTree.DontRepl);
            xml.WriteNodeToXml();
            //act
            //Assert

        }
    }
}

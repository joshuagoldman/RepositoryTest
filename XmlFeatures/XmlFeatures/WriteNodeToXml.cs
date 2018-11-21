using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        delegate bool OverWriteDelegate(Replicate x);
        
        OverWriteDelegate DoReplicate =
            (Replicate x) => x == Replicate.Yes ? true : false;

        XDocument Xdoc;

        FindXLocation fxl;

        public void WriteNodeToXml()
        {
            Xdoc = XDocument.Load(FilePath);
            fxl = new FindXLocation
            {
                Doc = doc,
                XDoc = Xdoc
            };
            var Result = ExistanceCheck();
            if (NeitherExist(Result))
            {
                if (!string.IsNullOrEmpty((FromRoot[0])))
                {

                }
            }
            else if (RootExists(Result))
            {
                var newEl = new XElement(Child[0],
                                new XAttribute("Value", Child[1]),
                                    new XElement(Node[0], Node[1]));

                fxl.FindByElement(FromRoot);
                fxl.ChildParentElement.Add(newEl);
                fxl.XDoc.Save(FilePath);
            }
            else if (ChildDoesExist(Result) ||
                     NodeDoesExist(Result) && DoReplicate(ReplicateChoice))
            {
                var newEl = new XElement(Child[1], Node[1]);

                fxl.FindByElement(Child);
                fxl.ChildParentElement.Add(newEl);
                fxl.XDoc.Save(FilePath);
            }
            else
            {
                throw new Exception("Node awith provided parent already exists. Allow Overwriting and retry.");
            }
        }
        ~ExEmEl() => Console.WriteLine("Finalizer Executing");
    }
}

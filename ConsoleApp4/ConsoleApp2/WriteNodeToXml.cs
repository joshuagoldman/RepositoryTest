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
            if (NeitherExist(Result) && FromRoot != null)
            {            
                var newEl = new XElement(Child[0],
                                new XAttribute(Child[1], Child[2]),
                                    new XElement(Node[0], Node[1]));

                fxl.FindByElement(FromRoot);
                fxl.ChildParentElement.Add(newEl);
                Xdoc.Save(FilePath);
            }
            if (ChildDoesExist(Result) || NodeDoesExist(Result) && DoReplicate(ReplicateChoice))
            {
                var newEl = new XElement(Node[1], Node[2]);

                if (NeitherExist(Result))
                {
                    Xdoc.Root.Add(newEl);
                }
                else if (NodeDoesExist(Result) && DoReplicate(ReplicateChoice))
                {
                    fxl.FindByElement(Node);
                }
                else
                {
                    fxl.FindByElement(Child);
                    fxl.ChildParentElement.Add(newEl);
                    Xdoc.Save(FilePath);
                }
            }
            else
            {
                throw new Exception("Node already exists. Allow replication and retry");
            }
        }
        ~ExEmEl() => Console.WriteLine("Finalizer Executing");
    }
}

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
        XDocument Xdoc;

        FindXLocation fxl;

        XElement newEl { get; set; }

        public void WriteNodeToXml()
        {
            Xdoc = XDocument.Load(FilePath);
            fxl = new FindXLocation
            {
                Doc = doc,
                XDoc = Xdoc,
                ParentAboveChildnoAttr = ParentAboveChildnoAttr ?? null
            };
            var Result = ExistanceCheck();
            if (NeitherExist(Result))
            {
                if (!string.IsNullOrEmpty((FromRoot[0])) &&
                    WriteTree(WriteTreeChoice))
                {
                    var newEl = new XElement(FromRoot[0],
                                    new XAttribute(FromRoot[1], FromRoot[2]),
                                        new XElement(Child[0],
                                            new XAttribute(Child[1], Child[2]),
                                                new XElement(Node[0], Node[1])));

                    fxl.XDoc.Root.Add(newEl);
                    fxl.XDoc.Save(FilePath);
                }
            }
            else if (RootExists(Result))
            {
                var newEl = new XElement(Child[0],
                                new XAttribute(Child[1], Child[2]),
                                    new XElement(Node[0], Node[1]));

                fxl.FindByElement(FromRoot);
                fxl.ChildParentElement.Add(newEl);
                fxl.XDoc.Save(FilePath);
            }
            else if (ChildDoesExist(Result) ||
                     NodeDoesExist(Result) && DoReplicate(ReplicateChoice))
            {
                var newEl = new XElement(Node[0], Node[1]);

                fxl.FindByElement(Child);
                fxl.ChildParentElement.Add(newEl);
                fxl.XDoc.Save(FilePath);
            }
            else
            {
                throw new NodeWithParentException("Node with provided parent already exists. Allow replication and retry.");
            }
        }
        ~ExEmEl() => Console.WriteLine("Finalizer Executing");
    }

    public class NodeWithParentException : Exception
    {
        public NodeWithParentException(string message)
            : base(message)
        {
        }
    }
}

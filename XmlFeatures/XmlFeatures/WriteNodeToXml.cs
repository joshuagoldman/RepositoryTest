using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlFeatures.XmlDoc
{
    public partial class ExEmEl
    {        

        public string ParentAboveChildnoAtt { get; set; }

        public void WriteNodeToXml()
        {
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

                    Find.XDoc.Root.Add(newEl);
                    Find.XDoc.Save(FilePath);
                }
            }
            else if (RootExists(Result))
            {
                var newEl = new XElement(Child[0],
                                new XAttribute(Child[1], Child[2]),
                                    new XElement(Node[0], Node[1]));

                Find.FindByElement(FromRoot);
                Find.ChildParentElement.Add(newEl);
                Find.XDoc.Save(FilePath);
            }
            else if (ChildDoesExist(Result) ||
                     NodeDoesExist(Result) && DoReplicate(ReplicateChoice))
            {
                var newEl = new XElement(Node[0], Node[1]);

                Find.FindByElement(Child);
                Find.ChildParentElement.Add(newEl);
                Find.XDoc.Save(FilePath);
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

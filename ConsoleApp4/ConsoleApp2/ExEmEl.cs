using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        public XmlDocument doc;

        public enum NewDocument { Yes, No }

        public enum DocumentChoice { CurrentDoc, NewDoc, ThrowException}

        public string FilePath { get; set; }

        NewDocument YesOrNo;

        public ExEmEl(string filepath, NewDocument yesorno = NewDocument.No )
        {
            FilePath = filepath;
            YesOrNo = yesorno;
            DocumentChoice Result = !File.Exists(FilePath) ?
                           Result = YesOrNo == NewDocument.No ?
                           DocumentChoice.ThrowException :
                           DocumentChoice.NewDoc :
                           DocumentChoice.CurrentDoc;

            if (Result == DocumentChoice.ThrowException)
            {
                throw new Exception("File does not exist. Allow overwriting and retry.");
            }

            else if (Result == DocumentChoice.NewDoc)
            {
                doc = doc ?? new XmlDocument();
                doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.Save(FilePath);
            }

            else if (Result == DocumentChoice.CurrentDoc)
            {
                if (doc == null)
                {
                    doc = doc ?? new XmlDocument();
                    doc.Load(FilePath);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace XmlFeatures.XmlDoc
{
    public partial class ExEmEl
    {
        public XDocument XDoc;

        public enum NewDocument { Yes, No }

        public enum DocumentChoice { CurrentDoc, NewDoc, ThrowException}

        public string FilePath { get; set; }

        NewDocument YesOrNo;

        public ExEmEl(string filepath = null, NewDocument yesorno = NewDocument.No, Stream stream = null )
        {
            FilePath = filepath;
            YesOrNo = yesorno;
            DocumentChoice Result = !File.Exists(FilePath) && stream == null ?
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
                XDoc = XDoc ?? new XDocument();
                XDoc.Declaration = new XDeclaration("1.0", "UTF-8", null);
                XDoc.Save(FilePath);
            }

            else if (Result == DocumentChoice.CurrentDoc)
            {
                if (XDoc == null)
                {
                    XDoc = XDoc ?? new XDocument();
                    XDoc = stream == null ? XDocument.Load(FilePath) : XDocument.Load(stream);                    
                }
            }
        }
    }
}

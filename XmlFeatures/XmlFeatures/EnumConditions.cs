using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        private enum ElementExistance { NoneExist, RootExists, ChildExists, NodeExists }

        delegate bool ExistanceDelegate(ElementExistance x);

        delegate bool ReplicateDelegate(ReplicateOrNewTree x);

        ExistanceDelegate NeitherExist;

        ExistanceDelegate RootExists;

        ExistanceDelegate ChildDoesExist;

        ExistanceDelegate NodeDoesExist;

        ReplicateDelegate DoReplicate;

        ReplicateDelegate WriteTree;

        private void EnumConditions()
        {
            NeitherExist =
                (ElementExistance x) => x == ElementExistance.NoneExist ? true : false;

            RootExists =
                (ElementExistance x) => x == ElementExistance.RootExists ? true : false;

            ChildDoesExist =
                (ElementExistance x) => x == ElementExistance.ChildExists ? true : false;

            NodeDoesExist =
                (ElementExistance x) => x == ElementExistance.NodeExists ? true : false;

            DoReplicate =
                (ReplicateOrNewTree x) => x == ReplicateOrNewTree.Repl ? true : false;

            WriteTree =
                (ReplicateOrNewTree x) => x == ReplicateOrNewTree.NewTree ? true : false;
        }
    }
}

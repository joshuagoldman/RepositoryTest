using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        private enum ElementExistance { NoneExist, RootExists, ChildExists, NodeExists }

        delegate bool ExistanceDelegate(ElementExistance x);

        ExistanceDelegate NeitherExist;

        ExistanceDelegate RootExists;

        ExistanceDelegate ChildDoesExist;

        ExistanceDelegate NodeDoesExist;


        private void EnumConditions()
        {
            NeitherExist =
                (ElementExistance x) => x == ElementExistance.NoneExist ? true : false;

            RootExists =
                (ElementExistance x) => x == ElementExistance.NoneExist ? true : false;

            ChildDoesExist =
                (ElementExistance x) => x == ElementExistance.ChildExists ? true : false;

            NodeDoesExist =
                (ElementExistance x) => x == ElementExistance.NodeExists ? true : false;
        }
    }
}

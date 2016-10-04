namespace Aisino.Fwkp.Xtgl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class TreeHelper
    {
        public static IEnumerable TraversalLeafNodes(TreeNode pNode)
        {
            Stack iteratorVariable0 = new Stack();
            iteratorVariable0.Push(pNode);
        Label_PostSwitchInIterator:;
            while (iteratorVariable0.Count > 0)
            {
                TreeNode iteratorVariable1 = iteratorVariable0.Pop() as TreeNode;
                if (iteratorVariable1.Nodes.Count > 0)
                {
                    foreach (TreeNode node in iteratorVariable1.Nodes)
                    {
                        iteratorVariable0.Push(node);
                    }
                }
                else
                {
                    yield return iteratorVariable1;
                    goto Label_PostSwitchInIterator;
                }
            }
        }

    }
}


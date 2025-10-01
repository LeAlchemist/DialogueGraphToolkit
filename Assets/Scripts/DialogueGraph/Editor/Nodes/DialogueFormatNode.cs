using System;
using UnityEngine;
using Unity.GraphToolkit.Editor;

namespace DialogueGraph.Editor.Nodes
{
    [Serializable]
    class DialogueFormatNode : DialogueGraphNode
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<string>("")
                .Build();
        }
    }
}
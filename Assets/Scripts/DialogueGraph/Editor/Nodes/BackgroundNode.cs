using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace DialogueGraph.Editor.Nodes
{
    [Serializable]
    class BackgroundNode : DialogueGraphNode
    {
        public const string IN_PORT_BACKGROUND_NAME = "Background";
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            AddInputOutputExecutionPorts(context);

            context.AddInputPort<Sprite>(IN_PORT_BACKGROUND_NAME)
                .Build();
        }
    }
}
using System;
using UnityEngine;
using Unity.GraphToolkit.Editor;

namespace DialogueGraph.Editor.Nodes
{
    [Serializable]
    class ActorNode : DialogueGraphNode
    {
        public const string PORT_ACTOR_NAME_NAME = "Actor Name";
        public const string PORT_ACTOR_IMAGE_NAME = "Actor Sprite";

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<string>(PORT_ACTOR_NAME_NAME)
                .Build();
            context.AddOutputPort<string>(PORT_ACTOR_NAME_NAME)
                .WithDisplayName(string.Empty)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
            context.AddInputPort<Sprite>(PORT_ACTOR_IMAGE_NAME)
                .Build();
            context.AddOutputPort<Sprite>(PORT_ACTOR_IMAGE_NAME)
                .WithDisplayName(string.Empty)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
        }
    }
}
using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
class StartNode : DialogueGraphNode
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort(EXECUTION_PORT_DEFAULT_NAME)
            .WithDisplayName(string.Empty)
            .WithConnectorUI(PortConnectorUI.Arrowhead)
            .Build();
    }

    public override void OnEnable()
    {
        Subtitle = "This is the start node";
        DefaultColor = Color.white;
    }
}
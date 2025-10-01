using System;
using Unity.GraphToolkit.Editor;

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
}
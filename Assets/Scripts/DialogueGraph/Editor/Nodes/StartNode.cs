using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal class StartNode : DialogueGraphNode
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort(OutputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();
    }
}
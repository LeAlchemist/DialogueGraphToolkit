using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal class EndNode : DialogueGraphNode
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort(InputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();
    }
}
using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal abstract class DialogueGraphNode : Node
{
    public const string InputPortName = "Input";
    public const string OutputPortName = "Output";

    protected void AddInputOutputExecutionPorts(IPortDefinitionContext context)
    {
        context.AddInputPort(InputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();

        context.AddOutputPort(OutputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();
    }

}
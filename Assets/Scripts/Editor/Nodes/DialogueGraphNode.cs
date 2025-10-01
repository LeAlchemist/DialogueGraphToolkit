using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal abstract class DialogueGraphNode : Node
{
    public const string EXECUTION_PORT_DEFAULT_NAME = "ExecutionPort";

    protected void AddInputOutputExecutionPorts(IPortDefinitionContext context)
    {
        context.AddInputPort(EXECUTION_PORT_DEFAULT_NAME)
            .WithDisplayName(string.Empty)
            .WithConnectorUI(PortConnectorUI.Arrowhead)
            .Build();

        context.AddOutputPort(EXECUTION_PORT_DEFAULT_NAME)
            .WithDisplayName(string.Empty)
            .WithConnectorUI(PortConnectorUI.Arrowhead)
            .Build();
    }
}
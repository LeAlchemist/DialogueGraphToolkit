using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal class DialogueNode : DialogueGraphNode
{
    public const string DialogueText = "Dialogue Text";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);

        context.AddInputPort<string>(DialogueText)
        .WithDisplayName(DialogueText)
        .WithDefaultValue("Dialogue Text Here")
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();
    }
}
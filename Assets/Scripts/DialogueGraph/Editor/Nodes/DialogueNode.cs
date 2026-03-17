using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Node("Node", "", "Dialogue")]
[Serializable]
class DialogueNode : Node
{
    public const string InPortName = "In";
    public const string OutPortName = "Out";
    public const string SpeakerPortName = "Speaker";
    public const string DialoguePortName = "Dialogue";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort(InPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();

        context.AddInputPort<string>(SpeakerPortName)
        .Build();

        context.AddInputPort<string>(DialoguePortName)
        .Build();

        context.AddOutputPort(OutPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();
    }

    public override void OnEnable()
    {
        Subtitle = "This is a Dialogue node";
        DefaultColor = Color.green;
    }
}
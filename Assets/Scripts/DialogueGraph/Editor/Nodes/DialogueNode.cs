using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
internal class DialogueNode : DialogueGraphNode
{
    public const string ActorName = "Actor Name";
    public const string ActorPortrait = "Actor Portrait";
    public const string ActorSprite = "Actor Sprite";
    public const string DialogueText = "Dialogue Text";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);

        context.AddInputPort<string>(ActorName)
        .WithDefaultValue(ActorName)
        .WithDisplayName(ActorName)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<Sprite>(ActorPortrait)
        .WithDisplayName(ActorPortrait)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<Sprite>(ActorSprite)
        .WithDisplayName(ActorSprite)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<string>(DialogueText)
        .WithDisplayName(DialogueText)
        .WithDefaultValue("Dialogue Text Here")
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();
    }
}
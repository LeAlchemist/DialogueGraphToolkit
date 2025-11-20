using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
internal class DialogueNode : DialogueGraphNode
{
    public enum ActorLocation
    {
        Left = 0,
        Center = 1,
        Right = 2,
    };
    public enum PortraitLocation
    {
        Left = 0,
        Right = 1,
    };
    public const string ActorName = "Actor Name";
    public const string ActorPortraitSprite = "Actor Portrait";
    public const string ActorPortraitLocation = "Actor Portrait Location";
    public const string ActorSprite = "Actor Sprite";
    public const string ActorSpriteLocation = "Actor Sprite Location";
    public const string DialogueText = "Dialogue Text";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);

        DialogueInputPorts(context);
    }

    protected void DialogueInputPorts(IPortDefinitionContext context)
    {
        context.AddInputPort<string>(ActorName)
        .WithDefaultValue(ActorName)
        .WithDisplayName(ActorName)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<Sprite>(ActorPortraitSprite)
        .WithDisplayName(ActorPortraitSprite)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<PortraitLocation>(ActorPortraitLocation)
        .WithDisplayName(ActorPortraitLocation)
        .Build();

        context.AddInputPort<Sprite>(ActorSprite)
        .WithDisplayName(ActorSprite)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        context.AddInputPort<ActorLocation>(ActorSpriteLocation)
        .WithDisplayName(ActorSpriteLocation)
        .Build();

        context.AddInputPort<string>(DialogueText)
        .WithDisplayName(DialogueText)
        .WithDefaultValue("Dialogue Text Here")
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();
    }
}
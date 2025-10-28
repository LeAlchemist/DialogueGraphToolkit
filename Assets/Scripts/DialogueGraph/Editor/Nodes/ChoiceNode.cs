using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
internal class ChoiceNode : DialogueGraphNode
{
    public const string ActorName = "Actor Name";
    public const string ActorPortrait = "Actor Portrait";
    public const string ActorSprite = "Actor Sprite";

    public const string DialogueText = "Dialogue Text";
    public const string DialogueChoice = "Choices";

    protected override void OnDefineOptions(IOptionDefinitionContext context)
    {
        context.AddOption<int>(DialogueChoice)
        .WithDisplayName($"{DialogueChoice}:")
        .Delayed()
        .WithDefaultValue(2)
        .Build();
    }

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort(InputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();

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

        var option = GetNodeOptionByName(DialogueChoice);
        option.TryGetValue(out int portCount);
        for (int i = 0; i < portCount; i++)
        {
            context.AddInputPort<string>($"{DialogueChoice}{i}")
            .WithDisplayName($"Choice {i}")
            .Build();

            context.AddOutputPort($"{DialogueChoice}{i}")
            .WithDisplayName(string.Empty)
            .WithConnectorUI(PortConnectorUI.Arrowhead)
            .Build();
        }
    }
}

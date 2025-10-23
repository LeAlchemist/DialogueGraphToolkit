using System;
using Unity.GraphToolkit.Editor;

[Serializable]
internal class ChoiceNode : DialogueGraphNode
{
    public const string DialogueChoice = "Choices";
    public const string DialogueText = "Dialogue Text";

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

        context.AddInputPort<string>(DialogueText)
        .WithDisplayName(DialogueText)
        .WithDefaultValue("Dialogue Text Here")
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();

        var option = GetNodeOptionByName(DialogueChoice);
        option.TryGetValue(out int portCount);
        for (int i = 0; i < portCount; i++)
        {
            context.AddInputPort<string>($"Choice Text {i}")
            .Build();

            context.AddOutputPort($"{OutputPortName}{i}")
            .WithDisplayName(string.Empty)
            .WithConnectorUI(PortConnectorUI.Arrowhead)
            .Build();
        }
    }
}

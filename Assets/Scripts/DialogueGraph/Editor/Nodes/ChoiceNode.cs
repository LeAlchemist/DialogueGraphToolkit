using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
internal class ChoiceNode : DialogueNode
{
    public const string DialogueChoice = "Choices";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddInputPort(InputPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();

        DialogueInputPorts(context);

        var portCount = 3;
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

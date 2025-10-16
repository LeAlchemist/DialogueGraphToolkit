using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
internal class BackgroundNode : DialogueGraphNode
{
    public const string BackgroundImage = "Background Image";
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);

        context.AddInputPort<Sprite>(BackgroundImage)
        .WithDisplayName(BackgroundImage)
        .WithConnectorUI(PortConnectorUI.Circle)
        .Build();
    }
}
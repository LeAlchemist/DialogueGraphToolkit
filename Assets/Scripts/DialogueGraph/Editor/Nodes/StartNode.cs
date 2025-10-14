using UnityEngine;
using Unity.GraphToolkit.Editor;
using System;

[Serializable]
public class StartNode : Node
{
    public const string OutPortName = "Out";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        context.AddOutputPort(OutPortName)
        .WithDisplayName(string.Empty)
        .WithConnectorUI(PortConnectorUI.Arrowhead)
        .Build();
    }
}
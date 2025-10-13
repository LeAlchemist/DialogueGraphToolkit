using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace DialogueGraph.Editor.Nodes
{
    [Serializable]
    class ChoiceNode : DialogueGraphNode
    {
        const string optionID = "portCount";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<int>(optionID)
                .WithDisplayName("Choices:")
                .WithDefaultValue(2)
                .Delayed()
                .Build();
        }
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort(EXECUTION_PORT_DEFAULT_NAME)
                .WithDisplayName(string.Empty)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();

            //defining output choice ports
            var option = GetNodeOptionByName(optionID);
            option.TryGetValue(out int portCount);
            for (int i = 0; i < portCount; i++)
            {
                context.AddInputPort<string>($"Choice Text {i}")
                    .Build();

                context.AddOutputPort($"Choice {i}")
                    .WithConnectorUI(PortConnectorUI.Arrowhead)
                    .Build();
            }
        }
    }
}
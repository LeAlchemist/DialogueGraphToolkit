using System;

[Serializable]
internal class WaitForInputNode : DialogueGraphNode
{
    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);
    }
}
using UnityEngine;
using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

[ScriptedImporter(1, DialogueGraph.AssetExtension)]
public class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DialogueGraph editorGraph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);

        if (editorGraph == null)
        {
            Debug.LogError($"Failed to load graph asset: {ctx.assetPath}");
            return;
        }

        RuntimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();
        var nodeIDMap = new Dictionary<INode, string>();

        foreach (var node in editorGraph.GetNodes())
        {
            nodeIDMap[node] = Guid.NewGuid().ToString();
        }

        var startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if (startNode != null)
        {
            var entryPort = startNode.GetOutputPorts().FirstOrDefault()?.firstConnectedPort;
            if (entryPort != null)
            {
                runtimeGraph.EntryNodeID = nodeIDMap[entryPort.GetNode()];
            }
        }

        foreach (var iNode in editorGraph.GetNodes())
        {
            if (iNode is StartNode) continue;

            var runtimeNode = new RuntimeDialogueNode
            {
                NodeID = nodeIDMap[iNode]
            };
            if (iNode is DialogueNode dialogueNode)
            {
                ProcessDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            }

            runtimeGraph.AllNodes.Add(runtimeNode);
        }

        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }

    private void ProcessDialogueNode(DialogueNode node, RuntimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName(DialogueNode.SpeakerPortName));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName(DialogueNode.DialoguePortName));

        var nextNodePort = node.GetOutputPortByName(DialogueNode.OutPortName)?.firstConnectedPort;
        if (nextNodePort != null)
        {
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
        }
    }

    private T GetPortValue<T>(IPort port)
    {
        T value = default;

        if (port.isConnected)
        {
            switch (port.firstConnectedPort.GetNode())
            {
                case IVariableNode variableNode:
                    variableNode.variable.TryGetDefaultValue<T>(out value);
                    return value;
                case IConstantNode constantNode:
                    constantNode.TryGetValue<T>(out value);
                    return value;
                default:
                    break;
            }
        }
        else
        {
            port.TryGetValue(out value);
        }

        return value;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, DialogueGraph.AssetExtension)]
internal class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var graph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);

        if (graph == null)
        {
            Debug.LogError($"Failed to load Dialogue Graph asset {ctx.assetPath}");
            return;
        }

        var startNode = graph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if (startNode == null)
        {
            return;
        }

        var runtimeAsset = ScriptableObject.CreateInstance<DialogueGraphRuntime>();
        var nextNode = GetNextNode(startNode);

        while (nextNode != null)
        {
            var runtimeNodes = TranslateNodestoRuntimeNodes(nextNode);
            runtimeAsset.nodes.AddRange(runtimeNodes);

            nextNode = GetNextNode(nextNode);
        }

        ctx.AddObjectToAsset("RuntimeAsset", runtimeAsset);
        ctx.SetMainObject(runtimeAsset);
    }

    private INode GetNextNode(INode currentNode)
    {
        var outputNode = currentNode.GetOutputPortByName(DialogueGraphNode.OutputPortName);
        var nextNodePort = outputNode.firstConnectedPort;
        var nextNode = nextNodePort?.GetNode();

        return nextNode;
    }

    static List<DialogueGraphNodeRuntime> TranslateNodestoRuntimeNodes(INode node)
    {
        var runtimeNodes = new List<DialogueGraphNodeRuntime>();
        switch (node)
        {
            case ActorNode actorNode:
                runtimeNodes.Add(new ActorNodeRuntime
                {
                    ActorName = GetInputPortValue<string>(actorNode.GetInputPortByName(ActorNode.ActorName)),
                    ActorPortrait = GetInputPortValue<Sprite>(actorNode.GetInputPortByName(ActorNode.ActorPortrait)),
                    ActorSprite = GetInputPortValue<Sprite>(actorNode.GetInputPortByName(ActorNode.ActorSprite))
                });
                break;
            case BackgroundNode backgroundNode:
                var nodeName = "Background Node";
                runtimeNodes.Add(new BackgroundNodeRuntime
                {
                    name = nodeName,
                    BackgroundImage = GetInputPortValue<Sprite>(backgroundNode.GetInputPortByName(BackgroundNode.BackgroundImage))
                });
                break;
            case DialogueNode dialogueNode:
                runtimeNodes.Add(new DialogueNodeRuntime
                {
                    DialogueText = GetInputPortValue<string>(dialogueNode.GetInputPortByName(DialogueNode.DialogueText))
                });
                break;
            default:
                throw new ArgumentException($"Unsupported node model type: {node.GetType()}");
        }
        return runtimeNodes;
    }

    static T GetInputPortValue<T>(IPort port)
    {
        T value = default;

        if (port.isConnected)
        {
            switch (port.firstConnectedPort.GetNode())
            {
                case IVariableNode variableNode:
                    variableNode.variable.TryGetDefaultValue<T>(out value);
                    break;
                case IConstantNode constantNode:
                    constantNode.TryGetValue<T>(out value);
                    break;
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
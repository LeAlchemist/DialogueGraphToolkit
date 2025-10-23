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
        var nodeMap = new Dictionary<INode, int>();

        // First pass: Create all runtime nodes (without connections)
        CreateRuntimeNodes(startNode, runtimeAsset, nodeMap);

        // Second pass: Set up connections using the indices
        SetupConnections(startNode, runtimeAsset, nodeMap);

        ctx.AddObjectToAsset("RuntimeAsset", runtimeAsset);
        ctx.SetMainObject(runtimeAsset);
    }

    void CreateRuntimeNodes(INode startNode, DialogueGraphRuntime runtimeGraph, Dictionary<INode, int> nodeMap)
    {
        var nodesToProcess = new Queue<INode>();
        nodesToProcess.Enqueue(startNode);

        while (nodesToProcess.Count > 0)
        {
            var currentNode = nodesToProcess.Dequeue();

            if (nodeMap.ContainsKey(currentNode))
                continue;

            var runtimeNodes = TranslateNodestoRuntimeNodes(currentNode);

            foreach (var runtimeNode in runtimeNodes)
            {
                nodeMap[currentNode] = runtimeGraph.nodes.Count;
                runtimeGraph.nodes.Add(runtimeNode);
            }

            // Queue up all connected nodes
            for (int i = 0; i < currentNode.outputPortCount; i++)
            {
                var port = currentNode.GetOutputPort(i);

                if (port.isConnected)
                {
                    nodesToProcess.Enqueue(port.firstConnectedPort.GetNode());
                }
            }
        }
    }

    void SetupConnections(INode startNode, DialogueGraphRuntime runtimeGraph, Dictionary<INode, int> nodeMap)
    {
        foreach (var kvp in nodeMap)
        {
            var editorNode = kvp.Key;
            var runtimeIndex = kvp.Value;
            var runtimeNode = runtimeGraph.nodes[runtimeIndex];

            for (int i = 0; i < editorNode.outputPortCount; i++)
            {
                var port = editorNode.GetOutputPort(i);

                if (port.isConnected && nodeMap.TryGetValue(port.firstConnectedPort.GetNode(), out int nextIndex))
                {
                    runtimeNode.NextNodeIndices.Add(nextIndex);
                }
            }
        }
    }

    static List<DialogueGraphNodeRuntime> TranslateNodestoRuntimeNodes(INode node)
    {
        var runtimeNodes = new List<DialogueGraphNodeRuntime>();
        switch (node)
        {
            case ActorNode actorNode:
                {
                    var nodeName = "Actor Node";
                    runtimeNodes.Add(new ActorNodeRuntime
                    {
                        name = nodeName,
                        ActorName = GetInputPortValue<string>(actorNode.GetInputPortByName(ActorNode.ActorName)),
                        ActorPortrait = GetInputPortValue<Sprite>(actorNode.GetInputPortByName(ActorNode.ActorPortrait)),
                        ActorSprite = GetInputPortValue<Sprite>(actorNode.GetInputPortByName(ActorNode.ActorSprite))
                    });
                }
                break;
            case BackgroundNode backgroundNode:
                {
                    var nodeName = "Background Node";
                    runtimeNodes.Add(new BackgroundNodeRuntime
                    {
                        name = nodeName,
                        BackgroundImage = GetInputPortValue<Sprite>(backgroundNode.GetInputPortByName(BackgroundNode.BackgroundImage))
                    });
                }
                break;
            case ChoiceNode choiceNode:
                {
                    var nodeName = "Choice Node";
                    runtimeNodes.Add(new ChoiceNodeRuntime
                    {
                        name = nodeName,
                        DialogueText = GetInputPortValue<string>(choiceNode.GetInputPortByName(ChoiceNode.DialogueText))
                    });
                }
                break;
            case DialogueNode dialogueNode:
                {
                    var nodeName = "Dialogue Node";
                    runtimeNodes.Add(new DialogueNodeRuntime
                    {
                        name = nodeName,
                        DialogueText = GetInputPortValue<string>(dialogueNode.GetInputPortByName(DialogueNode.DialogueText))
                    });
                }
                break;
            case StartNode startNode:
                {
                    var nodeName = "Start Node";
                    runtimeNodes.Add(new StartNodeRuntime
                    {
                        name = nodeName,
                    });
                }
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
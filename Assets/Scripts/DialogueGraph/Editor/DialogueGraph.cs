using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor;

[Serializable]
[Graph(AssetExtension)]
internal class DialogueGraph : Graph
{
    const string graphName = "Dialogue Graph";
    internal const string AssetExtension = "graph";

    [MenuItem("Assets/Create/Dialogue Graph")]
    static void CreateAssetFile()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DialogueGraph>(graphName);
    }

    public override void OnGraphChanged(GraphLogger graphLogger)
    {
        base.OnGraphChanged(graphLogger);

        CheckGraphErrors(graphLogger);
    }

    void CheckGraphErrors(GraphLogger graphLogger)
    {
        List<StartNode> startNodes = GetNodes().OfType<StartNode>().ToList();

        switch (startNodes.Count)
        {
            case 0:
                graphLogger.LogError("Add a StartNode in your Dialogue Graph", this);
                break;
            case >= 1:
                foreach (var startNode in startNodes.Skip(1))
                {
                    graphLogger.LogError("Dialogue Graph only supports one StartNode per graph, only the first one will be used", startNode);
                }
                break;
        }

        List<ChoiceNode> choiceNodes = GetNodes().OfType<ChoiceNode>().ToList();

        foreach (var choiceNode in choiceNodes)
        {
            if (choiceNode.GetOutputPorts().Count() >= 6)
            {
                graphLogger.LogWarning("Too many choices may create issue with UI elements", choiceNode);
            }
        }
    }
}
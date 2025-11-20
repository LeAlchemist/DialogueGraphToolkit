using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGraphDirector : MonoBehaviour
{
    [Header("Dialogue Graph")]
    public DialogueGraphRuntime DialogueGraph;
    [Header("Scene Reference")]
    public Image Background;
    public List<Image> ActorSpriteLocation;
    public List<Image> ActorPortraitLocation;
    public GameObject DialoguePanel;
    public TextMeshProUGUI ActorNameText;
    public TextMeshProUGUI DialogueText;
    [Header("Settings")]
    public float GlobalFadeDuration = 0.5f;
    public float GlobalTextDelayPerCharacter = 0.03f;

    private async void Start()
    {
        var backgroundNodeExecutor = new BackgroundNodeExecutor();
        var dialogueNodeExecutor = new DialogueNodeExecutor();

        foreach (var node in DialogueGraph.nodes)
        {
            switch (node)
            {
                case BackgroundNodeRuntime backgroundNode:
                    await backgroundNodeExecutor.ExecuteAsync(backgroundNode, this);
                    break;
                case DialogueNodeRuntime dialogueNode:
                    await dialogueNodeExecutor.ExecuteAsync(dialogueNode, this);
                    break;
                case StartNodeRuntime:
                    break;
                default:
                    Debug.LogWarning($"No executor found for node type: {node.GetType()}");
                    break;
            }
        }
    }
}
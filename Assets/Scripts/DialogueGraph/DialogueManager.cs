using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public RuntimeDialogueGraph RuntimeGraph;

    [Header("UI Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
    private RuntimeDialogueNode _currentNode;

    private void Start()
    {
        //check if ui is present
        if (DialoguePanel == null)
        {
            Debug.LogWarning("Dialogue Panel not present");
        }
        if (SpeakerNameText == null)
        {
            Debug.LogWarning("Speaker Name Text not present");
        }
        if (DialogueText == null)
        {
            Debug.LogWarning("Dialogue Text not present");
        }

        foreach (var node in RuntimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }

        if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID))
        {
            ShowNode(RuntimeGraph.EntryNodeID);
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null)
        {
            if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                ShowNode(_currentNode.NextNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void ShowNode(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            EndDialogue();
            return;
        }

        _currentNode = _nodeLookup[nodeID];

        if (DialoguePanel != null)
        {
            DialoguePanel.SetActive(true);
        }
        if (SpeakerNameText != null)
        {
            SpeakerNameText.SetText(_currentNode.SpeakerName);
        }
        if (DialogueText != null)
        {
            DialogueText.SetText(_currentNode.DialogueText);
        }

        Debug.Log($"{_currentNode.SpeakerName}: {_currentNode.DialogueText}");
    }

    private void EndDialogue()
    {
        if (DialoguePanel != null)
        {
            DialoguePanel.SetActive(false);
        }

        _currentNode = null;
    }
}
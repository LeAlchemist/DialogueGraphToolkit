using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueGraphDirector : MonoBehaviour
{
    [Header("Dialogue Graph")]
    public DialogueGraphRuntime DialogueGraph;
    public int currentNode;
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
    [Header("Inputs")]
    [SerializeField] InputActionReference next;

    public void Update()
    {
        switch (DialogueGraph.nodes[currentNode])
        {
            case StartNodeRuntime startNode:
                DialogueText.text = "";
                NextNode(currentNode);
                break;
            case EndNodeRuntime endNode:
                break;
            case WaitForInputNodeRuntime waitForInputNode:
                NextNodeInput(currentNode);
                break;
            case BackgroundNodeRuntime backgroundNode:
                Background.sprite = backgroundNode.BackgroundImage;
                NextNode(currentNode);
                break;
            case DialogueNodeRuntime dialogueNode:
                if (string.IsNullOrEmpty(dialogueNode.DialogueText))
                {
                    DialoguePanel.SetActive(false);
                    return;
                }

                //set actor name
                DialoguePanel.SetActive(true);
                ActorNameText.text = dialogueNode.ActorName;

                //set actor sprite
                foreach (var location in ActorSpriteLocation)
                    location.enabled = false;

                if (dialogueNode.ActorSprite != null)
                {
                    var img = ActorSpriteLocation[dialogueNode.ActorSpriteIndex];
                    img.enabled = true;
                    img.sprite = dialogueNode.ActorSprite;
                }

                //dialogue text
                DialogueText.text = dialogueNode.DialogueText;

                NextNodeInput(currentNode);
                break;
            default:
                Debug.LogWarning($"No executor found for node type: {DialogueGraph.nodes[currentNode].GetType()}");
                break;
        }
    }

    public void NextNode(int _currentNode)
    {
        currentNode = DialogueGraph.nodes[_currentNode].NextNode.FirstOrDefault();
    }

    void NextNodeInput(int _currentNode)
    {
        if (next.action != null)
        {
            next.action.performed += context =>
            {
                if (context.ReadValueAsButton() == true)
                {
                    currentNode = DialogueGraph.nodes[_currentNode].NextNode.FirstOrDefault();
                }
            };
        }
    }

    public void NextNodeChoice(int _currentNode)
    {

    }
}
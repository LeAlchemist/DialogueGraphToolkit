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
    public int CurrentNode;
    [Header("Scene Reference")]
    public Image Background;
    public List<Image> ActorSpriteLocation;
    public List<Image> ActorPortraitLocation;
    public GameObject DialoguePanel;
    public TextMeshProUGUI ActorNameText;
    public TextMeshProUGUI DialogueText;
    public List<Button> ChoiceButtons;
    [Header("Settings")]
    public float GlobalFadeDuration = 0.5f;
    public float GlobalTextDelayPerCharacter = 0.03f;
    [Header("Inputs")]
    [SerializeField] InputActionReference next;

    public void Update()
    {
        switch (DialogueGraph.nodes[CurrentNode])
        {
            case StartNodeRuntime startNode:
                DialogueText.text = "";
                NextNode(CurrentNode);
                break;
            case EndNodeRuntime endNode:
                break;
            case WaitForInputNodeRuntime waitForInputNode:
                NextNodeInput(CurrentNode);
                break;
            case BackgroundNodeRuntime backgroundNode:
                Background.sprite = backgroundNode.BackgroundImage;
                NextNode(CurrentNode);
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

                if (DialogueGraph.nodes[CurrentNode].GetType().ToString() != "DialogueNodeRuntime")
                {
                    Debug.Log("supposed to add buttons here");
                    var button = ChoiceButtons[0];
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "test";
                    button.GetComponent<Button>().onClick.AddListener(() => NextNodeChoice(CurrentNode, 1, button));
                }
                else
                {
                    NextNodeInput(CurrentNode);
                }
                break;
            default:
                Debug.LogWarning($"No executor found for node type: {DialogueGraph.nodes[CurrentNode].GetType()}");
                break;
        }
    }

    public void NextNode(int _currentNode)
    {
        CurrentNode = DialogueGraph.nodes[_currentNode].NextNode.FirstOrDefault();
    }

    void NextNodeInput(int _currentNode)
    {
        if (next.action != null)
        {
            next.action.performed += context =>
            {
                if (context.ReadValueAsButton() == true)
                {
                    CurrentNode = DialogueGraph.nodes[_currentNode].NextNode.FirstOrDefault();
                }
            };
        }
    }

    public void NextNodeChoice(int _currentNode, int _choice, Button _button)
    {
        CurrentNode = DialogueGraph.nodes[_currentNode].NextNode[_choice];
        _button.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class DialogueNodeRuntime : DialogueGraphNodeRuntime
{
    public string ActorName;
    public Sprite ActorPortrait;
    public Sprite ActorSprite;
    public string DialogueText;
}
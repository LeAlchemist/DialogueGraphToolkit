using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class ChoiceNodeRuntime : DialogueGraphNodeRuntime
{
    public string ActorName;
    public Sprite ActorPortrait;
    public Sprite ActorSprite;
    public string DialogueText;
    public List<ChoiceData> Choices = new();
}

[Serializable]
public class ChoiceData
{
    public string ChoiceText;
}


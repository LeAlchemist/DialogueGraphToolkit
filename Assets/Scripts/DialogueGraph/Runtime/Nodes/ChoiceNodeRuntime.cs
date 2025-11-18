using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class ChoiceNodeRuntime : DialogueNodeRuntime
{
    public List<ChoiceData> Choices = new();
}

[Serializable]
public class ChoiceData
{
    public string ChoiceText;
}


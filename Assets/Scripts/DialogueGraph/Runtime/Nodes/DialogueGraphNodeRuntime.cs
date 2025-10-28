using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class DialogueGraphNodeRuntime
{
    [HideInInspector]
    public string name;
    public List<int> NextNode = new();
}
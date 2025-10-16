using System.Collections.Generic;
using UnityEngine;

public class DialogueGraphRuntime : ScriptableObject
{
    [SerializeReference]
    public List<DialogueGraphNodeRuntime> nodes = new();
}
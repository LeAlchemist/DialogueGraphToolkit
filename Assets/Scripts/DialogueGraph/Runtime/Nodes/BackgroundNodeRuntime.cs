using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class BackgroundNodeRuntime : DialogueGraphNodeRuntime
{
    [HideInInspector]
    public string name = "Background Node";
    public Sprite BackgroundImage;
}
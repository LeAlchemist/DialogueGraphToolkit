using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

[Serializable]
public class DialogueNodeRuntime : DialogueGraphNodeRuntime
{
    public string ActorName;
    public Sprite ActorPortrait;
    public int ActorPortraitIndex;
    public Sprite ActorSprite;
    public int ActorSpriteIndex;
    public string DialogueText;
}
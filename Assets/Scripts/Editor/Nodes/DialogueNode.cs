using System;
using UnityEditor;
using UnityEngine;
using Unity.GraphToolkit.Editor;

[Serializable]
class DialogueNode : DialogueGraphNode
{
    public const string IN_PORT_ACTOR_NAME_NAME = "Actor Name";
    public const string IN_PORT_ACTOR_SPRITE_NAME = "Actor Sprite";
    public const string IN_PORT_ACTOR_LOCATION_NAME = "Actor Location";
    public enum ActorLocation
    {
        Left,
        Middle,
        Right,
    }
    public const string IN_PORT_DIALOGUE_LOCATION_NAME = "Dialogue Box Location";
    public enum DialogueLocation
    {
        Top,
        Middle,
        Bottom,
    }
    public const string IN_PORT_DIALOGUE_NAME = "Dialogue";

    protected override void OnDefinePorts(IPortDefinitionContext context)
    {
        AddInputOutputExecutionPorts(context);

        context.AddInputPort<string>(IN_PORT_ACTOR_NAME_NAME)
            .Build();
        context.AddInputPort<Sprite>(IN_PORT_ACTOR_SPRITE_NAME)
            .Build();
        context.AddInputPort<ActorLocation>(IN_PORT_ACTOR_LOCATION_NAME)
            .Build();
        context.AddInputPort<DialogueLocation>(IN_PORT_DIALOGUE_LOCATION_NAME)
            .Build();
        context.AddInputPort<string>(IN_PORT_DIALOGUE_NAME)
            .Build();

        //TODO: Set up options or an input for font/styling
    }
}
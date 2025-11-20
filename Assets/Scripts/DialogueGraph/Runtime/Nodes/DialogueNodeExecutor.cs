using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogueNodeExecutor : IDialogueGraphNodeExecutor<DialogueNodeRuntime>
{
    public async Task ExecuteAsync(DialogueNodeRuntime node, DialogueGraphDirector ctx)
    {
        if (string.IsNullOrEmpty(node.DialogueText))
        {
            ctx.DialoguePanel.SetActive(false);
            return;
        }

        ctx.DialoguePanel.SetActive(true);
        ctx.ActorNameText.text = node.ActorName;

        foreach (var location in ctx.ActorSpriteLocation)
            location.enabled = false;

        if (node.ActorSprite != null)
        {
            var img = ctx.ActorSpriteLocation[node.ActorSpriteIndex];
            img.enabled = true;
            img.sprite = node.ActorSprite;
        }

        await TypeTextAsync(node.DialogueText, ctx);
    }

    public async Task TypeTextAsync(string dialogueText, DialogueGraphDirector ctx)
    {
        var label = ctx.DialogueText;
        var delayPerCharSeconds = ctx.GlobalTextDelayPerCharacter;

        label.text = "";
        var builder = new StringBuilder();

        var insideRichTag = false;

        foreach (var c in dialogueText)
        {
            // Handle rich text tags (e.g., <b>, </i>)
            if (c == '<')
                insideRichTag = true;

            builder.Append(c);

            if (c == '>')
                insideRichTag = false;

            // Skip delay if rich text
            if (insideRichTag || char.IsWhiteSpace(c)) continue;

            label.text = builder.ToString();

            var timer = 0f;
            while (timer < delayPerCharSeconds)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    label.text = dialogueText;
                    return;
                }
                timer += Time.deltaTime;
                await Task.Yield();
            }
        }

        label.text = dialogueText;
    }
}
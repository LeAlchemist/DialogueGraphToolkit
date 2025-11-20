using System.Threading.Tasks;

public class BackgroundNodeExecutor : IDialogueGraphNodeExecutor<BackgroundNodeRuntime>
{
    public async Task ExecuteAsync(BackgroundNodeRuntime node, DialogueGraphDirector ctx)
    {
        ctx.Background.sprite = node.BackgroundImage;
        await Task.Yield();
    }
}
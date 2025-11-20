using System.Threading.Tasks;

public interface IDialogueGraphNodeExecutor<in TNode> where TNode : DialogueGraphNodeRuntime
{
    Task ExecuteAsync(TNode node, DialogueGraphDirector ctx);
}
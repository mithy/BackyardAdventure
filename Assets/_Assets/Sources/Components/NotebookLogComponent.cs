using Entitas;

[Game]
public class NotebookLogComponent : IComponent {
	public NotebookPagesEnum Page;
	public string Text;
	public bool ShouldAppend;
}
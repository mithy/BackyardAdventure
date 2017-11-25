using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IndexableEntityComponent : IComponent {
	[EntityIndex]
	public string uuid;	
}
using Entitas;

[Game]
public class PlayerCollectInputComponent : IComponent {
	public CollectibleTypesEnum Type;
	public string UUID;
}
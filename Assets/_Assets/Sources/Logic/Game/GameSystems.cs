public class GameSystems : Feature {
	public GameSystems(Contexts contexts) : base("Game Systems") {
		Add(new LevelLoaderSystem(contexts));
		Add(new PickedObjectHandlerSystem(contexts));
		Add(new DroppedObjectHandlerSystem(contexts));
	}
}
public class GameSystems : Feature {
	public GameSystems(Contexts contexts) : base("Game Systems") {
		Add(new LevelLoaderSystem(contexts));
		Add(new MissionSystem(contexts));
		Add(new PickedObjectHandlerSystem(contexts));
		Add(new MovingObjectHandlerSystem(contexts));
		Add(new DroppedObjectHandlerSystem(contexts));
		Add(new ContainedObjectHandlerSystem(contexts));
		Add(new StarCollectHandlerSystem(contexts));
		Add(new NotebookLoggerSystem(contexts));
		Add(new BasketHoopValidatorSystem(contexts));
	}
}
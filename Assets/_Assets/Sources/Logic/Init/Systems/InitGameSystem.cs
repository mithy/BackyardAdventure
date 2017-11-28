using Entitas;

public class InitGameSystem : IInitializeSystem {

	private GameContext _gameContext;

	public InitGameSystem(Contexts contexts) {
		_gameContext = contexts.game;
	}

	public void Initialize() {
		GameEntity initGame = _gameContext.CreateEntity();
		initGame.AddLoadLevelTrigger(LevelsEnum.DayOne);
	}
}
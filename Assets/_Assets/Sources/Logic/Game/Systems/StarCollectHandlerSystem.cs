using System;
using System.Collections.Generic;
using Entitas;

public class StarCollectHandlerSystem : ReactiveSystem<GameEntity>, IInitializeSystem {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	public StarCollectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;
	}

	public void Initialize() {
		throw new NotImplementedException();
	}
	protected override void Execute(List<GameEntity> entities) {
		throw new NotImplementedException();
	}

	protected override bool Filter(GameEntity entity) {
		throw new NotImplementedException();
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		throw new NotImplementedException();
	}
}
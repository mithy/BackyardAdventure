using System.Collections.Generic;
using Entitas;

public class StarCollectHandlerSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private List<string> _collectedStars = new List<string>();

	public StarCollectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			if (!_collectedStars.Contains(entity.playerCollectInput.UUID)) {
				_collectedStars.Add(entity.playerCollectInput.UUID);
                _gameContext.globals.value.starView.UpdateAmount(_collectedStars.Count);
			}

			entity.RemovePlayerCollectInput();
		}

        if (_collectedStars.Count >= 21) {
            _gameContext.globals.value.fpsController.CanDoubleJump = true;
        }
    }

	protected override bool Filter(GameEntity entity) {
		return entity.hasPlayerCollectInput;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.PlayerCollectInput);
	}
}
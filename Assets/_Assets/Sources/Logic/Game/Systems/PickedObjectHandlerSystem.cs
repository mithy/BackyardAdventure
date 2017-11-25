using System.Collections.Generic;
using Entitas;

public class PickedObjectHandlerSystem : ReactiveSystem<GameEntity> {
	
	private GameContext _gameContext;

	private PlayerView _playerView;
	private PlayerView PlayerView {
		get {
			if (_playerView == null) {
				_playerView = _gameContext.globals.value.player.view.value.GetComponent<PlayerView>();		
			}

			return _playerView;
		}
	}

	public PickedObjectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();

			interactibleView.TogglePickedUp(true);
			interactibleView.transform.position = PlayerView.TargetPick.position;
		}
	}

	protected override bool Filter(GameEntity entity) {
		return entity.isPickedUp;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.PickedUp);
	}
}
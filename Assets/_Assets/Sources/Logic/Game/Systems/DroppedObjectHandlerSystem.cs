using System.Collections.Generic;
using Entitas;

public class DroppedObjectHandlerSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;

	public DroppedObjectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
	}
	
	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			PlayerView playerView = _gameContext.globals.value.player.view.value.GetComponent<PlayerView>();
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();

			interactibleView.TogglePickedUp(false);
			interactibleView.Throw(playerView.ForwardDirection, entity.dropped.Force * 1000);

			entity.RemoveDropped();
		}
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasDropped;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.Dropped);
	}
}
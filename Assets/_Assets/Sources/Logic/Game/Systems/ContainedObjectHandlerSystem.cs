using System.Collections.Generic;
using Entitas;

public class ContainedObjectHandlerSystem : ReactiveSystem<GameEntity> {

	private GameContext _gameContext;

	public ContainedObjectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
	}
	
	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();
			interactibleView.ToggleContainer(entity.containedObject.ParentEntity);

			if (interactibleView.Type == InteractibleTypesEnum.Fruit) {
				TriggerEvent(EventsEnum.FruitInCrate, -1);
			}
		}
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasContainedObject;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.ContainedObject);
	}

	private void TriggerEvent(EventsEnum evt, int index) {
		GameEntity entity = _gameContext.CreateEntity();
		entity.AddEventTrigger(evt, index);
	}
}
using System.Collections.Generic;
using Entitas;

public class GrabInputHandlerSystem : ReactiveSystem<InputEntity> {

	private GameContext _gameContext;

	public GrabInputHandlerSystem(Contexts contexts) : base(contexts.input) {
		_gameContext = contexts.game;
	}

	protected override void Execute(List<InputEntity> entities) {
		foreach (var input in entities) {
			ProcessInput(input);	
		}
	}

	protected override bool Filter(InputEntity entity) {
		return entity.hasGrabHandle;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
		return context.CreateCollector(InputMatcher.GrabHandle);
	}
	private void ProcessInput(InputEntity input) {
		if (input.hasGrabHandle && input.grabHandle.Target != null) {
			GameEntity entity = input.grabHandle.Target;
			entity.isPickedUp = true;
		}
	}
}
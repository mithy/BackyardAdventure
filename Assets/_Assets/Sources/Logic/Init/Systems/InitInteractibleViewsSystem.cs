using System;
using Entitas;

public class InitInteractibleViewsSystem : IInitializeSystem {

	private GameContext _gameContext;

	public InitInteractibleViewsSystem(Contexts contexts) {
		_gameContext = contexts.game;
	}

	public void Initialize() {
		InteractibleView[] interactibles = UnityEngine.Object.FindObjectsOfType(typeof(InteractibleView)) as InteractibleView[];

		foreach (var interactibleObject in interactibles) {
			string uuid = Guid.NewGuid().ToString();
			GameEntity entity = _gameContext.CreateEntity();

			entity.AddIndexableEntity(uuid);
			entity.isPickable = true;

			// Link view with model.
			interactibleObject.EntityLink.Link(entity);
		}
	}
}
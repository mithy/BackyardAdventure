using System;
using Entitas;

public class InitPlayerSystem : IInitializeSystem {

	private GameContext _gameContext;

	public InitPlayerSystem(Contexts contexts) {
		_gameContext = contexts.game;
	}

	public void Initialize() {
		PlayerView playerView = UnityEngine.Object.FindObjectOfType(typeof(PlayerView)) as PlayerView;

		string uuid = Guid.NewGuid().ToString();
		GameEntity entity = _gameContext.CreateEntity();

		entity.AddIndexableEntity(uuid);
		entity.isPlayer = true;
		//TODO: entity.AddPlayerClickInput(null);
		entity.AddView(playerView.gameObject);

		// Link view with model.
		playerView.EntityLink.Link(entity);
		_gameContext.globals.value.player = entity;
	}
}
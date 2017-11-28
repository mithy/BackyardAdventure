using Entitas;
using UnityEngine;

public class MovingObjectHandlerSystem : IExecuteSystem {

	private const float FORWARD_POWER = 30;
	private const float BACKWARD_POWER = 50;
	private const float SIDE_POWER = 45;

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

	private readonly IGroup<GameEntity> _movingObjects;

	public MovingObjectHandlerSystem(Contexts contexts) {
		_gameContext = contexts.game;

		_movingObjects = _gameContext.GetGroup(GameMatcher.Moving);
	}

	public void Execute() {
		GameEntity[] movingObjects = _movingObjects.GetEntities();

		foreach (var entity in movingObjects) {
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();


			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
				interactibleView.MoveObject(PlayerView.transform.forward, FORWARD_POWER);
			}

			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
				interactibleView.MoveObject(-PlayerView.transform.forward, BACKWARD_POWER);
			}

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
				interactibleView.MoveObject(-PlayerView.transform.right, SIDE_POWER);
			}

			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
				interactibleView.MoveObject(PlayerView.transform.right, SIDE_POWER);
			}
		}
	}
}
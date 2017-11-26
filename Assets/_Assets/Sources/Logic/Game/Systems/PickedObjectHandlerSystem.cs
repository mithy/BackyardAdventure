using Entitas;
using UnityEngine;

public class PickedObjectHandlerSystem : IExecuteSystem {

	private const float PICK_TRAVEL_TIME = 10.0f;
	
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

	private readonly IGroup<GameEntity> _pickedUpObjects;

	public PickedObjectHandlerSystem(Contexts contexts) {
		_gameContext = contexts.game;

		_pickedUpObjects = _gameContext.GetGroup(GameMatcher.PickedUp);
	}

	public void Execute() {
		GameEntity[] pickedUpObjects = _pickedUpObjects.GetEntities();

		foreach (var entity in pickedUpObjects) {
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();

			if (!interactibleView.IsPickedUp) {
				interactibleView.TogglePickedUp(true);
				entity.ReplacePickedUp(entity.pickedUp.InitialTime, entity.pickedUp.InitialPosition, 
				                       Vector3.Distance(entity.pickedUp.InitialPosition, PlayerView.TargetPick.position));
			}

			float distCovered = (Time.time - entity.pickedUp.InitialTime) * PICK_TRAVEL_TIME;
			float fracJourney = distCovered / entity.pickedUp.TotalTravelDistance;
			interactibleView.transform.position = Vector3.Lerp(entity.pickedUp.InitialPosition, PlayerView.TargetPick.position, fracJourney);
		}
	}
}
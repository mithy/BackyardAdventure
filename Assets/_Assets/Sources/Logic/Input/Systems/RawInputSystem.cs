using Entitas;
using UnityEngine;

public class RawInputSystem : IExecuteSystem, ICleanupSystem {

	private const int MOUSE_LEFT_BUTTON = 0;
	private const int MOUSE_RIGHT_BUTTON = 1;

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _unfilteredPlayerClickInput;
	private readonly IGroup<GameEntity> _pickedUpObjects;

	private float _timedThrow;

	public RawInputSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_unfilteredPlayerClickInput = _gameContext.GetGroup(GameMatcher.PlayerClickInput);
		_pickedUpObjects = _gameContext.GetGroup(GameMatcher.PickedUp);
	}

	public void Execute() {
		GameEntity[] pickedUpObjects = _pickedUpObjects.GetEntities();

		ProcessGrabInput(pickedUpObjects);
		ProcessThrowInput(pickedUpObjects);
	}

	public void Cleanup() {
		foreach (var unfilteredPlayerClickInput in _unfilteredPlayerClickInput.GetEntities()) {
			unfilteredPlayerClickInput.RemovePlayerClickInput();
		}
	}

	private void ProcessGrabInput(GameEntity[] pickedUpObjects) {
		bool isObjectPicked = pickedUpObjects.Length > 0;

		if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON) && !isObjectPicked) {
			foreach (var unfilteredInput in _unfilteredPlayerClickInput) {
				unfilteredInput.playerClickInput.Target.isPickedUp = true;
			}		}			
	}

	private void ProcessThrowInput(GameEntity[] pickedUpObjects) {
		bool isObjectPicked = pickedUpObjects.Length > 0;
		
		if (Input.GetMouseButton(MOUSE_RIGHT_BUTTON)) {
			foreach (var entity in pickedUpObjects) {
				entity.AddDropped(_timedThrow);
				entity.isPickedUp = false;
			}

			_timedThrow = Mathf.Clamp(_timedThrow, 0.0f, 1.5f);
			_timedThrow += Time.deltaTime;
		}

		if (Input.GetMouseButtonUp(MOUSE_RIGHT_BUTTON)) {
			_timedThrow = 0;
		}
	}
}
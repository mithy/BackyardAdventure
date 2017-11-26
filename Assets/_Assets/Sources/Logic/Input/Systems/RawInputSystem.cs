using Entitas;
using UnityEngine;

public class RawInputSystem : IExecuteSystem, ICleanupSystem {

	private const int MOUSE_LEFT_BUTTON = 0;
	private const int MOUSE_RIGHT_BUTTON = 1;
	private const float THROW_DELAY = 1.5f;

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _actionInput;
	private readonly IGroup<GameEntity> _pickedUpObjects;

	private float _timedThrow;

	public RawInputSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_actionInput = _gameContext.GetGroup(GameMatcher.PlayerActionInput);
		_pickedUpObjects = _gameContext.GetGroup(GameMatcher.PickedUp);
	}

	public void Execute() {
		GameEntity[] pickedUpObjects = _pickedUpObjects.GetEntities();

		ProcessActions();
		ProcessThrowInput(pickedUpObjects);
	}

	public void Cleanup() {
		foreach (var unfilteredPlayerClickInput in _actionInput.GetEntities()) {
		//	unfilteredPlayerClickInput.RemovePlayerClickInput();
		}
	}

	private void ProcessActions() {
		foreach (var action in _actionInput) {
			InteractionTypesEnum interactionType = action.playerActionInput.Interactible.InteractionType;

			switch (interactionType) {
				case InteractionTypesEnum.Pickable:
					if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON)) {
						ProcessPick(action.playerActionInput.Entity);
					}
					break;
			}
		}

	}

	private void ProcessPick(GameEntity entity) {
		// Prevent picking more than one object at the same time.
		if (_pickedUpObjects.count > 0) {
			return;
		}

		// If the object is out from a contained space, additional actions are required.
		if (entity.hasContainedObject) {
			entity.RemoveContainedObject();
			entity.view.value.GetComponent<InteractibleView>().ToggleContainer(null);
		}

		entity.AddPickedUp(Time.time, entity.view.value.transform.position, 0);
	}

	private void ProcessThrowInput(GameEntity[] pickedUpObjects) {
		bool isObjectPicked = pickedUpObjects.Length > 0;
		
		if (Input.GetMouseButton(MOUSE_RIGHT_BUTTON)) {
			_timedThrow %= THROW_DELAY;
			_timedThrow += Time.deltaTime;
		}

		if (Input.GetMouseButtonUp(MOUSE_RIGHT_BUTTON)) {
			foreach (var entity in pickedUpObjects) {
				entity.AddDropped(_timedThrow);
				entity.RemovePickedUp();
			}

			_timedThrow = 0;
		}

		_gameContext.globals.value.throwPower?.SetPower(_timedThrow);
	}
}
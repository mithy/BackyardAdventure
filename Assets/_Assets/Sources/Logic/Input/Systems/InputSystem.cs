using System;
using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem, ICleanupSystem {

	private const int MOUSE_LEFT_BUTTON = 0;
	private const int MOUSE_RIGHT_BUTTON = 1;
	private const float THROW_DELAY = 1.5f;
	private const float MAXIMUM_MOVE_DISTANCE = 3.5f;
    private const float ROTATE_SPEED = 100.0f;

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _actionInput;
	private readonly IGroup<GameEntity> _pickedUpObjects;
	private readonly IGroup<GameEntity> _movingObjects;

	private float _timedThrow;

	public InputSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_actionInput = _gameContext.GetGroup(GameMatcher.PlayerActionInput);
		_pickedUpObjects = _gameContext.GetGroup(GameMatcher.PickedUp);
		_movingObjects = _gameContext.GetGroup(GameMatcher.Moving);
	}

	public void Execute() {
		GameEntity[] pickedUpObjects = _pickedUpObjects.GetEntities();
		GameEntity[] movingObjects = _movingObjects.GetEntities();

        if (Input.GetKeyDown(KeyCode.Return)) {
            GameEntity advanceNextDayInput = _gameContext.CreateEntity();
            advanceNextDayInput.isPlayerAdvanceNextDayInput = true;
        }

		ProcessActions();
		ProcessThrowInput(pickedUpObjects);
		ProcessLetGo(movingObjects);
		ProcessScreenshot();
		ProcessQuit();

        UpdateUI(pickedUpObjects, movingObjects);
	}

	public void Cleanup() {
		foreach (var actionInputs in _actionInput.GetEntities()) {
			actionInputs.RemovePlayerActionInput();
		}
	}

	private void ProcessActions() {
		foreach (var action in _actionInput) {
			InteractionTypesEnum interactionType = action.playerActionInput.Interactible.InteractionType;
			InteractibleTypesEnum type = action.playerActionInput.Interactible.Type;

			switch (interactionType) {
				case InteractionTypesEnum.Pickable:
					if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON)) {
						ProcessPick(action.playerActionInput.Entity);
					}

					break;

				case InteractionTypesEnum.Movable:
					if (Input.GetMouseButtonDown(MOUSE_LEFT_BUTTON)) {
						ProcessMove(action.playerActionInput.Entity);
					}
					break;
			}			           
		}
	}

	private void ProcessPick(GameEntity entity) {
		// Prevent picking more than one object at the same time.
		if (_pickedUpObjects.count > 0 || _movingObjects.count > 0) {
			return;
		}

		// If the object is out from a contained space, additional actions are required.
		if (entity.hasContainedObject) {
			InteractibleView interactibleView = entity.view.value.GetComponent<InteractibleView>();
				
			entity.RemoveContainedObject();
			interactibleView.ToggleContainer(null);

            if (interactibleView.Type == InteractibleTypesEnum.Fruit) {
                TriggerEvent(EventsEnum.FruitInCrate, 1);
            }
        }

		entity.AddPickedUp(Time.time, entity.view.value.transform.position, 0);
	}

	private void ProcessMove(GameEntity entity) {
		// Prevent picking more than one object at the same time.
		if (_pickedUpObjects.count > 0 || _movingObjects.count > 0) {
			return;
		}

		GameObject player = _gameContext.globals.value.player.view.value;
		GameObject target = entity.view.value;

		if (Vector3.Distance(target.transform.position, player.transform.position) < MAXIMUM_MOVE_DISTANCE) {
			entity.isMoving = true;
		}
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

		if (isObjectPicked) {
			_gameContext.globals.value.throwPowerView?.SetPower(_timedThrow);
		}
	}

	private void ProcessLetGo(GameEntity[] movingObjects) {
		foreach (var entity in movingObjects) {
			if (Input.GetMouseButtonDown(MOUSE_RIGHT_BUTTON)) {
				entity.isMoving = false;
			} else {
				GameObject player = _gameContext.globals.value.player.view.value;
				GameObject target = entity.view.value;

				if (Vector3.Distance(target.transform.position, player.transform.position) > MAXIMUM_MOVE_DISTANCE) {
					entity.isMoving = false;
				}
			}
		}
	}

    private void UpdateUI(GameEntity[] pickedUpObjects, GameEntity[] movingObjects) {
		_gameContext.globals.value.actionHelperView.HideAll();

		if (pickedUpObjects.Length > 0) {
			_gameContext.globals.value.actionHelperView.TogglePickup();
		}

		if (movingObjects.Length > 0) {
			_gameContext.globals.value.actionHelperView.ToggleMove();
		}

        if (Input.GetKeyDown(KeyCode.Tab)) {
            _gameContext.globals.value.notebookView.Toggle(!_gameContext.globals.value.notebookView.IsShown);

            GameEntity alert = _gameContext.CreateEntity();
            alert.AddNotebookAlert(false);
        }
	}

    private void TriggerEvent(EventsEnum evt, int index) {
        GameEntity entity = _gameContext.CreateEntity();
        entity.AddEventTrigger(evt, index);
    }

	private void ProcessScreenshot() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            string path = Application.dataPath;
            path += "/Screenshot";
            path += DateTime.Now.ToString("yy-MM-dd-hh-mm-ss");
            path += ".png";

            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("Screen captured " + path);
        }
    }

	private void ProcessQuit() {
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)) {
			_gameContext.globals.value.exitExitView.ToggleVisibility(false);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (_gameContext.globals.value.exitExitView.IsActive) {
				Application.Quit();
			}

			_gameContext.globals.value.exitExitView.ToggleVisibility(true);
		}
	}
}
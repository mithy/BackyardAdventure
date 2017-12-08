using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class LevelLoaderSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _interactibleEntities;

	private TextHelper _textHelper;
	private LevelsEnum _sceneToLoad;
    private string _sceneToUnload;
    private int _maxLevels;

	public LevelLoaderSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

        _maxLevels = Enum.GetNames(typeof(LevelsEnum)).Length;
        _textHelper = _gameContext.globals.value.textHelper;

		_interactibleEntities = _gameContext.GetGroup(GameMatcher.Interactible);

		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
            if (entity.isLoadNextLevelTrigger) {
                ClearInteractibleObjects();

                if ((int) _sceneToLoad + 1 < _maxLevels) {
                    _sceneToLoad = _sceneToLoad + 1;
                    _sceneToUnload = _gameContext.globals.value.GetSceneForLevel(_sceneToLoad - 1);
                }
            }

            if (entity.hasLoadLevelTrigger) {
                if (_sceneToLoad != entity.loadLevelTrigger.Level) {
                    _sceneToLoad = entity.loadLevelTrigger.Level;
                    _sceneToUnload = string.Empty;
                }
            }

			if (_sceneToUnload != string.Empty) {
				SceneManager.UnloadSceneAsync(_sceneToUnload);
			}

            _gameContext.globals.value.sceneIntroView.FadeIn();
            LoadNewScene();

            entity.Destroy();
		}
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasLoadLevelTrigger || entity.isLoadNextLevelTrigger;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.LoadLevelTrigger, GameMatcher.LoadNextLevelTrigger));
	}

	private void LoadNewScene() {
		string sceneName = _gameContext.globals.value.GetSceneForLevel(_sceneToLoad);
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		_gameContext.globals.value.sceneIntroView.FadeIn();
	}

	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
		InitializeInteractibleObjects();
		PrepareForNewDay();

        SceneManager.SetActiveScene(scene);

		_gameContext.globals.value.sceneIntroView.Activate(_sceneToLoad, _gameContext.globals.value.missions.GetEventsForDay(_sceneToLoad)[EventsEnum.DayStarted].Text);
	}

	private void InitializeInteractibleObjects() {
		InteractibleView[] interactibles = UnityEngine.Object.FindObjectsOfType(typeof(InteractibleView)) as InteractibleView[];

		foreach (var interactible in interactibles) {
			string uuid = Guid.NewGuid().ToString();
			GameEntity entity = _gameContext.CreateEntity();

			entity.AddIndexableEntity(uuid);
			entity.isInteractible = true;
			entity.AddView(interactible.gameObject);

			switch (interactible.InteractionType) {
				case InteractionTypesEnum.Pickable:
					entity.isPickable = true;
					break;
			}

			// Link view with model.
			interactible.EntityLink.Link(entity);
		}
	}

	private void ClearInteractibleObjects() {
		GameEntity[] interactibleEntites = _interactibleEntities.GetEntities();

		foreach (GameEntity entity in interactibleEntites) {
			entity.Destroy();
		}
	}

	private void PrepareForNewDay() {
		GameEntity entity = _gameContext.CreateEntity();
		entity.AddEventTrigger(EventsEnum.DayStarted, (int) _sceneToLoad);

		GameEntity log = _gameContext.CreateEntity();
        log.isNotebookClear = true;
	}

	private string GetDayText() {
		string text = string.Empty;

		switch (_sceneToLoad) {
			case LevelsEnum.DayOne:
				text = "Welcome to the backyard";
				break;

			case LevelsEnum.DayTwo:
				text = "1-2 Basket";
				break;

			case LevelsEnum.DayThree:
				text = "The tricky generator";
				break;

            case LevelsEnum.DayFour:
                text = "The Windmill action";
                break;

            case LevelsEnum.DayFive:
                text = "A well deserved rest";
                break;
		}

		return _textHelper.GetTranslation(text);
	}
}
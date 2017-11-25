using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class LevelLoaderSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _interactibleEntities;

	public LevelLoaderSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_interactibleEntities = _gameContext.GetGroup(GameMatcher.Interactible);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			int currentLevelIndex = (int)entity.loadLevelTrigger.Level;
			string currentLevel = _gameContext.globals.value.GetSceneForLevel((LevelsEnum) currentLevelIndex);
			string previousLevel = currentLevelIndex > 1 ? _gameContext.globals.value.GetSceneForLevel((LevelsEnum)currentLevelIndex) : string.Empty;

			SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
			//SceneManager.UnloadSceneAsync();
		}
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasLoadLevelTrigger;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.LoadLevelTrigger);
	}

	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
		ClearInteractibleObjects();
		InitializeInteractibleObjects();
	}

	private void InitializeInteractibleObjects() {
		InteractibleView[] interactibles = UnityEngine.Object.FindObjectsOfType(typeof(InteractibleView)) as InteractibleView[];

		foreach (var interactibleObject in interactibles) {
			string uuid = Guid.NewGuid().ToString();
			GameEntity entity = _gameContext.CreateEntity();

			entity.AddIndexableEntity(uuid);
			entity.isPickable = true;
			entity.isInteractible = true;
			entity.AddView(interactibleObject.gameObject);

			// Link view with model.
			interactibleObject.EntityLink.Link(entity);
		}
	}

	private void ClearInteractibleObjects() {
		GameEntity[] interactibleEntites = _interactibleEntities.GetEntities();

		foreach (GameEntity entity in interactibleEntites) {
			entity.Destroy();
		}
	}
}
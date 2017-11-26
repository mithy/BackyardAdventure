using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class LevelLoaderSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _interactibleEntities;

	private LevelsEnum _currentLoadedLevel;

	public LevelLoaderSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_interactibleEntities = _gameContext.GetGroup(GameMatcher.Interactible);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			_currentLoadedLevel = entity.loadLevelTrigger.Level;

			int currentLevelIndex = (int)entity.loadLevelTrigger.Level;
			string currentLevel = _gameContext.globals.value.GetSceneForLevel((LevelsEnum) currentLevelIndex);
			string previousLevel = currentLevelIndex > 1 ? _gameContext.globals.value.GetSceneForLevel((LevelsEnum)currentLevelIndex) : string.Empty;

			SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
			//SceneManager.UnloadSceneAsync();

			_gameContext.globals.value.sceneIntro.FadeIn();
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

		_gameContext.globals.value.sceneIntro.Activate(_currentLoadedLevel);
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

	private void InitializeStars() {
		StarView[] stars = UnityEngine.Object.FindObjectsOfType(typeof(StarView)) as StarView[];

		foreach (var star in stars) {
			GameEntity entity = _gameContext.CreateEntity();

		}
	}

	private void ClearInteractibleObjects() {
		GameEntity[] interactibleEntites = _interactibleEntities.GetEntities();

		foreach (GameEntity entity in interactibleEntites) {
			entity.Destroy();
		}
	}
}
using System.Collections.Generic;
using Entitas;

public class StarCollectHandlerSystem : ReactiveSystem<GameEntity> {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private List<string> _collectedStars = new List<string>();

	public StarCollectHandlerSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			if (!_collectedStars.Contains(entity.playerCollectInput.UUID)) {
				_collectedStars.Add(entity.playerCollectInput.UUID);
				_gameContext.globals.value.notebookMessagesView.DisplayMessage("Star Collected");
			}

			entity.RemovePlayerCollectInput();
		}

        UpdateAchievements();
    }

	protected override bool Filter(GameEntity entity) {
		return entity.hasPlayerCollectInput;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.PlayerCollectInput);
	}

    private void UpdateAchievements() {
        string str = string.Empty;

        if (_collectedStars.Count < 21) {
            str = "Stars needed " + _collectedStars.Count + "/21";
        } else {
            str = "Stars Collected!!!";
        }

        GameEntity log = _gameContext.CreateEntity();
        log.AddNotebookLog(NotebookPagesEnum.Achivements, str, false);
    }
}
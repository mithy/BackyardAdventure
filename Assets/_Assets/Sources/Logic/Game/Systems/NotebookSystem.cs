using Entitas;

public class NotebookSystem : IExecuteSystem, ICleanupSystem {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

    private readonly IGroup<GameEntity> _clears;
    private readonly IGroup<GameEntity> _logs;
    private readonly IGroup<GameEntity> _alerts;

	private TextHelper _textHelper;

	public NotebookSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

        _clears = _gameContext.GetGroup(GameMatcher.NotebookClear);
        _logs = _gameContext.GetGroup(GameMatcher.NotebookText);
        _alerts = _gameContext.GetGroup(GameMatcher.NotebookAlert);

		_textHelper = _gameContext.globals.value.textHelper;
	}

	public void Execute() {
        GameEntity[] clearLogs = _clears.GetEntities();
		GameEntity[] logs = _logs.GetEntities();
        GameEntity[] alerts = _alerts.GetEntities();

        foreach (var clearLog in clearLogs) {
            ClearContent();
        }

		foreach (var log in logs) {
			SetContent(log.notebookText.Text);
		}

        foreach (var alert in alerts) {
            _gameContext.globals.value.notebookAlertsView.ToggleAlert(alert.notebookAlert.IsDisplayingAlert);
        }
	}

    public void Cleanup() {
        foreach (var clear in _clears.GetEntities()) {
            clear.Destroy();
        }

        foreach (var log in _logs.GetEntities()) {
            log.Destroy();
        }

        foreach (var alert in _alerts.GetEntities()) {
            alert.Destroy();
        }
    }

	private void SetContent(string text) {
        _gameContext.globals.value.notebookView.SetText(text);
	}

	private void ClearContent() {
        _gameContext.globals.value.notebookView.SetText(string.Empty);
    }   
}
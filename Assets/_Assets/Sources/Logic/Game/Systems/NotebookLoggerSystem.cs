using System.Collections.Generic;
using System.Text;using Entitas;
using UnityEngine;

public class NotebookLoggerSystem : IExecuteSystem {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private Dictionary<NotebookPagesEnum, string> _content = new Dictionary<NotebookPagesEnum, string>();

	private readonly IGroup<GameEntity> _logs;

	private NotebookPagesEnum _currentPage;
	private TextHelper _textHelper;

	public NotebookLoggerSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_logs = _gameContext.GetGroup(GameMatcher.NotebookLog);

		_currentPage = NotebookPagesEnum.Notes;
		_textHelper = _gameContext.globals.value.textHelper;
	}

	public void Execute() {
		GameEntity[] logs = _logs.GetEntities();

		foreach (var log in logs) {
			if (log.notebookLog.Page == NotebookPagesEnum.Clear) {
				Clear();
			} else {
				AddContent(log.notebookLog.Text, log.notebookLog.Page, log.notebookLog.ShouldAppend);
			}

			log.Destroy();
		}

		HandleInput();
		HandleHelperText();
	}

	private void HandleInput() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			_currentPage = NotebookPagesEnum.Notes;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			_currentPage = NotebookPagesEnum.Objectives;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			_currentPage = NotebookPagesEnum.Achivements;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			_currentPage = NotebookPagesEnum.Help;
		}
	}

	private void HandleHelperText() {
		StringBuilder sb = new StringBuilder();

		sb.Append(_currentPage == NotebookPagesEnum.Notes ? "> 1 " : "[1] ");
		sb.Append(_textHelper.GetTranslation("Notes"));
		sb.AppendLine();

		sb.Append(_currentPage == NotebookPagesEnum.Objectives ? "> 2 " : "[2] ");
		sb.Append(_textHelper.GetTranslation("Objectives"));
		sb.AppendLine();

		sb.Append(_currentPage == NotebookPagesEnum.Achivements ? "> 3 " : "[3]");
		sb.Append(_textHelper.GetTranslation("Achivements"));
		sb.AppendLine();

		sb.Append(_currentPage == NotebookPagesEnum.Help ? "> 4 " : "[4] ");
		sb.Append(_textHelper.GetTranslation("Help"));
		sb.AppendLine();

		_gameContext.globals.value.notebookView.SetHelperText(sb.ToString());

		if (_content.ContainsKey(_currentPage)) {
			_gameContext.globals.value.notebookView.SetText(_content[_currentPage]);
		}
	}

	private void AddContent(string text, NotebookPagesEnum page, bool shouldAppend) {
		_content[page] = shouldAppend ? _content[page] + "\n\n" + text : text;
	}

	private void Clear() {
		_content.Clear();

		_content.Add(NotebookPagesEnum.Clear, string.Empty);
		_content.Add(NotebookPagesEnum.Notes, string.Empty);
		_content.Add(NotebookPagesEnum.Objectives, string.Empty);
		_content.Add(NotebookPagesEnum.Achivements, string.Empty);
		_content.Add(NotebookPagesEnum.Help, _textHelper.Help);
	}
}
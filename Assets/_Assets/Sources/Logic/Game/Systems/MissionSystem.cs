using Entitas;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MissionSystem : ReactiveSystem<GameEntity>, ICleanupSystem {

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _eventTriggers;

	private Dictionary<EventsEnum, MissionsDB.Mission> _missions;
	private List<EventsEnum> _discoveredEvents = new List<EventsEnum>();

	private TextHelper _textHelper;

	public MissionSystem(Contexts contexts) : base(contexts.game) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_textHelper = _gameContext.globals.value.textHelper;

		_eventTriggers = _gameContext.GetGroup(GameMatcher.EventTrigger);
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			if (entity.eventTrigger.Evt == EventsEnum.DayStarted) {
				ProcessNewDay(entity.eventTrigger.Index);
			} else {
				ProcessEvent(entity.eventTrigger.Evt, entity.eventTrigger.Index);	
			}

            if (!entity.isInteractible) {
                entity.Destroy();
            } else {
                entity.RemoveEventTrigger();
            }
		}
	}

	public void Cleanup() {
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasEventTrigger;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.EventTrigger);
	}

	private void ProcessNewDay(int index) {
		_missions = _gameContext.globals.value.missions.GetEventsForDay((LevelsEnum) index);
		_discoveredEvents.Clear();

        UpdateObjectives();
    }

	private void ProcessEvent(EventsEnum evt, int index) {
		//Debug.Log(evt.ToString());

		if (_missions.ContainsKey(evt)) {
			MissionsDB.Mission mission = _missions[evt];

			if (mission.IsObjective) {
				mission.Index += index;
                mission.Index = Mathf.Clamp(mission.Index, 0, int.MaxValue);

				_missions[evt] = mission;
			}

			if (mission.IsDiscoverable && !_discoveredEvents.Contains(evt)) {
				ProcessDiscoveredEvent(evt, mission.Text);
				_discoveredEvents.Add(evt);
			}
		}

		UpdateObjectives();
		CheckEndMission();
	}

	private void ProcessDiscoveredEvent(EventsEnum evt, string text) {
		switch (evt) {
			case EventsEnum.FruitPicked:
				text = _textHelper.GetTranslation(text);
				break;
		}

		if (!string.IsNullOrEmpty(text)) {
			GameEntity log = _gameContext.CreateEntity();
			log.AddNotebookLog(NotebookPagesEnum.Notes, text, true);

			_gameContext.globals.value.notebookMessagesView.DisplayMessage(_textHelper.NotesUpdated);
		}
	}

	private void UpdateObjectives() {
		StringBuilder sb = new StringBuilder();

		foreach (KeyValuePair<EventsEnum, MissionsDB.Mission> pair in _missions) {
			if (pair.Value.IsObjective) {
				sb.Append(pair.Value.Text);
				sb.Append(" (needed: ");
				sb.Append(pair.Value.Index);
				sb.Append(")");
                sb.Append("\n\n");
			}
        }

		GameEntity log = _gameContext.CreateEntity();
		log.AddNotebookLog(NotebookPagesEnum.Objectives, sb.ToString(), false);
	}

	private void CheckEndMission() {
		bool isMissionEnded = true;

		foreach (KeyValuePair<EventsEnum, MissionsDB.Mission> pair in _missions) {
			if (pair.Value.Index > 0 && pair.Value.IsObjective) {
				isMissionEnded = false;
				break;
			}
        }

		if (isMissionEnded) {
			_gameContext.globals.value.endGameView.Toggle(true);
		}
	}
}
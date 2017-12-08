using Entitas;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MissionSystem : ReactiveSystem<GameEntity>, ICleanupSystem {

    private readonly GameContext _gameContext;
    private readonly InputContext _inputContext;

    private readonly IGroup<GameEntity> _eventTriggers;

    private Dictionary<EventsEnum, MissionsDB.Mission> _missions;

    private TextHelper _textHelper;

    private bool _isDayFinished;

    public MissionSystem(Contexts contexts) : base(contexts.game) {
        _gameContext = contexts.game;
        _inputContext = contexts.input;

        _textHelper = _gameContext.globals.value.textHelper;

        _eventTriggers = _gameContext.GetGroup(GameMatcher.EventTrigger);
    }

    protected override void Execute(List<GameEntity> entities) {
        foreach (var entity in entities) {
            if (entity.hasEventTrigger) {
                if (entity.eventTrigger.Evt == EventsEnum.DayStarted) {
                    ProcessNewDay(entity.eventTrigger.Index);
                }
                else {
                    ProcessEvent(entity.eventTrigger.Evt, entity.eventTrigger.Index);
                }
            }

            if (entity.isPlayerAdvanceNextDayInput && _isDayFinished) {
                GameEntity advanceNextDay = _gameContext.CreateEntity();
                advanceNextDay.isLoadNextLevelTrigger = true;

                _gameContext.globals.value.endGameView.Toggle(false);
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
        return entity.hasEventTrigger || entity.isPlayerAdvanceNextDayInput;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.EventTrigger, GameMatcher.PlayerAdvanceNextDayInput));
	}

	private void ProcessNewDay(int index) {
        _isDayFinished = false;
        _missions = _gameContext.globals.value.missions.GetEventsForDay((LevelsEnum) index);

        UpdateObjectives();
    }

	private void ProcessEvent(EventsEnum evt, int index) {
		//Debug.Log(evt.ToString());

		if (_missions.ContainsKey(evt)) {
			MissionsDB.Mission mission = _missions[evt];

			if (mission.IsObjective) {
				mission.Index += index;
                mission.Index = Mathf.Clamp(mission.Index, 0, int.MaxValue);

                GameEntity alert = _gameContext.CreateEntity();
                alert.AddNotebookAlert(true);

                _missions[evt] = mission;
			}
		}

		UpdateObjectives();
		CheckEndMission();
	}

	private void UpdateObjectives() {
		StringBuilder objectivesString = new StringBuilder();

		foreach (KeyValuePair<EventsEnum, MissionsDB.Mission> pair in _missions) {
			if (pair.Value.IsObjective) {
                if (pair.Value.Index <= 0) {
                    objectivesString.Append("✓ ");
                }
                else {
                    objectivesString.Append("[");
                    objectivesString.Append(pair.Value.Index);
                    objectivesString.Append("] ");
                }

                objectivesString.Append(pair.Value.Text);
                objectivesString.Append("\n\n");
			}
        }

		GameEntity log = _gameContext.CreateEntity();
		log.AddNotebookText(objectivesString.ToString());
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
            _isDayFinished = true;
            _gameContext.globals.value.endGameView.Toggle(true);
		}
	}
}
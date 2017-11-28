using Entitas;
using UnityEngine;

public class BasketHoopValidatorSystem : IExecuteSystem {

	private const float MAXIMUM_WAIT_TIME = 0.5f;

	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;

	private readonly IGroup<GameEntity> _hoopInput;

	private float _timerCountdown;

	public BasketHoopValidatorSystem(Contexts contexts) {
		_gameContext = contexts.game;
		_inputContext = contexts.input;

		_hoopInput = _gameContext.GetGroup(GameMatcher.BasketHoopTrigger);
	}

	public void Execute() {
		GameEntity[] hoopInput = _hoopInput.GetEntities();
		bool wasHoopTop = false;
		bool wasHoopBottom = false;

		foreach (var entity in hoopInput) {
			if (entity.basketHoopTrigger.IsTopHoop) {
				wasHoopTop = true;
				_timerCountdown = MAXIMUM_WAIT_TIME;
			} else {
				wasHoopBottom = true;

				if (_timerCountdown > 0) {
					GameEntity evt = _gameContext.CreateEntity();
					evt.AddEventTrigger(EventsEnum.BasketballHoop, -1);
				}
			}

			entity.RemoveBasketHoopTrigger();
		}

		_timerCountdown -= Time.deltaTime;
		_timerCountdown = Mathf.Clamp(_timerCountdown, 0, float.MaxValue);
	}
}
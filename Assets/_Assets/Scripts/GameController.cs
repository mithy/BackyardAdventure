using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Globals _globals;

	[SerializeField]
	private UIContainerView _uiContainer;

	private Systems _systems;
    private Contexts _contexts;

    private void Start() {
		Random.InitState(321);

		_uiContainer.InitializeGlobals(_globals);

		_contexts = Contexts.sharedInstance;
		_contexts.game.SetGlobals(_globals);

		_systems = CreateSystems(_contexts);
		_systems.Initialize();
    }

	private void Update() {
		_systems.Execute();
		_systems.Cleanup();
	}

	private Systems CreateSystems(Contexts contexts) {
		return new Feature("Systems")
			.Add(new InitSystems(contexts))
			.Add(new InputSystems(contexts))
			.Add(new GameSystems(contexts));
	}
}
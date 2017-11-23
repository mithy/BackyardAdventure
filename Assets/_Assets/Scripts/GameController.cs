using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour {

	private Systems _systems;
    private Contexts _contexts;

    private void Start() {
		Random.InitState (321);

		_contexts = Contexts.sharedInstance;

		_systems = CreateSystems(_contexts);
		_systems.Initialize();
    }

	private void Update() {
		_systems.Execute();
		_systems.Cleanup();
	}

	private Systems CreateSystems(Contexts contexts) {
		return new Feature("Systems");
	}
}
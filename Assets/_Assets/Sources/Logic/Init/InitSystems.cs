public class InitSystems : Feature {
	public InitSystems(Contexts contexts) : base("Init Systems") {
		Add(new InitGameSystem(contexts));
		Add(new InitPlayerSystem(contexts));
	}
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public NotebookLogComponent notebookLog { get { return (NotebookLogComponent)GetComponent(GameComponentsLookup.NotebookLog); } }
    public bool hasNotebookLog { get { return HasComponent(GameComponentsLookup.NotebookLog); } }

    public void AddNotebookLog(NotebookPagesEnum newPage, string newText, bool newShouldAppend) {
        var index = GameComponentsLookup.NotebookLog;
        var component = CreateComponent<NotebookLogComponent>(index);
        component.Page = newPage;
        component.Text = newText;
        component.ShouldAppend = newShouldAppend;
        AddComponent(index, component);
    }

    public void ReplaceNotebookLog(NotebookPagesEnum newPage, string newText, bool newShouldAppend) {
        var index = GameComponentsLookup.NotebookLog;
        var component = CreateComponent<NotebookLogComponent>(index);
        component.Page = newPage;
        component.Text = newText;
        component.ShouldAppend = newShouldAppend;
        ReplaceComponent(index, component);
    }

    public void RemoveNotebookLog() {
        RemoveComponent(GameComponentsLookup.NotebookLog);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherNotebookLog;

    public static Entitas.IMatcher<GameEntity> NotebookLog {
        get {
            if (_matcherNotebookLog == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NotebookLog);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNotebookLog = matcher;
            }

            return _matcherNotebookLog;
        }
    }
}

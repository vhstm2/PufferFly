

public enum State 
{
    None,Main,Game,GameEnd
}
public class StateMachine : MonoSington<StateMachine>
{
    [System.NonSerialized] public GameStateBase currentState = null;
    public static bool StateEnter = false;

    public void ChangeState(State state)
    {
        if (currentState != null)
            currentState.End();     

         currentState = CreateStateInstance(state);
         currentState.Enter();
    }

    public GameStateBase CreateStateInstance(State nextState)
    {
        switch (nextState)
        {
            case State.Main: return new MainState(this);
            case State.Game: return new GameState(this);
            case State.GameEnd: return new GameEndState(this);
        }
        return null;
    }
    private void Update()
    {
        if (currentState != null)  {   currentState.UpdateBase();   }
    }
}
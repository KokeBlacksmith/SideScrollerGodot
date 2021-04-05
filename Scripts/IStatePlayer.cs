using System.Collections.Generic;
using KokeBlacksmith;

namespace KokeBlacksmith.States
{
    public interface IStatePlayer<T>
    {
        /* Collection of states that can play the character.*/
        Dictionary<string, IState<T>> States { get; }
        void FetchStates();

        IState<T> GetCurrentState();
        IState<T> ChangeCurrentState(IState<T> newState);
    }

}
using System;

namespace LightsOffCore.Core
{
    [Serializable]
    public class Tile
    {
        private TileState _state = TileState.Dontshine;
        public TileState GetState ()
        {
            return this._state;
        }

        public void SetState(TileState state)
        {
            this._state = state;
        }

    }

    public enum TileState
    {
        Shine,Dontshine
    }
}
    
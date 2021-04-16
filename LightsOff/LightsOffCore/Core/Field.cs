using System;

namespace LightsOffCore.Core
{
    [Serializable]
    public class Field
    {
        private readonly Tile[,] _tiles;
            
        public int RowCount { get; private set; }

        public int ColumnCount { get; private set; }

        public int Level { get; private set; }
        
        public  int Score { get; private set; }

        private int _previousRow = -1;

        private int _previousColumn = -1;

        public string PlayersName { get; private set; }
        
        public GameState _gameState = GameState.Playing;

        
        public Field(int rowCount, int columnCount ,int level,string playersName)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;   
            Level = level;
            PlayersName = playersName;
            Score = 0;
            _tiles = new Tile[RowCount, ColumnCount];
            Initialize();
        }

        
        private void Initialize()
        {
            for (var row = 0; row < RowCount; row++)
            {
                for (var column = 0; column < ColumnCount; column++)
                {
                    _tiles[row, column] = new Tile();
                }
            }
            Generate();
        }
        private void Generate()
        {
            if(Level>0&&Level<=10) {
                SetLights(Level);
            }
        }
        

        private void SetLights(int generateCount)
        {
            while (generateCount!=0)
            {
                Random random = new Random();
                int row = random.Next(RowCount);
                int column = random.Next(ColumnCount);
                if (row == _previousRow && column == _previousColumn)
                    continue;
                CrossDotsChange(row,column);
                _previousRow = row;
                _previousColumn = column;
                generateCount--;
            }
        }

        private void CrossDotsChange(in int row, in int column)
        {
            ChangeLightState(_tiles[row, column]);
            if (row - 1 >= 0) {
                ChangeLightState(_tiles[row - 1,column]);
            }
            if (row + 1 < RowCount) {
                ChangeLightState(_tiles[row + 1,column]);
            }
            if (column - 1 >= 0) {
                ChangeLightState(_tiles[row,column - 1]);
            }
            if (column + 1 < ColumnCount)  {
                ChangeLightState(_tiles[row,column+1]);
            }
        }

        private void ChangeLightState(Tile tile)
        {
            tile.SetState(tile.GetState() == TileState.Shine ? TileState.Dontshine : TileState.Shine);
        }
        
        public void ShineDots(int row, int column) {
                    if (_gameState == GameState.Playing) {
                        CrossDotsChange(row, column);
                        Score++;
                        if (IsSolved()) {
                            _gameState = GameState.Solved;
                        }
                    }
        }
        public bool IsSolved() {
            int lightsCount = 0;
            for (int row = 0; row < RowCount; row++) {
                for (int column = 0; column < ColumnCount; column++) {
                    Tile tile = _tiles[row,column];
                    if (tile.GetState() == TileState.Shine) {
                        lightsCount++;
                    }
                }
            }
            if (lightsCount == 0) {
                return true;
            }
            return false;
        }
        
        public Tile GetTile(int row, int column)
        {
            return _tiles[row, column];
        }

        public Tile this[int row, int column]
        {
            get { return _tiles[row, column]; }
        }

        public GameState GetState() {
            return _gameState;
        }

    }
}
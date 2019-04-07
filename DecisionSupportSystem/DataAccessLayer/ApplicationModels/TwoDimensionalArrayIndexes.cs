using System;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    [Serializable]
    public class TwoDimensionalArrayIndexes
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public TwoDimensionalArrayIndexes(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
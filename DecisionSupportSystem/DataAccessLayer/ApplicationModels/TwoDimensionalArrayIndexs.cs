using System;

namespace DecisionSupportSystem.DataAccessLayer.ApplicationModels
{
    [Serializable]
    public class TwoDimensionalArrayIndexs
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public TwoDimensionalArrayIndexs(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
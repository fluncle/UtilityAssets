using System.Collections.Generic;

namespace GoogleSheetsReaderMaster
{
    public class CommonMasterData
    {
        private enum DataType
        {
            comboMax = 1,
            comboCoefficientIncrement,
        }

        private const int VALUE_COLUMN = 1;

        private int _comboMax;
        public int ComboMax => _comboMax;

        private float _comboCoefficientIncrement;
        public float ComboCoefficientIncrement => _comboCoefficientIncrement;

        public CommonMasterData(IList<IList<object>> sheetList)
        {
            _comboMax = int.Parse(sheetList[(int)DataType.comboMax][VALUE_COLUMN].ToString());
            _comboCoefficientIncrement = float.Parse(sheetList[(int)DataType.comboCoefficientIncrement][VALUE_COLUMN].ToString());
        }
    }
}

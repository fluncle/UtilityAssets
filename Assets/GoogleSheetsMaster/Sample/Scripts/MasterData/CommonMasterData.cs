using System.Collections.Generic;

namespace GoogleSheetsReaderMaster
{
    /// <summary>
    /// 共通設定マスタデータ
    /// 項目+値という単純なマスタデータとしてのサンプル
    /// </summary>
    public class CommonMasterData
    {
        /// <summary>
        /// データの種類
        /// 行順で定義してください
        /// </summary>
        private enum DataType
        {
            comboMax = 1,
            comboCoefficientIncrement,
        }

        /// <summary>値を定義する行</summary>
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

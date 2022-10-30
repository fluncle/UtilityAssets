using System.Collections.Generic;

namespace GoogleSheetsReaderMaster
{
    public class EnemyMasterData
    {
        private enum DataType
        {
            id = 0,
            name,
            hp,
            attack,
        }

        private int _id;
        public int ID => _id;

        private string _name;
        public string Name => _name;

        private int _hp;
        public int HP => _hp;

        private int _attack;
        public int Attack => _attack;

        public EnemyMasterData(IList<object> sheetList)
        {
            _id = int.Parse(sheetList[(int)DataType.id].ToString());
            _name = sheetList[(int)DataType.name].ToString();
            _hp = int.Parse(sheetList[(int)DataType.hp].ToString());
            _attack = int.Parse(sheetList[(int)DataType.attack].ToString());
        }
    }
}

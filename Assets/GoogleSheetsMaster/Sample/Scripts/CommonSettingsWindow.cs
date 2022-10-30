using UnityEngine;

namespace GoogleSheetsReaderMaster
{
    public class CommonSettingsWindow : MonoBehaviour
    {
        [SerializeField]
        private ItemCommon _itemPrefab;

        [SerializeField]
        private Transform _content;

        public void Initialize()
        {
            ClearContent();

            var commonMaster = MasterDataManager.Instance.CommonMaster;
            AddItem("最大コンボ数", commonMaster.ComboMax.ToString());
            AddItem("コンボによるダメージ係数上昇値", commonMaster.ComboCoefficientIncrement.ToString());
        }

        private void ClearContent()
        {
            for (int i = _content.childCount - 1; i >= 0; i--)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }

        private void AddItem(string itemName, string value)
        {
            var item = Instantiate(_itemPrefab, _content);
            item.SetInfo(itemName, value);
        }
    }
}

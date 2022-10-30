using UnityEngine;

namespace GoogleSheetsReaderMaster
{
    /// <summary>
    /// 共通設定マスタデータウィンドウ
    /// </summary>
    public class CommonSettingsWindow : MonoBehaviour
    {
        [SerializeField]
        private ItemCommon _itemPrefab;

        [SerializeField]
        private Transform _content;

        public void Initialize()
        {
            ClearContent();

            // 共通設定マスタのデータを順にウィンドウに並べて表示
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

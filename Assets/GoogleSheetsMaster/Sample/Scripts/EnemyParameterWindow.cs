using UnityEngine;
using UnityEngine.UI;

namespace GoogleSheetsReaderMaster
{
    public class EnemyParameterWindow : MonoBehaviour
    {
        [SerializeField]
        private ItemCommon _itemPrefab;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private Text _labelText;

        public void SetInfo(EnemyMasterData data)
        {
            _labelText.text = data.Name;

            ClearContent();

            AddItem("HP", data.HP.ToString());
            AddItem("攻撃力", data.Attack.ToString());
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

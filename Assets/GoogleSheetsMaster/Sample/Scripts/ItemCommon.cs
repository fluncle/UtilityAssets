using UnityEngine;
using UnityEngine.UI;

namespace GoogleSheetsReaderMaster
{
    public class ItemCommon : MonoBehaviour
    {
        [SerializeField]
        private Text _itemText;

        [SerializeField]
        private Text _valueText;

        public void SetInfo(string itemName, string value)
        {
            _itemText.text = itemName;
            _valueText.text = value;
        }
    }
}

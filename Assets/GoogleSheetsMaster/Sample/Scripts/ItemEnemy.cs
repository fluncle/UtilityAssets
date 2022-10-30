using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GoogleSheetsReaderMaster
{
    public class ItemEnemy : MonoBehaviour
    {
        [SerializeField]
        private Text _idText;

        [SerializeField]
        private Text _nameText;

        [SerializeField]
        private Toggle _toggle;

        public void SetInfo(int id, string name)
        {
            _idText.text = id.ToString();
            _nameText.text = name;
        }

        public void SetOnClickEvent(UnityAction<bool> onClick, ToggleGroup toggleGroup)
        {
            _toggle.onValueChanged.AddListener(onClick);
            _toggle.group = toggleGroup;
        }

        public void Select()
        {
            _toggle.isOn = true;
        }
    }
}

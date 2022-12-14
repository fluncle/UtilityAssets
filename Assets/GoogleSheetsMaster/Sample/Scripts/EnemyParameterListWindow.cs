using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleSheetsReaderMaster
{
    /// <summary>
    /// 敵マスタデータリストウィンドウ
    /// </summary>
    public class EnemyParameterListWindow : MonoBehaviour
    {
        [SerializeField]
        private ItemEnemy _enemyPrefab;

        [SerializeField]
        private Transform _content;

        [SerializeField]
        private EnemyParameterWindow _enemyParameterWindow;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        public void Initialize()
        {
            ClearContent();

            var items = new List<ItemEnemy>();
            // 敵マスタのデータを順にウィンドウに並べて表示
            foreach (var kvp in MasterDataManager.Instance.EnemyMaster)
            {
                var id = kvp.Key;
                var data = kvp.Value;
                var item = AddItem(id, data.Name);
                // 項目選択時に詳細ウィンドウで全てのパラメータを表示
                item.SetOnClickEvent(isOn => _enemyParameterWindow.SetInfo(data), _toggleGroup);
                items.Add(item);
            }
            items[0].Select();
        }

        private void ClearContent()
        {
            for (int i = _content.childCount - 1; i >= 0; i--)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }

        private ItemEnemy AddItem(int id, string value)
        {
            var item = Instantiate(_enemyPrefab, _content);
            item.SetInfo(id, value);
            return item;
        }
    }
}

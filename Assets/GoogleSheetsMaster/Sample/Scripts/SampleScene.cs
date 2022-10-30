using UnityEngine;

namespace GoogleSheetsReaderMaster
{
    /// <summary>
    /// GoogleSheetsReaderMasterのサンプルシーン
    /// </summary>
    public class SampleScene : MonoBehaviour
    {
        [SerializeField]
        private LoagingLayer _loagingLayer;

        [SerializeField]
        private CommonSettingsWindow _commonSettingsWindow;

        [SerializeField]
        private EnemyParameterListWindow _enemyParameterListWindow;

        private void Awake()
        {
            LoadMaster();
        }

        public void LoadMaster()
        {
            _loagingLayer.Play();
            StartCoroutine(MasterDataManager.Instance.LoadMaster(OnLoadComplete, true));
        }

        private void OnLoadComplete()
        {
            // 共通設定マスタデータウィンドウの初期化
            _commonSettingsWindow.Initialize();
            // 敵マスタデータウィンドウの初期化
            _enemyParameterListWindow.Initialize();
            _loagingLayer.End();
        }
    }
}

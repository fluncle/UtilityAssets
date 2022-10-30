using UnityEngine;

namespace GoogleSheetsReaderMaster
{
    public class SampleScene : MonoBehaviour
    {
        [SerializeField]
        private LoagingLayer _loagingLayer;

        [SerializeField]
        private CommonSettingsWindow _commonSettingsWindow;

        [SerializeField]
        private EnemyParameterListWindow _enemyParameterListWindow;

        private MasterDataManager _masterDataMgr;

        private void Awake()
        {
            _masterDataMgr = new MasterDataManager();
            LoadMaster();
        }

        public void LoadMaster()
        {
            _loagingLayer.Play();
            StartCoroutine(_masterDataMgr.LoadMaster(OnLoadComplete, true));
        }

        private void OnLoadComplete()
        {
            _commonSettingsWindow.Initialize();
            _enemyParameterListWindow.Initialize();
            _loagingLayer.End();
        }
    }
}

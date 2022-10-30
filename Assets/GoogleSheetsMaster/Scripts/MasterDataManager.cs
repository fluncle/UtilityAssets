using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// マスタデータの管理
/// </summary>
public class MasterDataManager
{
    /// <summary>シングルトンインスタンス</summary>
    private static MasterDataManager _instance = new MasterDataManager();
    public static MasterDataManager Instance => _instance;

    /// <summary>
    /// 利用するスプレッドシートのID
    /// サンプルのスプレッドシート: https://docs.google.com/spreadsheets/d/1nJKzmF_gT9jYIcqHxVYIW-B66NijcEC76AOH_aYSTr4
    /// </summary>
    // 適宜使用するシートのIDに置き換えて利用してください
    private const string SHEET_ID = "1nJKzmF_gT9jYIcqHxVYIW-B66NijcEC76AOH_aYSTr4";

    #region サンプルデータ
    private const string COMMON_SHEET_NAME = "共通";
    public GoogleSheetsReaderMaster.CommonMasterData CommonMaster { get; private set; }

    private const string ENEMY_SHEET_NAME = "敵";
    public Dictionary<int, GoogleSheetsReaderMaster.EnemyMasterData> EnemyMaster { get; private set; }
    #endregion サンプルデータ

    private MasterDataManager() { }

    /// <summary>
    /// マスタの非同期読み込み
    /// </summary>
    /// <param name="onComplete">読み込み完了時のコールバック</param>
    /// <param name="isPrint">読み込んだデータをログ出力するか否か</param>
    public IEnumerator LoadMaster(Action onComplete = null, bool isPrint = false)
    {
        #region サンプルデータ
        yield return GoogleSheetsReader.LoadSheet(SHEET_ID, COMMON_SHEET_NAME, list => CommonMaster = new GoogleSheetsReaderMaster.CommonMasterData(list), isPrint);

        EnemyMaster = new Dictionary<int, GoogleSheetsReaderMaster.EnemyMasterData>();
        yield return GoogleSheetsReader.LoadSheet(SHEET_ID, ENEMY_SHEET_NAME, list =>
        {
            for (int i = 1; i < list.Count; i++)
            {
                var monster = new GoogleSheetsReaderMaster.EnemyMasterData(list[i]);
                EnemyMaster.Add(monster.ID, monster);
            }
        },
        isPrint);
        #endregion サンプルデータ

        onComplete?.Invoke();
    }
}

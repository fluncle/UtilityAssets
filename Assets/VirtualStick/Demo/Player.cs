using UnityEngine;

namespace VirtualStick
{
    /// <summary>
    /// デモ用のプレイヤー
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private VirtualStickHandler _stickHandler;

        [SerializeField]
        private VirtualStickView _stickView;

        [SerializeField]
        private Transform _camera;

        [SerializeField]
        private float _speed = 3f;

        [SerializeField]
        private float _angularVelocity = 360f;

        private void Awake()
        {
            // VirtualStickHandlerに各種イベントを登録する
            _stickHandler.OnBeginDragEvent = (dragPosition, basePosition) =>
            {
                // UIの開始演出
                _stickView.Begin(dragPosition, basePosition);
            };

            _stickHandler.OnDragEvent = (dragPosition, basePosition) =>
            {
                // ドラッグ操作時のUIの更新
                var dragVector = dragPosition - basePosition;
                _stickView.SetDragVector(dragVector, basePosition);
            };

            _stickHandler.OnEndDragEvent = () =>
            {
                // UIの終了演出
                _stickView.End();
            };

            // 最大入力と判定するドラッグ量をUIに設定
            _stickView.SetDragMaxDistance(_stickHandler.DragMaxDistance);
        }

        private void Update()
        {
            // ドラッグ操作中か否か
            if (_stickHandler.IsDrag)
            {
                // 操作の入力方向と、ドラッグ量（割合）を使って移動する
                Move(_stickHandler.Vector, _stickHandler.Rate);
                // 入力方向とは別で、キャラクターの向いてる方向をリアルタイムでUIに表示
                _stickView.SetAngleAxisOut(_camera.eulerAngles.y - transform.eulerAngles.y);
            }
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="vector">入力方向</param>
        /// <param name="speedRate">速度係数</param>
        private void Move(Vector2 vector, float speedRate)
        {
            // 速度係数から今回の更新の速度(m/s)を計算
            var speed = Mathf.Lerp(0f, _speed, speedRate);
            var distanceDelta = speed * Time.deltaTime;
            // カメラ角度を使って画面に対しての移動方向を計算
            var offset = Quaternion.Euler(0f, _camera.eulerAngles.y, 0f) * new Vector3(vector.x, 0f, vector.y) * distanceDelta;
            transform.position += offset;

            // 移動量が0なら角度更新処理はスキップ
            if(offset == Vector3.zero)
            {
                return;
            }

            // プレイヤーの向きを更新
            var eulerAngles = transform.eulerAngles;
            var moveAngles = Quaternion.LookRotation(offset).eulerAngles;
            var angleDiff = Mathf.DeltaAngle(eulerAngles.y, moveAngles.y);
            var angularVelocity = Mathf.Lerp(0f, _angularVelocity, Mathf.Clamp01(Mathf.Abs(angleDiff) / 90f));
            eulerAngles.y += Mathf.Min(angularVelocity * Time.deltaTime, Mathf.Abs(angleDiff)) * Mathf.Sign(angleDiff);
            transform.eulerAngles = eulerAngles;
        }
    }
}

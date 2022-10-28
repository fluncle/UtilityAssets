using UnityEngine;

namespace SpriteAnimation
{
    /// <summary>
    /// デモ用のプレイヤー
    /// </summary>
    public class Player : MonoBehaviour
    {
        /// <summary>待機アニメーションのスプライトパスの文字列フォーマット</summary>
        private const string IDLE_SPRITE_PATH_FORMAT = "Sprites/player/idle/player-idle-{0}";

        /// <summary>走りアニメーションのスプライトパスの文字列フォーマット</summary>
        private const string RUN_SPRITE_PATH_FORMAT = "Sprites/player/run/player-run-{0}";

        [SerializeField]
        private RectTransform _rect;

        [SerializeField]
        private SpriteImageAnimationView _spriteAnim;

        [SerializeField]
        private float _speed = 100f;

        private bool _isMove;

        private void Start()
        {
            // 待機アニメーションを再生
            _spriteAnim.Play(IDLE_SPRITE_PATH_FORMAT, 4, 1);
        }

        private void Update()
        {
            var vector = GetMoveVector();
            _rect.anchoredPosition += vector * Time.deltaTime * _speed;

            if(_isMove && vector == Vector2.zero)
            {
                // 待機アニメーションを再生
                _spriteAnim.Play(IDLE_SPRITE_PATH_FORMAT, 4, 1);
                _isMove = false;
            }
            else if (!_isMove && vector != Vector2.zero)
            {
                // 走りアニメーションを再生
                _spriteAnim.Play(RUN_SPRITE_PATH_FORMAT, 6, 1, 0.075f);
                _isMove = true;

                // 左移動なら反転表示する
                _rect.localScale = vector.x > 0 ? Vector3.one : new Vector3(-1f, 1f);
            }
        }

        private Vector2 GetMoveVector()
        {
            var vector = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                vector += Vector2.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                vector += Vector2.left;
            }
            return vector;
        }
    }
}

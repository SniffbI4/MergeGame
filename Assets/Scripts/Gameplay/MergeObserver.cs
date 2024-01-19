using System.Collections;
using FloatingText;
using Scripts.Audio;
using Scripts.Game;
using Scripts.Progress;
using UnityEngine;

namespace Scripts.GamePlay
{
    public class MergeObserver : MonoBehaviour,
                                 IGameInitListener
    {
        [SerializeField] private float _mergeTime = 0.3f;

        private ObjectSpawner _spawner;

        private AudioManager _audioManager;
        private ScoreCurrency _scoreCurrency;
        private GameProgressController _progressController;
        private FloatingTextController _floatingText;

        public void OnGameInit()
        {
            _spawner = GameManager.Instance.GetService<ObjectSpawner>();
            _audioManager = GameManager.Instance.GetService<AudioManager>();
            _scoreCurrency = GameManager.Instance.GetService<ScoreCurrency>();
            _progressController = GameManager.Instance.GetService<GameProgressController>();
            _floatingText = GameManager.Instance.GetService<FloatingTextController>();
            
            MergeComponent.OnCollision += OnCollisionDetect;
        }

        private void OnDestroy()
        {
            MergeComponent.OnCollision -= OnCollisionDetect;
        }

        private void OnCollisionDetect(MergeElement elem1, MergeElement elem2)
        {
            elem1.RigidbodyComponent.simulated = false;
            elem2.RigidbodyComponent.simulated = false;

            StartCoroutine(CollisionRoutine(elem1, elem2));
        }

        private IEnumerator CollisionRoutine(MergeElement _block1, MergeElement _block2)
        {
            float elapced = 0f;
            
            var score = _progressController.GetScoreByLevel(_block1.Level);
            _floatingText.CreateFloatingText($"+{score}", _block1.transform, false);
            
            var targetPoint = (_block1.transform.position + _block2.transform.position) * 0.5f;
            var targetScale = Vector3.zero;
            
            var block1startPos = _block1.transform.position;
            var block2StartPos = _block2.transform.position;
            var block1Scale = _block1.transform.localScale;
            var block2Scale = _block2.transform.localScale;
            
            while (elapced < _mergeTime)
            {
                float progress = elapced / _mergeTime;
                
                _block1.transform.position = Vector2.Lerp(block1startPos, targetPoint, progress);
                _block2.transform.position = Vector2.Lerp(block2StartPos, targetPoint, progress);
                
                _block1.transform.localScale = Vector3.Lerp(block1Scale, targetScale, progress);
                _block2.transform.localScale = Vector3.Lerp(block2Scale, targetScale, progress);

                elapced += Time.deltaTime;
                yield return null;
            }

            _block1.transform.position = targetPoint;
            _block2.transform.position = targetPoint;
            _block1.transform.localScale = targetScale;
            _block2.transform.localScale = targetScale;

            _audioManager.PlayClipByType(ClipType.Puf);

            _scoreCurrency.AddCurrency(score);

            _spawner.CreateBlockBetween(_block1, _block2);
        }
    }
}
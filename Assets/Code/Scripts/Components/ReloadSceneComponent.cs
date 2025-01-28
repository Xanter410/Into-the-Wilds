using UnityEngine;
using UnityEngine.SceneManagement;

namespace IntoTheWilds
{
    public class ReloadSceneComponent : MonoBehaviour
    {
        private float _timeBeforeRestart;
        private bool _isRestarting = false;
        private bool _isReadyToRestart = false;

        public void RestartScene(float DelayBeforeRestart = 0f)
        {
            _timeBeforeRestart = DelayBeforeRestart;
            _isReadyToRestart = true;
        }

        private void Update()
        {
            if (_isReadyToRestart)
            {
                if (_timeBeforeRestart > 0)
                {
                    _timeBeforeRestart -= Time.deltaTime;
                }
                else if (_isRestarting == false)
                {
                    ReloadCurrentLevel();
                }
            }
        }

        private void ReloadCurrentLevel()
        {
            _isRestarting = true;
            SceneTransition.SwitchToScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

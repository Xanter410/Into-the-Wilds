using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Tools
{
    [RequireComponent(typeof(Animator))]
    public class PlayerOneAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationClip _AnimationClip;
        private PlayableGraph _playableGraph;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            _playableGraph.Play();
        }

        private void OnDestroy()
        {
            _playableGraph.Destroy();
        }

        private void Initialize()
        {
            _playableGraph = PlayableGraph.Create();

            _playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            var playableOutput = AnimationPlayableOutput.Create(_playableGraph, "Animation", GetComponent<Animator>());

            var clipPlayable = AnimationClipPlayable.Create(_playableGraph, _AnimationClip);

            playableOutput.SetSourcePlayable(clipPlayable);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnimation = false;
    private static float _musicVolume;

    private PlayAudioSceneTransition _playAudioSceneTransition;

    private AsyncOperation _loadingSceneOperation;

    private UIDocument _uiDocument;
    private VisualElement _transitionFigure;

    private static string _styleOpeningStart = "openingStart";
    private static string _styleOpeningEnd = "openingEnd";
    private static string _styleEndingStart = "endingStart";
    private static string _styleEndingEnd = "endingEnd";

    public static void SwitchToScene(int sceneIndex)
    {
        float volume;
        _instance._audioMixer.GetFloat("MasterVolume", out volume);
        _musicVolume = Mathf.Pow(10, volume / 20);

        _instance.StartCoroutine(FadeMixerGroup.StartFade(_instance._audioMixer, "MasterVolume", 1f, 0f));

        _instance.StartCoroutine(EndingAnimation(_instance._transitionFigure, _instance._playAudioSceneTransition));

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);

        _instance._loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        _instance = this;

        Vector3 newPosition = Camera.main.transform.position;
        newPosition.z = Camera.main.transform.position.z + 1;

        transform.position = newPosition;

        _playAudioSceneTransition = GetComponent<PlayAudioSceneTransition>();

        _uiDocument = GetComponent<UIDocument>();
        _transitionFigure = _uiDocument.rootVisualElement.Q<VisualElement>("TransitionFigure");

        if (_shouldPlayOpeningAnimation)
        {
            _instance.StartCoroutine(FadeMixerGroup.StartFade(_instance._audioMixer, "MasterVolume", 1f, _musicVolume));

            _instance.StartCoroutine(OpeningAnimation(_transitionFigure, _playAudioSceneTransition));

            _shouldPlayOpeningAnimation = false;
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnimation = true;

        _loadingSceneOperation.allowSceneActivation = true;
    }

    public static IEnumerator OpeningAnimation(VisualElement transitionFigure, PlayAudioSceneTransition audioPlayer)
    {
        transitionFigure.ClearClassList();

        transitionFigure.AddToClassList(_styleOpeningStart);

        transitionFigure.visible = true;

        yield return new WaitForSeconds(0.01f);

        audioPlayer.PlayClipOpening();
        transitionFigure.AddToClassList(_styleOpeningEnd);

        yield return new WaitForSeconds(1f);

        transitionFigure.visible = false;
    }

    public static IEnumerator EndingAnimation(VisualElement transitionFigure, PlayAudioSceneTransition audioPlayer)
    {
        transitionFigure.ClearClassList();

        transitionFigure.AddToClassList(_styleEndingStart);

        transitionFigure.visible = true;

        yield return new WaitForSeconds(0.1f);

        audioPlayer.PlayClipEnding();
        transitionFigure.AddToClassList(_styleEndingEnd);

        yield return new WaitForSeconds(1f);

        transitionFigure.visible = false;

        _instance.OnAnimationOver();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public enum TAudioMixerType { Master, BGM, SFX }

public class ButtonsFunction : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public GameObject settingPanel;
    //public Image soundWaveImg;
    //public List<Sprite> soundWaves;
    public AudioSource sliderAudioSc;
    private AudioSource buttonAudioSc;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        buttonAudioSc = GetComponent<AudioSource>();

        // 슬라이더 초기값 설정 (AudioMixer에서 현재 값을 불러와 반영)
        float volume;
        if (audioMixer.GetFloat("Master", out volume)) masterSlider.value = Mathf.Pow(10, volume / 20);
        if (audioMixer.GetFloat("BGM", out volume)) bgmSlider.value = Mathf.Pow(10, volume / 20);
        if (audioMixer.GetFloat("SFX", out volume)) sfxSlider.value = Mathf.Pow(10, volume / 20);

        // 슬라이더 값 변경 시 이벤트 연결
        masterSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.Master, value));
        bgmSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.BGM, value));
        sfxSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.SFX, value));
    }

    public void SingleMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);

        // 스토리 씬
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        // 멀티 기능 추가
    }

    public void SetAudioVolume(TAudioMixerType audioMixerType, float volume)
    {
        // 오디오 믹서의 값은 -80 ~ 0까지이기 때문에 0.0001 ~ 1의 Log10 * 20을 한다.
        float mixerVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(audioMixerType.ToString(), mixerVolume);
    }

    public void OpenMenu()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        settingPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        settingPanel.SetActive(false);
    }

    public void SaveSettingData()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        print("Save");
        settingPanel.SetActive(false);
    }

    public void Exit()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        // 게임 종료
#if UNITY_EDITOR //전처리기로 유니티 에디터가 실행중일때 플레이를 멈추도록함.
        UnityEditor.EditorApplication.isPlaying = false; //어플리케이션 플레이를 false로 함.
#else //유니티에디터가 실행중이 아닐때 작동
                Application.Quit(); //어플리케이션을 종료
#endif
    }
}

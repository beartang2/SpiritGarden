using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ButtonsFunction : MonoBehaviour
{
    public Slider slider;
    public GameObject settingPanel;
    //public Image soundWaveImg;
    //public List<Sprite> soundWaves;
    public AudioSource sliderAudioSc;
    private AudioSource buttonAudioSc;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        buttonAudioSc = GetComponent<AudioSource>();
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

    public void soundVolume()
    {
        // 슬라이더 값에 따라 AudioMixer의 마스터 볼륨을 조정
        float sliderValue = slider.value; // 슬라이더 값 (0 ~ 1)

        sliderAudioSc.volume = sliderValue;

        /*
        if (slider.value == 0) soundWaveImg.sprite = soundWaves[0];
        else if (slider.value < 0.25) soundWaveImg.sprite = soundWaves[1];
        else if (slider.value < 0.5) soundWaveImg.sprite = soundWaves[2];
        else if (slider.value >= 0.75) soundWaveImg.sprite = soundWaves[3];
        */
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

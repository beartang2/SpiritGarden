using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ButtonsFunction : MonoBehaviour
{
    public Slider slider;
    //public Image soundWaveImg;
    //public List<Sprite> soundWaves;
    public AudioMixer audioMixer; // AudioMixer 참조

    public void SingleMode()
    {
        // 스토리 씬
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        // 멀티 기능 추가
    }

    public void soundVolume()
    {
        // 슬라이더 값에 따라 AudioMixer의 마스터 볼륨을 조정
        float sliderValue = slider.value; // 슬라이더 값 (0 ~ 1)

        // dB로 변환 (-80 ~ 0)
        float volumeDb = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;

        audioMixer.SetFloat("MasterVolume", volumeDb); // "MasterVolume"은 AudioMixer의 exposed parameter

        /*
        if (slider.value == 0) soundWaveImg.sprite = soundWaves[0];
        else if (slider.value < 0.25) soundWaveImg.sprite = soundWaves[1];
        else if (slider.value < 0.5) soundWaveImg.sprite = soundWaves[2];
        else if (slider.value >= 0.75) soundWaveImg.sprite = soundWaves[3];
        */
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SaveSettingData()
    {
        print("Save");
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        // 게임 종료
#if UNITY_EDITOR //전처리기로 유니티 에디터가 실행중일때 플레이를 멈추도록함.
        UnityEditor.EditorApplication.isPlaying = false; //어플리케이션 플레이를 false로 함.
#else //유니티에디터가 실행중이 아닐때 작동
                Application.Quit(); //어플리케이션을 종료
#endif
    }
}

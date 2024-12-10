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

        // �����̴� �ʱⰪ ���� (AudioMixer���� ���� ���� �ҷ��� �ݿ�)
        float volume;
        if (audioMixer.GetFloat("Master", out volume)) masterSlider.value = Mathf.Pow(10, volume / 20);
        if (audioMixer.GetFloat("BGM", out volume)) bgmSlider.value = Mathf.Pow(10, volume / 20);
        if (audioMixer.GetFloat("SFX", out volume)) sfxSlider.value = Mathf.Pow(10, volume / 20);

        // �����̴� �� ���� �� �̺�Ʈ ����
        masterSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.Master, value));
        bgmSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.BGM, value));
        sfxSlider.onValueChanged.AddListener(value => SetAudioVolume(TAudioMixerType.SFX, value));
    }

    public void SingleMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);

        // ���丮 ��
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        buttonAudioSc.PlayOneShot(audioClip);
        // ��Ƽ ��� �߰�
    }

    public void SetAudioVolume(TAudioMixerType audioMixerType, float volume)
    {
        // ����� �ͼ��� ���� -80 ~ 0�����̱� ������ 0.0001 ~ 1�� Log10 * 20�� �Ѵ�.
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
        // ���� ����
#if UNITY_EDITOR //��ó����� ����Ƽ �����Ͱ� �������϶� �÷��̸� ���ߵ�����.
        UnityEditor.EditorApplication.isPlaying = false; //���ø����̼� �÷��̸� false�� ��.
#else //����Ƽ�����Ͱ� �������� �ƴҶ� �۵�
                Application.Quit(); //���ø����̼��� ����
#endif
    }
}

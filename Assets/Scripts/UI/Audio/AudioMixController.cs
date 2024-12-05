using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;	// audioMixer를 사용하기 위해
using UnityEngine.UI;	// Slider, Toggle을 사용하기 위해

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;	// 볼륨을 조절할 Slider
    [SerializeField] private Slider BGMSlider;	// 볼륨을 조절할 Slider
    [SerializeField] private Slider EffectSlider;	// 볼륨을 조절할 Slider


    private void Awake()
    {
    	// 슬라이더의 값이 변경될 때 AddListener를 통해 이벤트 구독
        MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        // 슬라이더의 값이 변경될 때 AddListener를 통해 이벤트 구독
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        // 슬라이더의 값이 변경될 때 AddListener를 통해 이벤트 구독
        EffectSlider.onValueChanged.AddListener(SetEffectVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
    	SetVolumeBeforeStart();

		// audioMixer.SetFloat("audioMixer에 설정해놓은 Parameter", float 값)
        // audioMixer에 미리 설정해놓은 parameter 값을 변경하는 코드.
        // Mathf.Log10(BGMSlider.value) * 20 : 데시벨이 비선형적이기 때문에 해당 방식으로 값을 계산.

        audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);
    }

    void SetVolumeBeforeStart()
    {
        // PlayerPrefs에 Volume 값이 저장되어 있을 경우,
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
        	// Slider의 값을 저장해 놓은 값으로 변경.
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            MasterSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            MasterSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        }

    }

	// Slider를 통해 걸어놓은 이벤트
    public void SetMasterVolume(float volume)
    {
    	// 변경된 Slider의 값 volume으로 audioMixer의 Volume 변경하기
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);

        // 변경된 Volume 값 저장하기
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
    }
    public void SetBGMVolume(float volume)
    {
    	// 변경된 Slider의 값 volume으로 audioMixer의 Volume 변경하기
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        
        // 변경된 Volume 값 저장하기
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }
    public void SetEffectVolume(float volume)
    {
    	// 변경된 Slider의 값 volume으로 audioMixer의 Volume 변경하기
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        
        // 변경된 Volume 값 저장하기
        PlayerPrefs.SetFloat("EffectVolume", EffectSlider.value);
    }
}
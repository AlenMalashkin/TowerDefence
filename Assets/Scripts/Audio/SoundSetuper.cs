using UnityEngine;
using UnityEngine.UI;

public class SoundSetuper : MonoBehaviour
{
	[SerializeField] private SoundType type;
	[SerializeField] private Slider audioSetupSlider;
	[SerializeField] private Text sliderValueInPercent;

	private AudioSource _currentAudioSource;
	
	private void OnEnable()
	{
		audioSetupSlider.onValueChanged.AddListener(UpdateSoundValueInPercent);
		
		switch (type)
		{
			case SoundType.Music:
				var musicVolume = PlayerPrefs.GetFloat("MusicVolume");
				audioSetupSlider.value = musicVolume;
				sliderValueInPercent.text = Mathf.RoundToInt(musicVolume * 100) + "";
				break;
			case SoundType.Sfx:
				var sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
				audioSetupSlider.value = sfxVolume;
				sliderValueInPercent.text = Mathf.RoundToInt(sfxVolume * 100) + "";
				break;
		}
	}

	private void UpdateSoundValueInPercent(float value)
	{
		sliderValueInPercent.text = Mathf.RoundToInt(value * 100) + "";
		
		switch (type)
		{
			case SoundType.Music:
				SoundPlayer.Instance.SetupMusicVolume(value);
				break;
			case SoundType.Sfx:
				SoundPlayer.Instance.SetupSfxVolume(value);
				break;
		}
	}
}

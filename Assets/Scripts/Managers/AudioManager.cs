using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kebab.Managers;

namespace Kebab.BattleEngine.Audio
{
	public class AudioManager : Manager<AudioManager>
	{
		[SerializeField] private AudioSource audioSource = null;

		protected override void Awake()
		{
			base.Awake();
			if (audioSource == null)
				audioSource = gameObject.AddComponent<AudioSource>();
		}

		public void PlaySFX(AudioClip clip)
		{
			if (clip == null)
				return;
			audioSource.PlayOneShot(clip, SFXVolume);
		}

		public float SFXVolume
		{
			get => PlayerPrefs.GetFloat("SFXVolume", 0);
			set => PlayerPrefs.SetFloat("SFXVolume", value);
		}
	}
}
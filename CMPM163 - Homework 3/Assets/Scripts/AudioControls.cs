using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour
{
    // array of audio tracks to play
    public AudioClip[] tracks = new AudioClip[4];
    private int index = 3;

    // text box to display track name
    public Text textBox;

    // audio player component
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        ChangeAudioClip(index, 1.0f);
    }

    void Update()
    {
        // control track playing with arrow keys
        if(Input.GetKeyDown("left"))
        {
            // update index, fix it within bounds
            index -= 1;
            if(index < 0)
            {
                index = tracks.Length - 1;
            }

            ChangeAudioClip(index, 0.0f);
        }
        else if (Input.GetKeyDown("right"))
        {
            // update index, fix it within bounds
            index += 1;
            if(index >= tracks.Length)
            {
                index = 0;
            }

            ChangeAudioClip(index, 0.0f);
        }
        else if(Input.GetKeyDown("space"))
        {
            // pause/unpause the audio
            if(audio.isPlaying)
            {
                audio.Pause();
            }
            else
            {
                audio.UnPause();
            }
        }
        else if(Input.GetKeyDown("escape") || Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
    }

    void ChangeAudioClip(int index, float delay)
    {
        // set the new track on the audio source and play it
        audio.clip = tracks[index];
        audio.PlayDelayed(delay);

        // update text box with new song name.
        textBox.text = "Now Playing: " + tracks[index].name;
    }
}

using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace KaitoPath.Rhythm
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private NoteName noteRestriction;
        [SerializeField] private Key inputKeyboard;
        [SerializeField] private GamepadButton inputGamepad;
        [SerializeField] private GameObject notePrefab;
        private List<Note> notes = new();
        private List<double> timeStamps = new();

        private int spawnIndex = 0;
        private int inputIndex = 0;

        private void Update()
        {
            if (spawnIndex < timeStamps.Count)
            {
                if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Singleton.NoteTime)
                {
                    var note = Instantiate(notePrefab, transform);
                    notes.Add(note.GetComponent<Note>());
                    note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                    spawnIndex++;
                }
            }

            if (inputIndex < timeStamps.Count)
            {
                double timeStamp = timeStamps[inputIndex];
                double errorMargin = SongManager.Singleton.ErrorMarginInSeconds;
                double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Singleton.InputDelayInMilliseconds / 1000.0);
                double nearHitMargin = errorMargin + SongManager.Singleton.ErrorDeviation;

                if (IsButtonPressed())
                {
                    double tapTime = Math.Abs(audioTime - timeStamp);

                    if (tapTime < errorMargin)
                    {
                        Hit();
                        inputIndex++;
                    }
                    else if (tapTime < nearHitMargin)
                    {
                        NearHit(audioTime, timeStamp);
                    }
                }

                if (timeStamp + errorMargin <= audioTime)
                {
                    Miss();
                    inputIndex++;
                }
            }
        }

        public bool IsButtonPressed()
        {
            return (InputSystem.devices.Count > 0 && Keyboard.current[inputKeyboard].wasPressedThisFrame) || (Gamepad.all.Count > 0 && Gamepad.current[inputGamepad].wasPressedThisFrame);
        }

        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            foreach (var note in array)
            {
                if (note.NoteName == noteRestriction)
                {
                    MetricTimeSpan metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.MidiFile.GetTempoMap());
                    timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                }
            }
        }

        private void Hit()
        {
            ScoreManager.Hit();
            notes[inputIndex].Hit();
        }

        private void NearHit(double audioTime, double timeStamp)
        {
            ScoreManager.NearHit();
            notes[inputIndex].NearHit();
        }

        private void Miss()
        {
            ScoreManager.Miss();
        }
    }
}

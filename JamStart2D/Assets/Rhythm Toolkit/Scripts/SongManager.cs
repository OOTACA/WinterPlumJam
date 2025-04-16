using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaitoPath.Rhythm
{
    public class SongManager : MonoBehaviour
    {
        public static SongManager Singleton;

        [HideInInspector]
        public static MidiFile MidiFile;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] Lane[] lanes;
        [SerializeField] private string midiLocation;

        [SerializeField] private int inputDelayInMilliseconds;
        public int InputDelayInMilliseconds => inputDelayInMilliseconds;

        [SerializeField] private double errorMarginInSeconds = 0.1;
        public double ErrorMarginInSeconds => errorMarginInSeconds;

        [SerializeField] private float noteTime;
        public float NoteTime => noteTime;

        [SerializeField] private float noteSpawnY;
        public float NoteSpawnY => noteSpawnY;

        [SerializeField] private GameObject noteTapLine;

        [SerializeField] private double errorDeviation = 0.2; // deviation to calculate near hit
        public double ErrorDeviation => errorDeviation;

        [SerializeField] private float startSongDelay;
        [SerializeField] private float delayAfterMusicStop = 0.5f;

        [SerializeField] private SpriteRenderer backgroundSR;
        [SerializeField] private SpriteRenderer tapLineSR;
        [SerializeField] private float fadeDuration;


        public float NoteDespawnY
        {
            get
            {
                float noteTapY = noteTapLine.transform.position.y;
                return noteTapY - (noteSpawnY - noteTapY);
            }
        }

        public int AmountOfNotes { get; private set; }

        [SerializeField] private string RhythmPlayTag = "RhythmPlay";

        [SerializeField] private UnityEvent onPlayStart;
        [SerializeField] private UnityEvent onPlayStop;

        private void Awake()
        {
            Singleton = this;
        }

        private void Start()
        {
            onPlayStart?.Invoke();
            ReadFromFile();
        }

        private IEnumerator RhythmSpriteFade(float startValue, float endValue)
        {
            float elapsedTime = 0;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / fadeDuration);
                backgroundSR.color = new Color(backgroundSR.color.r, backgroundSR.color.g, backgroundSR.color.b, newAlpha);
                tapLineSR.color = new Color(tapLineSR.color.r, tapLineSR.color.g, tapLineSR.color.b, newAlpha);
                yield return null;
            }
        }

        private void ReadFromFile()
        {
            MidiFile = MidiFile.Read(midiLocation);
            GetDataFromMidi();
        }

        public void GetDataFromMidi()
        {
            var notes = MidiFile.GetNotes();
            var notesArr = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
            notes.CopyTo(notesArr, 0);
            AmountOfNotes = notes.Count;

            foreach (var lane in lanes)
                lane.SetTimeStamps(notesArr);

            Invoke(nameof(StartSong), startSongDelay);
            StartCoroutine(RhythmSpriteFade(0, backgroundSR.color.a));
            Invoke(nameof(StopPlaying), audioSource.clip.length + delayAfterMusicStop);
        }

        private void StopPlaying()
        {
            onPlayStop?.Invoke();
            ScoreManager.CalculatePerfection();
            Debug.Log(ScoreManager.Perfection);
            StartCoroutine(RhythmSpriteFade(backgroundSR.color.a, 0));
            Invoke(nameof(DisableRhythm), fadeDuration);
        }

        private void DisableRhythm()
        {
            var gos = GameObject.FindGameObjectsWithTag(RhythmPlayTag);
            foreach (var go in gos)
                go.SetActive(false);
        }

        private void StartSong()
        {
            audioSource.Play();
        }

        public static double GetAudioSourceTime()
        {
            return (double)Singleton.audioSource.timeSamples / Singleton.audioSource.clip.frequency; // current song position
        }
    }
}
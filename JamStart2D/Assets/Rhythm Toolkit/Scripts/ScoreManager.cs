using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaitoPath.Rhythm
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager Singleton;

        [SerializeField] AudioSource hitSFX;
        [SerializeField] AudioSource missSFX;

        private int comboScore;
        public int ComboScore => comboScore;

        private float perfection;
        public static float Perfection => Singleton.perfection;

        private float score;

        [SerializeField] private UnityEvent onHit;
        [SerializeField] private UnityEvent onNearHit;
        [SerializeField] private UnityEvent onMiss;

        private void Start()
        {
            Singleton = this;
        }

        public static void Hit()
        {
            Singleton.comboScore++;
            Singleton.score++;
            Singleton.hitSFX?.Play();
            Singleton.onHit?.Invoke();
        }

        public static void NearHit()
        {
            Singleton.score += 0.5f;
            Singleton.onNearHit?.Invoke();
        }

        public static void Miss()
        {
            Singleton.comboScore = 0;
            Singleton.score--;
            Singleton.missSFX?.Play();
            Singleton.onMiss?.Invoke();
        }

        public static void CalculatePerfection()
        {
            Singleton.perfection = Singleton.score / SongManager.Singleton.AmountOfNotes;
        }
    }
}
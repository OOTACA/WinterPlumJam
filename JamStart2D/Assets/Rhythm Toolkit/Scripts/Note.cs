using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace KaitoPath.Rhythm
{
    public class Note : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;

        [SerializeField] private Sprite spriteKeyboard;
        [SerializeField] private Sprite spriteGamepad;
        [SerializeField] private Sprite hitSpriteKeyboard;
        [SerializeField] private Sprite hitSpriteGamepad;
        [SerializeField] private Sprite nearHitSpriteKeyboard;
        [SerializeField] private Sprite nearHitSpriteGamepad;
        [SerializeField] private Sprite missSpriteKeyboard;
        [SerializeField] private Sprite missSpriteGamepad;

        private double timeInstantiated; // time when song has started
        private SpriteRenderer sprite;
        private bool isGamepad;
        private bool isHit;
        private bool isNearHit;
        private bool isMiss;

        [HideInInspector]
        public float assignedTime;

        [SerializeField] private UnityEvent onSpawn;
        [SerializeField] private UnityEvent onDestroy;
        [SerializeField] private UnityEvent onHit;
        [SerializeField] private UnityEvent onNearHit;
        [SerializeField] private UnityEvent onMiss;

        private void Start()
        {
            timeInstantiated = assignedTime - SongManager.Singleton.NoteTime;
            sprite = GetComponent<SpriteRenderer>();
            onSpawn?.Invoke();
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke();
        }

        private void Update()
        {
            UpdateSprite();

            double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
            float time = (float)(timeSinceInstantiated / (SongManager.Singleton.NoteTime * 2));

            if (time > 1) // if time > 1 than the note is out of tap bounds
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Singleton.NoteSpawnY, Vector3.up * SongManager.Singleton.NoteDespawnY, time);
                sprite.enabled = true;
            }
        }


        private void UpdateSprite()
        {
            isGamepad = string.Equals(input.currentControlScheme, "Gamepad");

            if (isHit)
            {
                sprite.sprite = isGamepad ? hitSpriteGamepad : hitSpriteKeyboard;
            }
            else if (isNearHit)
            {
                sprite.sprite = isGamepad ? nearHitSpriteGamepad : nearHitSpriteKeyboard;
            }
            else if (isMiss)
            {
                sprite.sprite = isGamepad ? missSpriteGamepad : missSpriteKeyboard;
            }
            else
            {
                sprite.sprite = isGamepad ? spriteGamepad : spriteKeyboard;
            }
        }

        public void Hit()
        {
            isHit = true;
            onHit?.Invoke();
        }

        public void NearHit()
        {
            isNearHit = true;
            onNearHit?.Invoke();
        }

        public void Miss()
        {
            isMiss = true;
            onMiss?.Invoke();
        }
    }
}

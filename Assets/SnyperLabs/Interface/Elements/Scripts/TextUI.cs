using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Interface.Elements.Scripts
{
    [RequireComponent(typeof(Text))]
    public class TextUI : MonoBehaviour
    {
        private Text text;
        private string originalText = "";
        private bool isAnimating;
        private readonly char[] randomChars = {'#','%','$','&','A','G','R','F','T','P'};

        [Tooltip("Is all the text capitalized")]
        [SerializeField] private bool isCapitalized;
        
        [Header("Animation")]
        
        [Tooltip("Animate the text to look as if it is typing")]
        [SerializeField] private bool animateTypingOnStart;
        
        [Tooltip("Adds a trailing underscore while animating")]
        [SerializeField] private bool addTrailingUnderscore;
        
        [Tooltip("Adds random characters while animating")]
        [SerializeField] private bool addRandomCharacters;

        [Tooltip("The speed at which the typing animation plays")] 
        [SerializeField] private float typingSpeed = 12;

        [Header("Sounds")] 
        [SerializeField] private AudioClip typingSound;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void OnValidate()
        {
            text = GetComponent<Text>();

            if (!string.Equals(text.text, originalText, StringComparison.CurrentCultureIgnoreCase))
            {
                originalText = text.text;
            }
            
            if (isCapitalized)
                text.text = text.text.ToUpper();
            else
            {
                text.text = originalText;
            }
        }

        private void Start()
        {
            if (animateTypingOnStart)
                StartAnimation();
        }


        public void StartAnimation()
        {
            if (isAnimating)
            {
                // Stop the previous animation if already animating
                isAnimating = false;
            }

            StartCoroutine(AnimateTypingEffect(typingSpeed));
        }

        private IEnumerator AnimateTypingEffect(float speed)
        {
            isAnimating = true;
            
            var original = text.text;
            var modified = new StringBuilder();
            text.text = "";
            var i = 0;

            foreach (var c in original)
            {
                modified.Append(" ");
            }

            modified.Append(" ");
            if (addRandomCharacters && addTrailingUnderscore) modified.Append(" ");
            var time = 1 / speed;
            
            while (i < original.Length)
            {
                if (!isAnimating) yield break;
                modified[i] = original[i];
                var incr = 1;
                if (addTrailingUnderscore) modified[i + incr++] = '_';
                if (addRandomCharacters) modified[i + incr] = randomChars[Random.Range(0, randomChars.Length)];
                text.text = modified.ToString();
                
                if (addRandomCharacters)
                {
                    float counter = 0;
                    while (counter < time)
                    {
                        modified[i + incr] = randomChars[Random.Range(0, randomChars.Length)];
                        text.text = modified.ToString();
                        yield return new WaitForSeconds(0.02f);
                        counter += 0.02f;
                    }
                }
                else
                {
                    yield return new WaitForSeconds(time);
                }

                AudioManager.Play(typingSound);
                i++;
            }

            if (addTrailingUnderscore) modified.Remove(i, 1);
            if (addRandomCharacters) modified.Remove(i, 1);
            text.text = modified.ToString();

            isAnimating = false;
        }
    }
}
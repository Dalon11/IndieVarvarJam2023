using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes
{
    public class BeginStoryView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] spriteStories;
        [SerializeField] private int idNextScene = 1;

        private int idStory = 0;

        public void NextStory()
        {
            idStory++;
            if (idStory > spriteStories.Length)
            {
                SceneManager.LoadScene(idNextScene);
                return;
            }

            image.sprite = spriteStories[idStory];
        }
    }
}
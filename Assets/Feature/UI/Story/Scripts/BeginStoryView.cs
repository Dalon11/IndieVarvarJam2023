using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jam.UI.Story
{
    public class BeginStoryView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] spriteStories;

        private int idStory = 0;

        public void SkipStory() => LoadNextScene();

        public void OnPointerClick(PointerEventData eventData) => NextStory();

        private void NextStory()
        {
            idStory++;
            if (idStory >= spriteStories.Length)
            {
                LoadNextScene();
                return;
            }

            image.sprite = spriteStories[idStory];
        }

        private void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
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
        [SerializeField] private int idNextScene = 1;

        private int idStory = 0;

        private void NextStory()
        {
            idStory++;
            if (idStory >= spriteStories.Length)
            {
                SceneManager.LoadScene(idNextScene);
                return;
            }

            image.sprite = spriteStories[idStory];
        }

        public void OnPointerClick(PointerEventData eventData) => NextStory();
    }
}
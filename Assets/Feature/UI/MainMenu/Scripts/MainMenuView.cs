using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        public void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

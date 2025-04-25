using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedMenu : MonoBehaviour
{
    [SerializeField] private Button _repeatButton;

    private void OnEnable()
    {
        _repeatButton.onClick.AddListener(OnRepeat);
    }

    private void OnDisable()
    {
        _repeatButton.onClick.RemoveListener(OnRepeat);
    }

    private void OnRepeat()
    {
        SceneManager.LoadScene("Main");
    }
}

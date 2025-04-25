using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _characterController;
    [SerializeField] private Finish _finish;
    [SerializeField] private LevelCompletedMenu _levelCompletedMenu;

    private void Awake()
    {
        _finish.PlayerFinished += OnPlayerFinished;
    }

    private void OnPlayerFinished()
    {
        _characterController.SetActive(false);
        _levelCompletedMenu.gameObject.SetActive(true);
    }
}

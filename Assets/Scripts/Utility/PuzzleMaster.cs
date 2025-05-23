using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleMaster : MonoBehaviour
{
    /// <summary>
    /// The instance of the puzzle master accessible by all classes.
    /// </summary>
    public static PuzzleMaster Instance = null;
    
    /// <summary>
    /// The scene corresponding to the next puzzle.
    /// </summary>
    [SerializeField] private Object nextPuzzle;

    /// <summary>
    /// The NPCs to be killed for this puzzle.
    /// </summary>
    [SerializeField] private List<NPC> npcs;

    /// <summary>
    /// True if the puzzle has another puzzle level after it, false otherwise.
    /// </summary>
    [SerializeField] private bool hasNextPuzzle = false;

    /// <summary>
    /// True if the puzzle has been solved, false otherwise.
    /// </summary>
    private bool _puzzleSolved = false;

    /// <summary>
    /// The audio source component (plays background music).
    /// </summary>
    private AudioSource _audioSource;

    private Dictionary<string, string> _puzzleAndNextPuzzle = new Dictionary<string, string>()
    {
        {"PuzzleOne", "PuzzleTwo"},
        {"PuzzleTwo", "PuzzleThree"},
        {"PuzzleThree", "PuzzleFour"},
        {"PuzzleFour", "PuzzleFive"},
        {"PuzzleFive", "PuzzleSeven"},
        {"PuzzleSeven", "PuzzleEight"}
    };

    /// <summary>
    /// Initializes the instance.
    /// </summary>
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets the number of NPCs to kill and sets the player's health.
    /// </summary>
    private void Start()
    {
        PlayerControl.Instance.SetHealth(MainManager.Instance.health);
        PlayerControl.Instance.GetWeapon().SetAmmo(MainManager.Instance.ammo);
        
        InGameUI.Instance.SetActionText("HUNT!");
        
        MainManager.Instance.currentPuzzle = SceneManager.GetActiveScene().name;
        
        _audioSource.Play();
    }

    /// <summary>
    /// Kills an NPC and updates according fields.
    /// </summary>
    public void KillNPC(NPC npc)
    {
        npcs.Remove(npc);
        if (npcs.Count != 0) return;
        _puzzleSolved = true;
        InGameUI.Instance.StopCoroutine();
        Invoke(hasNextPuzzle ? nameof(LoadNextPuzzle) : nameof(Win), 1);
    }

    /// <summary>
    /// Loads the next puzzle.
    /// </summary>
    private void LoadNextPuzzle()
    {
        if (_audioSource.isPlaying) _audioSource.Stop();

        if (hasNextPuzzle)
        {
            GameEvent.SetNextPuzzle(nextPuzzle);
            Invoke(nameof(LoadNextPuzzle), 1);
        }
        else
        {
            Invoke(nameof(Win), 1);
        }

        string puzzleName = _puzzleAndNextPuzzle[SceneManager.GetActiveScene().name];
        SceneManager.LoadScene(puzzleName);
    }

    /// <summary>
    /// Takes the player to the win screen when they have completed all the levels.
    /// </summary>
    private void Win()
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        SceneManager.LoadScene("GameOverWin");
    }
}
using UnityEngine;

public static class GameEvent
{
    /// <summary>
    /// Handles the kill timer.
    /// </summary>
    public delegate void KillTimerHandler(int time);

    /// <summary>
    /// Handles the game ending.
    /// </summary>
    public delegate void GameOverHandler(bool won);

    /// <summary>
    /// Handles starting the hide timer.
    /// </summary>
    public delegate void StartHideTimerHandler(int time);
    
    /// <summary>
    /// Handles stopping the hide timer.
    /// </summary>
    public delegate void StopHideTimerHandler();

    /// <summary>
    /// Handles the puzzle the player will return to after the hide timer ends.
    /// </summary>
    public delegate void NextPuzzleHandler(Object puzzle);

    /// <summary>
    /// Detects when the kill timer should be changed.
    /// </summary>
    public static event KillTimerHandler OnKillTimerChange;

    /// <summary>
    /// Detects when the game is over.
    /// </summary>
    public static event GameOverHandler OnGameOver;
    
    /// <summary>
    /// Detects when the hide timer should be started.
    /// </summary>
    public static event StartHideTimerHandler OnHideTimerStart;
    
    /// <summary>
    /// Detects when the hide timer should be stopped.
    /// </summary>
    public static event StopHideTimerHandler OnHideTimerStop;
    
    /// <summary>
    /// Detects when the next puzzle should be loaded after the hide timer is up.
    /// </summary>
    public static event NextPuzzleHandler OnNextPuzzle;

    /// <summary>
    /// Increases/decreases the maximum kill timer duration (in seconds).
    /// </summary>
    /// <param name="time">The new timer modification (in seconds), positive being an increase and negative being
    /// a decrease.</param>
    public static void ChangeKillTimerDuration(int time) => OnKillTimerChange?.Invoke(time);

    /// <summary>
    /// Ends the game based on whether the player won or lost.
    /// </summary>
    /// <param name="won">True if the player won, false otherwise.</param>
    public static void EndGame(bool won) => OnGameOver?.Invoke(won);
    
    /// <summary>
    /// Starts the hide timer with the specified duration (in seconds).
    /// </summary>
    /// <param name="time">The timer duration (in seconds).</param>
    public static void StartHideTimer(int time) => OnHideTimerStart?.Invoke(time);
    
    /// <summary>
    /// Stops the hide timer.
    /// </summary>
    public static void StopHideTimer() => OnHideTimerStop?.Invoke();
    
    /// <summary>
    /// Sets the puzzle the player failed to complete when the hide timer is up.
    /// </summary>
    /// <param name="puzzle">The puzzle to be loaded.</param>
    public static void SetNextPuzzle(Object puzzle) => OnNextPuzzle?.Invoke(puzzle);
}
﻿using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject goalLine;
    [SerializeField] private GameObject goalText;
    [SerializeField] private string nextSceneName = "NextScene"; 
    [SerializeField] private AudioClip goalSound;

    private bool goalReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (goalReached) return;

        if (other.CompareTag("Player"))
        {
            goalReached = true;

            GameData.Score = CollectibleManager.Instance.GetScore();

            if (goalLine != null) goalLine.SetActive(false);

            if (goalSound != null)
                AudioSource.PlayClipAtPoint(goalSound, transform.position);

            if (goalText != null) goalText.gameObject.SetActive(true);

            UIManager.Instance?.StopTimer();

            Invoke(nameof(LoadNextScene), 3f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

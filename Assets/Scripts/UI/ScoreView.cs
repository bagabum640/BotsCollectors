using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ResourceCounter _scoreCounter;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable() =>
        _scoreCounter.ResourceChanged += OnScoreChanged;

    private void OnDisable() =>
        _scoreCounter.ResourceChanged -= OnScoreChanged;

    private void OnScoreChanged(int score) =>
        _scoreText.text = score.ToString();
}
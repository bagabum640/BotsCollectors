using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour, IScoreView
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ResourceCounter _scoreCounter;

    private void OnEnable() =>
        _scoreCounter.ResourceChanged += OnScoreChanged;

    private void OnDisable() =>
        _scoreCounter.ResourceChanged -= OnScoreChanged;

    public void OnScoreChanged(int score) =>
        _scoreText.text = score.ToString();
}
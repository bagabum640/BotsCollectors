using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable() =>
            _resourceCounter.ResourceChanged += OnScoreChanged;

    private void OnDisable() =>
           _resourceCounter.ResourceChanged -= OnScoreChanged;

    private void OnScoreChanged(int score) =>
        _scoreText.text = score.ToString();
}
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ResourceCounter _resourceCounter;
    [SerializeField] private TMP_Text _scoreText;

    public void Init(ResourceCounter resourceCounter)
    {
        resourceCounter.ResourceChanged += OnScoreChanged;
    }

    private void OnEnable()
    {
        if (_resourceCounter != null)
            _resourceCounter.ResourceChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        if (_resourceCounter != null)
            _resourceCounter.ResourceChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score) =>
        _scoreText.text = score.ToString();
}
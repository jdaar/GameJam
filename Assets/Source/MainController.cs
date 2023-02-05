using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainController : MonoBehaviour
{
    private int _score = 0;
    private int _round = 0;
    private int _life = 10;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _roundText;
    [SerializeField] private TMP_Text _lifeText;
    void Start()
    {
        _roundText.text = "Ronda " + _round;
        _scoreText.text = _score + (_score > 1 ? " puntos" : " punto");
        _lifeText.text = _life + (_life > 1 ? " vidas" : " vida");
        MainEvent.OnEnemyDeath += this.OnEnemyDeath;
        MainEvent.OnPlayerTakeDamage += this.OnPlayerTakeDamage;
        MainEvent.OnPlayerDeath += this.OnPlayerDeath;
        StartCoroutine(RestartEnemies());
    }


    void Update() { }

    public void OnEnemyDeath() {
        _score += 1;
        _scoreText.text = _score + (_score > 1 ? " puntos" : " punto");
    }

    public void OnPlayerTakeDamage() {
        _life -= 1;
        _lifeText.text = _life + (_life > 1 ? " vidas" : " vida");
        if (_life <= 0) {
            
            MainEvent.OnPlayerDeath?.Invoke();
        }
    }

    public void OnPlayerDeath() {
        
        SceneManager.LoadScene("Main Menu");
        
    }

    private IEnumerator RestartEnemies() {
     for(;;) {
        _round += 1;
        _roundText.text = "Ronda " + _round;
        MainEvent.OnEnemyRestart?.Invoke();
        yield return new WaitForSeconds(1*60);
     }
 }
}

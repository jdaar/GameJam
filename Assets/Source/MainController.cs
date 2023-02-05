using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(RestartEnemies());
    }


    void Update() { }

    private IEnumerator RestartEnemies() {
     for(;;) {
         MainEvent.OnEnemyRestart?.Invoke();
         yield return new WaitForSeconds(1*60);
     }
 }
}

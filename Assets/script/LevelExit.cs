using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]private float timeDelay=1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag=="Player")
        {
            StartCoroutine(exit());
        }
    }
    IEnumerator exit()
    {
        yield return new WaitForSeconds(timeDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

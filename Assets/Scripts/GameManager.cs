using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public List<GameObject> levels;
    int enemyCount;
    int level = -1;
    UIManager um;

    //For AudioFW test
    Dictionary<KeyCode, string> bindings = new Dictionary<KeyCode, string>();

    void Start() {
        um = FindObjectOfType<UIManager>();
        SetLevel();

        // For AudioFW test
        bindings.Add(KeyCode.P, "TargetExplodes");
    }

    public void EnemyKilled() {
        enemyCount--;
        if(enemyCount < 1) {
            LevelComplete();
        }
    }

    public void LevelComplete() {
        if(level > levels.Count - 2) {
            um.SetUIText("The Whole Game Completed");
            Invoke("GameOver", 5f);
            print("Game Over");
            return;
        }
        um.SetUIText("Level " + (level + 1) + " Complete");

        Invoke("SetLevel", 2f);
    }

    public void SetLevel() {
        level++;
        um.SetUIText("Level " + (level + 1));
        HideLevel();
        Invoke("DisplayLevel", 2f);
    }

    void DisplayLevel() {
        for(int i = 0; i < levels.Count; i++) {
            if(i == level) {
                levels[i].gameObject.SetActive(true);
            } else {
                levels[i].gameObject.SetActive(false);
            }
        }
        var e = GameObject.FindObjectsOfType<Destructible>();
        enemyCount = e.Length;
    }

    void HideLevel() {
        for(int i = 0; i < levels.Count; i++) {
            levels[i].gameObject.SetActive(false);
        }
    }

    
    void GameOver() {
        SceneManager.LoadScene(0);
    }

    //void Update() {
    //    // Audio Manager test (AudioFW.cs)
    //    foreach (var kc in bindings.Keys) {
    //        if (Input.GetKeyDown(kc))
    //            AudioFW.Play(bindings[kc]);
    //    }
    //}

}

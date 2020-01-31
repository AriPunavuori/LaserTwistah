using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public List<GameObject> levels;
    int level = -1;
    UIManager um;
    void Start() {
        um = FindObjectOfType<UIManager>();
        SetLevel();
    }

    public void LevelComplete() {
        if(level > levels.Count - 2) {
            um.SetUIText("The Whole Game Completed");
            Invoke("GameOver", 5f);
            print("Game Over");
            return;
        }

        um.SetUIText("Level " + (level + 1) + " Complete");
        print("Level Complete");
        Invoke("SetLevel", 2f);
    }

    public void SetLevel() {
        level++;
        um.SetUIText("Level " + (level + 1));
        for(int i = 0; i < levels.Count; i++) {
            if (i == level) {
                levels[i].gameObject.SetActive(true);
            } else {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
    void GameOver() {
        SceneManager.LoadScene(0);
    }
}

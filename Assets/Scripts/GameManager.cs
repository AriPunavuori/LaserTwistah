using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public List<GameObject> levels;
    int enemyCount;
    int level = 0;
    UIManager um;
    Laser laser;

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

    public void Friendykilled() {
        um.SetUIText("Level " + (level + 1) + " Failed", 2f);
        laser.TurnOff();
        Invoke("LevelFailed", 2f);
    }

    public void LevelComplete() {
        if(level > levels.Count - 2) {
            um.SetUIText("Congratulations!\nGame Completed", 10f);
            Invoke("GameOver", 5f);
            print("Game Over");
            laser.TurnOff();
            return;
        }
        um.SetUIText("Level " + (level + 1) + " Complete", 2f);
        laser.TurnOff();
        Invoke("NextLevel", 2f);
    }

    void NextLevel() {
        level++;
        SetLevel();
    }
    void LevelFailed() {
        SetLevel();
    }
    public void SetLevel() {
        um.SetUIText("Level " + (level + 1), 3f);
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
        laser = GameObject.FindObjectOfType<Laser>();
        laser.TurnOn();
        enemyCount = 0;
        foreach(var d in e) {
            if(!d.isFriendly)
                enemyCount++;
        }
        //enemyCount = e.Length;
        print(enemyCount);
    }

    void HideLevel() {
        for(int i = 0; i < levels.Count; i++) {
            levels[i].gameObject.SetActive(false);
        }
    }

    void GameOver() {
        SceneManager.LoadScene(0);
    }

    void Update() {
        // Audio Manager test (AudioFW.cs)
        foreach(var kc in bindings.Keys) {
            if(Input.GetKeyDown(kc))
                AudioFW.Play(bindings[kc]);
        }
    }
}

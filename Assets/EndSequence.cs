using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequence : MonoBehaviour
{
    [SerializeField]
    GameObject pressEtoKill;

    [SerializeField]
    GameInput gameInput;

    //[SerializeField]
    //GameObject finalCutSceneTimeline;

    private void Start()
    {
        gameInput.FinalCutScenePlay += GameInput_FinalCutScenePlay;
    }

    private void GameInput_FinalCutScenePlay(object sender, System.EventArgs e)
    {
        //finalCutSceneTimeline.SetActive(true);
        SceneManager.LoadScene("FinalCutScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pressEtoKill.SetActive(true);
            GameManager.canFinalKill = true;
        }
    }



}

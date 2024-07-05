using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Video : MonoBehaviour
{

    public GameObject video;
    // Start is called before the first frame update
    void Start()
    {
        video.SetActive(true);
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);

    }
}

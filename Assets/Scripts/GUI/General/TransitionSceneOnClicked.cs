using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSceneOnClicked : MonoBehaviour
{
    public string destSceneName;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(onClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClicked() 
    {
        SceneManager.LoadScene(destSceneName);
    }

    private void Reset()
    {
        destSceneName = "SampleScene";
        button=GetComponent<Button>();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Scene: MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen; 
    [SerializeField] private string sceneToLoadName;
    // Start is called before the first frame update
    public void loadScene()
    {
        StartCoroutine(LoadSceneCoroutine()); 
    }

    // Update is called once per frame
    private IEnumerator LoadSceneCoroutine()
    {
        
        var loadingScreenInstance = Instantiate(loadingScreen);
        DontDestroyOnLoad(loadingScreenInstance);
        var loadingAnimator = loadingScreenInstance.GetComponent<Animator>();
        var currentAnimTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;
        var loading = SceneManager.LoadSceneAsync(sceneToLoadName);
        loading.allowSceneActivation = false;
        while (!loading.isDone)

            if (loading.progress >= 0.9f)

                loadingAnimator.SetTrigger("Disappear");
                loading.allowSceneActivation = true; 

            yield return new WaitForSeconds(currentAnimTime);
    }
}

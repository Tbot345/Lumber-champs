using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void LoadSceneByName(string name){
        SceneManager.LoadScene(name);
    }

    public void LoadSceneByID(int index){
        SceneManager.LoadScene(index);
    }
}

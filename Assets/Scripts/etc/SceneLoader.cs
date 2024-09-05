using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>{
    public void LoadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadEndLess(){
        SceneManager.LoadScene("EndlessMode");
    }
}
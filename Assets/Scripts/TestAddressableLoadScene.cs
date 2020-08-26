using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public class TestAddressableLoadScene : MonoBehaviour    
{

    public string AddressableNameOfTextFile;

    private AsyncOperationHandle<SceneInstance> handle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene() {
        //Addressables.LoadSceneAsync("001_500-game-the-house", loadMode: LoadSceneMode.Single);
        //Addressables.LoadSceneAsync("001_001-the-shapes", loadMode: LoadSceneMode.Single).Completed += SceneCompleted;
        //Addressables.LoadScene("001_500-game-the-house", loadMode: LoadSceneMode.Additive);
    }

    private void SceneCompleted(AsyncOperationHandle<SceneInstance> obj) {
        if (obj.Status == AsyncOperationStatus.Succeeded) {
            Debug.Log("Successfully loaded scene");
            handle = obj;
        }
    }

    public async void LoadTextFile()
    {
        
        TextAsset textFile = await AddressablesExtension.LoadAsset<TextAsset>(this.AddressableNameOfTextFile);
        Debug.Log(textFile.text);

    }

}

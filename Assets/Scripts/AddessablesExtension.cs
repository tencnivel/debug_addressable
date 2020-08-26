#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;


    // Credit: https://medium.com/@5argon/unity-addressables-7c417e14fe2c
    //         https://raw.githubusercontent.com/5argon/E7Unity/master/AddressablesExtension/AddressablesExtension.cs

    public static class AddressablesExtension
    {
        

        /// <summary>
        /// Use the Addressables system if in real play, use `AssetDatabase` if in edit mode.
        /// </summary>
        /// <param name="keyOrPath">Addressable key for addressable or path to asset in AssetDatabase (you need the file extension as well)</param>
        public static async Task<T> LoadAsset<T>(string keyOrPath,
                                                 bool exceptionIfNotFound = true,
                                                 bool forceLoadingFromAssetDatabase = false) where T : UnityEngine.Object
        {

            string errorMsg = string.Format("Unable to find asset[{0}]", keyOrPath);

#if UNITY_EDITOR
            if (!Application.isPlaying || forceLoadingFromAssetDatabase)
            {
                T o = AssetDatabase.LoadAssetAtPath<T>(keyOrPath);
                if (o == null)
                {
                    if (exceptionIfNotFound)
                    {
                        throw new Exception(errorMsg);
                    }
                    else
                    {
                        Debug.LogWarning(errorMsg);
                        return null;
                    }
                }
                return o;
            }
#endif

            if (Application.isPlaying)
            {
                try
                {

                    Debug.LogFormat("## DEBUG Load {0} from Addressables", keyOrPath);
                    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyOrPath);
                    //Debug.LogFormat("## LoadAsset HERE0");
                    await handle.Task; // wait for the task to complete before we try to get the result
                    T result = (T)handle.Result;
                    Debug.LogFormat("## DEBUG LoadAsset (result == null)[{0}] for [{1}]", (result == null), keyOrPath);
                    Debug.LogFormat("## DEBUG handle.Status[{0}] for [{1}]", handle.Status.ToString(), keyOrPath);
                    if (result == null) {                        
                        throw handle.OperationException;
                    }
                    //Addressables.Release(handle); // release the handler for memory usage
                    //Debug.LogFormat("## LoadAsset HERE3 (result == null)[{0}]", (result == null));
                    return result;
                }
                catch (InvalidKeyException e)
                {
                    if (exceptionIfNotFound)
                    {
                        Debug.LogWarning(errorMsg);
                        throw e;
                    }
                    else
                    {
                        Debug.LogWarning(errorMsg);
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning(errorMsg);
                    throw e;

                }


        }

            throw new Exception(errorMsg);
            //return null;

        }
     
    }


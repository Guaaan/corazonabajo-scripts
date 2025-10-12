using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMgr : MonoBehaviour
{

    public void CargaNivel(string NombreNivel)
    {
        SceneManager.LoadScene(NombreNivel);
    }
}
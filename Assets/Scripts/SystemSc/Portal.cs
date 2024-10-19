using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // ¿Ãµø«“ ∏ ¿« æ¿ ≥—πˆ º≥¡§ (≥Û¿Â:0, µø±º:1, ªÁ≥…≈Õ:2)
    [SerializeField] private int portalID = 0;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(portalID);
    }
}

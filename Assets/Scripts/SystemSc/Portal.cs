using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // ¿Ãµø«“ ∏ ¿« æ¿ ≥—πˆ º≥¡§ (≥Û¿Â:1, µø±º:2, ªÁ≥…≈Õ:3)
    [SerializeField] private int portalID = 0;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(portalID);
    }
}

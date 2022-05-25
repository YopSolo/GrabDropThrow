using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGrabbableObject : MonoBehaviour
{
    public GameObject grabbableObject = null;
    public Color originalColor;
    public Color selectedColor;

    public float grabDistance = 3.0f;

    void FixedUpdate()
    {
        // on teste le layer Grabbable (le layer 8), en ligne droite depuis la camera sur une distance de 3 mètres
        int layerMask = 1 << 8;
        RaycastHit hit;

        // pour voir les raycast de debug on peut passer dans l'onglet #Scene, le rayon sera jaune si il touche un objet grabbable et blanc dans le cas contraire
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance, layerMask))
        {
            grabbableObject = hit.rigidbody.gameObject;
            grabbableObject.GetComponent<Renderer>().material.color = selectedColor;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        } else {
            if (grabbableObject != null)
            {
                grabbableObject.GetComponent<Renderer>().material.color = originalColor;
            }
            grabbableObject = null;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * grabDistance, Color.white);
        }
    }
}

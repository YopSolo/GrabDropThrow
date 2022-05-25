using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    CheckForGrabbableObject _checkComponentRef;

    [SerializeField]
    private GameObject _currentObject = null;

    [Range(30.0f, 1000.0f)]
    public float ThrowPower = 300.0f;

    private void Awake()
    {
        _checkComponentRef = GetComponent<CheckForGrabbableObject>();
    }

    public string Grab()
    {
        // si on a les mains vides
        if (_currentObject == null)
        {
            // si il y a quelque chose a grab à distance
            if (_checkComponentRef.grabbableObject != null)
            {
                // on rammasse l'objet
                _currentObject = _checkComponentRef.grabbableObject;

                // le code pour accrocher l'objet au joueur
                _currentObject.GetComponent<Rigidbody>().isKinematic = true;
                _currentObject.transform.parent = gameObject.transform;

                //
                return "J'ai ramassé l'objet : " + _checkComponentRef.grabbableObject;
            }
            else
            {
                // sinon, si il n'y a rien, renvoi le message ci dessous
                return "Il n'y a rien à ramasser ici.";
            }
        }
        else
        {
            // ici on porte deja quelque chose, du coup on appelle la fonction private ReleaseGrabbedObject
            var message = ReleaseGrabbedObject();
            _currentObject = null;

            return message;
        }
    }

    private string ReleaseGrabbedObject()
    {
        var message = "Je lache l'objet : " + _currentObject.name;
        _currentObject.GetComponent<Rigidbody>().isKinematic = false;
        _currentObject.transform.parent = null;

        return message;
    }

    public string Throw()
    {
        if (_currentObject)
        {
            string message = "Je jette l'objet : " + _currentObject.name;
            // on lache l'objet
            ReleaseGrabbedObject();
            // et on ajoute une force
            _currentObject.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowPower);
            
            _currentObject = null;

            return message;
        }

        return "Je n'ai rien à jetter.";
    }
}

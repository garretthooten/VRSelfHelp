using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketReparent : MonoBehaviour
{

    public GameObject vase;
    [SerializeField] private XRSocketInteractor socket;

    // Start is called before the first frame update
    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    public void Reparent()
    {
        Debug.Log("Inside of the socket!");
        Vector3 scale = socket.selectTarget.transform.localScale;
        socket.selectTarget.transform.SetParent(vase.transform, true);
        socket.selectTarget.transform.localScale = scale;
    }

    public void Unparent()
    {
        Debug.Log("Outside of the socket!");
        socket.selectTarget.transform.parent = null;
    }
}

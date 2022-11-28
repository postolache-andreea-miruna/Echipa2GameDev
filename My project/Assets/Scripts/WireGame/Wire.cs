using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public LineRenderer Line;
    public Transform EndWire;
    public AudioClip SelectSound;
    public AudioClip ConnectSound;
    public AudioClip ErrorSound;

    bool Dragging = false;
    bool Connected = false;
    Vector3 OriginalPosition;

    void Start()
    {
        OriginalPosition = transform.position;       
    }

    void Update()
    {
        if (Dragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            SetPosition(convertedMousePosition);

            Vector3 endWireDifference = convertedMousePosition - EndWire.position;
            if (endWireDifference.magnitude < .5f)
            {
                SetPosition(EndWire.position);
                Dragging = false;
                Connected = true;
                AudioSource.PlayClipAtPoint(ConnectSound, Camera.main.transform.position);
            }
        }
        
        
    }

    void SetPosition(Vector3 position)
    {
        transform.position = position;

        Vector3 positionDifference = position - Line.transform.position;
        Line.SetPosition(2, positionDifference - new Vector3(.5f, 0, 0)); // Moving a little to the left
        Line.SetPosition(3, positionDifference - new Vector3(.15f, 0, 0));
    }

    void ResetPosition()
    {
        SetPosition(OriginalPosition);
    }

    private void OnMouseDown()
    {
        Dragging = true;
        AudioSource.PlayClipAtPoint(SelectSound, Camera.main.transform.position);
    }
    private void OnMouseUp()
    {
        Dragging = false;
        if (!Connected)
        {
            AudioSource.PlayClipAtPoint(ErrorSound, Camera.main.transform.position);
            ResetPosition();
        }
    }

    public bool IsConnected()
    {
        return Connected;
    }
    public void SetConnected(bool connected)
    {
        Connected = connected;
        if(!Connected)
        {
            ResetPosition();
        }
    }
}

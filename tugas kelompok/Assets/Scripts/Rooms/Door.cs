using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    // New variables
    [SerializeField] private bool isFinalDoor = false;
    [SerializeField] private GameObject gameFinishUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isFinalDoor)
            {
                // Show game finish UI
                gameFinishUI.SetActive(true);
                // Optionally, stop player movement or other actions
                // collision.GetComponent<PlayerController>().enabled = false;
            }
            else
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    cam.MoveToNewRoom(nextRoom);
                    nextRoom.GetComponent<Room>().ActivateRoom(true);
                    previousRoom.GetComponent<Room>().ActivateRoom(false);
                }
                else
                {
                    cam.MoveToNewRoom(previousRoom);
                    previousRoom.GetComponent<Room>().ActivateRoom(true);
                    nextRoom.GetComponent<Room>().ActivateRoom(false);
                }
            }
        }
    }
}

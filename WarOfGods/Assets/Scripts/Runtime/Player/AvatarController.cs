using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class AvatarController : NetworkBehaviour
{
    [Header("Refernces")]
    [SerializeField] private CharacterController _charCon;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;

    [ClientCallback]
    private void Update()
    {
        if (!isOwned) return;

        var movement = new Vector3();

        if (Keyboard.current.wKey.isPressed) { movement.z += 1; }
        if (Keyboard.current.sKey.isPressed) { movement.z -= 1; }
        if (Keyboard.current.aKey.isPressed) { movement.x += 1; }
        if (Keyboard.current.dKey.isPressed) { movement.x -= 1; }

        _charCon.Move(movement * movementSpeed * Time.deltaTime);

        if (_charCon.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }    
}

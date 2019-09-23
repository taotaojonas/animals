using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.state == GameManager.State.Play)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Rotate
                GameManager.instance.blockManager.activeBlock.Rotate(1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                // Move Left
                GameManager.instance.blockManager.activeBlock.Move(Vector3.left * GameManager.instance.blockManager.horizontalMoveLength);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GameManager.instance.blockManager.activeBlock.Move(Vector3.right * GameManager.instance.blockManager.horizontalMoveLength);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                GameManager.instance.blockManager.activeBlock.Move(Vector3.down * GameManager.instance.blockManager.horizontalMoveLength);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            GameManager.instance.Restart();
        }
    }
}

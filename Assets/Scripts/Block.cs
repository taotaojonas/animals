using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        Giraffe, Panda, Crocodile, Moose, Chicken, Elephant, Fox
    }

    public delegate void BlockRemoveDelegate(int score);
    public static event BlockRemoveDelegate OnBlockRemoved;

    public delegate void BlockHitDelegate(Block block, GameObject otherObject);
    public static event BlockHitDelegate OnBlockHit;
    
    public int score = 10;
    public BlockType blockType;

    Rigidbody2D body;
    private bool alive = true;
    private Vector2 velocity;
    Collider2D myCollider;
    // Start is called before the first frame update
    Collider2D[] collider2Ds = new Collider2D[10];
    ContactFilter2D contactFilter2D = new ContactFilter2D();
    private Vector3 previousPosition;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Remove()
    {
        OnBlockRemoved?.Invoke(score);
        Destroy(gameObject);
    }

    public void Rotate(int rotation)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.eulerAngles.z + 90f);
    }

    internal void Move(Vector3 v)
    {
        Vector3 newVector = transform.position + v;
        previousPosition = transform.position;
        //if (newVector.x < -2.6f || newVector.x >  2.6f || !alive)
        //{
        //    return;
        //}
        body.AddForce(v);
        //body.MovePosition(newVector);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!alive) return;

        Collider2D coll = collision.collider;
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = coll.bounds.center;

        if (collision.gameObject.GetComponent<Block>())
        {
            alive = false;
            collision.gameObject.GetComponent<Block>().alive = false;
            Stop();
            
            // event to stop block. deactivate active block
            OnBlockHit?.Invoke(this, collision.gameObject);
            
        }
        else if (collision.gameObject.GetComponent<Floor>())
        {
            alive = false;
            Stop();
            
            OnBlockHit?.Invoke(this, collision.gameObject);
            
        }
    }

    void Stop()
    {
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Scipt controlling PacMans actions and interactions.
//Gives a constant speed to PacMan to mimic the game.
//directions are stored until useful.


public class player_move: MonoBehaviour {         

    private Rigidbody rb2d;
	 Vector3 movement = Vector3.right;  
	 public float speed = 2f;
    public int direction = 2;

     public canturn left;  
     public canturn right; 
     public canturn up; 
     public canturn down; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody> ();

        left = GameObject.Find("left").GetComponent<canturn>();
        right = GameObject.Find("right").GetComponent<canturn>();
        up = GameObject.Find("up").GetComponent<canturn>();
        down = GameObject.Find("down").GetComponent<canturn>();
    }

    // direction is changed and stored so PacMan can use it when this direction if free.
    void FixedUpdate()
    {
       if (Input.GetKey(KeyCode.RightArrow)){
            direction = 2;
             }else if (Input.GetKey(KeyCode.DownArrow)){
            direction = 4;
            }else if (Input.GetKey(KeyCode.LeftArrow)){
                direction = 1;
            }else if (Input.GetKey(KeyCode.UpArrow)){
                 direction = 3;
            }


        //PacMan will only turn if it's possible. Otherwise, it will go on with previous direction
        //until he hits a wall.

        if (direction == 1 && left.turn == true)
        movement = Vector3.left;
        else if (direction == 2 && right.turn == true)
        movement = Vector3.right;    
        else if (direction == 3 && up.turn == true)
        movement = Vector3.up;
        else if (direction == 4 && down.turn == true)
        movement = Vector3.down;


        //PacMan moves constantly.
        
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime );
    }

    //If PacMan collides with a pellet, destroys it.
    //If PacMan collides with a ghost, go to lose screen.

     void OnCollisionEnter(Collision col) {
         if (col.gameObject.tag == "pellet")
        {
            Destroy(col.gameObject);
        } 

        if (col.gameObject.tag == "ghost" && col.gameObject.GetComponent<Chasing>().scared == false)
        {
           
            SceneManager.LoadScene("lose", LoadSceneMode.Single);
        }
}
}
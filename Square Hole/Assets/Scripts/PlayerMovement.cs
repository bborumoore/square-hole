using UnityEngine;

[System.Serializable]
public enum SIDE {Left, Mid, Right};
// public enum HitX {Left, Mid, Right, None};
// public enum HitY {Up, Mid, Down, None};
// public enum HitZ {Forward, Mid, Backward, None};
public class PlayerMovement : MonoBehaviour
{
    public SIDE m_SIDE = SIDE.Mid;
    float NewXPos = 0f;
    private float x_pos;
    private float y_pos;
    private float z_pos;
    public float XValue;
    public bool moveRight = false;
    public bool moveLeft = false;
    public bool colorUp = false;
    public bool colorDown = false;
    // variables to be used if I need to detect player/object orientation at collision
    // public HitX hitX = HitX.None;
    // public HitY hitY = HitY.None;
    // public HitZ hitZ = HitZ.None;
    
    private CharacterController m_char;
    // public Rigidbody rb;
    public float forwardSpeed = 7f;
    
    // Variables to allow player to change color in game
    public Material[] material;
    public int x_mat;
    Renderer rend;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        x_mat = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x_mat];
    }

    // public void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     // hitX = GetHitX(col);
    //     Debug.Log(hit);
    // }

    // Potentially usefull code to tell orientation of character to object in collision
    // public HitX GetHitX(Collider col)
    // {
    //     Bounds char_bounds = m_char.bounds;
    //     Bounds col_bounds = col.bounds;
    //     float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
    //     float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
    //     float average = (min_x + max_x) / 2f - col_bounds.min.x;
    //     HitX hit;
    //     if (average > col_bounds.size.x - .33f)
    //     {
    //         hit = HitX.Right;
    //     } else if (average < .33f)
    //     {
    //         hit = HitX.Left;
    //     } else {
    //         hit = HitX.Mid;
    //     } return hit;
    // }

    void Update()
    {
      
        // Check for player input to switch character left/right one lane
        moveLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        // Chunk of code to determine next side for player to be on
        if (moveLeft)
        {
            if (m_SIDE == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_SIDE = SIDE.Left;
            }
            else if (m_SIDE == SIDE.Right)
            {
                NewXPos = 0;
                m_SIDE = SIDE.Mid;
            }
        }
        else if (moveRight)
        {
            if (m_SIDE == SIDE.Mid)
            {
                NewXPos = XValue;
                m_SIDE = SIDE.Right;
            }
            else if (m_SIDE == SIDE.Left)
            {
                NewXPos = 0;
                m_SIDE = SIDE.Mid;
            }
        }

        // Check for player input to change color up/down
        colorUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        colorDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        // Code to determine next color in the case of player input
        if (colorUp) 
        {
            if (x_mat < 2)
            {
                x_mat++;
            } else {
                x_mat = 0;
            }
        } else if (colorDown)
        {   
            if (x_mat > 0 )
            {
                x_mat--;
            } else {
                x_mat = 2;
            } 
        }   

        // Move the player consistently forward
        Vector3 moveVector = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        m_char.Move(moveVector);
       
    }

    void FixedUpdate(){    
        // Add a consistent forward force to the player
        // rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);

        // Position change is in FixedUpdate because it plays better with Unity Physics
        m_char.Move((NewXPos-transform.position.x) * Vector3.right);

         // This code will render the new color based on player input
        rend.sharedMaterial = material[x_mat];
    }
}

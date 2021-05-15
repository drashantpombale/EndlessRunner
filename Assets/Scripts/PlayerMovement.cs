using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Vector3 touchstart, touchend;
    private Rigidbody rgbd;
    private CapsuleCollider collider;
    public GameObject Life;
    public GameObject GameOverScreen;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button slideButton;
    private Image[] hpmeter;
    enum POS {left,mid,right}
    private POS current;
    bool isGrounded;
    private bool isSliding;
    private bool ishurt;




    private int coins;
    private int score;
    public Text coinscore;
    public Text footscore;
    public Text time;
    private int hp;
    float timevalue;
    // Start is called before the first frame update
    void Start()
    {
        timevalue = 0.0f;
        ishurt = false;
        hpmeter = Life.GetComponentsInChildren<Image>();
        coins = 0;
        score = 0;
        hp = 3;
        current = POS.mid;
        isGrounded = true;
        isSliding = false;
        anim = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        leftButton.onClick.AddListener(MoveLeftWrapper);
        rightButton.onClick.AddListener(MoveRightWrapper);
        jumpButton.onClick.AddListener(JumpWrapper);
        slideButton.onClick.AddListener(SlideWrapper);
    }

    // Update is called once per frame
    void Update()
    {
        timevalue += Time.deltaTime;
        coinscore.text = "Score: "+score;
        footscore.text = "Coins: " +coins;
        time.text = "Time: " + timevalue.ToString("F");
        
    }

    private void FixedUpdate()
    {
        if (!ishurt)
        {
            transform.position += new Vector3(0, 0, 10f) * Time.deltaTime;
        }
        score = (int)(transform.position.z + 19f);
        
        /*if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                touchstart = touch.position;
            }
            if (touch.phase == TouchPhase.Ended) {
                touchend = touch.position;
                //Debug.Log("Touches-"+touchstart + "--" + touchend);
                if (Mathf.Abs(touchend.x-touchstart.x) > 400)
                {
                    if (touchend.x > touchstart.x)
                    {
                        anim.Play("Right");
                        if (current!=POS.right) { 
                            StartCoroutine(moveRight());
                            if (current == POS.left)
                            {
                                current = POS.mid;
                            }
                            else current = POS.right;
                            Debug.Log(current);
                        }
                    }
                    else if (touchend.x < touchstart.x) {
                        anim.Play("Left");
                        if (current != POS.left) { 
                            StartCoroutine(moveLeft());
                            if (current == POS.right)
                            {
                                current = POS.mid;
                            }
                            else current = POS.left;
                            Debug.Log(current);
                        }
                    }
                }

                else if (Mathf.Abs(touchend.y - touchstart.y) > 200)
                {
                    if (touchend.y > touchstart.y)
                    {
                        if (isGrounded) {

                            anim.Play("Jump"); 
                            StartCoroutine(jump()); 
                        }
                    }
                    else if (touchend.y < touchstart.y)
                    {
                        if (!isSliding) { 
                            StartCoroutine(slide());
                            anim.Play("Slide");
                        }
                    }
                }

            }
          
        }*/
        
    }

 

    private void SlideWrapper()
    {
        StartCoroutine(slide());
    }
    private void MoveLeftWrapper()
    {
        StartCoroutine(moveLeft());
    }
    private void MoveRightWrapper()
    {
        StartCoroutine(moveRight());
    }
    private void JumpWrapper()
    {
        if (isGrounded) { StartCoroutine(jump()); }
    }
    private IEnumerator slide()
    {
        if (!isSliding)
        {
            anim.Play("Slide");
            isSliding = true;
            collider.height /= 2;
            Vector3 movePosition = collider.center;
            movePosition.y /= 2;
            collider.center = movePosition;
            yield return new WaitForSeconds(1f);
            collider.height *= 2;
            movePosition.y *= 2;
            collider.center = movePosition;
            isSliding = false;
        }

    }

    private IEnumerator jump()
    {
            anim.Play("Jump");
            yield return new WaitForSeconds(0.2f);
            rgbd.AddForce(new Vector3(0, 70, 0), ForceMode.Impulse);
        
    }

    private IEnumerator moveLeft()
    {
        if (current != POS.left)
        {
            anim.Play("Left");
            Vector3 movePosition = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.15f);
            transform.position = Vector3.Lerp(transform.position, movePosition, 1f);
            if (current == POS.right) current = POS.mid;
            else current = POS.left;
        }
    }

    private IEnumerator moveRight()
    {
        if (current != POS.right)
        {
            anim.Play("Right");
            Vector3 movePosition = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.15f);
            transform.position = Vector3.Lerp(transform.position, movePosition, 1f);
            if (current == POS.left) current = POS.mid;
            else current = POS.right;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer==10)
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isGrounded = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) {
            coins++;
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.layer == 13) {
            hp--;
            collision.gameObject.layer = 0;
            rgbd.AddForce(0, 50f, -150f);
            StartCoroutine(isHurt());
            hpmeter[hp].gameObject.SetActive(false);
            if (hp == 0) {
                GameOverScreen.gameObject.SetActive(true);
                coinscore.gameObject.SetActive(false);
                footscore.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator isHurt() {
        ishurt = true;
        yield return new WaitForSeconds(1f);
        ishurt = false;
    }
}

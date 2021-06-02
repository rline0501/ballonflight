using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�L�[���͗p�̕�����w��iInputManager��Horizontal�̓��͂𔻒肷�邽�߂̕�����j
    private string horizontal = "Horizontal";

    //�L�[���͗p�̕�����w��
    private string jump = "Jump";

    //�R���|�[�l���g�擾�p
    private Rigidbody2D rb;
    private Animator anim;

    //�������̐����l
    private float limitPosX = 8.5f;

    //�c�����̐����l
    private float limitPosY = 4.45f;

    //���߂ăo���[���𐶐��������𔻒肷�邽�߂̕ϐ��i��قǊO���X�N���v�g�ł����p���邽��public�Ő錾����j
    public bool isFirstGenerateBallon;

    //�����̐ݒ�ɗ��p����
    private float scale;

    //�ړ����x
    public float moveSpeed;

    //�W�����v�E���V��
    public float jumpPower;

    public bool isGrounded;

    //GameObject�^�̔z��B�C���X�y�N�^�[����q�G�����L�[�ɂ���Ballon�Q�[���I�u�W�F�N�g�Q���A�T�C������
    public GameObject[] ballons;

    //�o���[���𐶐�����ő吔
    public int maxBallonCount;

    //�o���[���̐����ʒu�̔z��
    public Transform[] ballonTrans;

    //�o���[���̃v���t�@�u
    public GameObject BallonPrefab;

    //�o���[���𐶐����鎞��
    public float generateTime;

    //�o���[�����������ǂ������肷��
    public bool isGenerating;

    //�G�ƐڐG�����ۂɐ�����΂�����
    public float knockbackPower;

    [SerializeField, Header("Linecast�p�n�ʔ��背�C���[")]
    private LayerMask groundLayer;

    [SerializeField]
    private StartChecker startChecker;

    void Start()
    {
        //�K�v�ȃR���|�[�l���g���擾���ĕϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        scale = transform.localScale.x;

        //�z��̏������i�o���[���̍ő吶���������z��̗v�f����p�ӂ���j
        ballons = new GameObject[maxBallonCount];

    }

    void Update()
    {

        //�n�ʐڒn Physics2D.Linecast���\�b�h�����s����GroundLayer�ƃL�����̃R���C�_�[���ڐG���Ă��鋗�����ǂ������m�F��
        //�ݒu���Ă���Ȃ�true�A�ݒu���Ă��Ȃ��Ȃ�false��߂�
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        //Scene�r���[��Physics2D.Linecast���\�b�h��Line��\������
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        //Ballons�ϐ��̍ŏ��̗v�f����łȂ��Ȃ� = �o���[�����P���������Ƃ��̗v�f�ɒl���������� = �o���[�����P����Ȃ�
        //���@������ύX����@��
        if (ballons[0] != null)
        {



            //�W�����v
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }

            // �ڒn���Ă��Ȃ��i�󒆂ɂ���j�ԂŁA�������̏ꍇ
            if (isGrounded == false && rb.velocity.y < 0.15f)
            {
                //�����A�j�����J��Ԃ�
                anim.SetTrigger("Fall");
            }

            else
            if (isGrounded == true) 
            {
                anim.ResetTrigger("Fall");
            }

        }
        else
        {
            Debug.Log("�o���[�����Ȃ��B�W�����v�s��");
        }

        //Velocity�̒l��5.0f�𒴂���ꍇ�i�W�����v��A���ŉ������ꍇ�j
        if(rb.velocity.y > 5.0f)
        {
            //Velocity�̒l�ɐ�����������i���������ɏ��őҋ@�ł��錻�ۂ�h�����߁j
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }

        //�n�ʂɐڐG���Ă��āA�o���[���𐶐����Ă��Ȃ��ꍇ
        if(isGrounded == true && isGenerating == false)
        {
            //Q�{�^������������
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //�o���[�����P�쐬����
                StartCoroutine(GenerateBallon());
            }

        }


    }

    /// <summary>
    /// �W�����v�Ƌ󒆕��V
    /// </summary>
    private void Jump()
    {
        //�L�����̈ʒu��������ֈړ�������i�W�����v�E���V�j
        rb.AddForce(transform.up * jumpPower);

        //jump(Up + Mid)�A�j���[�V�������Đ�����
        anim.SetTrigger("Jump");
    }


    void FixedUpdate()
    {
        //�ړ�
        Move();
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        //�����i���j�����ւ̓��͎�t
        //InputManager��Horizontal�ɓo�^����Ă���L�[�̓��͂����邩�ǂ����̊m�F���s��
        float x = Input.GetAxis(horizontal);

        //x�̒l���O�łȂ��ꍇ�@���@�L�[���͂�����ꍇ
        if (x != 0)
        {
            //velocity�i���x�j�ɐV�����l�������Ĉړ�
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

            //temp�ϐ��Ɍ��݂�localScale�l����
            Vector3 temp = transform.localScale;

            //���݂̃L�[���͒lx��temp.x�ɑ��
            temp.x = x;

            //�������ς��Ƃ��ɏ����ɂȂ�ƃL�������k��Ō����Ă��܂��̂Ő����l�ɂ���
            if (temp.x > 0)
            {
                //�������O���傫����΂��ׂĂP�ɂ���
                temp.x = scale;
            }
            else
            {
                //�������O������������΂��ׂā[�P�ɂ���
                temp.x = -scale;

            }

            //�L�����̌������ړ������ɍ��킹��
            transform.localScale = temp;


            //�ҋ@��Ԃ̃A�j���̍Đ����~�߂āA����A�j���̍Đ��ւ̑J�ڂ��s��

            //��Idle�A�j���[�V������false�ɂ��āA�ҋ@�A�j���[�V�������~����
            anim.SetBool("Idle", false);

            //��Run�A�j���[�V�����ɑ΂��āA0.5f�̒l�����Ƃ��ēn��
            //�J�ڏ�����greater 0.1�Ȃ̂�0.1�ȏ�̒l��n���Ə�������������Run�A�j���[�V�������Đ������
            anim.SetFloat("Run", 0.5f);

        }
        else
        {
            //���E�̓��͂��Ȃ������牡�ړ��̑��x���O�ɂ��ăs�^�b�Ǝ~�܂�悤�ɂ���
            rb.velocity = new Vector2(0, rb.velocity.y);


            //����A�j���̍Đ����~�߂đҋ@��Ԃ̃A�j���̍Đ��ւ̑J�ڂ��s��

            //��Run�A�j���[�V�����ɑ΂��āA0.f�̒l�����Ƃ��ēn���B
            //�J�ڏ�����less 0.1�Ȃ̂ŁA0.1�ȉ��̒l��n���Ə�������������Run�A�j���[�V��������~�����
            anim.SetFloat("Run", 0.0f);

            //��Idle�A�j���[�V������true�ɂ��āA�ҋ@�A�j���[�V�������Đ�����
            anim.SetBool("Idle", true);
        }

        //���݂̈ʒu��񂪈ړ������̐����͈͂𒴂��Ă��Ȃ����m�F����B�����Ă�����͈͓��Ɏ��߂�
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        //���݂̈ʒu���X�V�i�����͈͂𒴂����ꍇ�A�����ňړ��͈̔͂𐧌�����j
        transform.position = new Vector2(posX, posY);

    }

  ///<summary>
  /// �o���[������
  ///</summary>
  ///<returns></returns>
  private IEnumerator GenerateBallon()
    {
        //���ׂĂ̔z��̗v�f�Ƀo���[�������݂��Ă���ꍇ�ɂ́A�o���[���𐶐����Ȃ�
        if(ballons[1] != null)
        {
            yield break;
        }

        //��������Ԃɂ���
        isGenerating = true;

        //isFirstGenerateBallon�ϐ��̒l��false�A�܂�Q�[�����J�n���Ă���܂��o���[�����P����������Ă��Ȃ��̂Ȃ�
        if(isFirstGenerateBallon  == false)
        {
            //����o���[���������s�����Ɣ��f���Atrue�ɕύX���� = ����ȍ~�̓o���[���𐶐����Ă��Aif���̏����𖞂����Ȃ��Ȃ�A���̏����ɂ͓���Ȃ�
            isFirstGenerateBallon = true;

            Debug.Log("����̃o���[������");

            //startChecker�ϐ��ɑ������Ă���StartChecker�X�N���v�g�ɃA�N�Z�X���āASetInitialSpeed���\�b�h�����s����
            startChecker.SetInitialSpeed();
        }

        //�P�ڂ̔z��̗v�f����Ȃ�
        if(ballons[0] == null){
            //�P�ڂ̃o���[�������𐶐����āA�P�Ԗڂ̔z��֑��
            ballons[0] = Instantiate(BallonPrefab, ballonTrans[0]);

            ballons[0].GetComponent<Ballon>().SetUpBallon(this);
        }
        else
        {
            //�Q�ڂ̃o���[�������𐶐����āA�Q�Ԗڂ̔z��֑��
            ballons[1] = Instantiate(BallonPrefab, ballonTrans[1]);

            ballons[1].GetComponent<Ballon>().SetUpBallon(this);
        }

        //�������ԕ��ҋ@
        yield return new WaitForSeconds(generateTime);

        //��������ԏI���B�ēx�����ł���悤�ɂ���
        isGenerating = false;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //�ڐG����R���C�_�[�����Q�[���I�u�W�F�N�g��Tag��Enemy�Ȃ�
        if(col.gameObject.tag == "Enemy")
        {
            //�L�����ƓG�̈ʒu���狗���ƕ������v�Z
            Vector3 direction = (transform.position - col.transform.position).normalized;

            //�G�̔��Α��ɃL�����𐁂���΂�
            transform.position += direction * knockbackPower;

        }
    }

    ///<summary>
    ///
    ///</summary>
    public void DestroyBallon()
    {
        //TODO ��قǁA�o���[�����j�󂳂��ۂɁu���ꂽ�v�悤�Ɍ�����A�j�����o��ǉ�����
        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if (ballons[0] != null)
        {
            Destroy(ballons[0]);
        }

    }


}

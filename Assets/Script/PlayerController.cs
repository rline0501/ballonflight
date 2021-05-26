using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�L�[���͗p�̕�����w��iInputManager��Horizontal�̓��͂𔻒肷�邽�߂̕�����j
    private string horizontal = "Horizontal";

    //�R���|�[�l���g�擾�p
    private Rigidbody2D rb;

    private Animator anim;

    //�����̐ݒ�ɗ��p����
    private float scale;

    //�ړ����x
    public float moveSpeed;
   
    void Start()
    {
        //�K�v�ȃR���|�[�l���g���擾���ĕϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        scale = transform.localScale.x;

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
            if(temp.x > 0)
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
            anim.SetBool("Idol", true);
        }
    }


}

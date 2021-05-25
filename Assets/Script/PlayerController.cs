using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�L�[���͗p�̕�����w��iInputManager��Horizontal�̓��͂𔻒肷�邽�߂̕�����j
    private string horizontal = "Horizontal";

    //�R���|�[�l���g�擾�p
    private Rigidbody2D rb;

    //�����̐ݒ�ɗ��p����
    private float scale;

    //�ړ����x
    public float moveSpeed;
   
    void Start()
    {
        //�K�v�ȃR���|�[�l���g���擾���ĕϐ��ɑ��
        rb = GetComponent<Rigidbody2D>();

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


        }
        else
        {
            //���E�̓��͂��Ȃ������牡�ړ��̑��x���O�ɂ��ăs�^�b�Ǝ~�܂�悤�ɂ���
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


}

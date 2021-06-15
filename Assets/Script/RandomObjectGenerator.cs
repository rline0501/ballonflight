using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objPrefab;

    [SerializeField]
    private Transform generateTran;

    [Header("�����܂ł̑ҋ@����")]
    //�P�񐶐�����܂ł̑ҋ@���ԁB�ǂ̈ʂ̊Ԋu�Ŏ����������s�����ݒ�
    public Vector2 waitTimeRange;

    private float waitTime;

    //�ҋ@���Ԃ̌v���p
    private float timer;

    //�����̏�Ԃ��m�F���A�������s�����ǂ����̔���ɗ��p����Btrue�Ȃ琶�����Afalse�Ȃ琶�����Ȃ�
    private bool isActivate;

    private GameDirector gameDirector;
   
    void Start()
    {
        SetGenerateTime();
    }

    /// <summary>
    /// �����܂ł̎��Ԃ�ݒ�
    /// </summary>
    private void SetGenerateTime()
    {
        //�����܂ł̑ҋ@���Ԃ��A�ŏ��l�ƍő�l�̊Ԃ��烉���_���Őݒ�
        waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
    }

    void Update()
    {
        //��~���͐������s��Ȃ�
        if(isActiveAndEnabled == false)
        {
            return;
        }

        //�v���p�^�C�}�[�����Z
        timer += Time.deltaTime;

        //�v���p�^�C�}�[���ҋ@���ԂƓ�������������
        if (timer >= waitTime)
        {
            //�^�C�}�[�����Z�b�g���āA�ēx�v�Z�ł����Ԃɂ���
            timer = 0;

            //�����_���ȃI�u�W�F�N�g�𐶐�
            RandomGenerateObject();
        }
    }

    ///<summary>
    ///�����_���ȃI�u�W�F�N�g�𐶐�
    ///</summary>
    private void RandomGenerateObject()
    {
        //��������v���t�@�u�̔ԍ��������_���ɐݒ�
        int randomIndex = Random.Range(0, objPrefab.Length);

        //�v���t�@�u�����ƂɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
        GameObject obj = Instantiate(objPrefab[randomIndex], generateTran);

        //�����_���Ȓl���擾
        float randomPosY = Random.Range(-4.0f, 4.0f);

        //�������ꂽ�Q�[���I�u�W�F�N�g��Y���Ƀ����_���Ȓl�����Z���āA��������邽�тɍ����̈ʒu��ύX����
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);

        //���̐����܂ł̎��Ԃ��Z�b�g����
        SetGenerateTime();

    }

    /// <summary>
    /// ������Ԃ̃I��/�I�t��؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }



}

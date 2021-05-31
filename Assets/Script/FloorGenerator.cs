using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    //�v���t�@�u�ɂ���AerialFloor_Mid�Q�[���I�u�W�F�N�g���C���X�y�N�^�[����A�T�C������
    private GameObject aerialFloorPrefab;

    [SerializeField]
    //�v���t�@�u�̃N���[���𐶐�����ʒu�̐ݒ�
    private Transform generateTran;

    [Header("�����܂ł̑ҋ@����")]
    //�P�񐶐�����܂ł̑ҋ@���ԁB
    public float waitTime;

    //�ҋ@���Ԃ̌v�Z�p
    private float timer;
   
    void Update()
    {
        //���Ԃ��v������
        timer += Time.deltaTime;

        //�v�����Ă��鎞�Ԃ�waitTime�̒l�Ɠ������A��������
        if(timer >= waitTime)
        {
            //����̌v���p�ɁAtimer���O�ɂ���
            timer = 0;

            //�N���[�������p�̃��\�b�h���Ăяo��
            GenerateFloor();


        }
        
    }

    ///<summary>
    ///�v���t�@�u�����ɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
    ///</summary>
    private void GenerateFloor()
    {
        //�󒆏��̃v���t�@�u�����ƂɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
        GameObject obj = Instantiate(aerialFloorPrefab, generateTran);

        //�����_���Ȓl���擾
        float randomPosY = Random.Range(-4.0f, 4.0f);

        //�������ꂽ�Q�[���I�u�W�F�N�g��Y���Ƀ����_���Ȓl�����Z���āA��������邽�тɍ����̈ʒu��ύX����
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);
    }
}
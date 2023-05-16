using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnityCamera : MonoBehaviour
{
    /************************************************************************
	 * ī�޶� (Camera)
	 * 
	 * ���ӿ��带 �÷��̾�� �����ִ� ��ġ
	 * �ϳ��� ���� ī�޶� ���ϴ� ��ŭ �߰��� �� ����
	 * ī�޶� �������ϴ� ������ ȭ���� ��ġ�� �����ϸ�, �Ϻθ� �������ϵ��� ��������
	 ************************************************************************/

    // <�ֿ� �Ӽ�>
    // Clear Flags : ȭ���� �׸����� ���� �κ��� ��� ����� ����
    // Background : ��ī�̹ڽ��� ���� ��� ���� ȭ�鿡 ������ ����
    // Culling Mask : ī�޶� �������� ������Ʈ�� ���̾� ����
    // Projection : ī�޶��� ���ٰ� ����
    //				Perspective : ī�޶��� ���ٰ��� ����
    //				Orthographic : ī�޶��� ���ٰ� ���� ����
    // Clipping Planes : �������� ���� �� �����ϱ� ���� ī�޶������ �Ÿ�
    // Viewport Rect : ī�޶� �䰡 ��ο�� ȭ���� ��ġ�� ��Ÿ���� �簢��


    // <�ó׸ӽ� ��Ű��>
    // ī�޶��� �Կ���ġ�� �����ϱ� ���� ����� ������ ��Ű��
    // �ó׸ӽ� �극�� : ī�޶� �����Ǿ� ������ ���� �켱������ ���� �ó׸ӽ�(�Կ����)���� �����
    // �ó׸ӽ� : �Կ�������� �̵����, �Կ����, ��Ÿ ȿ���� ���� ����
}
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject follow;
    [SerializeField]
    private GameObject lookAt;

    [SerializeField]
    private Vector3 offset;

    private void LateUpdate()
    {
        transform.position = follow.transform.position + offset;
        transform.LookAt(lookAt.transform);
    }
}
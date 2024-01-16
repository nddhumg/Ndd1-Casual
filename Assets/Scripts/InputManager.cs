using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        Click();
    }
    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            // Tạo ray từ vị trí chuột trên màn hình
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Điều chỉnh vị trí của điểm cuối của raycast theo trục Z
            float targetZ = 10f;
            Vector3 targetPosition = ray.origin + ray.direction * targetZ;

            // Vẽ đường raycast từ vị trí bắt đầu của ray đến vị trí mới
            Debug.DrawRay(ray.origin, targetPosition - ray.origin, Color.red, 0.5f);

            RaycastHit hit;

            // Kiểm tra va chạm với đối tượng
            if (Physics.Raycast(ray, out hit))
            {
                Block block = hit.transform.GetComponent<Block>();
                if (block == null)
                    return;
                block.Click();
            }
        }
    }
}

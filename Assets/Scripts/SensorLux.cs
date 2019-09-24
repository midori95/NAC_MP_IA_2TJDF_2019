using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArdJoystick;

public class SensorLux : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        ProcessData();
    }
    private IEnumerator ProcessData()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            try
            {
                //string result = serialPort.ReadLine();

                //Test
                string result = SimulateReadLine();
                Debug.Log(result);
                Debug.Log("Test");

                string[] resultData = result.Split(';');
                ArdKeyCode[] = (ArdKeyCode[])Enum.GetValues(typeof(ArdKeyCode));
                Debug.Log("Tamanho vetor: " + keyCodes.Lenght);

                for (int i = 0; i < keyCodes.Lenght; i++)
                {
                    ArdKeyCode keyCode = keyCodes[i];
                    ArdButton button = buttons[keyCode];
                    data = int.Parse(resultData[i]);

                    button.ProcessData(data);
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

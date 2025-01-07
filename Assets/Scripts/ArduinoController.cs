using UnityEngine;
using System.IO.Ports;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.Globalization;
public class ArduinoController : MonoBehaviour
{
    private SerialPort serial;
    [SerializeField] private string serialPort = "COM12";
    [SerializeField] private int baudrate = 115200;

    private Keyboard keyboard;
    private KeyboardState stateA;
    private KeyboardState stateB;

    [SerializeField] GameObject player;

    void Start()
    {
        keyboard = InputSystem.GetDevice<Keyboard>();
        stateA = new KeyboardState();
        stateA.Press(Key.Space);
        stateB = new KeyboardState();
        stateB.Release(Key.Space);

        serial = new SerialPort(serialPort, baudrate);
        serial.NewLine = "\n";
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (!serial.IsOpen) return;
        if (serial.BytesToRead <= 0) return;

        var message = serial.ReadLine().Trim();
        var messageParts = message.Split(' ');

        switch (messageParts[0])
        {
            case "JUMP":
                JumpInput();
                break;

            case "DIST":
                MovingPlatform movingPlatform = player.GetComponentInParent<MovingPlatform>();
                if (movingPlatform != null && float.TryParse(messageParts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float distance))
                {
                    movingPlatform.UpdateDistance(distance);
                }
                break;

            case "DEBUG":
                //Debug.Log(messageParts[1]);
                break;

            default:
                Debug.Log($"Unknown message: {message}");
                break;
        }
    }

    public void CoinLED()
    {
        if (!serial.IsOpen) return;
        serial.WriteLine("COIN");
    }

    public void UpdateHealth(float health)
    {
        if (!serial.IsOpen) return;
        Debug.Log("led " + health);
        serial.WriteLine(health.ToString());
    }

    private void JumpInput()
    {
        InputSystem.QueueStateEvent(keyboard, stateA);
        InputSystem.QueueStateEvent(keyboard, stateB);
    }

    private void OnDestroy()
    {
        if (!serial.IsOpen) return;
        serial.Close();
    }
}

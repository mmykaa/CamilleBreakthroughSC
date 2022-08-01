using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    private bool showConsole;
    private bool showHelp;

    private GUIStyle GUIInfoStyle = new GUIStyle();
    private string fpsLabel = "";
    private float fpsCount;

    private string CPUInfo = "";
    private string GPUInfo = "";
    private string OSInfo = "";
    private string APIInfo = "";

    public List<object> commandList;
    private string input;
    public static DebugCommand HELP;
    public static DebugCommand RESPAWN;
    public static DebugCommand MY_PRECIOUS;
    public static DebugCommand TO_INFINITY_AND_BEYOUND;
    public static DebugCommand PLEASE_MAKE_IT_STOP;
    public static DebugCommand TURN_THE_LIGHTS_OFF;
    public static DebugCommand NOW_YOU_SEE_ME_NOW_YOU_DONT;

    [SerializeField] private GameObject darkGlobalLight;
    [SerializeField] private GameObject levelLights;
    [SerializeField] private GameObject securityCameras;


    private void Awake()
    {
        CPUInfo = "CPU: " + SystemInfo.processorType;
        GPUInfo = "GPU: " + SystemInfo.graphicsDeviceName + " / " + SystemInfo.graphicsMemorySize + " MB";
        OSInfo = "OS: " + SystemInfo.operatingSystem;
        APIInfo = "API: " + SystemInfo.graphicsDeviceVersion;


        //assign commands and add them to the command list
        HELP = new DebugCommand("help", "help, is here to help!", "help", () =>
        {
            showHelp = true;
        });

        RESPAWN = new DebugCommand("respawn", "Respawn Camille in the last checkpoint", "respawn", () =>
        {
            GameManager.Instance.OnDeathReposition();
        });

        MY_PRECIOUS = new DebugCommand("my precious", "Gets all the collectibles from the level", "my precious", () =>
        {
            GameManager.Instance.GetAllCollectibles();
            GameManager.Instance.CheckAmountOfCollectibles();
        });

        TO_INFINITY_AND_BEYOUND = new DebugCommand("to infinity and beyound", "Camille runs faster than light", "to infinity and beyound", () =>
        {
            PlayerController.Instance.EnableSpeedCheat();
        });

        PLEASE_MAKE_IT_STOP = new DebugCommand("please make it stop", "Camille no longer runs faster than light", "please make it stop", () =>
        {
            PlayerController.Instance.DisableSpeedCheat();
        });

        TURN_THE_LIGHTS_OFF = new DebugCommand("turn the lights off", "Someone forgot to pay the eletricity bill", "turn the lights off", () =>
        {
            levelLights.SetActive(false);
            darkGlobalLight.SetActive(true);
        });

        NOW_YOU_SEE_ME_NOW_YOU_DONT = new DebugCommand("now you see me now you dont", "All the cameras turn off", "now you see me now you dont", () =>
        {
            securityCameras.SetActive(false);
        });


        commandList = new List<object>
        {
            HELP,
            RESPAWN,
            MY_PRECIOUS,
            TO_INFINITY_AND_BEYOUND,
            PLEASE_MAKE_IT_STOP,
            TURN_THE_LIGHTS_OFF,
            NOW_YOU_SEE_ME_NOW_YOU_DONT
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            showConsole = !showConsole;
        }
    }

    IEnumerator Start()
    {
        GUI.depth = 2;

        while (true)
        {
            if (Time.timeScale == 1)
            {
                yield return new WaitForSeconds(0.1f);
                fpsCount = (1 / Time.deltaTime);
                fpsLabel = "FPS: " + (Mathf.Round(fpsCount));
            }
            else
            {
                fpsLabel = "Paused";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnGUI() //OnGUI is called for rendering and handling GUI events
    {
        if (!showConsole)
        {
            Cursor.visible = false;
            return;
        }

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 150), "");
            y += 150;

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 150, 20);
                GUI.Label(labelRect, label);
            }
        }

        if (Event.current.keyCode == KeyCode.Return) //Check if enter is pressed inside a GUI textfield
        {
            HandleInput();
            input = "";
        }

        Cursor.visible = true;
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        GUI.color = Color.white;
        GUI.Label(new Rect(5, y + 40, 1000, 25), fpsLabel); //fps info
        GUI.Label(new Rect(5, y + 70, 1000, 25), CPUInfo); //cpu info
        GUI.Label(new Rect(5, y + 100, 1000, 25), GPUInfo); //gpu info
        GUI.Label(new Rect(5, y + 130, 1000, 25), OSInfo); //os info
        GUI.Label(new Rect(5, y + 160, 1000, 25), APIInfo); //api info
    }

    private void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID))  //check if the input string contains the command id
            {
                if (commandList[i] as DebugCommand != null) //testing to see if the type of the object fits the cast here
                {
                    (commandList[i] as DebugCommand).Invoke(); //and if it does we cast the object back to its original form and then invoke it
                }
            }
        }
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using System.Collections;

// public class Lesson
// {
//     public string title;
//     public string summary;
//     public Question[] questions;
// }

public class Initializer : MonoBehaviour
{
    private Camera cam = null;
    private GameObject mainBG = null;
    private GameObject canvas = null;
    private TextMeshPro nameText = null;
    private TextMeshPro levelText = null;
    private TextMeshPro xpText = null;
    private GameObject xpBar = null;
    private GameObject bsBtn = null;
    private GameObject advBtn = null;
    private GameObject backToMainBtn = null;
    private GameObject bsbg = null;
    private GameObject addBookBtn = null;
    private BookshelfHandler bookHandler = null;
    private GameObject camBG = null;
    private GameObject camBtn = null;
    private GameObject backToBookBtn = null;
    private GameObject shadowLayer = null;
    private GameObject confirmNameBtn = null;
    private JSONNode node = null;

    private Coroutine routine = null;

    // Components for camera___________________________________________________________
    private GameObject rawImage = null;
    private AspectRatioFitter aspectFitter = null;
    private WebCamTexture webcamTexture = null;
    private Texture2D capturedImage = null;

    // 🔹 This will store the file path of the latest picture taken
    public string lastSnapshotPath;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Creates Front-End Environment for the Main Screen
        InitializeMainScreen();
        InitializeBookshelfScreen();
        // InitializeCameraScreen();

        StartCoroutine(GetTomadachi());
    }

    private void InitializeMainScreen()
    {
        // Main Menu Background_____________________________________________________________________________________________
        mainBG = new GameObject("Main BG");

            // Sets BG position, assigned it a sprite, and changed the sorting order (Z-Axis) to be the lowest.
            mainBG.transform.position = new Vector2(0, 0);
            mainBG.AddComponent<SpriteRenderer>();
            mainBG.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/BG");
            mainBG.GetComponent<SpriteRenderer>().sortingOrder = -1;

        // Icon Background_______________________________________________________________
        GameObject IconBG = new GameObject("Icon BG");

            // Sets IconBG as child of BG and sets local position while assigning it a sprite.
            IconBG.transform.parent = mainBG.transform;
            IconBG.transform.localPosition = new Vector2(0, 3.5f);
            IconBG.AddComponent<SpriteRenderer>();
            IconBG.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/CharacterBG");
        
            // Icon Background Child (Spritemask)________________________________________
            GameObject IconBGMask = new GameObject("Mask");

                // Makes IconBGMask the child of IconBG, sets its local position, and assigns it a spritemask.
                IconBGMask.transform.parent = IconBG.transform;
                IconBGMask.transform.localPosition = new Vector2(0, 0);
                IconBGMask.AddComponent<SpriteMask>();

                // Creates a temporary variable for the spritemask component to avoid repetetive fetches.
                SpriteMask sprt = IconBGMask.GetComponent<SpriteMask>();

                    // Sets spritemask shape and range (makes sure sprites do not poke out of IconBG)
                    sprt.sprite = Resources.Load<Sprite>("art/test_assets/CharacterBG");
                    sprt.isCustomRangeActive = true;
                    sprt.frontSortingOrder = 3;
                    sprt.backSortingOrder = 2;

                // Monster name Object_______________________________________________________
                GameObject monsterName = new GameObject("Monster Name");

                    // Sets monsterName as child of IconBG, sets local position while assigning it text.
                    monsterName.transform.parent = IconBG.transform;
                    monsterName.transform.localPosition = new Vector2(0, -1.5f);
                    monsterName.AddComponent<TextMeshPro>();

                    // Assigns textbox size.
                    monsterName.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 0.7f);

                    // Assigned to gloval variable to pass in other scripts.
                    nameText = monsterName.GetComponent<TextMeshPro>();            

                        // Sets all necessary values for text.
                        nameText.text = "";
                        nameText.color = Color.white;
                        nameText.fontSize = 5;
                        nameText.alignment = TextAlignmentOptions.Center;
                        nameText.verticalAlignment = VerticalAlignmentOptions.Middle;
                        nameText.fontWeight = TMPro.FontWeight.Bold;

            // Monster Sprite____________________________________________________________
            GameObject monsterSprite = new GameObject("Monster");

                // Makes monsterSprite as the child of IconBG, sets its local position, and assigns it a sprite renderer/Animator.
                monsterSprite.transform.parent = IconBG.transform;
                monsterSprite.transform.localPosition = new Vector2(0, 0);
                monsterSprite.AddComponent<SpriteRenderer>();
                monsterSprite.AddComponent<Animator>(); 

                // Set Sorting Order (Z-Axis), its interaction with masks, and assigned an animation controller.
                monsterSprite.GetComponent<SpriteRenderer>().sortingOrder = 3;
                monsterSprite.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                monsterSprite.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("art/Animation Controllers/Monsters/Pup/Pup"); 

            // Monster Level Object______________________________________________________
            GameObject monsterLevel = new GameObject("Monster Level");

                // Sets monsterLevel as child of BG, sets transform while assigning it a sprite renderer.
                monsterLevel.transform.parent = mainBG.transform;
                monsterLevel.transform.localPosition = new Vector2(0, 0.5f);
                monsterLevel.transform.localScale = new Vector2(0.6f, 1);
                monsterLevel.AddComponent<SpriteRenderer>();

                // Assigned a meter sprite.
                monsterLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/Meter");

                    // Level Text Label__________________________________________________
                    GameObject levelLabel = new GameObject("Label");

                        // Makes levelLabel the child of monsterLevel, sets its local position, and assigns it text.
                        levelLabel.transform.parent = monsterLevel.transform;
                        levelLabel.transform.localPosition = new Vector2(0, 0.5686f);
                        levelLabel.AddComponent<TextMeshPro>();

                        // Assigns the textbox size.
                        levelLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(3.2f, 0.5f);
                        levelLabel.GetComponent<RectTransform>().localScale = new Vector2(1.6f, 1);

                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        TextMeshPro tmp = levelLabel.GetComponent<TextMeshPro>();            

                            // Sets all necessary values for text.
                            tmp.text = "Current Level";
                            tmp.color = Color.white;
                            tmp.fontSize = 3.5f;
                            tmp.alignment = TextAlignmentOptions.Center;
                            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;

                    // Level Text________________________________________________________
                    GameObject level = new GameObject("Level");

                        // Makes level the child of monsterLevel, sets its local position, and assigns it text.
                        level.transform.parent = monsterLevel.transform;
                        level.transform.localPosition = new Vector2(0, 0);
                        level.AddComponent<TextMeshPro>();

                        // Assigns the textbox size.
                        level.GetComponent<RectTransform>().sizeDelta = new Vector2(3.3623f, 0.586f);
                        level.GetComponent<RectTransform>().localScale = new Vector2(1.6f, 1);

                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        levelText = level.GetComponent<TextMeshPro>();            

                            // Sets all necessary values for text.
                            levelText.text = "";
                            levelText.color = Color.white;
                            levelText.fontSize = 4;
                            levelText.alignment = TextAlignmentOptions.Center;
                            levelText.verticalAlignment = VerticalAlignmentOptions.Middle;
       
            // Monster Experience Points Meter Object_______________________________________________________________________
            GameObject xpMeter = new GameObject("XP Meter");

                // Sets xpMeter as child of Bg and sets its local positon while assigning it a sprite.
                xpMeter.transform.parent = mainBG.transform;
                xpMeter.transform.localPosition = new Vector2(0, -0.866f);
                xpMeter.AddComponent<SpriteRenderer>();

                xpMeter.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/Meter");
                xpMeter.GetComponent<Transform>().localScale = new Vector2(1, 1);

                // Experience Points Text_____________________________________________________
                GameObject xp = new GameObject("XP");

                    // Makes xpMeterText the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xp.transform.parent = xpMeter.transform;
                    xp.transform.localPosition = new Vector2(0, 0);
                    xp.AddComponent<TextMeshPro>();

                    xp.GetComponent<RectTransform>().sizeDelta = new Vector2(3.362f, 0.587f);
                    xp.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    xp.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the TMP component to avoid repetetive fetches.
                    xpText = xp.GetComponent<TextMeshPro>();            

                        // Sets all necessary values for text.
                        xpText.text = $"{ 0 } / 10";
                        xpText.color = Color.gray;
                        xpText.fontSize = 3.5f;
                        xpText.alignment = TextAlignmentOptions.Center;
                        xpText.verticalAlignment = VerticalAlignmentOptions.Middle;
                        xpText.sortingOrder = 6;

                // Experience Label______________________________________________________
                GameObject xpLabel = new GameObject("Label");

                    // Makes xpMeterText the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xpLabel.transform.parent = xpMeter.transform;
                    xpLabel.transform.localPosition = new Vector2(0, 0);
                    xpLabel.AddComponent<TextMeshPro>();

                    xpLabel.GetComponent<RectTransform>().sizeDelta = new Vector2(3.362f, 0.5f);
                    xpLabel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0.57f);
                    xpLabel.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the TMP component to avoid repetetive fetches.
                    tmp = xpLabel.GetComponent<TextMeshPro>();            

                        // Sets all necessary values for text.
                        tmp.text = "Experience Points";
                        tmp.color = Color.white;
                        tmp.fontSize = 3.5f;
                        tmp.alignment = TextAlignmentOptions.Center;
                        tmp.verticalAlignment = VerticalAlignmentOptions.Middle;

                // Exp Meter (Sprite Mask)_______________________________________________
                GameObject xpMeterMask = new GameObject("Mask");

                    // Makes xpMeterMask the child of xpMeter, sets its local position, and assigns it a spritemask.
                    xpMeterMask.transform.parent = xpMeter.transform;
                    xpMeterMask.transform.localPosition = new Vector2(0, 0);
                    xpMeterMask.AddComponent<SpriteMask>();

                    xpMeterMask.GetComponent<Transform>().localPosition = new Vector2(0, 0);
                    xpMeterMask.GetComponent<Transform>().localScale = new Vector2(1, 1);

                    // Creates a temporary variable for the spritemask component to avoid repetetive fetches.
                    sprt = xpMeterMask.GetComponent<SpriteMask>();

                        // Sets spritemask shape and range (makes sure sprites do not poke out of IconBG)
                        sprt.sprite = Resources.Load<Sprite>("art/test_assets/MeterMask");
                        sprt.isCustomRangeActive = true;
                        sprt.frontSortingOrder = 5;
                        sprt.backSortingOrder = 4;

                // Experience Points Meter Bar___________________________________________
                xpBar = new GameObject("XP Bar");

                    // Makes IconBGMask the child of IconBG, sets its local position, and assigns it a spritemask.
                    xpBar.transform.parent = xpMeter.transform;
                    xpBar.transform.localPosition = new Vector2(-3.364f, 0);
                    xpBar.AddComponent<SpriteRenderer>();

                    // Sets all necessary values for sprite.
                    xpBar.GetComponent<Transform>().localScale = new Vector2(1, 1);
                    xpBar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("art/test_assets/XP Bar");
                    xpBar.GetComponent<SpriteRenderer>().sortingOrder = 5;
                    xpBar.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        // GUI Canvas_______________________________________________________________________________________________________
        canvas = new GameObject("Canvas");

            // Adds components for GUI canvas necessities.
            canvas.AddComponent<RectTransform>();
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            // Sets canvas to screen size and assigns the appropriate camera.
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvas.GetComponent<Canvas>().worldCamera = cam;

            // Bookshelf Button_____________________________________________________________________________________________
            bsBtn = new GameObject("Bookshelf Button");

                // Sets bsBtn as child of canvas and adds components for GUI button necessities.
                bsBtn.transform.parent = canvas.transform;
                bsBtn.AddComponent<CanvasRenderer>();
                bsBtn.AddComponent<Image>();
                bsBtn.AddComponent<Button>();

                // Targets the MoveToBookshelf function when button bsBtn is pressed.
                bsBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("M->B");});

                // Sets bsBtn sprite and transform.
                bsBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Button");
                bsBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, -230);
                bsBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 100);
                bsBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

            // Bookshelf Button Text_____________________________________________________
            GameObject bsText = new GameObject("Text");
    
                // Sets bsText as child of bsBtn and adds components for GUI text necessities.
                bsText.transform.parent = bsBtn.transform;
                bsText.AddComponent<CanvasRenderer>();
                bsText.AddComponent<TextMeshProUGUI>(); 

                // Sets appropriate size and position.
                bsText.GetComponent<RectTransform>().sizeDelta = new Vector2(340, 86.2f);
                bsText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                bsText.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                
                // Creates a temporary variable for the text component to avoid repetetive fetches.
                TextMeshProUGUI tmpgui = bsText.GetComponent<TextMeshProUGUI>();            

                    // Sets all necessary values for text.
                    tmpgui.text = "Bookshelf";
                    tmpgui.color = Color.white;
                    tmpgui.fontSize = 36;
                    tmpgui.alignment = TextAlignmentOptions.Center;
                    tmpgui.verticalAlignment = VerticalAlignmentOptions.Middle;

            // Adventure Button_____________________________________________________________________________________________
            advBtn = new GameObject("Adventure Button");

                // Sets advBtn as child of canvas and adds components for GUI button necessities.
                advBtn.transform.parent = canvas.transform;
                advBtn.AddComponent<CanvasRenderer>();
                advBtn.AddComponent<Image>();
                advBtn.AddComponent<Button>();

                // THIS DISABLES THE BUTTON COMPONENT TO AVOID MAKING IT FUNCTION. WILL ENABLE WHEN BUILDING LARGER SCALE.
                advBtn.GetComponent<Button>().enabled = false;
                
                // Targets the MoveToAdventure function when button advBtn is pressed.
                bsBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("M->A");});

                // Sets advBtn sprite and transform.
                advBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/DisabledBtn");

                // Sets appropriate size and position.
                advBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, -345);
                advBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 100);
                advBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

                    // Adventure Button Text_____________________________________________
                    GameObject advText = new GameObject("Text");
            
                        // Sets bsText as child of advBtn and adds components for GUI text necessities.
                        advText.transform.parent = advBtn.transform;
                        advText.AddComponent<CanvasRenderer>();
                        advText.AddComponent<TextMeshProUGUI>(); 

                        // Sets appropriate size and position.
                        advText.GetComponent<RectTransform>().sizeDelta = new Vector2(340, 86.2f);
                        advText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                        advText.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                        
                        // Creates a temporary variable for the text component to avoid repetetive fetches.
                        tmpgui = advText.GetComponent<TextMeshProUGUI>();            

                            // Sets all necessary values for text.
                            tmpgui.text = "Adventure";
                            tmpgui.color = Color.black;
                            tmpgui.fontSize = 36;
                            tmpgui.alignment = TextAlignmentOptions.Center;
                            tmpgui.verticalAlignment = VerticalAlignmentOptions.Middle;
    }

    private void InitializeBookshelfScreen()
    {
        // Bookshelf Menu Background_____________________________________________________________________________________________
        GameObject bkshlf = new GameObject("Bookshelf Handler");

            bkshlf.transform.position = new Vector2(16, 0);

            // Adds Bookshelf script for bookshelf menu GUI.
            bkshlf.AddComponent<BookshelfHandler>();    
            bookHandler = bkshlf.GetComponent<BookshelfHandler>();


        // bsbg to Main Menu Button_________________________________________________________________________________________
        bsbg = new GameObject("Bookshelf BG");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            bsbg.transform.parent = canvas.transform;
            bsbg.AddComponent<CanvasRenderer>();
            bsbg.AddComponent<Image>();
            bsbg.AddComponent<RectMask2D>();

            bsbg.GetComponent<RectMask2D>().padding = new Vector4(12, 90.5f, 12, 147.3f);

            // Sets bsBtn sprite and transform.
            bsbg.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/Bookshelf BG");
            bsbg.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            bsbg.GetComponent<RectTransform>().sizeDelta = new Vector2(448, 998);
            bsbg.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        // Back to Main Menu Button_________________________________________________________________________________________
        addBookBtn = new GameObject("Add Book Button");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            addBookBtn.transform.parent = canvas.transform;
            addBookBtn.AddComponent<CanvasRenderer>();
            addBookBtn.AddComponent<Image>();
            addBookBtn.AddComponent<Button>();


            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            addBookBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("B->C"); });

            // Sets bsBtn sprite and transform.
            addBookBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/AddButtton");
            addBookBtn.GetComponent<RectTransform>().localPosition = new Vector2(132, 420);
            addBookBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            addBookBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        // Back to Main Menu Button_________________________________________________________________________________________
        backToMainBtn = new GameObject("Back to Main Button");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            backToMainBtn.transform.parent = canvas.transform;
            backToMainBtn.AddComponent<CanvasRenderer>();
            backToMainBtn.AddComponent<Image>();
            backToMainBtn.AddComponent<Button>();

            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            backToMainBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("B->M");});

            // Sets bsBtn sprite and transform.
            backToMainBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/BackButtton");
            backToMainBtn.GetComponent<RectTransform>().localPosition = new Vector2(-132, 420);
            backToMainBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            backToMainBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);


            backToMainBtn.SetActive(false);
            addBookBtn.SetActive(false);
            bsbg.SetActive(false);
    }
    
    private void InitializeCameraScreen()
    {
        // private RawImage rawImage = null;
        // private AspectRatioFitter aspectFitter = null;
        // private WebCamTexture webcamTexture = null;
        // private Texture2D capturedImage = null;     

        // bsbg to Main Menu Button_________________________________________________________________________________________
        camBG = new GameObject("Camera BG");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            camBG.transform.parent = canvas.transform;
            camBG.AddComponent<CanvasRenderer>();
            camBG.AddComponent<Image>();

            // Sets bsBtn sprite and transform.
            camBG.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/camera bg");
            camBG.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            camBG.GetComponent<RectTransform>().sizeDelta = new Vector2(448, 998);
            camBG.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            camBG.SetActive(false);

        // bsbg to Main Menu Button_________________________________________________________________________________________
        GameObject camHolder = new GameObject("Camera Holder");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            camHolder.transform.parent = camBG.transform;
            camHolder.AddComponent<CanvasRenderer>();
            camHolder.AddComponent<Image>();

            // Sets bsBtn sprite and transform.
            camHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/camArea");
            camHolder.GetComponent<RectTransform>().localPosition = new Vector2(0, 35);
            camHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(4.48f, 5.68f);
            camHolder.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        // Back to Main Menu Button_________________________________________________________________________________________
        camBtn = new GameObject("CamBtn");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            camBtn.transform.parent = camBG.transform;
            camBtn.AddComponent<CanvasRenderer>();
            camBtn.AddComponent<Image>();
            camBtn.AddComponent<Button>();


            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            camBtn.GetComponent<Button>().onClick.AddListener(delegate{ TakeSnapshot(); });

            // Sets bsBtn sprite and transform.
            camBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/camBtn");
            camBtn.GetComponent<RectTransform>().localPosition = new Vector2(0, -373);
            camBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
            camBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);      

        // Back to Main Menu Button_________________________________________________________________________________________
        backToBookBtn = new GameObject("Back to Book Button");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            backToBookBtn.transform.parent = camBG.transform;
            backToBookBtn.AddComponent<CanvasRenderer>();
            backToBookBtn.AddComponent<Image>();
            backToBookBtn.AddComponent<Button>();

            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            backToBookBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("C->B");});

            // Sets bsBtn sprite and transform.
            backToBookBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/CameraBackBtn");
            backToBookBtn.GetComponent<RectTransform>().localPosition = new Vector2(-132, 407);
            backToBookBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            backToBookBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        rawImage = new GameObject("CameraPreview", typeof(RawImage), typeof(AspectRatioFitter));
        rawImage.transform.parent = camHolder.transform;
        rawImage.AddComponent<RawImage>();
        rawImage.AddComponent<AspectRatioFitter>();
        rawImage.AddComponent<RectTransform>();

        aspectFitter = rawImage.GetComponent<AspectRatioFitter>();

        aspectFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;

        WebCamDevice[] devices = WebCamTexture.devices;
        
        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name);
            rawImage.gameObject.GetComponent<RawImage>().texture = webcamTexture;
            webcamTexture.Play();
        }

        // bsbg to Main Menu Button_________________________________________________________________________________________
        shadowLayer = new GameObject("Shadow Layer");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            shadowLayer.transform.parent = canvas.transform;
            shadowLayer.AddComponent<CanvasRenderer>();
            shadowLayer.AddComponent<Image>();

            // Sets bsBtn sprite and transform.
            shadowLayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/DarkenBG");
            shadowLayer.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            shadowLayer.GetComponent<RectTransform>().sizeDelta = new Vector2(448, 998);
            shadowLayer.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            shadowLayer.SetActive(false);  

        // Back to Main Menu Button_________________________________________________________________________________________
        backToBookBtn = new GameObject("Cancel Button");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            backToBookBtn.transform.parent = shadowLayer.transform;
            backToBookBtn.AddComponent<CanvasRenderer>();
            backToBookBtn.AddComponent<Image>();
            backToBookBtn.AddComponent<Button>();

            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            backToBookBtn.GetComponent<Button>().onClick.AddListener(delegate{ MoveScreens("C->B");});

            // Sets bsBtn sprite and transform.
            backToBookBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/CameraBackBtn");
            backToBookBtn.GetComponent<RectTransform>().localPosition = new Vector2(-132, 407);
            backToBookBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            backToBookBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        // Back to Main Menu Button_________________________________________________________________________________________
        confirmNameBtn = new GameObject("Confirm Name Button");

            // Sets bsBtn as child of canvas and adds components for GUI button necessities.
            confirmNameBtn.transform.parent = shadowLayer.transform;
            confirmNameBtn.AddComponent<CanvasRenderer>();
            confirmNameBtn.AddComponent<Image>();
            confirmNameBtn.AddComponent<Button>();

            // Targets the MoveToBookshelf function when button bsBtn is pressed.
            confirmNameBtn.GetComponent<Button>().onClick.AddListener(delegate{ CloseNameWindow(); });

            // Sets bsBtn sprite and transform.
            confirmNameBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("art/test_assets/CameraBackBtn");
            confirmNameBtn.GetComponent<RectTransform>().localPosition = new Vector2(132, 407);
            confirmNameBtn.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            confirmNameBtn.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
    }

    private void CloseNameWindow()
    {
        shadowLayer.SetActive(false);
    }


    // Switches to Bookshelf screen.
    private void MoveScreens(string from)
    {
        switch(from)
        {
            // Main Menu -> Bookshelf Menu
            case "M->B":
                // Disables Main Menu Buttons.
                bsBtn.SetActive(false);
                advBtn.SetActive(false);
                bsbg.SetActive(true);

                // Enables Bookshelf Menu Buttons.
                addBookBtn.SetActive(true);
                backToMainBtn.SetActive(true);

                if(bookHandler.rootBook != null)
                {
                    bookHandler.rootBook.bookObject.SetActive(true);
                }

                // Moves camera to new menu location.
                cam.transform.position = new Vector3(16, 0, -10);
                break;

            // Main Menu -> Adventure Menu
            case "M->A":    

                // Disables Main Menu Buttons.
                bsBtn.SetActive(false);
                advBtn.SetActive(false);

                break;

            // Bookshelf Menu -> Main Menu
            case "B->M":

                // Enables Main Menu Buttons.
                bsBtn.SetActive(true);
                advBtn.SetActive(true);
                
                if(bookHandler.rootBook != null)
                {
                    bookHandler.rootBook.BulkPopupDisable();
                    bookHandler.rootBook.bookObject.SetActive(false);        
                }

                bsbg.SetActive(false);
                addBookBtn.SetActive(false);
                backToMainBtn.SetActive(false);

                // Moves camera to new menu location.
                cam.transform.position = new Vector3(mainBG.transform.position.x, mainBG.transform.position.y, -10);
                break;

            case "B->C":

                // Disables Main Menu Buttons.

                
                if(bookHandler.rootBook != null)
                {
                    bookHandler.rootBook.BulkPopupDisable();
                    bookHandler.rootBook.bookObject.SetActive(false);        
                }

                bsbg.SetActive(false);
                addBookBtn.SetActive(false);
                backToMainBtn.SetActive(false);

                if(rawImage == null)
                {
                    InitializeCameraScreen();
                }

                camBG.SetActive(true);
                shadowLayer.SetActive(true);

                cam.transform.position = new Vector3(25, 0, -10);
                break;

            case "C->B":
            camBG.SetActive(false);

            // Enables Bookshelf Menu Buttons.
            addBookBtn.SetActive(true);
            backToMainBtn.SetActive(true);
            bsbg.SetActive(true);

            if(bookHandler.rootBook != null)
            {
                bookHandler.rootBook.bookObject.SetActive(true);
            }       

            cam.transform.position = new Vector3(16, 0, -10);
            break;

            // Adventure Menu -> Main Menu
            case "A->M":

                // Disables Main Menu Buttons.
                bsBtn.SetActive(true);
                advBtn.SetActive(true);

                // Moves camera to new menu location.
                cam.transform.position = new Vector3(mainBG.transform.position.x, mainBG.transform.position.y, -10);
                break;
        }

    }

    public void TakeSnapshot()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
            snap.SetPixels(webcamTexture.GetPixels());
            snap.Apply();

            capturedImage = snap;

            string fileName = "tmpPic.png";

            string path = Path.Combine(Application.persistentDataPath, fileName);

            File.WriteAllBytes(path, capturedImage.EncodeToPNG());
            
            if(File.Exists(path))
            {
                Debug.Log("File found: " + path);
            }
        }
    }

    IEnumerator GetTomadachi()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://studyden-api-737125230711.us-east1.run.app/api/tomadachi/1");
        request.SetRequestHeader("accept", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("Done");

        if (request.result == UnityWebRequest.Result.Success)
        {

            string json = request.downloadHandler.text;
            Debug.Log("Received JSON: " + json);

            node = JSON.Parse(json);
        }

        yield return new WaitForSecondsRealtime(10);

        routine = null;
    }

    void Update()
    {
        if (webcamTexture != null && webcamTexture.width > 100)
        {
            float ratio = (float)webcamTexture.width / (float)webcamTexture.height;
            aspectFitter.aspectRatio = ratio;
        }

        if(node != null)
        {
            nameText.text = node[0]["name"];
            levelText.text = node[0]["level"].AsInt.ToString();
            xpText.text = $"{node[0]["experience"].AsInt} / 10";
        }

        if(routine == null)
        {
            routine = StartCoroutine(GetTomadachi());
        }
    }

    // Getters.
    
    public Camera Cam { get { return cam; } }
    public GameObject MainBG { get { return mainBG; } }
    public GameObject Canvas { get { return canvas; } }
    public TextMeshPro NameText { get { return nameText; } }
    public TextMeshPro LevelText { get { return LevelText; } }
    public TextMeshPro XpText { get { return XpText; } }
    public TextMeshPro XpBar { get { return XpBar; } }
    public GameObject BsBtn { get { return BsBtn; } }
    public GameObject AdvBtn { get { return AdvBtn; } }
    public GameObject BackToMainBtn { get { return backToMainBtn; } }
    public GameObject BSBG { get { return bsbg; } }
    public GameObject AddBookBtn { get { return addBookBtn; } }
}

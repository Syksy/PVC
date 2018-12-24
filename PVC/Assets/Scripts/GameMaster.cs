using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameMaster handles game logic on a higher level; spawns enemies, ends and starts games, etc.

public class GameMaster : MonoBehaviour {
    public bool gameOn;
    // Spawnable GameObject
    public GameObject Creep;
    public GameObject Cannon; // Princess is for the time being attached to the cannon; maybe in future it's a separate sprite that can be changed
    // Objects for the menus
    public GameObject MenuStart;
    public GameObject MenuRestart;
    public GameObject MenuHelp;
    public GameObject MenuYes;
    public GameObject MenuNo;
    // Game menu
    public GameObject Counter;
    public GameObject GameStatusText;
    // Input interaction from raycast hits
    private RaycastHit hit;
    // Handle cannon behavior
    private Cannon cannon;
    // Game status; naive state machine
    public int gameStatus = 0;
    /*
     * 0 = Game just started, in starting menu
     * 1 = Game is on
     * 2 = Options menu
     * 3 = Game wipe / restart
     * 4 = Game end
     */ 
    // Timer until next Creep spawn
    public float nextSpawn = 4.0f;
    public float timeSpawn = 10.0f;
    // Spawn distance from origin
    public float spawnDistance = 12.0f;
    // Status of unicorns
    private GameObject[] unicorns;


	// Use this for initialization
	void Start () {
        // Plain application start through a naive state machine
        this.changeGameState(0);
	}
	
    // Whenever new game state is started do changes accordingly
    void changeGameState(int newState)
    {
        Debug.Log("New gameState: " + newState);
        this.gameStatus = newState;
        switch (newState){
            // Fresh application start
            case 0:
                this.gameOn = false;
                // Start button to start the game itself
                GameObject menuButtonStart = GameObject.Instantiate(this.MenuStart);
                menuButtonStart.transform.position = new Vector3(0, 3, -2);
                // Help button to go to the settings and/or help
                GameObject menuButtonHelp = GameObject.Instantiate(this.MenuHelp);
                menuButtonHelp.transform.position = new Vector3(0, -3, -2);
                // Mark down unicorns
                this.unicorns = GameObject.FindGameObjectsWithTag("Unicorn");
                break;
            // Start a fresh new game
            case 1:
                this.gameOn = true;
                // Clean starting menu
                GameObject.Destroy(GameObject.Find("Button_Play(Clone)"));
                GameObject.Destroy(GameObject.Find("Button_Help(Clone)"));
                // Create objects essential for gameplay
                GameObject Cannon = GameObject.Instantiate(this.Cannon);
                // Restart button for restarting the game
                GameObject menuButtonRestart = GameObject.Instantiate(this.MenuRestart);
                menuButtonRestart.transform.position = new Vector3(-7, 7, -2);
                // No/cross button for returning to menus
                GameObject menuButtonNo = GameObject.Instantiate(this.MenuNo);
                menuButtonNo.transform.position = new Vector3(7, 7, -2);
                // Creep counter
                GameObject counter = GameObject.Instantiate(this.Counter);
                counter.transform.position = new Vector3(0, 8, -2);
                // Reset unicorns
                foreach (GameObject unicorn in GameObject.FindGameObjectsWithTag("Unicorn"))
                {
                    unicorn.GetComponent<Animator>().SetTrigger("UnicornRecover");
                }
                this.cannon = Cannon.GetComponent<Cannon>();
                break;
            // Go to the menus
            case 2:
                this.gameOn = false;
                // Redundant menus; just a yes button to get out of it
                GameObject menuButtonYes = GameObject.Instantiate(this.MenuYes);
                menuButtonYes.transform.position = new Vector3(0, 0, -2);
                break;
            // Game wipe / restart
            case 3:
                // Naive wipe of game
                //GameObject.Destroy(GameObject.Find("CannonballPrefab(Clone)"));
                //GameObject.Destroy(GameObject.Find("CreepPrefab(Clone)"));
                GameObject.Find("Counter").GetComponent<TextMesh>().text = "0";
                // Destroy list of existing creeps
                foreach(GameObject creep in GameObject.FindGameObjectsWithTag("Creep"))
                {
                    GameObject.Destroy(creep);
                }
                // Destroy list of existing cannonballs
                foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Cannonball"))
                {
                    GameObject.Destroy(ball);
                }
                // Reset unicorns
                foreach (GameObject unicorn in GameObject.FindGameObjectsWithTag("Unicorn"))
                {
                    unicorn.GetComponent<Animator>().SetTrigger("UnicornRecover");
                }
                // Play a game status update
                GameObject update = GameObject.Instantiate(this.GameStatusText);
                update.GetComponent<GameStatusText>().setText("New game!");
                // Reset gamestatus
                this.gameOn = true;
                this.gameStatus = 1;
                // NEEDS TODO
                break;
            // Game end
            case 4:
                // Clean starting menu
                GameObject.Destroy(GameObject.Find("Button_Restart(Clone)"));
                GameObject.Destroy(GameObject.Find("Button_No(Clone)"));
                GameObject.Destroy(GameObject.Find("CreepCounter(Clone)"));
                GameObject.Destroy(GameObject.Find("Cannon(Clone)"));
                GameObject.Destroy(GameObject.Find("CreepPrefab(Clone)"));
                // Reset unicorns
                foreach (GameObject unicorn in GameObject.FindGameObjectsWithTag("Unicorn"))
                {
                    unicorn.GetComponent<Animator>().SetTrigger("UnicornRecover");
                }
                this.changeGameState(0); // Fresh start
                // NEEDS TODO
                break;
            default:
                break;
        }
    }



	// Update is called once per frame
	void Update () {
        // Interacting with user input
        // Input class can be used to interact both with the PC mouse as well as a mobile mouse finger, though finger indexing info is lost
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit: " + hit.transform.name + " with current gameState as " + this.gameStatus);
                // Ray hit an object; based on game state we will do different things
                switch (this.gameStatus)
                {
                    // Start menu
                    case 0:
                        // User wants to start game
                        if(hit.transform.name == "Button_Play(Clone)")
                        {
                            // Play a game status update
                            GameObject update = GameObject.Instantiate(this.GameStatusText);
                            update.GetComponent<GameStatusText>().setText("New game!");
                            // Start game
                            this.changeGameState(1);
                        }
                        // Go to help menus / options
                        else if(hit.transform.name == "Button_Help(Clone)")
                        {

                        }
                        break;
                    // On-going game
                    case 1:
                        // User wants to end game
                        if (hit.transform.name == "Button_No(Clone)")
                        {
                            // Run cleanup and return to menus
                            this.changeGameState(4);
                        }
                        // Restart gameplay
                        else if (hit.transform.name == "Button_Restart(Clone)")
                        {
                            // Restart game
                            this.changeGameState(3);
                        }
                        // User clicked background - shoot cannon
                        else if (hit.transform.name == "Background")
                        {
                            this.cannon.Shoot();
                        }
                        // User clicked cannon - turn cannon
                        else if (hit.transform.name == "Cannon(Clone)")
                        {
                            this.cannon.Turn();
                        }
                        break;
                    // Settings
                    case 2:
                        break;
                    // Game wipe / restart
                    case 3:
                        break;
                    // Game end
                    case 4:
                        break;
                    default:
                        break;
                }
            }
        }

        // Gameplay when game is on
        if (this.gameOn)
        {
            // Reduce time to next Creep spawn
            nextSpawn -= Time.deltaTime;
            if (nextSpawn < 0)
            {
                // Reset spawn timer
                this.nextSpawn = this.timeSpawn;
                // Spawn a Creep randomly to either side      
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f)
                {
                    GameObject creep = GameObject.Instantiate(this.Creep);
                    creep.transform.position = new Vector2(this.spawnDistance, 0);
                }
                // Other side
                else
                {
                    GameObject creep = GameObject.Instantiate(this.Creep);
                    creep.transform.position = new Vector2(-this.spawnDistance, 0);
                }
            }
            // If all unicorns are without horns, restart game
            if (Array.TrueForAll(unicorns, y => y.GetComponent<SpriteRenderer>().sprite.name=="unicorn_without_horn"))
            {
                // Turn game off
                this.gameOn = false;
                // Run cleanup and return to menus
                this.changeGameState(3);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    // Movement and state
    protected StateMachine _stateMachine;
    public WanderingDestinationSetter _wanderingDestinationSetter;
    private DragCat dragStatus;

    public SpriteRenderer renderer;
    public Animator animator;

    // Unique traits
    public string catName;
    public Color catColor;

    // Emotional states
    public float sleepiness = 0f;
    public float fallsAsleepAt = 20f;
    public float hunger = 0f;
    public float getsHungryAt = 4f;
    public Dictionary<CatController, float> friends;

    void Awake()
    {
        dragStatus = GetComponent<DragCat>();

        // Set the cat's name
        catName = GetRandomName();
        // Set the cat's colour
        catColor = Random.ColorHSV();
        renderer.color = catColor;
        // TODO: Add some patches/spots/etc

        fallsAsleepAt = Random.Range(3f, 20f);
        getsHungryAt = Random.Range(3f, 20f);

        SetupStateMachine();
    }

    private void SetupStateMachine()
    {

        // Set up state machine 
        _stateMachine = new StateMachine();

        // Init all the states
        var asleep = new Asleep(this);
        var wandering = new Wandering(this);
        var pickedUp = new PickedUp(this);
        // var lookForFood = new LookForFood(this);
        // var eat = new Eat(this);
        // var play = new Play(this);
        // var patrolTerritory = new PatrolTerritory(this);
        // var pickedUp = new PickedUp(this);
        // var findWarmSpot = new FindWarmSpot(this);


        // Set state transitions (note last value takes a function that returns a bool)
        _stateMachine.AddTransition(wandering, pickedUp, () => dragStatus.isDragged); // Have we picked up the cat?
        _stateMachine.AddTransition(asleep, wandering, () => sleepiness <= 0); // if find player - move towards the player
        _stateMachine.AddTransition(wandering, asleep, () => sleepiness >= fallsAsleepAt); // Is the little kitty to tired?
        _stateMachine.AddTransition(pickedUp, wandering, () => !dragStatus.isDragged); // Did we put it back down?

        // _stateMachine.AddTransition(findFood,wanderTerritory,() => this.TargetPlayer != null); // if find player - move towards the player
        // _stateMachine.AddTransition(wanderTerritory, dickAround, () => this._isIdle); //
        // _stateMachine.AddTransition(dickAround, findFood, () => !this._isIdle);
        // _stateMachine.AddTransition(wanderTerritory,findFood,() => this.TargetPlayer == null); // return to hunting for player if player disappears
        // _stateMachine.AddTransition(wanderTerritory,distracted,() => this._inAttackRange); // attack player if move into attack range
        // _stateMachine.AddTransition(distracted, findFood, () => this.TargetPlayer == null || !this._inAttackRange); // if player leaves range,  re-evaluate player location

        _stateMachine.SetState(wandering); // Set initial/starting state

    }

    public IState GetCurrentState()
    {
        return _stateMachine.GetCurrentState();
    }


    public void AddFriendliness(float friendliness, CatController cat)
    {
        // Add the cat if it doesn't already exist
        if (friends.ContainsKey(cat))
        {
            friends[cat] += friendliness;
        }
        else
        {
            friends.Add(cat, friendliness);
        }

    }

    protected void FixedUpdate()
    {
        _stateMachine.Tick();
    }


    public List<string> catNames = new List<string> { "Agnes", "Alex", "Alexandria", "Alfie", "Ali", "Alonzo", "Amigo", "Andrew", "Angel", "Anna", "Annabelle", "Annalise", "Antonio", "Apollo", "April", "Arnold", "Arthur", "Ash", "Ashley", "Athena", "Aubrey", "Austin", "Avery", "Bashful", "Bear", "Beau", "Beauty", "Becca", "Bella", "Benjamin", "Bennett", "Bernadette", "Bingo", "Blake", "Blanca", "Blaze", "Blossom", "Blue", "Bobby", "Bonita", "Bonnie", "Boots", "Bowie", "Bradley", "Bronson", "Bryce", "Buddy", "Buttercup", "Butterscotch", "Cali", "Cameron", "Candy", "Caramel", "Carmen", "Caroline", "Casper", "Cassidy", "Catarina", "Catnip", "Celine", "Chance", "Chanel", "Charles", "Charlotte", "Cheetah", "Chester", "Chief", "Cinnamon", "Clara", "Clarke", "Clementine", "Clover", "Clown", "Coal", "Cocoa", "Cody", "Colby", "Colin", "Comet", "Connor", "Cotton", "Cressida", "Crosby", "Crystal", "Cupcake", "Cutie", "Daffodil", "Daisy", "Dani", "Daphne", "Dash", "Dawn", "Dazzle", "Delilah", "Devon", "Diva", "Dorothy", "Dot", "Dottie", "Dove", "Draco", "Dragon", "Duchess", "Duffy", "Dusty", "Dylan", "Ebony", "Edgar", "Edward", "Einstein", "Elizabeth", "Elliott", "Elsa", "Elton", "Emerald", "Emerson", "Esmé", "Evan", "Eve", "Everett", "Felicity", "Felix", "Fighter", "Finn", "Flora", "Fluffy", "Forrest", "Frances", "Frisky", "Frosty", "Gabby", "Garfield", "Gemma", "Genevieve", "Gertrude", "Gilles", "Ginger", "Gizmo", "Gobblin", "Goldie", "Goofball", "Goofy", "Grace", "Grady", "Grayson", "Gretchen", "Grey", "Grizzly", "Grumpy", "Guinevere", "Gumdrop", "Hadley", "Hallie", "Handsome", "Happy", "Harriet", "Harrison", "Harry", "Hector", "Hera", "Herbert", "Hollie", "Homer", "Honey", "Hope", "Hopper", "Hugh", "Hulk", "Hunter", "Iris", "Ivy", "Jacob", "James", "Jamison", "Jasmine", "Jasper", "Java", "Jaxon", "Jenna", "Jewel", "Jonathan", "Jordan", "Joss", "Joy", "Jude", "Julian", "Juliet", "June", "Katia", "Katrina", "Kelsie", "Kieran", "King", "Knight", "Lacy", "Lady", "Leo", "Leon", "Lewis", "Lexa", "Liam", "Licorice", "Lily", "Lina", "Lion", "Loki", "Lonnie", "Lorenzo", "Lottie", "Lucinda", "Lucky", "Lucy", "Luke", "Luna", "Luz", "Lyric", "Mae", "Maeve", "Magic", "Mandy", "Mango", "Margaret", "Mars", "Martha", "Maui", "Maura", "Maverick", "Max", "Meadow", "Midnight", "Miguel", "Mikey", "Mila", "Mildred", "Minnie", "Mischief", "Missy", "Misty", "Mitsy", "Mittens", "Moby", "Mocha", "Molly", "Morgan", "Mosaic", "Muffin", "Murphy", "Nigel", "Night", "Noah", "Noel", "Noelle", "Nosy", "Olive", "Oliver", "Olivia", "Olly", "Opal", "Oreo", "Owen", "Paris", "Patches", "Patchouli", "Paws", "Pax", "Peaches", "Peachie", "Peanut", "Pearl", "Penelope", "Penny", "Pepper", "Petra", "Petunia", "Phoebe", "Pierre", "Piers", "Piper", "Pippa", "Pixel", "Pixie", "Polly", "Pouncer", "Powder", "Precious", "Prince", "Princess", "Raina", "Raven", "Reina", "Richard", "Riley", "Rita", "River", "Rocco", "Rocket", "Rocky", "Romeo", "Rose", "Roxie", "Ruby", "Ruthie", "Ryan", "Sadie", "Sage", "Sahara", "Sailor", "Sam", "Samantha", "Sammie", "Sandy", "Sara", "Sasha", "Sassy", "Sebastian", "Seneca", "Seraphina", "Serena", "Seymour", "Shadow", "Shady", "Shakespeare", "Shawnee", "Sheila", "Shelby", "Sheldon", "Shell", "Shenea", "Shimmer", "Silky", "Silly", "Silver", "Simba", "Skyler", "Slate", "Smoke", "Smoky", "Sneaker", "Sneaky", "Snow", "Snowflake", "Snowy", "Snuggles", "Sophie", "Speckles", "Spice", "Spot", "Sprinkles", "Sprite", "Spunky", "Stanley", "Star", "Stinky", "Storm", "Stormy", "Stripes", "Sugar", "Sullivan", "Summer", "Sunny", "Sweetie", "Tabitha", "Taffy", "Tara", "Tasha", "Tennyson", "Thadeus", "Thea", "Theodore", "Thomas", "Thor", "Tiger", "Tigger", "Toby", "Tommy", "Topher", "Trevor", "Trey", "Tristan", "Tucker", "Twinkie", "Val", "Valentine", "Valeria", "Vance", "Velvet", "Vera", "Veronica", "Victor", "Victoria", "Vincent", "Vivian", "Walker", "Walter", "Warren", "Wells", "Wesley", "Whiskers", "William", "Winston", "Winter", "Wyatt", "Xander", "Xavier", "Zeke", "Zen", "Zeus" };

    public string GetRandomName()
    {
        return catNames[Random.Range(0, catNames.Count - 1)];
    }
}

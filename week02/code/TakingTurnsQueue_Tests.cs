using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

// <summary>
/// This queue is circular.  When people are added via add_person, then they are added to the 
/// back of the queue (per FIFO rules).  When get_next_person is called, the next person
/// in the queue is displayed and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
// Class representing a person with a name and a number of turns
public class Person
{
    // Read-only property for the person's name
    public string Name { get; }
    
    // Property to track the number of turns remaining
    public int Turns { get; set; }

    // Constructor to initialize a new person with a name and number of turns
    public Person(string name, int turns)
    {
        Name = name; // Set the person's name
        Turns = turns; // Set the initial number of turns
    }
}

// Class representing the Taking Turns Queue
public class TakingTurnsQueue
{
    private Queue<Person> queue = new Queue<Person>(); // Internal queue to store people

    // Method to add a person to the queue
    public void AddPerson(string name, int turns)
    {
        // Create a new person and enqueue them in the queue
        queue.Enqueue(new Person(name, turns));
    }

    // Method to get the next person from the queue
    public Person GetNextPerson()
    {
        // Check if the queue is empty and throw an exception if it is
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("No one in the queue."); // Exception message
        }

        // Dequeue the next person from the front of the queue
        var person = queue.Dequeue();

        // If the person has turns left (greater than 0), decrement their turns and re-add them
        if (person.Turns > 0)
        {
            person.Turns--; // Decrease the number of turns remaining
            queue.Enqueue(person); // Re-add the person to the queue
        }
        else
        {
            // If the person has infinite turns (0 or less), they are also re-added
            queue.Enqueue(person);
        }

        // Return the dequeued person
        return person;
    }

    // Property to get the current length of the queue
    public int Length => queue.Count;
}

// Test class for TakingTurnsQueue
[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
    // run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: Incorrect order of returned persons due to mismanagement of turns.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        // Expected sequence of names in order
        Person[] expectedResult = { bob, tim, sue, bob, tim, sue, tim, sue, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        // Continue processing until the queue is empty
        while (players.Length > 0)
        {
            // Check if we have processed all expected results
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson(); // Get the next person from the queue
            Assert.AreEqual(expectedResult[i].Name, person.Name); // Check if the returned name matches expected
            i++; // Move to the next expected person
        }
    }

    [TestMethod]
    // Scenario: Create a queue with Bob (2), Tim (5), Sue (3) and then add George (3) after 5 runs.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: Incorrect sequence after adding a new player midway through.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        // Expected sequence of names in order
        Person[] expectedResult = { bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        // Run the queue for 5 times before adding George
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        players.AddPerson("George", 3); // Add George to the queue

        // Continue processing until the queue is empty
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with Bob (2), Tim (0), Sue (3) and run 10 times.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: Logic modifies turns of a person with infinite turns (0 turns).
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var timTurns = 0; // Tim has infinite turns

        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        // Expected sequence of names in order
        Person[] expectedResult = { bob, tim, sue, bob, tim, sue, tim, sue, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        // Run the queue for 10 times
        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, 
            "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Create a queue with Tim (-3) and Sue (3). Run 10 times.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: Similar issue with infinite turns; negative turns were not handled correctly.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var timTurns = -3; // Tim has infinite turns
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        // Expected sequence of names in order
        Person[] expectedResult = { tim, sue, tim, sue, tim, sue, tim, tim, tim, tim };

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        // Run the queue for 10 times
        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, 
            "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: None; exception handling works correctly.
    public void TestTakingTurnsQueue_Empty()
    {
        var players = new TakingTurnsQueue();

        try
        {
            players.GetNextPerson(); // Attempt to get a person from the empty queue
            Assert.Fail("Exception should have been thrown."); // Fail the test if no exception is thrown
        }
        catch (InvalidOperationException e) // Check for the specific exception type
        {
            Assert.AreEqual("No one in the queue.", e.Message); // Verify the exception message
        }
        catch (AssertFailedException)
        {
            throw; // Re-throw any assertion failures
        }
        catch (Exception e) // Catch

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
// Class to represent an item in the priority queue
public class QueueItem
{
    public string Value { get; }
    public int Priority { get; }

    public QueueItem(string value, int priority)
    {
        Value = value; // Set the value of the item
        Priority = priority; // Set the priority of the item
    }
}

// Class representing the priority queue
public class PriorityQueue
{
    private List<QueueItem> queue = new List<QueueItem>(); // Internal list to store items

    // Method to add an item to the queue
    public void Enqueue(string value, int priority)
    {
        queue.Add(new QueueItem(value, priority)); // Add the new item to the back of the queue
    }

    // Method to remove and return the item with the highest priority
    public string Dequeue()
    {
        if (queue.Count == 0) // Check if the queue is empty
        {
            throw new InvalidOperationException("The queue is empty."); // Throw an exception if it is
        }

        // Find the item with the highest priority
        var highestPriorityItem = queue.OrderByDescending(item => item.Priority).ThenBy(item => queue.IndexOf(item)).First();
        queue.Remove(highestPriorityItem); // Remove the highest priority item from the queue
        return highestPriorityItem.Value; // Return the value of the removed item
    }

    // Property to get the current count of items in the queue
    public int Count => queue.Count;
}

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add multiple items with different priorities and dequeue them.
    // Expected Result: Dequeue returns items in order of priority.
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low Priority", 1);
        priorityQueue.Enqueue("Medium Priority", 5);
        priorityQueue.Enqueue("High Priority", 10);

        // Expected sequence based on priority
        var expectedResult = new[] { "High Priority", "Medium Priority", "Low Priority" };

        for (int i = 0; i < expectedResult.Length; i++)
        {
            var item = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], item); // Check if the dequeued item matches the expected result
        }
        // Summary: All items were dequeued in the correct priority order.
    }

    [TestMethod]
    // Scenario: Add multiple items with the same priority and dequeue them.
    // Expected Result: Dequeue returns items in the order they were added (FIFO).
    // Defect(s) Found: None
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item A", 5);
        priorityQueue.Enqueue("Item B", 5);
        priorityQueue.Enqueue("Item C", 5);

        // Expected sequence should follow the order of addition
        var expectedResult = new[] { "Item A", "Item B", "Item C" };

        for (int i = 0; i < expectedResult.Length; i++)
        {
            var item = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], item); // Check if the dequeued item matches the expected result
        }
        // Summary: All items were dequeued in the order they were added, confirming FIFO behavior.
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: Exception should be thrown with an appropriate message.
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue(); // Attempt to dequeue from the empty queue
            Assert.Fail("Exception should have been thrown."); // Fail the test if no exception is thrown
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message); // Verify the exception message
        }
        // Summary: Attempting to dequeue from an empty queue correctly raised an exception.
    }

    [TestMethod]
    // Scenario: Add items with negative priorities and ensure they are still handled correctly.
    // Expected Result: Items with negative priority should be dequeued appropriately.
    // Defect(s) Found: None
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Negative Priority 1", -1);
        priorityQueue.Enqueue("Negative Priority 2", -2);
        priorityQueue.Enqueue("High Priority", 10);

        // Expected sequence based on priority
        var expectedResult = new[] { "High Priority", "Negative Priority 1", "Negative Priority 2" };

        for (int i = 0; i < expectedResult.Length; i++)
        {
            var item = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], item); // Check if the dequeued item matches the expected result
        }
        // Summary: Items with negative priorities were dequeued correctly, demonstrating proper handling of all priorities.
    }
}


public static class Arrays
{
	/// <summary>
	/// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
	/// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
	/// integer greater than 0.
	/// </summary>
	/// <returns>array of doubles that are the multiples of the supplied number</returns>
	public static double[] MultiplesOf(double number, int length)
	{
		// TODO Problem 1 Start
		// Remember: Using comments in your program, write down your process for solving this problem
		// step by step before you write the code. The plan should be clear enough that it could
		// be implemented by another person.
		/* plan for the task is :
		Plan for MultiplesOf Function
				1.Input Validation: Ensure that the length parameter is a positive integer greater than 0.
				2.Array Initialization: Create a new array of doubles with the specified length.
				3.Loop through the Array: Use a for loop to populate the array with multiples of the given number.
				4.The first element will be the number itself.
				5.Each subsequent element will be calculated as the product of the number and the index (1-based).
				6.Return the Array: After populating the array, return it */
		
		
		// Step 1: Create an array of doubles with the specified length
	double[] multiplesArray = new double[length];

	// Step 2: Loop through the array to populate it with multiples of the number
	for (int i = 0; i < length; i++)
	{
		// Step 3: Calculate the multiple and assign it to the array
		multiplesArray[i] = number * (i + 1);
	}

	// Step 4: Return the populated array
	return multiplesArray; // replace this return statement with your own
	
	}

	/// <summary>
	/// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
	/// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
	/// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
	///
	/// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
	/// </summary>
	public static void RotateListRight(List<int> data, int amount)
	{
		// TODO Problem 2 Start
		// Remember: Using comments in your program, write down your process for solving this problem
		// step by step before you write the code. The plan should be clear enough that it could
		// be implemented by another person.
		/* Plan for RotateListRight Function
                1.Input Validation: Ensure that the amount parameter is within the valid range (1 to data.Count).
                2.Calculate Effective Rotation: Use the modulo operator to determine the effective number of positions to rotate, as rotating by the length of the list results in the same list.
                3.List Slicing: Use the GetRange method to create two sublists:
                4.The first sublist will contain the last amount elements.
                5.The second sublist will contain the remaining elements.
                6.Clear the Original List: Clear the original list to prepare for the new arrangement.
                7.Reconstruct the List: Add the first sublist followed by the second sublist back to the original list.
*/
		
		// Step 1: Calculate the effective rotation amount
	int count = data.Count;
	amount = amount % count; // Ensure the amount is within the bounds of the list length

	// Step 2: Create slices of the list
	List<int> lastPart = data.GetRange(count - amount, amount); // Last 'amount' elements
	List<int> firstPart = data.GetRange(0, count - amount); // Remaining elements

	// Step 3: Clear the original list
	data.Clear();

	// Step 4: Add the last part followed by the first part to the original list
	data.AddRange(lastPart);
	data.AddRange(firstPart);
	}
}

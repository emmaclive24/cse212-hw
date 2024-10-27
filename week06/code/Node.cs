public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data)
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        if (value == Data)
            return true;
        else if (value < Data && Left is not null)
            return Left.Contains(value);
        else if (value > Data && Right is not null)
            return Right.Contains(value);

        return false;
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        int leftHeight = 0;
        if (Left is not null)
            leftHeight = Left.GetHeight();

        int rightHeight = 0;
        if (Right is not null)
            rightHeight = Right.GetHeight();

        return Math.Max(leftHeight, rightHeight) + 1;

        //return 0; // Replace this line with the correct return statement(s)
    }
}

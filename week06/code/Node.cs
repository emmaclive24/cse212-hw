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
        // CHECK IF VALUE IS A DUPLICATE, DON'T ADD IT.
        if(value == Data) 
        {
            return;
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
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
        // CHECK ROOT VALUE
        if(value == Data)
        {
            return true;
        }

        // CHECK THE LEFT SUB TREE AND THEN THE RIGHT SUB TREE
        else if(value < Data) 
        {
            
            if (Left != null)
            {
                return Left.Contains(value);
            }
            else
            {
                return false;
            }

        }

        else 
        {

            if (Right != null)
            {
                return Right.Contains(value);
            }
            else
            {
                return false;
            }
            
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        int leftHeight;
        int rightHeight;

        // HEIGHT OF LEFT SUBTREE
        if (Left != null)
        {
            leftHeight = Left.GetHeight();
        }
        else
        {
            leftHeight = 0;
        }

        // HEIGHT OF RIGHT SUBTREE
        if (Right != null)
        {
            rightHeight = Right.GetHeight();
        }
        else
        {
            rightHeight = 0;
        }

        // HEIGHT OF TREE
        if (leftHeight > rightHeight)
        {
            return 1 + leftHeight;
        }
        else
        {
            return 1 + rightHeight;
        }
        // return 0; // Replace this line with the correct return statement(s)
    }

}

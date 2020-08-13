namespace EmulationModel
{
	public class SafeLinkedList
	{
		
	}

	public class Node<T>
	{
		public Node<T> Previous { get; set; }
		public Node<T> Next { get; set; }
		public T Value { get; set; }

		public Node(T value, Node<T> previous=null, Node<T> next=null)
		{
			Value = value;
			Previous = previous;
			Next = next;
		}
	}
}
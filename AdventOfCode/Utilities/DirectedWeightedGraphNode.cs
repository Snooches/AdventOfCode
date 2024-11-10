using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public class DirectedWeightedGraphNode<KeyType,PayloadType>
		where KeyType: notnull
	{
		public KeyType Key { get; }
		public PayloadType? Payload { get; set; }
		public ImmutableDictionary<KeyType,(DirectedWeightedGraphNode<KeyType, PayloadType> Node,double Weight)> Parents { get; private set; }
		public ImmutableDictionary<KeyType,(DirectedWeightedGraphNode<KeyType, PayloadType> Node ,double Weight)> Children { get; private set; }

		public DirectedWeightedGraphNode(KeyType key)
		{
			Key = key;
			Parents = ImmutableDictionary<KeyType,(DirectedWeightedGraphNode<KeyType, PayloadType> Node, double Weight)>.Empty;
			Children = ImmutableDictionary<KeyType,(DirectedWeightedGraphNode<KeyType, PayloadType> Node, double Weight)>.Empty;
		}

		public void AddParent(DirectedWeightedGraphNode<KeyType, PayloadType> newParent, double weight = 0)
		{
			if (!Parents.ContainsKey(newParent.Key))
			{
				Parents = Parents.Add(newParent.Key,(newParent,weight));
				newParent.AddChild(this,weight);
			}
		}
		public void AddChild(DirectedWeightedGraphNode<KeyType, PayloadType> newChild, double weight = 0)
		{
			if (!Parents.ContainsKey(newChild.Key))
			{
				Children = Children.Add(newChild.Key,(newChild, weight));
				newChild.AddParent(this,weight);
			}
		}

		public List<DirectedWeightedGraphNode<KeyType, PayloadType>> GetAncestors()
		{
			List<DirectedWeightedGraphNode<KeyType, PayloadType>> ancestors = new() { this };
			foreach (var parent in Parents.Values)
			{
				ancestors.AddRange(parent.Node.GetAncestors());
			}
			return ancestors;
		}
		public List<DirectedWeightedGraphNode<KeyType, PayloadType>> GetDescendants()
		{
			List<DirectedWeightedGraphNode<KeyType, PayloadType>> descendants = new() { this };
			foreach (var child in Children.Values)
			{
				descendants.AddRange(child.Node.GetDescendants());
			}
			return descendants;
		}

	}
}

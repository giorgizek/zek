namespace Zek.Utils
{
    public class TreeNode<T>
    {
        private readonly List<TreeNode<T>> _children = [];

        public T Value { get; set; }
        public TreeNode<T>? Parent { get; private set; }
        public IReadOnlyList<TreeNode<T>> Children => _children;

        public TreeNode(T value)
        {
            Value = value;
        }

        #region Add Methods

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value);
            AddChild(node);
            return node;
        }

        public void AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void AddChildren(IEnumerable<TreeNode<T>> children)
        {
            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        #endregion

        #region Remove Methods

        public void RemoveChild(TreeNode<T> child)
        {
            if (_children.Remove(child))
                child.Parent = null;
        }

        public void Remove()
        {
            Parent?.RemoveChild(this);
        }

        public void ClearChildren()
        {
            foreach (var child in _children)
                child.Parent = null;

            _children.Clear();
        }

        #endregion

        #region Navigation

        public TreeNode<T> GetRoot()
        {
            var node = this;
            while (node.Parent != null)
                node = node.Parent;

            return node;
        }

        public int GetLevel()
        {
            int level = 0;
            var node = Parent;
            while (node != null)
            {
                level++;
                node = node.Parent;
            }
            return level;
        }

        public bool IsRoot => Parent == null;
        public bool IsLeaf => !_children.Any();

        #endregion

        #region Search

        public TreeNode<T>? FindDFS(Func<TreeNode<T>, bool> predicate)
        {
            if (predicate(this))
                return this;

            foreach (var child in _children)
            {
                var result = child.FindDFS(predicate);
                if (result != null)
                    return result;
            }

            return null;
        }

        public TreeNode<T>? FindBFS(Func<TreeNode<T>, bool> predicate)
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (predicate(node))
                    return node;

                foreach (var child in node.Children)
                    queue.Enqueue(child);
            }

            return null;
        }

        #endregion

        #region Traversal

        public IEnumerable<TreeNode<T>> TraverseDFS()
        {
            yield return this;

            foreach (var child in _children)
            {
                foreach (var descendant in child.TraverseDFS())
                    yield return descendant;
            }
        }

        public IEnumerable<TreeNode<T>> TraverseBFS()
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;

                foreach (var child in node.Children)
                    queue.Enqueue(child);
            }
        }

        public IEnumerable<TreeNode<T>> Flatten() => TraverseDFS();

        #endregion

        #region Utilities

        public IEnumerable<TreeNode<T>> GetAncestors()
        {
            var node = Parent;
            while (node != null)
            {
                yield return node;
                node = node.Parent;
            }
        }

        public bool IsDescendantOf(TreeNode<T> node)
        {
            return GetAncestors().Contains(node);
        }

        public int CountDescendants()
        {
            return TraverseDFS().Count() - 1;
        }

        public string GetPath(Func<T, string>? selector = null, string separator = "/")
        {
            selector ??= (v => v?.ToString() ?? "");

            var stack = new Stack<string>();
            var node = this;

            while (node != null)
            {
                stack.Push(selector(node.Value));
                node = node.Parent;
            }

            return string.Join(separator, stack);
        }

        public void PrintTree(string indent = "", bool last = true)
        {
            Console.WriteLine(indent + (last ? "└─ " : "├─ ") + Value);

            indent += last ? "   " : "│  ";

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].PrintTree(indent, i == _children.Count - 1);
            }
        }

        #endregion

    }

}


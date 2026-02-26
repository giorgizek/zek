using Microsoft.AspNetCore.Routing;
using Zek.Contracts;

namespace Zek.Utils
{
    public class TreeHelper
    {
        public static List<TreeNodeDto> BuildTree(IEnumerable<NodeDto> nodes, bool safe = true)
        {
            var tree = new List<TreeNodeDto>();
            var map = new Dictionary<int, TreeNodeDto>();

            // Step 1: Build node map
            foreach (var item in nodes)
            {
                var node = new TreeNodeDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Checked = item.Checked,
                };

                map[node.Id] = node;
            }

            // Step 2: Build hierarchy
            foreach (var node in map.Values)
            {
                if (node.ParentId == null)
                {
                    // Root node
                    tree.Add(node);
                }
                else
                {
                    if (map.TryGetValue(node.ParentId.Value, out var parentNode))
                    {
                        parentNode.Children ??= new List<TreeNodeDto>();
                        parentNode.Children.Add(node);
                    }
                    else if (safe)
                    {
                        // If safe=true and parent not found → treat as root
                        tree.Add(node);
                    }
                }
            }

            return tree;
        }

        public static List<TreeNodeDto> BuildTree(IList<NodeDto>? nodes, bool safe = true)
        {
            if (nodes == null || nodes.Count == 0)
                return [];

            int count = nodes.Count;
            var map = new Dictionary<int, TreeNodeDto>(count);
            // Step 1: Create DTO map (O(n))
            for (int i = 0; i < count; i++)
            {
                var n = nodes[i];
                map[n.Id] = new TreeNodeDto
                {
                    Id = n.Id,
                    ParentId = n.ParentId,
                    Name = n.Name,
                    Checked = n.Checked
                };
            }

            //var roots = new List<TreeNodeDto>(count / 4); // heuristic
            var roots = new List<TreeNodeDto>(count); // heuristic

            // Step 2: Build hierarchy (O(n))
            foreach (var node in map.Values)
            {
                if (node.ParentId is int parentId && map.TryGetValue(parentId, out var parent))
                {
                    parent.Children.Add(node);
                }
                else if (safe || node.ParentId is null)
                {
                    roots.Add(node);
                }
            }

            return roots;
        }


        /// <summary>
        /// Flattens the tree into a single Enumerable using Pre-order traversal.
        /// </summary>
        public List<TreeNodeDto> Flatten(IEnumerable<TreeNodeDto> roots, bool includeRoot = true)
        {
            var result = new List<TreeNodeDto>();
            foreach (var root in roots)
            {
                FlattenNode(root, result, includeRoot);
            }
            return result;
        }

        private static void FlattenNode(TreeNodeDto node, List<TreeNodeDto> result, bool includeNode)
        {
            if (includeNode)
                result.Add(node);

            foreach (var child in node.Children)
            {
                FlattenNode(child, result, true);
            }
        }

        public static IEnumerable<TreeNodeDto> FlattenLazy(IEnumerable<TreeNodeDto> roots, bool includeRoot = true)
        {
            foreach (var root in roots)
            {
                foreach (var node in FlattenNodeLazy(root, includeRoot))
                    yield return node;
            }
        }
        private static IEnumerable<TreeNodeDto> FlattenNodeLazy(TreeNodeDto node, bool includeNode)
        {
            if (includeNode)
                yield return node;

            foreach (var child in node.Children)
            {
                foreach (var c in FlattenNodeLazy(child, true))
                    yield return c;
            }
        }


        public static IEnumerable<int> FindIdsByIdLazy(IEnumerable<TreeNodeDto> roots, int id, bool includeRoot = true)
        {
            var targetNode = FindNode(roots, id);

            if (targetNode == null)
            {
                yield break;
            }

            if (includeRoot)
            {
                yield return targetNode.Id;
            }

            if (targetNode.Children != null && targetNode.Children.Count > 0)
            {
                foreach (var childId in ExtractIdsLazy(targetNode.Children))
                {
                    yield return childId;
                }
            }
        }

        /// <summary>
        /// Helper method: Recursively extracts Ids using lazy evaluation.
        /// </summary>
        private static IEnumerable<int> ExtractIdsLazy(IEnumerable<TreeNodeDto> nodes)
        {
            if (nodes == null) yield break;

            foreach (var node in nodes)
            {
                yield return node.Id;

                if (node.Children != null && node.Children.Count > 0)
                {
                    foreach (var childId in ExtractIdsLazy(node.Children))
                    {
                        yield return childId;
                    }
                }
            }
        }

        private static TreeNodeDto? FindNode(IEnumerable<TreeNodeDto> nodes, int id)
        {
            if (nodes == null) return null;

            foreach (var node in nodes)
            {
                if (node.Id == id)
                    return node;

                var foundInChildren = FindNode(node.Children, id);
                if (foundInChildren != null)
                    return foundInChildren;
            }

            return null;
        }

    }

}


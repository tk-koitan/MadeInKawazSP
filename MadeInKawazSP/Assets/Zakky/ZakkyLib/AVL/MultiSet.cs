using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// set
/// 挿入, 削除, 検索をO(logN)で行える
/// 値は重複してもよい
/// </summary>

namespace ZakkyLib
{
    /// <summary>
    /// C-like multiset
    /// </summary>
    public class MultiSet<T> : Set<T> where T : IComparable
    {
        public override void Insert(T v)
        {
            if (_root == null) _root = new SB_BinarySearchTree<T>.Node(v);
            else _root = SB_BinarySearchTree<T>.Insert(_root, v);
        }
    }

}
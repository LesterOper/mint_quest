using System;
using System.Collections.Generic;
using System.Linq;
using Swimmer.Library.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.View
{
    public class OptimizedTypedListView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _itemSamples;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Orientation _orientation;
        
        private RectTransform _parent;

        private Dictionary<int, GameObjectPool> _freeItemsByType = new Dictionary<int, GameObjectPool>();
        
        private Dictionary<int, ViewData> _usedItemsByPosition = new Dictionary<int, ViewData>();
        
        private IListAdapter _adapter;

        private void Awake()
        {
            _parent = GetComponent<RectTransform>();
            _scrollRect.onValueChanged.AddListener(OnScroll);
        }

        protected void Start()
        {
            foreach (var itemSample in _itemSamples)
                itemSample.SetActive(false);

            if (_adapter != null)
            {
                UpdateCreateItems();
                SetupParentSize();
                UpdateVisibleItems();
            }
        }

        public void SetAdapter(IListAdapter adapter)
        {
            _adapter = adapter;
            
            OnItemsChanged();
        }

        public void OnItemsChanged()
        {
            UpdateCreateItems();
            SetupParentSize();
            UpdateVisibleItems();
        }

        public void OnItemChanged(int position)
        {
            SetupParentSize();
            
            if (_usedItemsByPosition.Keys.Contains(position))
                FreeItemView(position);
            if (IsItemVisible(position))
                SetupView(position);
            
            for (var i = position; i < _adapter.GetItemsCount(); i++)
            {
                if (_usedItemsByPosition.ContainsKey(i))
                    SetupItemPosition(i);
            }
        }

        public void OnItemRemoved(int position)
        {
            if (_usedItemsByPosition.Keys.Contains(position))
                FreeItemView(position);
            SetupParentSize();
            
            for (var i = position; i < _adapter.GetItemsCount() + 1; i++)
            {
                if (_usedItemsByPosition.ContainsKey(i))
                {
                    _usedItemsByPosition[i - 1] = _usedItemsByPosition[i];
                    _usedItemsByPosition.Remove(i);
                    SetupItemPosition(i - 1);
                }
            }
            
            UpdateVisibleItems();
        }

        public void OnItemAdded()
        {
            OnItemAdded(_adapter.GetItemsCount() - 1);
        }

        public void OnItemAdded(int position)
        {
            SetupParentSize();
            
            for (var i = _adapter.GetItemsCount() - 2; i >= position; i--)
            {
                if (_usedItemsByPosition.ContainsKey(i))
                {
                    _usedItemsByPosition[i + 1] = _usedItemsByPosition[i];
                    _usedItemsByPosition.Remove(i);
                    SetupItemPosition(i + 1);
                }
            }
            
            UpdateVisibleItems();
        }

        private void UpdateCreateItems()
        {
            var currentVisibleItems = new HashSet<int>(_usedItemsByPosition.Keys);
            foreach (var position in currentVisibleItems)
                FreeItemView(position);
            
            _usedItemsByPosition.Clear();

            for (var i = 0; i < _adapter.GetItemsCount(); i++)
            {
                if (IsItemVisible(i))
                {
                    SetupView(i);
                    SetupItemPosition(i);
                }
            }
        }

        private void FreeItemView(int position)
        {
            var usedItem = _usedItemsByPosition[position];
            _usedItemsByPosition.Remove(position);
            _freeItemsByType[usedItem.ViewType].Free(usedItem.ItemView);
            
            usedItem.ItemView.SetActive(false);
        }

        private void SetupView(int position)
        {
            var itemType = _adapter.GetViewTypeNumber(position);
                
            if (!_freeItemsByType.ContainsKey(itemType))
                _freeItemsByType[itemType] = new GameObjectPool(_itemSamples[itemType], _parent);

            var itemView = _freeItemsByType[itemType].Obtain();
            _usedItemsByPosition[position] = new ViewData
            {
                ViewType = itemType,
                ItemView = itemView
            };

            itemView.SetActive(true);
            
            _adapter.SetupView(position, itemType, itemView);
        }

        private void OnScroll(Vector2 scrollPosition)
        {
            UpdateVisibleItems();
        }

        private void UpdateVisibleItems()
        {
            var currentVisibleItems = new HashSet<int>(_usedItemsByPosition.Keys);

            foreach (var position in currentVisibleItems)
            {
                if (!IsItemVisible(position))
                    FreeItemView(position);
            }
            
            for (var i = 0; i < _adapter.GetItemsCount(); i++)
            {
                if (!currentVisibleItems.Contains(i) && IsItemVisible(i))
                {
                    SetupView(i);
                    SetupItemPosition(i);
                }
            }
        }
        
        private Vector2 GetItemSize(int position)
        {
            return _itemSamples[_adapter.GetViewTypeNumber(position)].GetComponent<RectTransform>().sizeDelta;
        }

        private Vector2 GetItemPosition(int position)
        {
            var vector2 = GetItemSize(position) / 2;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    
                    for (var i = 0; i < position; i++)
                        vector2.x += GetItemSize(i).x;
                    vector2.y = 0;
                    break;
                
                case Orientation.Vertical:
                    vector2.x = 0;
                    vector2.y *= -1;
                    for (var i = 0; i < position; i++)
                        vector2.y -= GetItemSize(i).y;
                    break;
            }

            return vector2;
        }

        private bool IsItemVisible(int position)
        {
            var parentLocalPosition = _parent.anchoredPosition;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    var sizeDeltaX = _scrollRect.GetComponent<RectTransform>().rect.width;
                    return GetItemPosition(position).x + GetItemSize(position).x / 2f + parentLocalPosition.x
                           > -sizeDeltaX * 0.55f &&
                           GetItemPosition(position).x - GetItemSize(position).x / 2f + parentLocalPosition.x
                           < sizeDeltaX * 0.55f;
                
                case Orientation.Vertical:
                    var sizeDeltaY = _scrollRect.GetComponent<RectTransform>().rect.height;
                    return GetItemPosition(position).y + GetItemSize(position).y / 2f + parentLocalPosition.y
                           > -sizeDeltaY * 1.05f &&
                           GetItemPosition(position).y - GetItemSize(position).y / 2f + parentLocalPosition.y
                           < sizeDeltaY * 0.05f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetupItemPosition(int position)
        {
            var itemView = _usedItemsByPosition[position].ItemView;
            itemView.GetComponent<RectTransform>().anchoredPosition = GetItemPosition(position);
        }

        private void SetupParentSize()
        {
            var width = 0f;
            var height = 0f;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    for (var i = 0; i < _adapter.GetItemsCount(); i++)
                        width += GetItemSize(i).x;
                    height = _parent.sizeDelta.y;
                    break;
                
                case Orientation.Vertical:
                    width = _parent.sizeDelta.x;
                    for (var i = 0; i < _adapter.GetItemsCount(); i++)
                        height += GetItemSize(i).y;
                    break;
            }
            
            _parent.sizeDelta = new Vector2(width, height);
        }

        public IListAdapter Adapter => _adapter;

        public interface IListAdapter
        {
            int GetItemsCount();
            int GetViewTypeNumber(int position);
            void SetupView(int position, int type, GameObject itemObject);
        }

        private class ViewData
        {
            public int ViewType { get; set; }
            public GameObject ItemView { get; set; }
        }

        public ScrollRect ScrollRect => _scrollRect;

        public enum Orientation
        {
            Horizontal,
            Vertical
        }
        
    }
}
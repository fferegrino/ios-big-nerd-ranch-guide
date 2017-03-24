using System;
using System.Collections.Generic;
using System.Text;
using FlickrNet;
using Foundation;
using UIKit;

namespace Photorama
{
    public class PhotoDataSource : NSObject, IUICollectionViewDataSource
    {
        public Photo[] Photos { get; set; }
        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return Photos?.Length ?? 0;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("UICollectionViewCell", indexPath) as UICollectionViewCell;
            return cell;
        }
    }
}

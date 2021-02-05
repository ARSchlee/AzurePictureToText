using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PictureToText
{
	class ImageMerger
	{
		FileManager fileManager = new FileManager();

		List<Bitmap> getBitmapsToMerge(string folderPath)
		{
			List<Bitmap> bitmapsToMerge = new List<Bitmap>();
			var imagesToMerge = fileManager.getFilesFromFolder(folderPath);

			foreach(var file in imagesToMerge)
			{
				bitmapsToMerge.Add(new Bitmap(file.FullName));
			}
			return bitmapsToMerge;
		}

		int getTotalHeightOfBitmaps(List<Bitmap> bitmaps)
		{
			var height = bitmaps.Select(x => x.Height).Sum();
			return height;
		}

		int getMaxWidthOfBitmaps(List<Bitmap> bitmaps)
		{
			var maxWidth = bitmaps.Select(x => x.Width).Max();
			return maxWidth;
		}

		public void MergeImageFilesVerticallyByFolder(DirectoryInfo folderName)
		{
			var subfolders = fileManager.scanForSubFolders(folderName.FullName);
			foreach (var folder in subfolders)
			{
				MergeImageFilesVertically(folder);
			}
		}

		public void MergeImageFilesVertically(DirectoryInfo folderName)
		{
			var bitmapsToMerge = getBitmapsToMerge(folderName.FullName);
			var mergedBitmap = new Bitmap(getMaxWidthOfBitmaps(bitmapsToMerge), getTotalHeightOfBitmaps(bitmapsToMerge));

			using(Graphics g = Graphics.FromImage(mergedBitmap))
			{
				var bottomHeightOfBitmap = 0;
				foreach(var bitmap in bitmapsToMerge)
				{
					g.DrawImage(bitmap, 0, bottomHeightOfBitmap);
					bottomHeightOfBitmap += bitmap.Height;
				}
			}
			saveBitmap(mergedBitmap, folderName.Name);
		}

		void saveBitmap(Bitmap bitmapToSave, string fileName)
		{
			bitmapToSave.Save(@"C:\Users\Lex Schlee\OneDrive\Documents\ImageToTextStuff\MergedImages\" + fileName +  ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		}
	}
}

using System;
using System.Collections;

// Token: 0x02000042 RID: 66
public class ImgByName
{
	// Token: 0x06000394 RID: 916 RVA: 0x0000482E File Offset: 0x00002A2E
	public static void SetImage(string name, Image img, sbyte nFrame)
	{
		ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00051260 File Offset: 0x0004F460
	public static MainImage getImagePath(string nameImg, MyHashTable hash)
	{
		MainImage mainImage = (MainImage)hash.get(string.Empty + nameImg);
		bool flag = mainImage == null;
		if (flag)
		{
			mainImage = new MainImage();
			MainImage fromRms = ImgByName.getFromRms(nameImg);
			bool flag2 = fromRms != null;
			if (flag2)
			{
				mainImage.img = fromRms.img;
				mainImage.nFrame = fromRms.nFrame;
			}
			hash.put(string.Empty + nameImg, mainImage);
		}
		mainImage.count = GameCanvas.timeNow / 1000L;
		bool flag3 = mainImage.img == null;
		if (flag3)
		{
			mainImage.timeImageNull--;
			bool flag4 = mainImage.timeImageNull <= 0;
			if (flag4)
			{
				Service.gI().getImgByName(nameImg);
				mainImage.timeImageNull = 200;
			}
		}
		return mainImage;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00051334 File Offset: 0x0004F534
	public static MainImage getFromRms(string nameImg)
	{
		string filename = mGraphics.zoomLevel + "ImgByName_" + nameImg;
		MainImage mainImage = null;
		sbyte[] array = Rms.loadRMS(filename);
		bool flag = array == null;
		MainImage result;
		if (flag)
		{
			result = mainImage;
		}
		else
		{
			try
			{
				result = new MainImage
				{
					nFrame = array[0],
					img = Image.createImage(array, 1, array.Length)
				};
			}
			catch (Exception)
			{
				result = null;
			}
		}
		return result;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000513B0 File Offset: 0x0004F5B0
	public static void saveRMS(string nameImg, sbyte nFrame, sbyte[] data)
	{
		string filename = mGraphics.zoomLevel + "ImgByName_" + nameImg;
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(nFrame);
			for (int i = 0; i < data.Length; i++)
			{
				dataOutputStream.writeByte(data[i]);
			}
			Rms.saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00051430 File Offset: 0x0004F630
	public static void checkDelHash(MyHashTable hash, int minute, bool isTrue)
	{
		MyVector myVector = new MyVector("checkDelHash");
		if (isTrue)
		{
			hash.clear();
		}
		else
		{
			IDictionaryEnumerator enumerator = hash.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MainImage mainImage = (MainImage)enumerator.Value;
				bool flag = GameCanvas.timeNow / 1000L - mainImage.count > (long)(minute * 60);
				if (flag)
				{
					string o = (string)enumerator.Key;
					myVector.addElement(o);
				}
			}
			for (int i = 0; i < myVector.size(); i++)
			{
				hash.remove((string)myVector.elementAt(i));
			}
		}
	}

	// Token: 0x0400081A RID: 2074
	public static MyHashTable hashImagePath = new MyHashTable();
}

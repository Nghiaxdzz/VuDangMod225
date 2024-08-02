using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class Rms
{
	// Token: 0x060007A7 RID: 1959 RVA: 0x0008771C File Offset: 0x0008591C
	public static void saveRMS(string filename, sbyte[] data)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		if (flag)
		{
			Rms.__saveRMS(filename, data);
		}
		else
		{
			Rms._saveRMS(filename, data);
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00087758 File Offset: 0x00085958
	public static sbyte[] loadRMS(string filename)
	{
		bool flag = Thread.CurrentThread.Name == Main.mainThreadName;
		sbyte[] result;
		if (flag)
		{
			result = Rms.__loadRMS(filename);
		}
		else
		{
			result = Rms._loadRMS(filename);
		}
		return result;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00087794 File Offset: 0x00085994
	public static string loadRMSString(string fileName)
	{
		sbyte[] array = Rms.loadRMS(fileName);
		bool flag = array == null;
		string result;
		if (flag)
		{
			result = null;
		}
		else
		{
			DataInputStream dataInputStream = new DataInputStream(array);
			try
			{
				string result2 = dataInputStream.readUTF();
				dataInputStream.close();
				return result2;
			}
			catch (Exception ex)
			{
				Cout.println(ex.StackTrace);
			}
			result = null;
		}
		return result;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00050938 File Offset: 0x0004EB38
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			bool flag = var[i] > 0;
			if (flag)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x000877FC File Offset: 0x000859FC
	public static void saveRMSString(string filename, string data)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(data);
			Rms.saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00087854 File Offset: 0x00085A54
	private static void _saveRMS(string filename, sbyte[] data)
	{
		bool flag = Rms.status != 0;
		if (flag)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
		}
		else
		{
			Rms.filename = filename;
			Rms.data = data;
			Rms.status = 2;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Rms.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Debug.LogError("TOO LONG TO SAVE RMS " + filename);
			}
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x000878EC File Offset: 0x00085AEC
	private static sbyte[] _loadRMS(string filename)
	{
		bool flag = Rms.status != 0;
		sbyte[] result;
		if (flag)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
			result = null;
		}
		else
		{
			Rms.filename = filename;
			Rms.data = null;
			Rms.status = 3;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				bool flag2 = Rms.status == 0;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = i == 500;
			if (flag3)
			{
				Debug.LogError("TOO LONG TO LOAD RMS " + filename);
			}
			result = Rms.data;
		}
		return result;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00087990 File Offset: 0x00085B90
	public static void update()
	{
		bool flag = Rms.status == 2;
		if (flag)
		{
			Rms.status = 1;
			Rms.__saveRMS(Rms.filename, Rms.data);
			Rms.status = 0;
		}
		else
		{
			bool flag2 = Rms.status == 3;
			if (flag2)
			{
				Rms.status = 1;
				Rms.data = Rms.__loadRMS(Rms.filename);
				Rms.status = 0;
			}
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x000879F4 File Offset: 0x00085BF4
	public static int loadRMSInt(string file)
	{
		sbyte[] array = Rms.loadRMS(file);
		return (int)((array != null) ? array[0] : -1);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00087A18 File Offset: 0x00085C18
	public static void saveRMSInt(string file, int x)
	{
		try
		{
			Rms.saveRMS(file, new sbyte[]
			{
				(sbyte)x
			});
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00087A54 File Offset: 0x00085C54
	public static string GetiPhoneDocumentsPath()
	{
		return Application.persistentDataPath;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00087A6C File Offset: 0x00085C6C
	private static void __saveRMS(string filename, sbyte[] data)
	{
		string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
		FileStream fileStream = new FileStream(text, FileMode.Create);
		fileStream.Write(ArrayCast.cast(data), 0, data.Length);
		fileStream.Flush();
		fileStream.Close();
		Main.setBackupIcloud(text);
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00087ABC File Offset: 0x00085CBC
	private static sbyte[] __loadRMS(string filename)
	{
		sbyte[] result;
		try
		{
			FileStream fileStream = new FileStream(Rms.GetiPhoneDocumentsPath() + "/" + filename, FileMode.Open);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			sbyte[] array2 = ArrayCast.cast(array);
			result = ArrayCast.cast(array);
		}
		catch (Exception)
		{
			result = null;
		}
		return result;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00087B2C File Offset: 0x00085D2C
	public static void clearAll()
	{
		Cout.LogError3("clean rms");
		FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			fileInfo.Delete();
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00087B80 File Offset: 0x00085D80
	public static void DeleteStorage(string path)
	{
		try
		{
			File.Delete(Rms.GetiPhoneDocumentsPath() + "/" + path);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00087BC0 File Offset: 0x00085DC0
	public static string ByteArrayToString(byte[] ba)
	{
		string text = BitConverter.ToString(ba);
		return text.Replace("-", string.Empty);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00087BEC File Offset: 0x00085DEC
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00087C38 File Offset: 0x00085E38
	public static void deleteRecord(string name)
	{
		try
		{
			PlayerPrefs.DeleteKey(name);
		}
		catch (Exception ex)
		{
			Cout.println("loi xoa RMS --------------------------" + ex.ToString());
		}
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00087C7C File Offset: 0x00085E7C
	public static void clearRMS()
	{
		Rms.deleteRecord("data");
		Rms.deleteRecord("dataVersion");
		Rms.deleteRecord("map");
		Rms.deleteRecord("mapVersion");
		Rms.deleteRecord("skill");
		Rms.deleteRecord("killVersion");
		Rms.deleteRecord("item");
		Rms.deleteRecord("itemVersion");
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00005BF5 File Offset: 0x00003DF5
	public static void saveIP(string strID)
	{
		Rms.saveRMSString("NRIPlink", strID);
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00087CE4 File Offset: 0x00085EE4
	public static string loadIP()
	{
		string text = Rms.loadRMSString("NRIPlink");
		bool flag = text == null;
		string result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = text;
		}
		return result;
	}

	// Token: 0x04000FC8 RID: 4040
	public static int status;

	// Token: 0x04000FC9 RID: 4041
	public static sbyte[] data;

	// Token: 0x04000FCA RID: 4042
	public static string filename;

	// Token: 0x04000FCB RID: 4043
	private const int INTERVAL = 5;

	// Token: 0x04000FCC RID: 4044
	private const int MAXTIME = 500;
}

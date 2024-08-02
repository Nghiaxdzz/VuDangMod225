using System;

namespace Assets.src.g
{
	// Token: 0x020000D5 RID: 213
	internal class ImageSource
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00006911 File Offset: 0x00004B11
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000A765C File Offset: 0x000A585C
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			bool flag = array == null;
			if (flag)
			{
				Service.gI().imageSource(myVector);
			}
			else
			{
				ImageSource.vRms = new MyVector();
				DataInputStream dataInputStream = new DataInputStream(array);
				bool flag2 = dataInputStream == null;
				if (!flag2)
				{
					try
					{
						short num = dataInputStream.readShort();
						string[] array2 = new string[(int)num];
						sbyte[] array3 = new sbyte[(int)num];
						for (int i = 0; i < (int)num; i++)
						{
							array2[i] = dataInputStream.readUTF();
							array3[i] = dataInputStream.readByte();
							ImageSource.vRms.addElement(new ImageSource(array2[i], array3[i]));
						}
						dataInputStream.close();
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
					Res.outz(string.Concat(new object[]
					{
						"vS size= ",
						ImageSource.vSource.size(),
						" vRMS size= ",
						ImageSource.vRms.size()
					}));
					for (int j = 0; j < ImageSource.vSource.size(); j++)
					{
						ImageSource imageSource = (ImageSource)ImageSource.vSource.elementAt(j);
						bool flag3 = !ImageSource.isExistID(imageSource.id);
						if (flag3)
						{
							myVector.addElement(imageSource);
						}
					}
					for (int k = 0; k < ImageSource.vRms.size(); k++)
					{
						ImageSource imageSource2 = (ImageSource)ImageSource.vRms.elementAt(k);
						bool flag4 = ImageSource.getVersionRMSByID(imageSource2.id) != ImageSource.getCurrVersionByID(imageSource2.id);
						if (flag4)
						{
							myVector.addElement(imageSource2);
						}
					}
					Service.gI().imageSource(myVector);
				}
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000A7850 File Offset: 0x000A5A50
		public static sbyte getVersionRMSByID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id);
				if (flag)
				{
					return ((ImageSource)ImageSource.vRms.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000A78B8 File Offset: 0x000A5AB8
		public static sbyte getCurrVersionByID(string id)
		{
			for (int i = 0; i < ImageSource.vSource.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vSource.elementAt(i)).id);
				if (flag)
				{
					return ((ImageSource)ImageSource.vSource.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000A7920 File Offset: 0x000A5B20
		public static bool isExistID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				bool flag = id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000A7974 File Offset: 0x000A5B74
		public static void saveRMS()
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeShort((short)ImageSource.vSource.size());
				for (int i = 0; i < ImageSource.vSource.size(); i++)
				{
					dataOutputStream.writeUTF(((ImageSource)ImageSource.vSource.elementAt(i)).id);
					dataOutputStream.writeByte(((ImageSource)ImageSource.vSource.elementAt(i)).version);
				}
				Rms.saveRMS("ImageSource", dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x040013A9 RID: 5033
		public sbyte version;

		// Token: 0x040013AA RID: 5034
		public string id;

		// Token: 0x040013AB RID: 5035
		public static MyVector vSource = new MyVector();

		// Token: 0x040013AC RID: 5036
		public static MyVector vRms = new MyVector();
	}
}

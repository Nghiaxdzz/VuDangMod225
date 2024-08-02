using System;
using System.Collections;
using Assets.src.e;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class mGraphics
{
	// Token: 0x060004CE RID: 1230 RVA: 0x0005B588 File Offset: 0x00059788
	private void cache(string key, Texture value)
	{
		bool flag = mGraphics.cachedTextures.Count > 400;
		if (flag)
		{
			mGraphics.cachedTextures.Clear();
		}
		bool flag2 = value.width * value.height < GameCanvas.w * GameCanvas.h;
		if (flag2)
		{
			mGraphics.cachedTextures.Add(key, value);
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0005B5E8 File Offset: 0x000597E8
	public void translate(int tx, int ty)
	{
		tx *= mGraphics.zoomLevel;
		ty *= mGraphics.zoomLevel;
		this.translateX += tx;
		this.translateY += ty;
		this.isTranslate = true;
		bool flag = this.translateX == 0 && this.translateY == 0;
		if (flag)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0005B64C File Offset: 0x0005984C
	public void translate(float x, float y)
	{
		this.translateXf += x;
		this.translateYf += y;
		this.isTranslate = true;
		bool flag = this.translateXf == 0f && this.translateYf == 0f;
		if (flag)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0005B6A8 File Offset: 0x000598A8
	public int getTranslateX()
	{
		return this.translateX / mGraphics.zoomLevel;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0005B6C8 File Offset: 0x000598C8
	public int getTranslateY()
	{
		return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0005B6EC File Offset: 0x000598EC
	public void setClip(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		this.clipTX = this.translateX;
		this.clipTY = this.translateY;
		this.clipX = x;
		this.clipY = y;
		this.clipW = w;
		this.clipH = h;
		this.isClip = true;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0005B75C File Offset: 0x0005995C
	public int getClipX()
	{
		return GameScr.cmx;
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0005B774 File Offset: 0x00059974
	public int getClipY()
	{
		return GameScr.cmy;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0005B78C File Offset: 0x0005998C
	public int getClipWidth()
	{
		return GameScr.gW;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0005B7A4 File Offset: 0x000599A4
	public int getClipHeight()
	{
		return GameScr.gH;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0005B7BC File Offset: 0x000599BC
	public void fillRect(int x, int y, int w, int h, int color, int alpha)
	{
		float alpha2 = 0.5f;
		this.setColor(color, alpha2);
		this.fillRect(x, y, w, h);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0005B7E8 File Offset: 0x000599E8
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		x1 *= mGraphics.zoomLevel;
		y1 *= mGraphics.zoomLevel;
		x2 *= mGraphics.zoomLevel;
		y2 *= mGraphics.zoomLevel;
		bool flag = y1 == y2;
		if (flag)
		{
			bool flag2 = x1 > x2;
			if (flag2)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			this.fillRect(x1, y1, x2 - x1, 1);
		}
		else
		{
			bool flag3 = x1 == x2;
			if (flag3)
			{
				bool flag4 = y1 > y2;
				if (flag4)
				{
					int num2 = y2;
					y2 = y1;
					y1 = num2;
				}
				this.fillRect(x1, y1, 1, y2 - y1);
			}
			else
			{
				bool flag5 = this.isTranslate;
				if (flag5)
				{
					x1 += this.translateX;
					y1 += this.translateY;
					x2 += this.translateX;
					y2 += this.translateY;
				}
				string key = string.Concat(new object[]
				{
					"dl",
					this.r,
					this.g,
					this.b
				});
				Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
				bool flag6 = texture2D == null;
				if (flag6)
				{
					texture2D = new Texture2D(1, 1);
					Color color = new Color(this.r, this.g, this.b);
					texture2D.SetPixel(0, 0, color);
					texture2D.Apply();
					this.cache(key, texture2D);
				}
				Vector2 vector = new Vector2((float)x1, (float)y1);
				Vector2 vector2 = new Vector2((float)x2, (float)y2);
				Vector2 vector3 = vector2 - vector;
				float num3 = 57.29578f * Mathf.Atan(vector3.y / vector3.x);
				bool flag7 = vector3.x < 0f;
				if (flag7)
				{
					num3 += 180f;
				}
				int num4 = (int)Mathf.Ceil(0f);
				GUIUtility.RotateAroundPivot(num3, vector);
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				bool flag8 = this.isClip;
				if (flag8)
				{
					num5 = this.clipX;
					num6 = this.clipY;
					num7 = this.clipW;
					num8 = this.clipH;
					bool flag9 = this.isTranslate;
					if (flag9)
					{
						num5 += this.clipTX;
						num6 += this.clipTY;
					}
				}
				bool flag10 = this.isClip;
				if (flag10)
				{
					GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
				}
				Graphics.DrawTexture(new Rect(vector.x - (float)num5, vector.y - (float)num4 - (float)num6, vector3.magnitude, 1f), texture2D);
				bool flag11 = this.isClip;
				if (flag11)
				{
					GUI.EndGroup();
				}
				GUIUtility.RotateAroundPivot(0f - num3, vector);
			}
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0005A524 File Offset: 0x00058724
	public Color setColorMiniMap(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		return new Color(num6, num5, num4);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0005BAA0 File Offset: 0x00059CA0
	public float[] getRGB(Color cl)
	{
		float num = 256f * cl.r;
		float num2 = 256f * cl.g;
		float num3 = 256f * cl.b;
		return new float[]
		{
			num,
			num2,
			num3
		};
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0005BAEC File Offset: 0x00059CEC
	public void drawRect(int x, int y, int w, int h)
	{
		int num = 1;
		this.fillRect(x, y, w, num);
		this.fillRect(x, y, num, h);
		this.fillRect(x + w, y, num, h + 1);
		this.fillRect(x, y + h, w + 1, num);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0005BB34 File Offset: 0x00059D34
	public void fillRect(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		bool flag = w < 0 || h < 0;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += this.translateX;
				y += this.translateY;
			}
			int num = 1;
			int num2 = 1;
			string key = string.Concat(new object[]
			{
				"fr",
				num,
				num2,
				this.r,
				this.g,
				this.b,
				this.a
			});
			Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
			bool flag3 = texture2D == null;
			if (flag3)
			{
				texture2D = new Texture2D(num, num2);
				Color color = new Color(this.r, this.g, this.b, this.a);
				texture2D.SetPixel(0, 0, color);
				texture2D.Apply();
				this.cache(key, texture2D);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			bool flag4 = this.isClip;
			if (flag4)
			{
				num3 = this.clipX;
				num4 = this.clipY;
				num5 = this.clipW;
				num6 = this.clipH;
				bool flag5 = this.isTranslate;
				if (flag5)
				{
					num3 += this.clipTX;
					num4 += this.clipTY;
				}
			}
			bool flag6 = this.isClip;
			if (flag6)
			{
				GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
			}
			GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), texture2D);
			bool flag7 = this.isClip;
			if (flag7)
			{
				GUI.EndGroup();
			}
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0005BD18 File Offset: 0x00059F18
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00004E6E File Offset: 0x0000306E
	public void setColor(Color color)
	{
		this.b = color.b;
		this.g = color.g;
		this.r = color.r;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0005BD78 File Offset: 0x00059F78
	public void setBgColor(int rgb)
	{
		bool flag = rgb != this.currentBGColor;
		if (flag)
		{
			this.currentBGColor = rgb;
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			Main.main.GetComponent<Camera>().backgroundColor = new Color(this.r, this.g, this.b);
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0005BE10 File Offset: 0x0005A010
	public void drawString(string s, int x, int y, GUIStyle style)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag2 = this.isClip;
		if (flag2)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag3 = this.isTranslate;
			if (flag3)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag4 = this.isClip;
		if (flag4)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		bool flag5 = this.isClip;
		if (flag5)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0005BEFC File Offset: 0x0005A0FC
	public void setColor(int rgb, float alpha)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = alpha;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0005BF58 File Offset: 0x0005A158
	public void drawString(string s, int x, int y, GUIStyle style, int w)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		bool flag2 = this.isClip;
		if (flag2)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag3 = this.isTranslate;
			if (flag3)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag4 = this.isClip;
		if (flag4)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2 - 4), (float)w, 100f), s, style);
		bool flag5 = this.isClip;
		if (flag5)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0005C044 File Offset: 0x0005A244
	private void UpdatePos(int anchor)
	{
		Vector2 vector = new Vector2(0f, 0f);
		if (anchor <= 17)
		{
			if (anchor <= 6)
			{
				if (anchor != 3)
				{
					if (anchor == 6)
					{
						vector = new Vector2(0f, (float)(Screen.height / 2));
					}
				}
				else
				{
					vector = new Vector2(this.size.x / 2f, this.size.y / 2f);
				}
			}
			else if (anchor != 10)
			{
				if (anchor == 17)
				{
					vector = new Vector2((float)(Screen.width / 2), 0f);
				}
			}
			else
			{
				vector = new Vector2((float)Screen.width, (float)(Screen.height / 2));
			}
		}
		else if (anchor <= 24)
		{
			if (anchor != 20)
			{
				if (anchor == 24)
				{
					vector = new Vector2((float)Screen.width, 0f);
				}
			}
			else
			{
				vector = new Vector2(0f, 0f);
			}
		}
		else if (anchor != 33)
		{
			if (anchor != 36)
			{
				if (anchor == 40)
				{
					vector = new Vector2((float)Screen.width, (float)Screen.height);
				}
			}
			else
			{
				vector = new Vector2(0f, (float)Screen.height);
			}
		}
		else
		{
			vector = new Vector2((float)(Screen.width / 2), (float)Screen.height);
		}
		this.pos = vector + this.relativePosition;
		this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
		this.pivot = new Vector2(this.rect.xMin + this.rect.width * 0.5f, this.rect.yMin + this.rect.height * 0.5f);
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0005C264 File Offset: 0x0005A464
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		x0 *= mGraphics.zoomLevel;
		y0 *= mGraphics.zoomLevel;
		w0 *= mGraphics.zoomLevel;
		h0 *= mGraphics.zoomLevel;
		this._drawRegion(arg0, (float)x0, (float)y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0005C2C4 File Offset: 0x0005A4C4
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= mGraphics.zoomLevel;
		y0 *= mGraphics.zoomLevel;
		w0 *= mGraphics.zoomLevel;
		h0 *= mGraphics.zoomLevel;
		this.__drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0005C324 File Offset: 0x0005A524
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
	{
		this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0005C348 File Offset: 0x0005A548
	public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += (float)this.translateX;
				y += (float)this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
			if (flag3)
			{
				num5 -= num / 2f;
			}
			bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
			if (flag4)
			{
				num6 -= num2 / 2f;
			}
			bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
			if (flag5)
			{
				num5 -= num;
			}
			bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
			if (flag6)
			{
				num6 -= num2;
			}
			x += num5;
			y += num6;
			int num10 = 0;
			int num11 = 0;
			bool flag7 = this.isClip;
			if (flag7)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				bool flag8 = this.isTranslate;
				if (flag8)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect(x, y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				bool flag9 = rect.width <= 0f || rect.height <= 0f;
				if (flag9)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			switch (transform)
			{
			case 1:
				num9 = -1;
				num15 += num2;
				break;
			case 2:
			{
				num14 += num;
				num7 = -1f;
				bool flag10 = this.isClip;
				if (flag10)
				{
					bool flag11 = (float)num10 > x;
					if (flag11)
					{
						num8 = 0f - num3;
					}
					else
					{
						bool flag12 = (float)(num10 + num11) < x + (float)w;
						if (flag12)
						{
							num8 = 0f - ((float)(num10 + num11) - x - (float)w);
						}
					}
				}
				break;
			}
			case 3:
				num9 = -1;
				num15 += num2;
				num7 = -1f;
				num14 += num;
				break;
			}
			int num16 = 0;
			int num17 = 0;
			bool flag13 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag13)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2(x, y);
				this.UpdatePos(3);
				if (transform != 5)
				{
					if (transform == 6)
					{
						this.UpdatePos(3);
					}
				}
				else
				{
					this.size = new Vector2((float)w, (float)h);
					this.UpdatePos(3);
				}
				switch (transform)
				{
				case 4:
				{
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num14 += num;
					num7 = -1f;
					bool flag14 = this.isClip;
					if (flag14)
					{
						bool flag15 = (float)num10 > x;
						if (flag15)
						{
							num8 = 0f - num3;
						}
						else
						{
							bool flag16 = (float)(num10 + num11) < x + (float)w;
							if (flag16)
							{
								num8 = 0f - ((float)(num10 + num11) - x - (float)w);
							}
						}
					}
					break;
				}
				case 5:
					GUIUtility.RotateAroundPivot(90f, this.pivot);
					break;
				case 6:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					break;
				case 7:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num9 = -1;
					num15 += num2;
					break;
				}
			}
			Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect(((float)x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - ((float)y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			bool flag17 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag17)
			{
				GUI.matrix = this.matrixBackup;
			}
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0005C810 File Offset: 0x0005AA10
	public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		bool flag = image == null;
		if (!flag)
		{
			bool flag2 = this.isTranslate;
			if (flag2)
			{
				x += this.translateX;
				y += this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
			if (flag3)
			{
				num5 -= num / 2f;
			}
			bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
			if (flag4)
			{
				num6 -= num2 / 2f;
			}
			bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
			if (flag5)
			{
				num5 -= num;
			}
			bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
			if (flag6)
			{
				num6 -= num2;
			}
			x += (int)num5;
			y += (int)num6;
			int num10 = 0;
			int num11 = 0;
			bool flag7 = this.isClip;
			if (flag7)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				bool flag8 = this.isTranslate;
				if (flag8)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect((float)x, (float)y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				bool flag9 = rect.width <= 0f || rect.height <= 0f;
				if (flag9)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			switch (transform)
			{
			case 1:
				num9 = -1;
				num15 += num2;
				break;
			case 2:
			{
				num14 += num;
				num7 = -1f;
				bool flag10 = this.isClip;
				if (flag10)
				{
					bool flag11 = num10 > x;
					if (flag11)
					{
						num8 = 0f - num3;
					}
					else
					{
						bool flag12 = num10 + num11 < x + w;
						if (flag12)
						{
							num8 = (float)(-(float)(num10 + num11 - x - w));
						}
					}
				}
				break;
			}
			case 3:
				num9 = -1;
				num15 += num2;
				num7 = -1f;
				num14 += num;
				break;
			}
			int num16 = 0;
			int num17 = 0;
			bool flag13 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag13)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2((float)x, (float)y);
				this.UpdatePos(3);
				if (transform != 5)
				{
					if (transform == 6)
					{
						this.UpdatePos(3);
					}
				}
				else
				{
					this.size = new Vector2((float)w, (float)h);
					this.UpdatePos(3);
				}
				switch (transform)
				{
				case 4:
				{
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num14 += num;
					num7 = -1f;
					bool flag14 = this.isClip;
					if (flag14)
					{
						bool flag15 = num10 > x;
						if (flag15)
						{
							num8 = 0f - num3;
						}
						else
						{
							bool flag16 = num10 + num11 < x + w;
							if (flag16)
							{
								num8 = (float)(-(float)(num10 + num11 - x - w));
							}
						}
					}
					break;
				}
				case 5:
					GUIUtility.RotateAroundPivot(90f, this.pivot);
					break;
				case 6:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					break;
				case 7:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num9 = -1;
					num15 += num2;
					break;
				}
			}
			Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			bool flag17 = transform == 5 || transform == 6 || transform == 4 || transform == 7;
			if (flag17)
			{
				GUI.matrix = this.matrixBackup;
			}
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0005CCC8 File Offset: 0x0005AEC8
	public void drawRegionGui(Image image, float x0, float y0, int w, int h, int transform, float x, float y, int anchor)
	{
		GUI.color = this.setColorMiniMap(807956);
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= (float)mGraphics.zoomLevel;
		y0 *= (float)mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0005CD28 File Offset: 0x0005AF28
	public void drawRegion2(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		GUI.color = image.colorBlend;
		bool flag = this.isTranslate;
		if (flag)
		{
			x += this.translateX;
			y += this.translateY;
		}
		string key = string.Concat(new object[]
		{
			"dg",
			x0,
			y0,
			w,
			h,
			transform,
			image.GetHashCode()
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
		bool flag2 = texture2D == null;
		if (flag2)
		{
			Image image2 = Image.createImage(image, (int)x0, (int)y0, w, h, transform);
			texture2D = image2.texture;
			this.cache(key, texture2D);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)w;
		float num6 = (float)h;
		float num7 = 0f;
		float num8 = 0f;
		bool flag3 = (anchor & mGraphics.HCENTER) == mGraphics.HCENTER;
		if (flag3)
		{
			num7 -= num5 / 2f;
		}
		bool flag4 = (anchor & mGraphics.VCENTER) == mGraphics.VCENTER;
		if (flag4)
		{
			num8 -= num6 / 2f;
		}
		bool flag5 = (anchor & mGraphics.RIGHT) == mGraphics.RIGHT;
		if (flag5)
		{
			num7 -= num5;
		}
		bool flag6 = (anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM;
		if (flag6)
		{
			num8 -= num6;
		}
		x += (int)num7;
		y += (int)num8;
		bool flag7 = this.isClip;
		if (flag7)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			bool flag8 = this.isTranslate;
			if (flag8)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		bool flag9 = this.isClip;
		if (flag9)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.DrawTexture(new Rect((float)(x - num), (float)(y - num2), (float)w, (float)h), texture2D);
		bool flag10 = this.isClip;
		if (flag10)
		{
			GUI.EndGroup();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0005CF70 File Offset: 0x0005B170
	public void drawImagaByDrawTexture(Image image, float x, float y)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		GUI.DrawTexture(new Rect(x + (float)this.translateX, y + (float)this.translateY, (float)image.getRealImageWidth(), (float)image.getRealImageHeight()), image.texture);
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0005CFC4 File Offset: 0x0005B1C4
	public void drawImage(Image image, int x, int y, int anchor)
	{
		bool flag = image != null;
		if (flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0005CFF8 File Offset: 0x0005B1F8
	public void drawImageFog(Image image, int x, int y, int anchor)
	{
		bool flag = image != null;
		if (flag)
		{
			this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0005D038 File Offset: 0x0005B238
	public void drawImage(Image image, int x, int y)
	{
		bool flag = image != null;
		if (flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0005D074 File Offset: 0x0005B274
	public void drawImage(Image image, float x, float y, int anchor)
	{
		bool flag = image != null;
		if (flag)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x00004E95 File Offset: 0x00003095
	public void drawRoundRect(int x, int y, int w, int h, int arcWidth, int arcHeight)
	{
		this.drawRect(x, y, w, h);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00004EA4 File Offset: 0x000030A4
	public void fillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
	{
		this.fillRect(x, y, width, height);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00004EB3 File Offset: 0x000030B3
	public void reset()
	{
		this.isClip = false;
		this.isTranslate = false;
		this.translateX = 0;
		this.translateY = 0;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0005D0A8 File Offset: 0x0005B2A8
	public Rect intersectRect(Rect r1, Rect r2)
	{
		float num = r1.x;
		float num2 = r1.y;
		float x = r2.x;
		float y = r2.y;
		float num3 = num;
		num3 += r1.width;
		float num4 = num2;
		num4 += r1.height;
		float num5 = x;
		num5 += r2.width;
		float num6 = y;
		num6 += r2.height;
		bool flag = num < x;
		if (flag)
		{
			num = x;
		}
		bool flag2 = num2 < y;
		if (flag2)
		{
			num2 = y;
		}
		bool flag3 = num3 > num5;
		if (flag3)
		{
			num3 = num5;
		}
		bool flag4 = num4 > num6;
		if (flag4)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		bool flag5 = num3 < -30000f;
		if (flag5)
		{
			num3 = -30000f;
		}
		bool flag6 = num4 < -30000f;
		if (flag6)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0005D1A4 File Offset: 0x0005B3A4
	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		GUI.color = Color.red;
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		bool flag = image != null;
		if (flag)
		{
			Graphics.DrawTexture(new Rect((float)(x + this.translateX), (float)(y + this.translateY), (float)((tranform != 0) ? (-(float)w) : w), (float)h), image.texture);
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0005D220 File Offset: 0x0005B420
	public void drawImageSimple(Image image, int x, int y)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		bool flag = image != null;
		if (flag)
		{
			Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), image.texture);
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00051150 File Offset: 0x0004F350
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00051168 File Offset: 0x0004F368
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0005D270 File Offset: 0x0005B470
	public static bool isNotTranColor(Color color)
	{
		bool flag = color == Color.clear || color == mGraphics.transParentColor;
		return !flag;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
	public static Image blend(Image img0, float level, int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		Color color = new Color(num6, num5, num4);
		Color[] pixels = img0.texture.GetPixels();
		float num7 = color.r;
		float num8 = color.g;
		float num9 = color.b;
		for (int i = 0; i < pixels.Length; i++)
		{
			Color color2 = pixels[i];
			bool flag = mGraphics.isNotTranColor(color2);
			if (flag)
			{
				float num10 = (num7 - color2.r) * level + color2.r;
				float num11 = (num8 - color2.g) * level + color2.g;
				float num12 = (num9 - color2.b) * level + color2.b;
				bool flag2 = num10 > 255f;
				if (flag2)
				{
					num10 = 255f;
				}
				bool flag3 = num10 < 0f;
				if (flag3)
				{
					num10 = 0f;
				}
				bool flag4 = num11 > 255f;
				if (flag4)
				{
					num11 = 255f;
				}
				bool flag5 = num11 < 0f;
				if (flag5)
				{
					num11 = 0f;
				}
				bool flag6 = num12 < 0f;
				if (flag6)
				{
					num12 = 0f;
				}
				bool flag7 = num12 > 255f;
				if (flag7)
				{
					num12 = 255f;
				}
				pixels[i].r = num10;
				pixels[i].g = num11;
				pixels[i].b = num12;
			}
		}
		Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
		image.texture.SetPixels(pixels);
		Image.setTextureQuality(image.texture);
		image.texture.Apply();
		Cout.LogError2("BLEND ----------------------------------------------------");
		return image;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x00050A00 File Offset: 0x0004EC00
	public static Color setColorObj(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		float num6 = (float)num3 / 256f;
		return new Color(num6, num5, num4);
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00004ED2 File Offset: 0x000030D2
	public void fillTrans(Image imgTrans, int x, int y, int w, int h)
	{
		this.setColor(0, 0.5f);
		this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0005D4B4 File Offset: 0x0005B6B4
	public static int blendColor(float level, int color, int colorBlend)
	{
		Color color2 = mGraphics.setColorObj(colorBlend);
		float num = color2.r * 255f;
		float num2 = color2.g * 255f;
		float num3 = color2.b * 255f;
		Color color3 = mGraphics.setColorObj(color);
		float num4 = (num + color3.r) * level + color3.r;
		float num5 = (num2 + color3.g) * level + color3.g;
		float num6 = (num3 + color3.b) * level + color3.b;
		bool flag = num4 > 255f;
		if (flag)
		{
			num4 = 255f;
		}
		bool flag2 = num4 < 0f;
		if (flag2)
		{
			num4 = 0f;
		}
		bool flag3 = num5 > 255f;
		if (flag3)
		{
			num5 = 255f;
		}
		bool flag4 = num5 < 0f;
		if (flag4)
		{
			num5 = 0f;
		}
		bool flag5 = num6 < 0f;
		if (flag5)
		{
			num6 = 0f;
		}
		bool flag6 = num6 > 255f;
		if (flag6)
		{
			num6 = 255f;
		}
		return (int)num6 & 255 + ((int)num5 << 8) & 255 + ((int)num4 << 16) & 255;
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0005D5F0 File Offset: 0x0005B7F0
	public static int getIntByColor(Color cl)
	{
		float num = cl.r * 255f;
		float num2 = cl.b * 255f;
		float num3 = cl.g * 255f;
		return ((int)num & 255) << 16 | ((int)num3 & 255) << 8 | ((int)num2 & 255);
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000511E0 File Offset: 0x0004F3E0
	public static int getRealImageWidth(Image img)
	{
		return img.w;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000511F8 File Offset: 0x0004F3F8
	public static int getRealImageHeight(Image img)
	{
		return img.h;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00004F07 File Offset: 0x00003107
	public void fillArg(int i, int j, int k, int l, int m, int n)
	{
		this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0005D648 File Offset: 0x0005B848
	public void CreateLineMaterial()
	{
		bool flag = !this.lineMaterial;
		if (flag)
		{
			this.lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {  Blend SrcAlpha OneMinusSrcAlpha  ZWrite Off Cull Off Fog { Mode Off }  BindChannels { Bind \"vertex\", vertex Bind \"color\", color }} } }");
			this.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0005D69C File Offset: 0x0005B89C
	public void drawlineGL(MyVector totalLine)
	{
		this.lineMaterial.SetPass(0);
		GL.PushMatrix();
		GL.Begin(1);
		for (int i = 0; i < totalLine.size(); i++)
		{
			mLine mLine = (mLine)totalLine.elementAt(i);
			GL.Color(new Color(mLine.r, mLine.g, mLine.b, mLine.a));
			int num = mLine.x1 * mGraphics.zoomLevel;
			int num2 = mLine.y1 * mGraphics.zoomLevel;
			int num3 = mLine.x2 * mGraphics.zoomLevel;
			int num4 = mLine.y2 * mGraphics.zoomLevel;
			bool flag = this.isTranslate;
			if (flag)
			{
				num += this.translateX;
				num2 += this.translateY;
				num3 += this.translateX;
				num4 += this.translateY;
			}
			for (int j = 0; j < mGraphics.zoomLevel; j++)
			{
				GL.Vertex(new Vector2((float)(num + j), (float)(num2 + j)));
				GL.Vertex(new Vector2((float)(num3 + j), (float)(num4 + j)));
				bool flag2 = j > 0;
				if (flag2)
				{
					GL.Vertex(new Vector2((float)(num + j), (float)num2));
					GL.Vertex(new Vector2((float)(num3 + j), (float)num4));
					GL.Vertex(new Vector2((float)num, (float)(num2 + j)));
					GL.Vertex(new Vector2((float)num3, (float)(num4 + j)));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		totalLine.removeAllElements();
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0005D858 File Offset: 0x0005BA58
	public void drawLine(mGraphics g, int x, int y, int xTo, int yTo, int nLine, int color)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < nLine; i++)
		{
			myVector.addElement(new mLine(x, y, xTo + i, yTo + i, color));
		}
		g.drawlineGL(myVector);
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00003F1F File Offset: 0x0000211F
	internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04000A8C RID: 2700
	public static int HCENTER = 1;

	// Token: 0x04000A8D RID: 2701
	public static int VCENTER = 2;

	// Token: 0x04000A8E RID: 2702
	public static int LEFT = 4;

	// Token: 0x04000A8F RID: 2703
	public static int RIGHT = 8;

	// Token: 0x04000A90 RID: 2704
	public static int TOP = 16;

	// Token: 0x04000A91 RID: 2705
	public static int BOTTOM = 32;

	// Token: 0x04000A92 RID: 2706
	private float r;

	// Token: 0x04000A93 RID: 2707
	private float g;

	// Token: 0x04000A94 RID: 2708
	private float b;

	// Token: 0x04000A95 RID: 2709
	private float a;

	// Token: 0x04000A96 RID: 2710
	public int clipX;

	// Token: 0x04000A97 RID: 2711
	public int clipY;

	// Token: 0x04000A98 RID: 2712
	public int clipW;

	// Token: 0x04000A99 RID: 2713
	public int clipH;

	// Token: 0x04000A9A RID: 2714
	private bool isClip;

	// Token: 0x04000A9B RID: 2715
	private bool isTranslate = true;

	// Token: 0x04000A9C RID: 2716
	private int translateX;

	// Token: 0x04000A9D RID: 2717
	private int translateY;

	// Token: 0x04000A9E RID: 2718
	private float translateXf;

	// Token: 0x04000A9F RID: 2719
	private float translateYf;

	// Token: 0x04000AA0 RID: 2720
	public static int zoomLevel = 1;

	// Token: 0x04000AA1 RID: 2721
	public const int BASELINE = 64;

	// Token: 0x04000AA2 RID: 2722
	public const int SOLID = 0;

	// Token: 0x04000AA3 RID: 2723
	public const int DOTTED = 1;

	// Token: 0x04000AA4 RID: 2724
	public const int TRANS_MIRROR = 2;

	// Token: 0x04000AA5 RID: 2725
	public const int TRANS_MIRROR_ROT180 = 1;

	// Token: 0x04000AA6 RID: 2726
	public const int TRANS_MIRROR_ROT270 = 4;

	// Token: 0x04000AA7 RID: 2727
	public const int TRANS_MIRROR_ROT90 = 7;

	// Token: 0x04000AA8 RID: 2728
	public const int TRANS_NONE = 0;

	// Token: 0x04000AA9 RID: 2729
	public const int TRANS_ROT180 = 3;

	// Token: 0x04000AAA RID: 2730
	public const int TRANS_ROT270 = 6;

	// Token: 0x04000AAB RID: 2731
	public const int TRANS_ROT90 = 5;

	// Token: 0x04000AAC RID: 2732
	public static Hashtable cachedTextures = new Hashtable();

	// Token: 0x04000AAD RID: 2733
	public static int addYWhenOpenKeyBoard;

	// Token: 0x04000AAE RID: 2734
	private int clipTX;

	// Token: 0x04000AAF RID: 2735
	private int clipTY;

	// Token: 0x04000AB0 RID: 2736
	private int currentBGColor;

	// Token: 0x04000AB1 RID: 2737
	private Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x04000AB2 RID: 2738
	private Rect rect;

	// Token: 0x04000AB3 RID: 2739
	private Matrix4x4 matrixBackup;

	// Token: 0x04000AB4 RID: 2740
	private Vector2 pivot;

	// Token: 0x04000AB5 RID: 2741
	public Vector2 size = new Vector2(128f, 128f);

	// Token: 0x04000AB6 RID: 2742
	public Vector2 relativePosition = new Vector2(0f, 0f);

	// Token: 0x04000AB7 RID: 2743
	public Color clTrans;

	// Token: 0x04000AB8 RID: 2744
	public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000AB9 RID: 2745
	private Material lineMaterial;
}

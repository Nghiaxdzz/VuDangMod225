using System;
using System.Threading;

// Token: 0x020000BB RID: 187
public class TField : IActionListener
{
	// Token: 0x0600097A RID: 2426 RVA: 0x0009A160 File Offset: 0x00098360
	public TField(mScreen parentScr)
	{
		this.text = string.Empty;
		this.parentScr = parentScr;
		this.init();
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0009A204 File Offset: 0x00098404
	public TField()
	{
		this.text = string.Empty;
		this.init();
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0009A2A0 File Offset: 0x000984A0
	public TField(int x, int y, int w, int h)
	{
		this.text = string.Empty;
		this.init();
		this.x = x;
		this.y = y;
		this.width = w;
		this.height = h;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0009A358 File Offset: 0x00098558
	public TField(string text, int maxLen, int inputType)
	{
		this.text = text;
		this.maxTextLenght = maxLen;
		this.inputType = inputType;
		this.init();
		this.isTfield = true;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0009A404 File Offset: 0x00098604
	public static bool setNormal(char ch)
	{
		bool flag = (ch < '0' || ch > '9') && (ch < 'A' || ch > 'Z') && (ch < 'a' || ch > 'z');
		return !flag;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void doChangeToTextBox()
	{
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0009A444 File Offset: 0x00098644
	public static void setVendorTypeMode(int mode)
	{
		bool flag = mode == TField.MOTO;
		if (flag)
		{
			TField.print[0] = "0";
			TField.print[10] = " *";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
		}
		else
		{
			bool flag2 = mode == TField.NOKIA;
			if (flag2)
			{
				TField.print[0] = " 0";
				TField.print[10] = "*";
				TField.print[11] = "#";
				TField.changeModeKey = 35;
			}
			else
			{
				bool flag3 = mode == TField.ORTHER;
				if (flag3)
				{
					TField.print[0] = "0";
					TField.print[10] = "*";
					TField.print[11] = " #";
					TField.changeModeKey = 42;
				}
			}
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0009A508 File Offset: 0x00098708
	public void init()
	{
		TField.CARET_HEIGHT = mScreen.ITEM_HEIGHT + 1;
		this.cmdClear = new Command(mResources.DELETE, this, 1000, null);
		bool isPC = Main.isPC;
		if (isPC)
		{
			TField.typeXpeed = 0;
		}
		bool flag = TField.imgTf == null;
		if (flag)
		{
			TField.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0009A568 File Offset: 0x00098768
	public void clearKeyWhenPutText(int keyCode)
	{
		bool flag = keyCode == -8 && this.timeDelayKyCode <= 0;
		if (flag)
		{
			bool flag2 = this.timeDelayKyCode <= 0;
			if (flag2)
			{
				this.timeDelayKyCode = 1;
			}
			this.clear();
		}
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0009A5B0 File Offset: 0x000987B0
	public void clearAllText()
	{
		this.text = string.Empty;
		bool flag = TField.kb != null;
		if (flag)
		{
			TField.kb.text = string.Empty;
		}
		this.caretPos = 0;
		this.setOffset(0);
		this.setPasswordTest();
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0009A5FC File Offset: 0x000987FC
	public void clear()
	{
		bool flag = this.caretPos > 0 && this.text.Length > 0;
		if (flag)
		{
			this.text = this.text.Substring(0, this.caretPos - 1);
			this.caretPos--;
			this.setOffset(0);
			this.setPasswordTest();
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0009A680 File Offset: 0x00098880
	public void clearAll()
	{
		bool flag = this.caretPos > 0 && this.text.Length > 0;
		if (flag)
		{
			this.text = this.text.Substring(0, this.text.Length - 1);
			this.caretPos--;
			this.setOffset();
			this.setPasswordTest();
			this.setFocusWithKb(true);
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = string.Empty;
			}
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0009A710 File Offset: 0x00098910
	public void setOffset()
	{
		bool flag = this.paintedText != null && mFont.tahoma_8b != null;
		if (flag)
		{
			bool flag2 = this.inputType == TField.INPUT_TYPE_PASSWORD;
			if (flag2)
			{
				this.paintedText = this.passwordText;
			}
			else
			{
				this.paintedText = this.text;
			}
			bool flag3 = this.offsetX < 0 && mFont.tahoma_8b.getWidth(this.paintedText) + this.offsetX < this.width - TField.TEXT_GAP_X - 13 - TField.typingModeAreaWidth;
			if (flag3)
			{
				this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText);
			}
			bool flag4 = this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) <= 0;
			if (flag4)
			{
				this.offsetX = -mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
				this.offsetX += 40;
			}
			else
			{
				bool flag5 = this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) >= this.width - 12 - TField.typingModeAreaWidth;
				if (flag5)
				{
					this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) - 2 * TField.TEXT_GAP_X;
				}
			}
			bool flag6 = this.offsetX > 0;
			if (flag6)
			{
				this.offsetX = 0;
			}
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0009A8C4 File Offset: 0x00098AC4
	private void keyPressedAny(int keyCode)
	{
		string[] array = (this.inputType != TField.INPUT_TYPE_PASSWORD && this.inputType != TField.INPUT_ALPHA_NUMBER_ONLY) ? TField.print : TField.printA;
		bool flag = keyCode == TField.lastKey;
		if (flag)
		{
			this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
			char c = array[keyCode - 48][this.indexOfActiveChar];
			object arg = (TField.mode == 0) ? char.ToLower(c) : ((TField.mode == 1) ? char.ToUpper(c) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c)));
			string str = this.text.Substring(0, this.caretPos - 1) + arg;
			bool flag2 = this.caretPos < this.text.Length;
			if (flag2)
			{
				str += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = str;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.setPasswordTest();
		}
		else
		{
			bool flag3 = this.text.Length < this.maxTextLenght;
			if (flag3)
			{
				bool flag4 = TField.mode == 1 && TField.lastKey != -1984;
				if (flag4)
				{
					TField.mode = 0;
				}
				this.indexOfActiveChar = 0;
				char c2 = array[keyCode - 48][this.indexOfActiveChar];
				object arg = (TField.mode == 0) ? char.ToLower(c2) : ((TField.mode == 1) ? char.ToUpper(c2) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c2)));
				string str2 = this.text.Substring(0, this.caretPos) + arg;
				bool flag5 = this.caretPos < this.text.Length;
				if (flag5)
				{
					str2 += this.text.Substring(this.caretPos, this.text.Length);
				}
				this.text = str2;
				this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset();
			}
		}
		TField.lastKey = keyCode;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0009AB4C File Offset: 0x00098D4C
	private void keyPressedAscii(int keyCode)
	{
		bool flag = (this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122);
		if (!flag)
		{
			bool flag2 = this.text.Length < this.maxTextLenght;
			if (flag2)
			{
				string str = this.text.Substring(0, this.caretPos) + ((char)keyCode).ToString();
				bool flag3 = this.caretPos < this.text.Length;
				if (flag3)
				{
					str += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
				}
				this.text = str;
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset(0);
			}
			bool flag4 = TField.kb != null;
			if (flag4)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0009AC6C File Offset: 0x00098E6C
	public static void setMode()
	{
		TField.mode++;
		bool flag = TField.mode > 3;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = TField.changeModeKey;
		TField.timeChangeMode = (long)(Environment.TickCount / 1000);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0009ACB8 File Offset: 0x00098EB8
	private void setDau()
	{
		this.timeDau = (long)(Environment.TickCount / 100);
		bool flag = this.indexDau == -1;
		if (flag)
		{
			for (int i = this.caretPos; i > 0; i--)
			{
				char c = this.text[i - 1];
				for (int j = 0; j < TField.printDau.Length; j++)
				{
					char c2 = TField.printDau[j];
					bool flag2 = c == c2;
					if (flag2)
					{
						this.indexTemplate = j;
						this.indexCong = 0;
						this.indexDau = i - 1;
						return;
					}
				}
			}
			this.indexDau = -1;
		}
		else
		{
			this.indexCong++;
			bool flag3 = this.indexCong >= 6;
			if (flag3)
			{
				this.indexCong = 0;
			}
			string str = this.text.Substring(0, this.indexDau);
			string str2 = this.text.Substring(this.indexDau + 1);
			string str3 = TField.printDau.Substring(this.indexTemplate + this.indexCong, 1);
			this.text = str + str3 + str2;
		}
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0009ADEC File Offset: 0x00098FEC
	public bool keyPressed(int keyCode)
	{
		bool flag = Main.isPC && keyCode == -8;
		bool result;
		if (flag)
		{
			this.clearKeyWhenPutText(-8);
			result = true;
		}
		else
		{
			bool flag2 = keyCode == 8 || keyCode == -8 || keyCode == 204;
			if (flag2)
			{
				this.clear();
				result = true;
			}
			else
			{
				bool flag3 = TField.isQwerty && keyCode >= 32;
				if (flag3)
				{
					this.keyPressedAscii(keyCode);
					result = false;
				}
				else
				{
					bool flag4 = keyCode == TField.changeDau && this.inputType == TField.INPUT_TYPE_ANY;
					if (flag4)
					{
						this.setDau();
						result = false;
					}
					else
					{
						bool flag5 = keyCode == 42;
						if (flag5)
						{
							keyCode = 58;
						}
						bool flag6 = keyCode == 35;
						if (flag6)
						{
							keyCode = 59;
						}
						bool flag7 = keyCode >= 48 && keyCode <= 59;
						if (flag7)
						{
							bool flag8 = this.inputType == TField.INPUT_TYPE_ANY || this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY;
							if (flag8)
							{
								this.keyPressedAny(keyCode);
							}
							else
							{
								bool flag9 = this.inputType == TField.INPUT_TYPE_NUMERIC;
								if (flag9)
								{
									this.keyPressedAscii(keyCode);
									this.keyInActiveState = 1;
								}
							}
						}
						else
						{
							this.indexOfActiveChar = 0;
							TField.lastKey = -1984;
							bool flag10 = keyCode == 14 && !this.lockArrow;
							if (flag10)
							{
								bool flag11 = this.caretPos > 0;
								if (flag11)
								{
									this.caretPos--;
									this.setOffset(0);
									this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
									return false;
								}
							}
							else
							{
								bool flag12 = keyCode == 15 && !this.lockArrow;
								if (flag12)
								{
									bool flag13 = this.caretPos < this.text.Length;
									if (flag13)
									{
										this.caretPos++;
										this.setOffset(0);
										this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
										return false;
									}
								}
								else
								{
									bool flag14 = keyCode == 19;
									if (flag14)
									{
										this.clear();
										return false;
									}
									TField.lastKey = keyCode;
								}
							}
						}
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0009B01C File Offset: 0x0009921C
	public void setOffset(int index)
	{
		bool flag = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		int num = mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
		if (index != -1)
		{
			if (index != 1)
			{
				this.offsetX = -(num - (this.width - 12));
			}
			else
			{
				bool flag2 = num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0;
				if (flag2)
				{
					this.offsetX -= mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
				}
			}
		}
		else
		{
			bool flag3 = num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length;
			if (flag3)
			{
				this.offsetX += mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
		}
		bool flag4 = this.offsetX > 0;
		if (flag4)
		{
			this.offsetX = 0;
		}
		else
		{
			bool flag5 = this.offsetX < 0;
			if (flag5)
			{
				int num2 = mFont.tahoma_8b.getWidth(this.paintedText) - (this.width - 12);
				bool flag6 = this.offsetX < -num2;
				if (flag6)
				{
					this.offsetX = -num2;
				}
			}
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0009B1C4 File Offset: 0x000993C4
	public void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text, string info)
	{
		g.setColor(0);
		if (iss)
		{
			g.drawRegion(TField.imgTf, 0, 81, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 135, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + w - 58, y, 0);
			for (int i = 0; i < (w - 58) / 29; i++)
			{
				g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + 29 + i * 29, y, 0);
			}
		}
		else
		{
			g.drawRegion(TField.imgTf, 0, 0, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 54, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + w - 58, y, 0);
			for (int j = 0; j < (w - 58) / 29; j++)
			{
				g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + 29 + j * 29, y, 0);
			}
		}
		g.setClip(x + 3, y + 1, w - 4, h);
		bool flag = text != null && !text.Equals(string.Empty);
		if (flag)
		{
			mFont.tahoma_8b.drawString(g, text, xText, yText, 0);
		}
		else
		{
			bool flag2 = info != null;
			if (flag2)
			{
				if (iss)
				{
					mFont.tahoma_7b_focus.drawString(g, info, xText, yText, 0);
				}
				else
				{
					mFont.tahoma_7b_unfocus.drawString(g, info, xText, yText, 0);
				}
			}
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0009B390 File Offset: 0x00099590
	public void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag = this.isFocused();
		bool flag2 = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag2)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		this.paintInputTf(g, flag, this.x, this.y - 1, this.width, this.height + 5, TField.TEXT_GAP_X + this.offsetX + this.x + 1, this.y + (this.height - mFont.tahoma_8b.getHeight()) / 2 + 2, this.paintedText, this.name);
		g.setClip(this.x + 3, this.y + 1, this.width - 4, this.height - 2);
		g.setColor(0);
		bool flag3 = flag && this.isPaintMouse && this.isPaintCarret;
		if (flag3)
		{
			bool flag4 = this.keyInActiveState == 0 && (this.showCaretCounter > 0 || this.counter / TField.CARET_SHOWING_TIME % 4 == 0);
			if (flag4)
			{
				g.setColor(7999781);
				g.fillRect(TField.TEXT_GAP_X + 1 + this.offsetX + this.x + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos) + "a") - TField.CARET_WIDTH - mFont.tahoma_8b.getWidth("a"), this.y + (this.height - TField.CARET_HEIGHT) / 2 + 5, TField.CARET_WIDTH, TField.CARET_HEIGHT);
			}
			GameCanvas.resetTrans(g);
			bool flag5 = this.text != null && this.text.Length > 0 && GameCanvas.isTouch;
			if (flag5)
			{
				g.drawImage(GameCanvas.imgClear, this.x + this.width - 13, this.y + this.height / 2 + 3, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0009B5B4 File Offset: 0x000997B4
	private bool isFocused()
	{
		return this.isFocus;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0009B5CC File Offset: 0x000997CC
	public string subString(string str, int index, int indexTo)
	{
		bool flag = index >= 0 && indexTo > str.Length - 1;
		string result;
		if (flag)
		{
			result = str.Substring(index);
		}
		else
		{
			bool flag2 = index < 0 || index > str.Length - 1 || indexTo < 0 || indexTo > str.Length - 1;
			if (flag2)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Empty;
				for (int i = index; i < indexTo; i++)
				{
					text += str[i].ToString();
				}
				result = text;
			}
		}
		return result;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0009B664 File Offset: 0x00099864
	private void setPasswordTest()
	{
		bool flag = this.inputType == TField.INPUT_TYPE_PASSWORD;
		if (flag)
		{
			this.passwordText = string.Empty;
			for (int i = 0; i < this.text.Length; i++)
			{
				this.passwordText += "*";
			}
			bool flag2 = this.keyInActiveState > 0 && this.caretPos > 0;
			if (flag2)
			{
				this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1].ToString() + this.passwordText.Substring(this.caretPos, this.passwordText.Length);
			}
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0009B738 File Offset: 0x00099938
	public void update()
	{
		this.isPaintCarret = true;
		bool isPC = Main.isPC;
		if (isPC)
		{
			bool flag = this.timeDelayKyCode > 0;
			if (flag)
			{
				this.timeDelayKyCode--;
			}
			bool flag2 = this.timeDelayKyCode <= 0;
			if (flag2)
			{
				this.timeDelayKyCode = 0;
			}
		}
		bool flag3 = TField.kb != null && TField.currentTField == this;
		if (flag3)
		{
			bool flag4 = TField.kb.text.Length < 40 && this.isFocus;
			if (flag4)
			{
				this.setText(TField.kb.text);
			}
			bool flag5 = TField.kb.done && this.cmdDoneAction != null;
			if (flag5)
			{
				this.cmdDoneAction.performAction();
			}
		}
		this.counter++;
		bool flag6 = this.keyInActiveState > 0;
		if (flag6)
		{
			this.keyInActiveState--;
			bool flag7 = this.keyInActiveState == 0;
			if (flag7)
			{
				this.indexOfActiveChar = 0;
				bool flag8 = TField.mode == 1 && TField.lastKey != TField.changeModeKey && this.isFocus;
				if (flag8)
				{
					TField.mode = 0;
				}
				TField.lastKey = -1984;
				this.setPasswordTest();
			}
		}
		bool flag9 = this.showCaretCounter > 0;
		if (flag9)
		{
			this.showCaretCounter--;
		}
		bool isPointerJustRelease = GameCanvas.isPointerJustRelease;
		if (isPointerJustRelease)
		{
			this.setTextBox();
		}
		bool flag10 = this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L;
		if (flag10)
		{
			this.indexDau = -1;
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0009B8EC File Offset: 0x00099AEC
	public void setTextBox()
	{
		bool flag = GameCanvas.isPointerHoldIn(this.x + this.width - 20, this.y, 40, this.height);
		if (flag)
		{
			this.clearAllText();
			this.isFocus = true;
		}
		else
		{
			bool flag2 = GameCanvas.isPointerHoldIn(this.x, this.y, this.width - 20, this.height);
			if (flag2)
			{
				this.setFocusWithKb(true);
			}
			else
			{
				this.setFocus(false);
			}
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0009B970 File Offset: 0x00099B70
	public void setFocus(bool isFocus)
	{
		bool flag = this.isFocus != isFocus;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
			bool flag2 = TField.kb != null;
			if (flag2)
			{
				TField.kb.text = TField.currentTField.text;
			}
		}
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0009B9F4 File Offset: 0x00099BF4
	public void setFocusWithKb(bool isFocus)
	{
		bool flag = this.isFocus != isFocus;
		if (flag)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		bool flag2 = isFocus;
		if (flag2)
		{
			TField.currentTField = this;
		}
		else
		{
			bool flag3 = TField.currentTField == this;
			if (flag3)
			{
				TField.currentTField = null;
			}
		}
		bool flag4 = Thread.CurrentThread.Name == Main.mainThreadName && TField.currentTField != null;
		if (flag4)
		{
			isFocus = true;
			TouchScreenKeyboard.hideInput = !TField.currentTField.showSubTextField;
			TouchScreenKeyboardType t = TouchScreenKeyboardType.ASCIICapable;
			bool flag5 = this.inputType == TField.INPUT_TYPE_NUMERIC;
			if (flag5)
			{
				t = TouchScreenKeyboardType.NumberPad;
			}
			bool type = false;
			bool flag6 = this.inputType == TField.INPUT_TYPE_PASSWORD;
			if (flag6)
			{
				type = true;
			}
			TField.kb = TouchScreenKeyboard.Open(TField.currentTField.text, t, false, false, type, false, TField.currentTField.name);
			bool flag7 = TField.kb != null;
			if (flag7)
			{
				TField.kb.text = TField.currentTField.text;
			}
			Cout.LogWarning("SHOW KEYBOARD FOR " + TField.currentTField.text);
		}
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0009BB44 File Offset: 0x00099D44
	public string getText()
	{
		return this.text;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0009BB5C File Offset: 0x00099D5C
	public void clearKb()
	{
		bool flag = TField.kb != null;
		if (flag)
		{
			TField.kb.text = string.Empty;
		}
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0009BB88 File Offset: 0x00099D88
	public void setText(string text)
	{
		bool flag = text != null;
		if (flag)
		{
			TField.lastKey = -1984;
			this.keyInActiveState = 0;
			this.indexOfActiveChar = 0;
			this.text = text;
			this.paintedText = text;
			bool flag2 = text == string.Empty;
			if (flag2)
			{
				TouchScreenKeyboard.Clear();
			}
			this.setPasswordTest();
			this.caretPos = text.Length;
			this.setOffset();
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0009BBF8 File Offset: 0x00099DF8
	public void insertText(string text)
	{
		this.text = this.text.Substring(0, this.caretPos) + text + this.text.Substring(this.caretPos);
		this.setPasswordTest();
		this.caretPos += text.Length;
		this.setOffset();
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0009BC58 File Offset: 0x00099E58
	public int getMaxTextLenght()
	{
		return this.maxTextLenght;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00006410 File Offset: 0x00004610
	public void setMaxTextLenght(int maxTextLenght)
	{
		this.maxTextLenght = maxTextLenght;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0009BC70 File Offset: 0x00099E70
	public int getIputType()
	{
		return this.inputType;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0000641A File Offset: 0x0000461A
	public void setIputType(int iputType)
	{
		this.inputType = iputType;
		this.setMaxTextLenght(500);
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0009BC88 File Offset: 0x00099E88
	public void perform(int idAction, object p)
	{
		bool flag = idAction == 1000;
		if (flag)
		{
			this.clear();
		}
	}

	// Token: 0x040011A2 RID: 4514
	public bool isFocus;

	// Token: 0x040011A3 RID: 4515
	public int x;

	// Token: 0x040011A4 RID: 4516
	public int y;

	// Token: 0x040011A5 RID: 4517
	public int width;

	// Token: 0x040011A6 RID: 4518
	public int height;

	// Token: 0x040011A7 RID: 4519
	public bool lockArrow;

	// Token: 0x040011A8 RID: 4520
	public bool justReturnFromTextBox;

	// Token: 0x040011A9 RID: 4521
	public bool paintFocus = true;

	// Token: 0x040011AA RID: 4522
	public const sbyte KEY_LEFT = 14;

	// Token: 0x040011AB RID: 4523
	public const sbyte KEY_RIGHT = 15;

	// Token: 0x040011AC RID: 4524
	public const sbyte KEY_CLEAR = 19;

	// Token: 0x040011AD RID: 4525
	public static int typeXpeed = 2;

	// Token: 0x040011AE RID: 4526
	private static readonly int[] MAX_TIME_TO_CONFIRM_KEY = new int[]
	{
		30,
		14,
		11,
		9,
		6,
		4,
		2
	};

	// Token: 0x040011AF RID: 4527
	private static int CARET_HEIGHT = 0;

	// Token: 0x040011B0 RID: 4528
	private static readonly int CARET_WIDTH = 1;

	// Token: 0x040011B1 RID: 4529
	private static readonly int CARET_SHOWING_TIME = 5;

	// Token: 0x040011B2 RID: 4530
	private static readonly int TEXT_GAP_X = 4;

	// Token: 0x040011B3 RID: 4531
	private static readonly int MAX_SHOW_CARET_COUNER = 10;

	// Token: 0x040011B4 RID: 4532
	public static readonly int INPUT_TYPE_ANY = 0;

	// Token: 0x040011B5 RID: 4533
	public static readonly int INPUT_TYPE_NUMERIC = 1;

	// Token: 0x040011B6 RID: 4534
	public static readonly int INPUT_TYPE_PASSWORD = 2;

	// Token: 0x040011B7 RID: 4535
	public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

	// Token: 0x040011B8 RID: 4536
	private static string[] print = new string[]
	{
		" 0",
		".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1",
		"abc2áàảãạâấầẩẫậăắằẳẵặ2",
		"def3đéèẻẽẹêếềểễệ3",
		"ghi4íìỉĩị4",
		"jkl5",
		"mno6óòỏõọôốồổỗộơớờởỡợ6",
		"pqrs7",
		"tuv8úùủũụưứừửữự8",
		"wxyz9ýỳỷỹỵ9",
		"*",
		"#"
	};

	// Token: 0x040011B9 RID: 4537
	private static string[] printA = new string[]
	{
		"0",
		"1",
		"abc2",
		"def3",
		"ghi4",
		"jkl5",
		"mno6",
		"pqrs7",
		"tuv8",
		"wxyz9",
		"0",
		"0"
	};

	// Token: 0x040011BA RID: 4538
	private static string[] printBB = new string[]
	{
		" 0",
		"er1",
		"ty2",
		"ui3",
		"df4",
		"gh5",
		"jk6",
		"cv7",
		"bn8",
		"m9",
		"0",
		"0",
		"qw!",
		"as?",
		"zx",
		"op.",
		"l,"
	};

	// Token: 0x040011BB RID: 4539
	private string text = string.Empty;

	// Token: 0x040011BC RID: 4540
	private string passwordText = string.Empty;

	// Token: 0x040011BD RID: 4541
	private string paintedText = string.Empty;

	// Token: 0x040011BE RID: 4542
	private int caretPos;

	// Token: 0x040011BF RID: 4543
	private int counter;

	// Token: 0x040011C0 RID: 4544
	private int maxTextLenght = 500;

	// Token: 0x040011C1 RID: 4545
	private int offsetX;

	// Token: 0x040011C2 RID: 4546
	private static int lastKey = -1984;

	// Token: 0x040011C3 RID: 4547
	private int keyInActiveState;

	// Token: 0x040011C4 RID: 4548
	private int indexOfActiveChar;

	// Token: 0x040011C5 RID: 4549
	private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

	// Token: 0x040011C6 RID: 4550
	private int inputType = TField.INPUT_TYPE_ANY;

	// Token: 0x040011C7 RID: 4551
	public static bool isQwerty = true;

	// Token: 0x040011C8 RID: 4552
	public static int typingModeAreaWidth;

	// Token: 0x040011C9 RID: 4553
	public static int mode = 0;

	// Token: 0x040011CA RID: 4554
	public static long timeChangeMode;

	// Token: 0x040011CB RID: 4555
	public static readonly string[] modeNotify = new string[]
	{
		"abc",
		"Abc",
		"ABC",
		"123"
	};

	// Token: 0x040011CC RID: 4556
	public static readonly int NOKIA = 0;

	// Token: 0x040011CD RID: 4557
	public static readonly int MOTO = 1;

	// Token: 0x040011CE RID: 4558
	public static readonly int ORTHER = 2;

	// Token: 0x040011CF RID: 4559
	public static readonly int BB = 3;

	// Token: 0x040011D0 RID: 4560
	public static int changeModeKey = 11;

	// Token: 0x040011D1 RID: 4561
	public static readonly sbyte abc = 0;

	// Token: 0x040011D2 RID: 4562
	public static readonly sbyte Abc = 1;

	// Token: 0x040011D3 RID: 4563
	public static readonly sbyte ABC = 2;

	// Token: 0x040011D4 RID: 4564
	public static readonly sbyte number123 = 3;

	// Token: 0x040011D5 RID: 4565
	public static TField currentTField;

	// Token: 0x040011D6 RID: 4566
	public bool isTfield;

	// Token: 0x040011D7 RID: 4567
	public bool isPaintMouse = true;

	// Token: 0x040011D8 RID: 4568
	public string name = string.Empty;

	// Token: 0x040011D9 RID: 4569
	public string title = string.Empty;

	// Token: 0x040011DA RID: 4570
	public string strInfo;

	// Token: 0x040011DB RID: 4571
	public Command cmdClear;

	// Token: 0x040011DC RID: 4572
	public Command cmdDoneAction;

	// Token: 0x040011DD RID: 4573
	private mScreen parentScr;

	// Token: 0x040011DE RID: 4574
	private int timeDelayKyCode;

	// Token: 0x040011DF RID: 4575
	private int holdCount;

	// Token: 0x040011E0 RID: 4576
	public static int changeDau;

	// Token: 0x040011E1 RID: 4577
	private int indexDau = -1;

	// Token: 0x040011E2 RID: 4578
	private int indexTemplate;

	// Token: 0x040011E3 RID: 4579
	private int indexCong;

	// Token: 0x040011E4 RID: 4580
	private long timeDau;

	// Token: 0x040011E5 RID: 4581
	private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

	// Token: 0x040011E6 RID: 4582
	public static Image imgTf;

	// Token: 0x040011E7 RID: 4583
	public int timePutKeyClearAll;

	// Token: 0x040011E8 RID: 4584
	public int timeClearFirt;

	// Token: 0x040011E9 RID: 4585
	public bool isPaintCarret;

	// Token: 0x040011EA RID: 4586
	public bool showSubTextField = true;

	// Token: 0x040011EB RID: 4587
	public static TouchScreenKeyboard kb;

	// Token: 0x040011EC RID: 4588
	public static int[][] BBKEY = new int[][]
	{
		new int[]
		{
			32,
			48
		},
		new int[]
		{
			49,
			69
		},
		new int[]
		{
			50,
			84
		},
		new int[]
		{
			51,
			85
		},
		new int[]
		{
			52,
			68
		},
		new int[]
		{
			53,
			71
		},
		new int[]
		{
			54,
			74
		},
		new int[]
		{
			55,
			67
		},
		new int[]
		{
			56,
			66
		},
		new int[]
		{
			57,
			77
		},
		new int[]
		{
			42,
			128
		},
		new int[]
		{
			35,
			137
		},
		new int[]
		{
			33,
			113
		},
		new int[]
		{
			63,
			97
		},
		new int[]
		{
			64,
			121,
			122
		},
		new int[]
		{
			46,
			111
		},
		new int[]
		{
			44,
			108
		}
	};
}

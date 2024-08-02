using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class mFont
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x00059D4C File Offset: 0x00057F4C
	public mFont(string strFont, string pathImage, string pathData, int space)
	{
		try
		{
			this.strFont = strFont;
			this.space = space;
			this.pathImage = pathImage;
			DataInputStream dataInputStream = null;
			this.reloadImage();
			try
			{
				dataInputStream = MyStream.readFile(pathData);
				this.fImages = new int[(int)dataInputStream.readShort()][];
				for (int i = 0; i < this.fImages.Length; i++)
				{
					this.fImages[i] = new int[4];
					this.fImages[i][0] = (int)dataInputStream.readShort();
					this.fImages[i][1] = (int)dataInputStream.readShort();
					this.fImages[i][2] = (int)dataInputStream.readShort();
					this.fImages[i][3] = (int)dataInputStream.readShort();
					this.setHeight(this.fImages[i][3]);
				}
				dataInputStream.close();
			}
			catch (Exception)
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex)
				{
					ex.StackTrace.ToString();
				}
			}
		}
		catch (Exception ex2)
		{
			ex2.StackTrace.ToString();
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00059EA8 File Offset: 0x000580A8
	public mFont(sbyte id)
	{
		string text = "chelthm";
		bool flag = (id > 0 && id < 10) || id == 19;
		if (flag)
		{
			this.yAdd = 1;
			text = "barmeneb";
		}
		else
		{
			bool flag2 = id >= 10 && id <= 18;
			if (flag2)
			{
				text = "chelthm";
				this.yAdd = 2;
			}
			else
			{
				bool flag3 = id > 24;
				if (flag3)
				{
					text = "staccato";
				}
			}
		}
		this.id = id;
		text = string.Concat(new object[]
		{
			"FontSys/x",
			mGraphics.zoomLevel,
			"/",
			text
		});
		this.myFont = (Font)Resources.Load(text);
		bool flag4 = id < 25;
		if (flag4)
		{
			this.color1 = this.setColorFont(id);
			this.color2 = this.setColorFont(id);
		}
		else
		{
			this.color1 = this.bigColor((int)id);
			this.color2 = this.bigColor((int)id);
		}
		this.wO = this.getWidthExactOf("o");
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00059FE8 File Offset: 0x000581E8
	public static void init()
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			mFont.tahoma_7b_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_red.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_blue.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_white.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellowSmall = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_dark = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_brown.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green2.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_focus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_focus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_unfocus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_unfocus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue1 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue1.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green2.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_yellow.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_grey = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_grey.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_red.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_white.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_8b = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_8b.png", "/myfont/tahoma_8b", -1);
			mFont.number_yellow = new mFont(" 0123456789+-", "/myfont/number_yellow.png", "/myfont/number", 0);
			mFont.number_red = new mFont(" 0123456789+-", "/myfont/number_red.png", "/myfont/number", 0);
			mFont.number_green = new mFont(" 0123456789+-", "/myfont/number_green.png", "/myfont/number", 0);
			mFont.number_gray = new mFont(" 0123456789+-", "/myfont/number_gray.png", "/myfont/number", 0);
			mFont.number_orange = new mFont(" 0123456789+-", "/myfont/number_orange.png", "/myfont/number", 0);
			mFont.bigNumber_red = mFont.number_red;
			mFont.bigNumber_While = mFont.tahoma_7b_white;
			mFont.bigNumber_yellow = mFont.number_yellow;
			mFont.bigNumber_green = mFont.number_green;
			mFont.bigNumber_orange = mFont.number_orange;
			mFont.bigNumber_blue = mFont.tahoma_7_blue1;
			mFont.nameFontRed = mFont.tahoma_7_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
		}
		else
		{
			mFont.gI = new mFont(0);
			mFont.tahoma_7b_red = new mFont(1);
			mFont.tahoma_7b_blue = new mFont(2);
			mFont.tahoma_7b_white = new mFont(3);
			mFont.tahoma_7b_yellow = new mFont(4);
			mFont.tahoma_7b_yellowSmall = new mFont(4);
			mFont.tahoma_7b_dark = new mFont(5);
			mFont.tahoma_7b_green2 = new mFont(6);
			mFont.tahoma_7b_green = new mFont(7);
			mFont.tahoma_7b_focus = new mFont(8);
			mFont.tahoma_7b_unfocus = new mFont(9);
			mFont.tahoma_7 = new mFont(10);
			mFont.tahoma_7_blue1 = new mFont(11);
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
			mFont.tahoma_7_green2 = new mFont(12);
			mFont.tahoma_7_yellow = new mFont(13);
			mFont.tahoma_7_grey = new mFont(14);
			mFont.tahoma_7_red = new mFont(15);
			mFont.tahoma_7_blue = new mFont(16);
			mFont.tahoma_7_green = new mFont(17);
			mFont.tahoma_7_white = new mFont(18);
			mFont.tahoma_8b = new mFont(19);
			mFont.number_yellow = new mFont(20);
			mFont.number_red = new mFont(21);
			mFont.number_green = new mFont(22);
			mFont.number_gray = new mFont(23);
			mFont.number_orange = new mFont(24);
			mFont.bigNumber_red = new mFont(25);
			mFont.bigNumber_yellow = new mFont(26);
			mFont.bigNumber_green = new mFont(27);
			mFont.bigNumber_While = new mFont(28);
			mFont.bigNumber_blue = new mFont(29);
			mFont.bigNumber_orange = new mFont(30);
			mFont.bigNumber_black = new mFont(31);
			mFont.nameFontRed = mFont.tahoma_7b_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.yAddFont = 1;
			bool flag2 = mGraphics.zoomLevel == 1;
			if (flag2)
			{
				mFont.yAddFont = -3;
			}
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00004E3B File Offset: 0x0000303B
	public void setHeight(int height)
	{
		this.height = height;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0005A524 File Offset: 0x00058724
	public Color setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		float b = (float)num / 256f;
		float g = (float)num2 / 256f;
		float r = (float)num3 / 256f;
		return new Color(r, g, b);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0005A57C File Offset: 0x0005877C
	public Color bigColor(int id)
	{
		Color[] array = new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.white,
			this.setColor(40404),
			Color.red,
			Color.black
		};
		return array[id - 25];
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00004E45 File Offset: 0x00003045
	public void setColorByID(int ID)
	{
		this.color1 = this.setColor(mFont.colorJava[ID]);
		this.color2 = this.setColor(mFont.colorJava[ID]);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0005A5FC File Offset: 0x000587FC
	public void setTypePaint(mGraphics g, string st, int x, int y, int align, sbyte idFont)
	{
		sbyte colorByID = this.id;
		bool flag = idFont > 0;
		if (flag)
		{
			colorByID = idFont;
		}
		x--;
		bool flag2 = this.id > 24;
		if (flag2)
		{
			Color[] array = new Color[]
			{
				this.setColor(6029312),
				this.setColor(7169025),
				this.setColor(7680),
				this.setColor(0),
				this.setColor(9264),
				this.setColor(6029312)
			};
			this.color1 = array[(int)(this.id - 25)];
			this.color2 = array[(int)(this.id - 25)];
			this._drawString(g, st, x + 1, y, align);
			this._drawString(g, st, x - 1, y, align);
			this._drawString(g, st, x, y - 1, align);
			this._drawString(g, st, x, y + 1, align);
			this._drawString(g, st, x + 1, y + 1, align);
			this._drawString(g, st, x + 1, y - 1, align);
			this._drawString(g, st, x - 1, y - 1, align);
			this._drawString(g, st, x - 1, y + 1, align);
			this.color1 = this.bigColor((int)this.id);
			this.color2 = this.bigColor((int)this.id);
		}
		else
		{
			this.setColorByID((int)colorByID);
		}
		this._drawString(g, st, x, y - this.yAdd, align);
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0005A7A0 File Offset: 0x000589A0
	public Color setColorFont(sbyte id)
	{
		return this.setColor(mFont.colorJava[(int)id]);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0005A7C0 File Offset: 0x000589C0
	public void drawString(mGraphics g, string st, int x, int y, int align)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			int length = st.Length;
			int num;
			if (align != 0)
			{
				if (align != 1)
				{
					num = x - (this.getWidth(st) >> 1);
				}
				else
				{
					num = x - this.getWidth(st);
				}
			}
			else
			{
				num = x;
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i].ToString() + string.Empty);
				bool flag2 = num2 == -1;
				if (flag2)
				{
					num2 = 0;
				}
				bool flag3 = num2 > -1;
				if (flag3)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					bool flag4 = num3 + num4 > this.imgFont.texture.height;
					if (flag4)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0005A938 File Offset: 0x00058B38
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.drawString(g, st, x, y, align);
		}
		else
		{
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0005A978 File Offset: 0x00058B78
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.drawString(g, st, x, y, align, font2);
		}
		else
		{
			this.drawStringBd(g, st, x, y, align, font2);
		}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0005A9B8 File Offset: 0x00058BB8
	public void drawStringBd(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		this.setTypePaint(g, st, x - 1, y - 1, align, 0);
		this.setTypePaint(g, st, x - 1, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y - 1, align, 0);
		this.setTypePaint(g, st, x + 1, y + 1, align, 0);
		this.setTypePaint(g, st, x, y - 1, align, 0);
		this.setTypePaint(g, st, x, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y, align, 0);
		this.setTypePaint(g, st, x - 1, y, align, 0);
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0005AA68 File Offset: 0x00058C68
	public void drawString(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			int length = st.Length;
			int num;
			if (align != 0)
			{
				if (align != 1)
				{
					num = x - (this.getWidth(st) >> 1);
				}
				else
				{
					num = x - this.getWidth(st);
				}
			}
			else
			{
				num = x;
			}
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i]);
				bool flag2 = num2 == -1;
				if (flag2)
				{
					num2 = 0;
				}
				bool flag3 = num2 > -1;
				if (flag3)
				{
					int x2 = this.fImages[num2][0];
					int num3 = this.fImages[num2][1];
					int w = this.fImages[num2][2];
					int num4 = this.fImages[num2][3];
					bool flag4 = num3 + num4 > this.imgFont.texture.height;
					if (flag4)
					{
						num3 -= this.imgFont.texture.height;
						x2 = this.imgFont.texture.width / 2;
					}
					bool flag5 = !GameCanvas.lowGraphic && font != null;
					if (flag5)
					{
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num + 1, y, 20);
						g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num, y + 1, 20);
					}
					g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
		}
		else
		{
			this.setTypePaint(g, st, x, y + 1, align, font.id);
			this.setTypePaint(g, st, x, y, align, 0);
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0005AC38 File Offset: 0x00058E38
	public MyVector splitFontVector(string src, int lineWidth)
	{
		MyVector myVector = new MyVector();
		string text = string.Empty;
		for (int i = 0; i < src.Length; i++)
		{
			bool flag = src[i] == '\n' || src[i] == '\b';
			if (flag)
			{
				myVector.addElement(text);
				text = string.Empty;
			}
			else
			{
				text += src[i].ToString();
				bool flag2 = this.getWidth(text) > lineWidth;
				if (flag2)
				{
					int num = text.Length - 1;
					while (num >= 0 && text[num] != ' ')
					{
						num--;
					}
					bool flag3 = num < 0;
					if (flag3)
					{
						num = text.Length - 1;
					}
					myVector.addElement(text.Substring(0, num));
					i = i - (text.Length - num) + 1;
					text = string.Empty;
				}
				bool flag4 = i == src.Length - 1 && !text.Trim().Equals(string.Empty);
				if (flag4)
				{
					myVector.addElement(text);
				}
			}
		}
		return myVector;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0005AD74 File Offset: 0x00058F74
	public string splitFirst(string str)
	{
		string text = string.Empty;
		bool flag = false;
		for (int i = 0; i < str.Length; i++)
		{
			bool flag2 = !flag;
			if (flag2)
			{
				string text2 = str.Substring(i);
				text = ((!this.compare(text2, " ")) ? (text + text2) : (text + str[i].ToString() + "-"));
				flag = true;
			}
			else
			{
				bool flag3 = str[i] == ' ';
				if (flag3)
				{
					flag = false;
				}
			}
		}
		return text;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0005AE0C File Offset: 0x0005900C
	public string[] splitStrInLine(string src, int lineWidth)
	{
		ArrayList arrayList = this.splitStrInLineA(src, lineWidth);
		string[] array = new string[arrayList.Count];
		for (int i = 0; i < arrayList.Count; i++)
		{
			array[i] = (string)arrayList[i];
		}
		return array;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0005AE5C File Offset: 0x0005905C
	public ArrayList splitStrInLineA(string src, int lineWidth)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = 0;
		int length = src.Length;
		bool flag = length < 5;
		ArrayList result;
		if (flag)
		{
			arrayList.Add(src);
			result = arrayList;
		}
		else
		{
			string text = string.Empty;
			try
			{
				for (;;)
				{
					bool flag2 = this.getWidthNotExactOf(text) < lineWidth;
					if (flag2)
					{
						text += src[num2].ToString();
						num2++;
						bool flag3 = src[num2] != '\n';
						if (flag3)
						{
							bool flag4 = num2 < length - 1;
							if (flag4)
							{
								continue;
							}
							num2 = length - 1;
						}
					}
					bool flag5 = num2 != length - 1 && src[num2 + 1] != ' ';
					if (flag5)
					{
						int num3 = num2;
						while (src[num2 + 1] != '\n' && (src[num2 + 1] != ' ' || src[num2] == ' ') && num2 != num)
						{
							num2--;
						}
						bool flag6 = num2 == num;
						if (flag6)
						{
							num2 = num3;
						}
					}
					string text2 = src.Substring(num, num2 + 1 - num);
					bool flag7 = text2[0] == '\n';
					if (flag7)
					{
						text2 = text2.Substring(1, text2.Length - 1);
					}
					bool flag8 = text2[text2.Length - 1] == '\n';
					if (flag8)
					{
						text2 = text2.Substring(0, text2.Length - 1);
					}
					arrayList.Add(text2);
					bool flag9 = num2 != length - 1;
					if (!flag9)
					{
						goto IL_1C4;
					}
					num = num2 + 1;
					while (num != length - 1 && src[num] == ' ')
					{
						num++;
					}
					bool flag10 = num == length - 1;
					if (flag10)
					{
						break;
					}
					num2 = num;
					text = string.Empty;
				}
				return arrayList;
				IL_1C4:
				result = arrayList;
			}
			catch (Exception ex)
			{
				Cout.LogWarning(string.Concat(new object[]
				{
					"EXCEPTION WHEN REAL SPLIT ",
					src,
					"\nend=",
					num2,
					"\n",
					ex.Message,
					"\n",
					ex.StackTrace
				}));
				arrayList.Add(src);
				result = arrayList;
			}
		}
		return result;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0005B0C0 File Offset: 0x000592C0
	public string[] splitFontArray(string src, int lineWidth)
	{
		MyVector myVector = this.splitFontVector(src, lineWidth);
		string[] array = new string[myVector.size()];
		for (int i = 0; i < myVector.size(); i++)
		{
			array[i] = (string)myVector.elementAt(i);
		}
		return array;
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0005B110 File Offset: 0x00059310
	public bool compare(string strSource, string str)
	{
		for (int i = 0; i < strSource.Length; i++)
		{
			bool flag = (string.Empty + strSource[i].ToString()).Equals(str);
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0005B164 File Offset: 0x00059364
	public int getWidth(string s)
	{
		bool flag = mGraphics.zoomLevel == 1;
		int result;
		if (flag)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				int num2 = this.strFont.IndexOf(s[i]);
				bool flag2 = num2 == -1;
				if (flag2)
				{
					num2 = 0;
				}
				num += this.fImages[num2][2] + this.space;
			}
			result = num;
		}
		else
		{
			result = this.getWidthExactOf(s);
		}
		return result;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0005B1E4 File Offset: 0x000593E4
	public int getWidthExactOf(string s)
	{
		int result;
		try
		{
			result = (int)new GUIStyle
			{
				font = this.myFont
			}.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel;
		}
		catch (Exception ex)
		{
			Cout.LogError(string.Concat(new string[]
			{
				"GET WIDTH OF ",
				s,
				" FAIL.\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			result = this.getWidthNotExactOf(s);
		}
		return result;
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0005B280 File Offset: 0x00059480
	public int getWidthNotExactOf(string s)
	{
		return s.Length * this.wO / mGraphics.zoomLevel;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0005B2A8 File Offset: 0x000594A8
	public int getHeight()
	{
		bool flag = mGraphics.zoomLevel == 1;
		int result;
		if (flag)
		{
			result = this.height;
		}
		else
		{
			bool flag2 = this.height > 0;
			if (flag2)
			{
				result = this.height / mGraphics.zoomLevel;
			}
			else
			{
				GUIStyle guistyle = new GUIStyle();
				guistyle.font = this.myFont;
				try
				{
					this.height = (int)guistyle.CalcSize(new GUIContent("Adg")).y + 2;
				}
				catch (Exception ex)
				{
					Cout.LogError("FAIL GET HEIGHT " + ex.StackTrace);
					this.height = 20;
				}
				result = this.height / mGraphics.zoomLevel;
			}
		}
		return result;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0005B368 File Offset: 0x00059568
	public void _drawString(mGraphics g, string st, int x0, int y0, int align)
	{
		y0 += mFont.yAddFont;
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.font = this.myFont;
		float num = 0f;
		float num2 = 0f;
		switch (align)
		{
		case 0:
			num = (float)x0;
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperLeft;
			break;
		case 1:
			num = (float)(x0 - GameCanvas.w);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperRight;
			break;
		case 2:
		case 3:
			num = (float)(x0 - GameCanvas.w / 2);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperCenter;
			break;
		}
		guistyle.normal.textColor = this.color1;
		g.drawString(st, (int)num, (int)num2, guistyle);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0005B424 File Offset: 0x00059624
	public static string[] splitStringSv(string _text, string _searchStr)
	{
		int num = 0;
		int startIndex = 0;
		int length = _searchStr.Length;
		int num2 = _text.IndexOf(_searchStr, startIndex);
		while (num2 != -1)
		{
			startIndex = num2 + length;
			num2 = _text.IndexOf(_searchStr, startIndex);
			num++;
		}
		string[] array = new string[num + 1];
		int num3 = _text.IndexOf(_searchStr);
		int num4 = 0;
		int num5 = 0;
		while (num3 != -1)
		{
			array[num5] = _text.Substring(num4, num3 - num4);
			num4 = num3 + length;
			num3 = _text.IndexOf(_searchStr, num4);
			num5++;
		}
		array[num5] = _text.Substring(num4, _text.Length - num4);
		return array;
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0005B4DC File Offset: 0x000596DC
	public void reloadImage()
	{
		bool flag = mGraphics.zoomLevel == 1;
		if (flag)
		{
			this.imgFont = GameCanvas.loadImage(this.pathImage);
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00003A0D File Offset: 0x00001C0D
	public void freeImage()
	{
	}

	// Token: 0x04000A42 RID: 2626
	public static int LEFT = 0;

	// Token: 0x04000A43 RID: 2627
	public static int RIGHT = 1;

	// Token: 0x04000A44 RID: 2628
	public static int CENTER = 2;

	// Token: 0x04000A45 RID: 2629
	public static int RED = 0;

	// Token: 0x04000A46 RID: 2630
	public static int YELLOW = 1;

	// Token: 0x04000A47 RID: 2631
	public static int GREEN = 2;

	// Token: 0x04000A48 RID: 2632
	public static int FATAL = 3;

	// Token: 0x04000A49 RID: 2633
	public static int MISS = 4;

	// Token: 0x04000A4A RID: 2634
	public static int ORANGE = 5;

	// Token: 0x04000A4B RID: 2635
	public static int ADDMONEY = 6;

	// Token: 0x04000A4C RID: 2636
	public static int MISS_ME = 7;

	// Token: 0x04000A4D RID: 2637
	public static int FATAL_ME = 8;

	// Token: 0x04000A4E RID: 2638
	public static int HP = 9;

	// Token: 0x04000A4F RID: 2639
	public static int MP = 10;

	// Token: 0x04000A50 RID: 2640
	private int space;

	// Token: 0x04000A51 RID: 2641
	private Image imgFont;

	// Token: 0x04000A52 RID: 2642
	private string strFont;

	// Token: 0x04000A53 RID: 2643
	private int[][] fImages;

	// Token: 0x04000A54 RID: 2644
	public static int yAddFont;

	// Token: 0x04000A55 RID: 2645
	public static int[] colorJava = new int[]
	{
		0,
		16711680,
		6520319,
		16777215,
		16755200,
		5449989,
		21285,
		52224,
		7386228,
		16771788,
		0,
		65535,
		21285,
		16776960,
		5592405,
		16742263,
		33023,
		8701737,
		15723503,
		7999781,
		16768815,
		14961237,
		4124899,
		4671303,
		16096312,
		16711680,
		16755200,
		52224,
		16777215,
		6520319,
		16096312
	};

	// Token: 0x04000A56 RID: 2646
	public static mFont gI;

	// Token: 0x04000A57 RID: 2647
	public static mFont tahoma_7b_red;

	// Token: 0x04000A58 RID: 2648
	public static mFont tahoma_7b_blue;

	// Token: 0x04000A59 RID: 2649
	public static mFont tahoma_7b_white;

	// Token: 0x04000A5A RID: 2650
	public static mFont tahoma_7b_yellow;

	// Token: 0x04000A5B RID: 2651
	public static mFont tahoma_7b_yellowSmall;

	// Token: 0x04000A5C RID: 2652
	public static mFont tahoma_7b_dark;

	// Token: 0x04000A5D RID: 2653
	public static mFont tahoma_7b_green2;

	// Token: 0x04000A5E RID: 2654
	public static mFont tahoma_7b_green;

	// Token: 0x04000A5F RID: 2655
	public static mFont tahoma_7b_focus;

	// Token: 0x04000A60 RID: 2656
	public static mFont tahoma_7b_unfocus;

	// Token: 0x04000A61 RID: 2657
	public static mFont tahoma_7;

	// Token: 0x04000A62 RID: 2658
	public static mFont tahoma_7_blue1;

	// Token: 0x04000A63 RID: 2659
	public static mFont tahoma_7_blue1Small;

	// Token: 0x04000A64 RID: 2660
	public static mFont tahoma_7_green2;

	// Token: 0x04000A65 RID: 2661
	public static mFont tahoma_7_yellow;

	// Token: 0x04000A66 RID: 2662
	public static mFont tahoma_7_grey;

	// Token: 0x04000A67 RID: 2663
	public static mFont tahoma_7_red;

	// Token: 0x04000A68 RID: 2664
	public static mFont tahoma_7_blue;

	// Token: 0x04000A69 RID: 2665
	public static mFont tahoma_7_green;

	// Token: 0x04000A6A RID: 2666
	public static mFont tahoma_7_white;

	// Token: 0x04000A6B RID: 2667
	public static mFont tahoma_8b;

	// Token: 0x04000A6C RID: 2668
	public static mFont number_yellow;

	// Token: 0x04000A6D RID: 2669
	public static mFont number_red;

	// Token: 0x04000A6E RID: 2670
	public static mFont number_green;

	// Token: 0x04000A6F RID: 2671
	public static mFont number_gray;

	// Token: 0x04000A70 RID: 2672
	public static mFont number_orange;

	// Token: 0x04000A71 RID: 2673
	public static mFont bigNumber_red;

	// Token: 0x04000A72 RID: 2674
	public static mFont bigNumber_While;

	// Token: 0x04000A73 RID: 2675
	public static mFont bigNumber_yellow;

	// Token: 0x04000A74 RID: 2676
	public static mFont bigNumber_green;

	// Token: 0x04000A75 RID: 2677
	public static mFont bigNumber_orange;

	// Token: 0x04000A76 RID: 2678
	public static mFont bigNumber_blue;

	// Token: 0x04000A77 RID: 2679
	public static mFont bigNumber_black;

	// Token: 0x04000A78 RID: 2680
	public static mFont nameFontRed;

	// Token: 0x04000A79 RID: 2681
	public static mFont nameFontYellow;

	// Token: 0x04000A7A RID: 2682
	public static mFont nameFontGreen;

	// Token: 0x04000A7B RID: 2683
	public static mFont tahoma_7_greySmall;

	// Token: 0x04000A7C RID: 2684
	public static mFont tahoma_7b_yellowSmall2;

	// Token: 0x04000A7D RID: 2685
	public static mFont tahoma_7b_green2Small;

	// Token: 0x04000A7E RID: 2686
	public static mFont tahoma_7_whiteSmall;

	// Token: 0x04000A7F RID: 2687
	public static mFont tahoma_7b_greenSmall;

	// Token: 0x04000A80 RID: 2688
	public Font myFont;

	// Token: 0x04000A81 RID: 2689
	private int height;

	// Token: 0x04000A82 RID: 2690
	private int wO;

	// Token: 0x04000A83 RID: 2691
	public Color color1 = Color.white;

	// Token: 0x04000A84 RID: 2692
	public Color color2 = Color.gray;

	// Token: 0x04000A85 RID: 2693
	public sbyte id;

	// Token: 0x04000A86 RID: 2694
	public int fstyle;

	// Token: 0x04000A87 RID: 2695
	public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

	// Token: 0x04000A88 RID: 2696
	public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

	// Token: 0x04000A89 RID: 2697
	public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

	// Token: 0x04000A8A RID: 2698
	private int yAdd;

	// Token: 0x04000A8B RID: 2699
	private string pathImage;
}

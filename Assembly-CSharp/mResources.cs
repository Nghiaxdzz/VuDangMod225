using System;

// Token: 0x02000070 RID: 112
public class mResources
{
	// Token: 0x06000561 RID: 1377 RVA: 0x00005143 File Offset: 0x00003343
	public static void loadLanguague()
	{
		mResources.loadLanguague(1);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00061C50 File Offset: 0x0005FE50
	public static void loadLanguague(sbyte newLanguage)
	{
		mResources.language = newLanguage;
		switch (mResources.language)
		{
		case 0:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1.png");
			T1.load();
			ServerListScreen.linkweb = "YOUTUBE EREN NROBALL";
			return;
		case 1:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
			T2.load();
			ServerListScreen.linkweb = "http://world.teamobi.com";
			return;
		case 2:
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
			T3.load();
			ServerListScreen.linkweb = "http://dragonball.indonaga.com";
			return;
		default:
			return;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00061CD8 File Offset: 0x0005FED8
	public static string replace(string str, string replacement)
	{
		return NinjaUtil.replace(str, "#", replacement);
	}

	// Token: 0x04000B75 RID: 2933
	public static string delacc = string.Empty;

	// Token: 0x04000B76 RID: 2934
	public static string notiINAPP = string.Empty;

	// Token: 0x04000B77 RID: 2935
	public static string notiRuby = string.Empty;

	// Token: 0x04000B78 RID: 2936
	public static string equip = string.Empty;

	// Token: 0x04000B79 RID: 2937
	public static string unlock = string.Empty;

	// Token: 0x04000B7A RID: 2938
	public static string radaCard = string.Empty;

	// Token: 0x04000B7B RID: 2939
	public static string not_enough_money_1 = string.Empty;

	// Token: 0x04000B7C RID: 2940
	public static string napngoc = string.Empty;

	// Token: 0x04000B7D RID: 2941
	public static string functionMaintain1 = string.Empty;

	// Token: 0x04000B7E RID: 2942
	public static string tang;

	// Token: 0x04000B7F RID: 2943
	public static string kquaVongQuay;

	// Token: 0x04000B80 RID: 2944
	public static string useGem;

	// Token: 0x04000B81 RID: 2945
	public static string autoFunction;

	// Token: 0x04000B82 RID: 2946
	public static string choitiep;

	// Token: 0x04000B83 RID: 2947
	public static string attack;

	// Token: 0x04000B84 RID: 2948
	public static string defend;

	// Token: 0x04000B85 RID: 2949
	public static string follow;

	// Token: 0x04000B86 RID: 2950
	public static string status;

	// Token: 0x04000B87 RID: 2951
	public static string gohome;

	// Token: 0x04000B88 RID: 2952
	public static string pet;

	// Token: 0x04000B89 RID: 2953
	public static string maychutathoacmatsong;

	// Token: 0x04000B8A RID: 2954
	public static string cauhinhthap;

	// Token: 0x04000B8B RID: 2955
	public static string cauhinhcao;

	// Token: 0x04000B8C RID: 2956
	public static string combineSpell;

	// Token: 0x04000B8D RID: 2957
	public static string combineFail;

	// Token: 0x04000B8E RID: 2958
	public static string combineSuccess;

	// Token: 0x04000B8F RID: 2959
	public static string turnOnAnalog;

	// Token: 0x04000B90 RID: 2960
	public static string turnOffAnalog;

	// Token: 0x04000B91 RID: 2961
	public static string analog;

	// Token: 0x04000B92 RID: 2962
	public static string inventory_Pass;

	// Token: 0x04000B93 RID: 2963
	public static string input_Inventory_Pass;

	// Token: 0x04000B94 RID: 2964
	public static string input_Inventory_Pass_wrong = string.Empty;

	// Token: 0x04000B95 RID: 2965
	public static string REGISTOPROTECT = string.Empty;

	// Token: 0x04000B96 RID: 2966
	public static string turnOnSound = string.Empty;

	// Token: 0x04000B97 RID: 2967
	public static string turnOffSound = string.Empty;

	// Token: 0x04000B98 RID: 2968
	public static string REGISTERING = string.Empty;

	// Token: 0x04000B99 RID: 2969
	public static string SENDINGMSG = string.Empty;

	// Token: 0x04000B9A RID: 2970
	public static string SENTMSG = string.Empty;

	// Token: 0x04000B9B RID: 2971
	public static string NOSENDMSG = string.Empty;

	// Token: 0x04000B9C RID: 2972
	public static string sendMsgSuccess = string.Empty;

	// Token: 0x04000B9D RID: 2973
	public static string cannotSendMsg = string.Empty;

	// Token: 0x04000B9E RID: 2974
	public static string sendGuessMsgSuccess = string.Empty;

	// Token: 0x04000B9F RID: 2975
	public static string sendMsgFail = string.Empty;

	// Token: 0x04000BA0 RID: 2976
	public static string ALERT_PRIVATE_PASS_1 = string.Empty;

	// Token: 0x04000BA1 RID: 2977
	public static string ALERT_PRIVATE_PASS_2 = string.Empty;

	// Token: 0x04000BA2 RID: 2978
	public static string INPUT_PRIVATE_PASS = string.Empty;

	// Token: 0x04000BA3 RID: 2979
	public static string change_account = string.Empty;

	// Token: 0x04000BA4 RID: 2980
	public static string alreadyHadAccount1 = string.Empty;

	// Token: 0x04000BA5 RID: 2981
	public static string alreadyHadAccount2 = string.Empty;

	// Token: 0x04000BA6 RID: 2982
	public static string userBlank = string.Empty;

	// Token: 0x04000BA7 RID: 2983
	public static string passwordBlank = string.Empty;

	// Token: 0x04000BA8 RID: 2984
	public static string accTooShort = string.Empty;

	// Token: 0x04000BA9 RID: 2985
	public static string phoneInvalid = string.Empty;

	// Token: 0x04000BAA RID: 2986
	public static string emailInvalid = string.Empty;

	// Token: 0x04000BAB RID: 2987
	public static string registerNewAcc = string.Empty;

	// Token: 0x04000BAC RID: 2988
	public static string selectServer = string.Empty;

	// Token: 0x04000BAD RID: 2989
	public static string selectServer2 = string.Empty;

	// Token: 0x04000BAE RID: 2990
	public static string forgetPass = string.Empty;

	// Token: 0x04000BAF RID: 2991
	public static string password = string.Empty;

	// Token: 0x04000BB0 RID: 2992
	public static string[] LOGINLABELS = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BB1 RID: 2993
	public static string msg = string.Empty;

	// Token: 0x04000BB2 RID: 2994
	public static string[] msgg = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BB3 RID: 2995
	public static string no_msg = string.Empty;

	// Token: 0x04000BB4 RID: 2996
	public static string cancelAccountProtection = string.Empty;

	// Token: 0x04000BB5 RID: 2997
	public static string plsCheckAcc = string.Empty;

	// Token: 0x04000BB6 RID: 2998
	public static string phone = string.Empty;

	// Token: 0x04000BB7 RID: 2999
	public static string email = string.Empty;

	// Token: 0x04000BB8 RID: 3000
	public static string acc = string.Empty;

	// Token: 0x04000BB9 RID: 3001
	public static string pwd = string.Empty;

	// Token: 0x04000BBA RID: 3002
	public static string goToWebForPassword = string.Empty;

	// Token: 0x04000BBB RID: 3003
	public static string dragon_ball = string.Empty;

	// Token: 0x04000BBC RID: 3004
	public static string character = string.Empty;

	// Token: 0x04000BBD RID: 3005
	public static string account = string.Empty;

	// Token: 0x04000BBE RID: 3006
	public static string account_server = string.Empty;

	// Token: 0x04000BBF RID: 3007
	public static string char_name_blank = string.Empty;

	// Token: 0x04000BC0 RID: 3008
	public static string char_name_short = string.Empty;

	// Token: 0x04000BC1 RID: 3009
	public static string char_name_long = string.Empty;

	// Token: 0x04000BC2 RID: 3010
	public static string changeNameChar = string.Empty;

	// Token: 0x04000BC3 RID: 3011
	public static string char_name = string.Empty;

	// Token: 0x04000BC4 RID: 3012
	public static string login = string.Empty;

	// Token: 0x04000BC5 RID: 3013
	public static string login2 = string.Empty;

	// Token: 0x04000BC6 RID: 3014
	public static string register = string.Empty;

	// Token: 0x04000BC7 RID: 3015
	public static string WAIT = string.Empty;

	// Token: 0x04000BC8 RID: 3016
	public static string PLEASEWAIT = string.Empty;

	// Token: 0x04000BC9 RID: 3017
	public static string CONNECTING = string.Empty;

	// Token: 0x04000BCA RID: 3018
	public static string LOGGING = string.Empty;

	// Token: 0x04000BCB RID: 3019
	public static string LOADING = string.Empty;

	// Token: 0x04000BCC RID: 3020
	public static string downloading_data = string.Empty;

	// Token: 0x04000BCD RID: 3021
	public static string select_server = string.Empty;

	// Token: 0x04000BCE RID: 3022
	public static string pls_restart_game_error = string.Empty;

	// Token: 0x04000BCF RID: 3023
	public static string lost_connection = string.Empty;

	// Token: 0x04000BD0 RID: 3024
	public static string check_3G = string.Empty;

	// Token: 0x04000BD1 RID: 3025
	public static string UPDATE = string.Empty;

	// Token: 0x04000BD2 RID: 3026
	public static string change_zone = string.Empty;

	// Token: 0x04000BD3 RID: 3027
	public static string select_zone = string.Empty;

	// Token: 0x04000BD4 RID: 3028
	public static string website = string.Empty;

	// Token: 0x04000BD5 RID: 3029
	public static string server = string.Empty;

	// Token: 0x04000BD6 RID: 3030
	public static string planet = string.Empty;

	// Token: 0x04000BD7 RID: 3031
	public static string[] MENUME = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BD8 RID: 3032
	public static string[] MENUNEWCHAR = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BD9 RID: 3033
	public static string[] MENUGENDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BDA RID: 3034
	public static string[] CHAR_ORDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BDB RID: 3035
	public static string[][] mainTab1 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x04000BDC RID: 3036
	public static string[][] mainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x04000BDD RID: 3037
	public static string[][] petMainTab = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x04000BDE RID: 3038
	public static string[][] petMainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x04000BDF RID: 3039
	public static string[] key_skill_qwerty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE0 RID: 3040
	public static string[] key_skill = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE1 RID: 3041
	public static string SKILL_FAIL = string.Empty;

	// Token: 0x04000BE2 RID: 3042
	public static string HP_EMPTY = string.Empty;

	// Token: 0x04000BE3 RID: 3043
	public static string ZONE_HERE = string.Empty;

	// Token: 0x04000BE4 RID: 3044
	public static string[] DES_TASK = new string[]
	{
		" ",
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE5 RID: 3045
	public static string[] DIES = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE6 RID: 3046
	public static string[] SYNTHESIS = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE7 RID: 3047
	public static string[] tips = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000BE8 RID: 3048
	public static string TASK_INPUT_CLASS = string.Empty;

	// Token: 0x04000BE9 RID: 3049
	public static string SERI_NUM = string.Empty;

	// Token: 0x04000BEA RID: 3050
	public static string CARD_CODE = string.Empty;

	// Token: 0x04000BEB RID: 3051
	public static string pay_card = string.Empty;

	// Token: 0x04000BEC RID: 3052
	public static string pay_card2 = string.Empty;

	// Token: 0x04000BED RID: 3053
	public static string serial_blank = string.Empty;

	// Token: 0x04000BEE RID: 3054
	public static string card_code_blank = string.Empty;

	// Token: 0x04000BEF RID: 3055
	public static string billion = string.Empty;

	// Token: 0x04000BF0 RID: 3056
	public static string million = string.Empty;

	// Token: 0x04000BF1 RID: 3057
	public static string MENU = string.Empty;

	// Token: 0x04000BF2 RID: 3058
	public static string CLOSE = string.Empty;

	// Token: 0x04000BF3 RID: 3059
	public static string ON = string.Empty;

	// Token: 0x04000BF4 RID: 3060
	public static string OFF = string.Empty;

	// Token: 0x04000BF5 RID: 3061
	public static string ENABLE = string.Empty;

	// Token: 0x04000BF6 RID: 3062
	public static string DELETE = string.Empty;

	// Token: 0x04000BF7 RID: 3063
	public static string VIEW = string.Empty;

	// Token: 0x04000BF8 RID: 3064
	public static string CONTINUE = string.Empty;

	// Token: 0x04000BF9 RID: 3065
	public static string NEXTSTEP = string.Empty;

	// Token: 0x04000BFA RID: 3066
	public static string USE = string.Empty;

	// Token: 0x04000BFB RID: 3067
	public static string SORT = string.Empty;

	// Token: 0x04000BFC RID: 3068
	public static string YES = string.Empty;

	// Token: 0x04000BFD RID: 3069
	public static string NO = string.Empty;

	// Token: 0x04000BFE RID: 3070
	public static string EXIT = string.Empty;

	// Token: 0x04000BFF RID: 3071
	public static string CHAT = string.Empty;

	// Token: 0x04000C00 RID: 3072
	public static string REVENGE = string.Empty;

	// Token: 0x04000C01 RID: 3073
	public static string OK = string.Empty;

	// Token: 0x04000C02 RID: 3074
	public static string retry = string.Empty;

	// Token: 0x04000C03 RID: 3075
	public static string uncheck = string.Empty;

	// Token: 0x04000C04 RID: 3076
	public static string remember = string.Empty;

	// Token: 0x04000C05 RID: 3077
	public static string ACCEPT = string.Empty;

	// Token: 0x04000C06 RID: 3078
	public static string CANCEL = string.Empty;

	// Token: 0x04000C07 RID: 3079
	public static string SELECT = string.Empty;

	// Token: 0x04000C08 RID: 3080
	public static string enter = string.Empty;

	// Token: 0x04000C09 RID: 3081
	public static string open_link = string.Empty;

	// Token: 0x04000C0A RID: 3082
	public static string DOYOUWANTEXIT = string.Empty;

	// Token: 0x04000C0B RID: 3083
	public static string NEWCHAR = string.Empty;

	// Token: 0x04000C0C RID: 3084
	public static string BACK = string.Empty;

	// Token: 0x04000C0D RID: 3085
	public static string LOCKED = string.Empty;

	// Token: 0x04000C0E RID: 3086
	public static string KILL = string.Empty;

	// Token: 0x04000C0F RID: 3087
	public static string KILLBOSS = string.Empty;

	// Token: 0x04000C10 RID: 3088
	public static string NOLOCK = string.Empty;

	// Token: 0x04000C11 RID: 3089
	public static string XU = string.Empty;

	// Token: 0x04000C12 RID: 3090
	public static string LUONG = string.Empty;

	// Token: 0x04000C13 RID: 3091
	public static string RUBY = string.Empty;

	// Token: 0x04000C14 RID: 3092
	public static string PK_NOW = string.Empty;

	// Token: 0x04000C15 RID: 3093
	public static string CUU_SAT = string.Empty;

	// Token: 0x04000C16 RID: 3094
	public static string NOT_ENOUGH_MP = string.Empty;

	// Token: 0x04000C17 RID: 3095
	public static string you_receive = string.Empty;

	// Token: 0x04000C18 RID: 3096
	public static string MONTH = string.Empty;

	// Token: 0x04000C19 RID: 3097
	public static string WEEK = string.Empty;

	// Token: 0x04000C1A RID: 3098
	public static string DAY = string.Empty;

	// Token: 0x04000C1B RID: 3099
	public static string HOUR = string.Empty;

	// Token: 0x04000C1C RID: 3100
	public static string SECOND = string.Empty;

	// Token: 0x04000C1D RID: 3101
	public static string MINUTE = string.Empty;

	// Token: 0x04000C1E RID: 3102
	public static string LEARN_SKILL = string.Empty;

	// Token: 0x04000C1F RID: 3103
	public static string rank = string.Empty;

	// Token: 0x04000C20 RID: 3104
	public static string active_point = string.Empty;

	// Token: 0x04000C21 RID: 3105
	public static string friend = string.Empty;

	// Token: 0x04000C22 RID: 3106
	public static string enemy = string.Empty;

	// Token: 0x04000C23 RID: 3107
	public static string no_friend = string.Empty;

	// Token: 0x04000C24 RID: 3108
	public static string chat_world = string.Empty;

	// Token: 0x04000C25 RID: 3109
	public static string change_flag = string.Empty;

	// Token: 0x04000C26 RID: 3110
	public static string gameInfo = string.Empty;

	// Token: 0x04000C27 RID: 3111
	public static string quayso = string.Empty;

	// Token: 0x04000C28 RID: 3112
	public static string option = string.Empty;

	// Token: 0x04000C29 RID: 3113
	public static string high = string.Empty;

	// Token: 0x04000C2A RID: 3114
	public static string medium = string.Empty;

	// Token: 0x04000C2B RID: 3115
	public static string low = string.Empty;

	// Token: 0x04000C2C RID: 3116
	public static string increase_vga = string.Empty;

	// Token: 0x04000C2D RID: 3117
	public static string decrease_vga = string.Empty;

	// Token: 0x04000C2E RID: 3118
	public static string serverchat_off = string.Empty;

	// Token: 0x04000C2F RID: 3119
	public static string serverchat_on = string.Empty;

	// Token: 0x04000C30 RID: 3120
	public static string x2Screen = string.Empty;

	// Token: 0x04000C31 RID: 3121
	public static string x1Screen = string.Empty;

	// Token: 0x04000C32 RID: 3122
	public static string changeSizeScreen = string.Empty;

	// Token: 0x04000C33 RID: 3123
	public static string aura_off = string.Empty;

	// Token: 0x04000C34 RID: 3124
	public static string aura_on = string.Empty;

	// Token: 0x04000C35 RID: 3125
	public static string hat_off = string.Empty;

	// Token: 0x04000C36 RID: 3126
	public static string hat_on = string.Empty;

	// Token: 0x04000C37 RID: 3127
	public static string chest = string.Empty;

	// Token: 0x04000C38 RID: 3128
	public static string[] chestt = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C39 RID: 3129
	public static string[] inventory = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3A RID: 3130
	public static string[] combine = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3B RID: 3131
	public static string[] mapp = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3C RID: 3132
	public static string[] item_give = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3D RID: 3133
	public static string[] item_receive = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3E RID: 3134
	public static string[] zonee = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C3F RID: 3135
	public static string zone = string.Empty;

	// Token: 0x04000C40 RID: 3136
	public static string map = string.Empty;

	// Token: 0x04000C41 RID: 3137
	public static string item_receive2 = string.Empty;

	// Token: 0x04000C42 RID: 3138
	public static string item = string.Empty;

	// Token: 0x04000C43 RID: 3139
	public static string give_upper = string.Empty;

	// Token: 0x04000C44 RID: 3140
	public static string receive_upper = string.Empty;

	// Token: 0x04000C45 RID: 3141
	public static string receive_all = string.Empty;

	// Token: 0x04000C46 RID: 3142
	public static string no_map = string.Empty;

	// Token: 0x04000C47 RID: 3143
	public static string go_to_quest = string.Empty;

	// Token: 0x04000C48 RID: 3144
	public static string from_earth = string.Empty;

	// Token: 0x04000C49 RID: 3145
	public static string from_namec = string.Empty;

	// Token: 0x04000C4A RID: 3146
	public static string from_sayda = string.Empty;

	// Token: 0x04000C4B RID: 3147
	public static string expire = string.Empty;

	// Token: 0x04000C4C RID: 3148
	public static string pow_request = string.Empty;

	// Token: 0x04000C4D RID: 3149
	public static string your_pow = string.Empty;

	// Token: 0x04000C4E RID: 3150
	public static string used = string.Empty;

	// Token: 0x04000C4F RID: 3151
	public static string place = string.Empty;

	// Token: 0x04000C50 RID: 3152
	public static string FOREVER = string.Empty;

	// Token: 0x04000C51 RID: 3153
	public static string NOUPGRADE = string.Empty;

	// Token: 0x04000C52 RID: 3154
	public static string NOTUPGRADE = string.Empty;

	// Token: 0x04000C53 RID: 3155
	public static string UPGRADE = string.Empty;

	// Token: 0x04000C54 RID: 3156
	public static string UPGRADING = string.Empty;

	// Token: 0x04000C55 RID: 3157
	public static string make_shortcut = string.Empty;

	// Token: 0x04000C56 RID: 3158
	public static string into_place = string.Empty;

	// Token: 0x04000C57 RID: 3159
	public static string move_to_chest = string.Empty;

	// Token: 0x04000C58 RID: 3160
	public static string move_to_chest2 = string.Empty;

	// Token: 0x04000C59 RID: 3161
	public static string press_chat_querty = string.Empty;

	// Token: 0x04000C5A RID: 3162
	public static string press_chat = string.Empty;

	// Token: 0x04000C5B RID: 3163
	public static string saying = string.Empty;

	// Token: 0x04000C5C RID: 3164
	public static string miss = string.Empty;

	// Token: 0x04000C5D RID: 3165
	public static string donate = string.Empty;

	// Token: 0x04000C5E RID: 3166
	public static string receive = string.Empty;

	// Token: 0x04000C5F RID: 3167
	public static string press_twice = string.Empty;

	// Token: 0x04000C60 RID: 3168
	public static string can_harvest = string.Empty;

	// Token: 0x04000C61 RID: 3169
	public static string do_accept_qwerty = string.Empty;

	// Token: 0x04000C62 RID: 3170
	public static string do_accept = string.Empty;

	// Token: 0x04000C63 RID: 3171
	public static string plsRestartGame = string.Empty;

	// Token: 0x04000C64 RID: 3172
	public static string is_online = string.Empty;

	// Token: 0x04000C65 RID: 3173
	public static string is_offline = string.Empty;

	// Token: 0x04000C66 RID: 3174
	public static string make_friend = string.Empty;

	// Token: 0x04000C67 RID: 3175
	public static string chat_player = string.Empty;

	// Token: 0x04000C68 RID: 3176
	public static string chat_with = string.Empty;

	// Token: 0x04000C69 RID: 3177
	public static string clan_capsuledonate = string.Empty;

	// Token: 0x04000C6A RID: 3178
	public static string clan_capsuleself = string.Empty;

	// Token: 0x04000C6B RID: 3179
	public static string clan_point = string.Empty;

	// Token: 0x04000C6C RID: 3180
	public static string give_pea = string.Empty;

	// Token: 0x04000C6D RID: 3181
	public static string receive_pea = string.Empty;

	// Token: 0x04000C6E RID: 3182
	public static string request_pea = string.Empty;

	// Token: 0x04000C6F RID: 3183
	public static string time = string.Empty;

	// Token: 0x04000C70 RID: 3184
	public static string received = string.Empty;

	// Token: 0x04000C71 RID: 3185
	public static string power = string.Empty;

	// Token: 0x04000C72 RID: 3186
	public static string join_date = string.Empty;

	// Token: 0x04000C73 RID: 3187
	public static string clan_leader = string.Empty;

	// Token: 0x04000C74 RID: 3188
	public static string clan_coleader = string.Empty;

	// Token: 0x04000C75 RID: 3189
	public static string power_point = string.Empty;

	// Token: 0x04000C76 RID: 3190
	public static string member = string.Empty;

	// Token: 0x04000C77 RID: 3191
	public static string[] memberr = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C78 RID: 3192
	public static string[] chatClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C79 RID: 3193
	public static string[] leaveClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C7A RID: 3194
	public static string[] createClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C7B RID: 3195
	public static string[] findClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C7C RID: 3196
	public static string[] khau_hieuu = new string[]
	{
		string.Empty
	};

	// Token: 0x04000C7D RID: 3197
	public static string[] bieu_tuongg = new string[]
	{
		string.Empty
	};

	// Token: 0x04000C7E RID: 3198
	public static string[] request_pea2 = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C7F RID: 3199
	public static string level = string.Empty;

	// Token: 0x04000C80 RID: 3200
	public static string clan_birthday = string.Empty;

	// Token: 0x04000C81 RID: 3201
	public static string clan_list = string.Empty;

	// Token: 0x04000C82 RID: 3202
	public static string create = string.Empty;

	// Token: 0x04000C83 RID: 3203
	public static string find = string.Empty;

	// Token: 0x04000C84 RID: 3204
	public static string leave = string.Empty;

	// Token: 0x04000C85 RID: 3205
	public static string not_join_clan = string.Empty;

	// Token: 0x04000C86 RID: 3206
	public static string[] clanEmpty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C87 RID: 3207
	public static string input_clan_name = string.Empty;

	// Token: 0x04000C88 RID: 3208
	public static string clan_name = string.Empty;

	// Token: 0x04000C89 RID: 3209
	public static string chat_clan = string.Empty;

	// Token: 0x04000C8A RID: 3210
	public static string input_clan_name_to_create = string.Empty;

	// Token: 0x04000C8B RID: 3211
	public static string input_clan_slogan = string.Empty;

	// Token: 0x04000C8C RID: 3212
	public static string do_u_want_join_clan = string.Empty;

	// Token: 0x04000C8D RID: 3213
	public static string select_clan_icon = string.Empty;

	// Token: 0x04000C8E RID: 3214
	public static string request_join_clan = string.Empty;

	// Token: 0x04000C8F RID: 3215
	public static string view_clan_member = string.Empty;

	// Token: 0x04000C90 RID: 3216
	public static string create_clan_co_leader = string.Empty;

	// Token: 0x04000C91 RID: 3217
	public static string create_clan_leader = string.Empty;

	// Token: 0x04000C92 RID: 3218
	public static string disable_clan_mastership = string.Empty;

	// Token: 0x04000C93 RID: 3219
	public static string kick_clan_mem = string.Empty;

	// Token: 0x04000C94 RID: 3220
	public static string clan_name_blank = string.Empty;

	// Token: 0x04000C95 RID: 3221
	public static string clan_slogan_blank = string.Empty;

	// Token: 0x04000C96 RID: 3222
	public static string cannot_find_clan = string.Empty;

	// Token: 0x04000C97 RID: 3223
	public static string ago = string.Empty;

	// Token: 0x04000C98 RID: 3224
	public static string findingClan = string.Empty;

	// Token: 0x04000C99 RID: 3225
	public static string trade = string.Empty;

	// Token: 0x04000C9A RID: 3226
	public static string not_lock_trade = string.Empty;

	// Token: 0x04000C9B RID: 3227
	public static string not_lock_trade_upper = string.Empty;

	// Token: 0x04000C9C RID: 3228
	public static string locked_trade = string.Empty;

	// Token: 0x04000C9D RID: 3229
	public static string locked_trade_upper = string.Empty;

	// Token: 0x04000C9E RID: 3230
	public static string lock_trade = string.Empty;

	// Token: 0x04000C9F RID: 3231
	public static string wait_opp_lock_trade = string.Empty;

	// Token: 0x04000CA0 RID: 3232
	public static string press_done = string.Empty;

	// Token: 0x04000CA1 RID: 3233
	public static string THROW = string.Empty;

	// Token: 0x04000CA2 RID: 3234
	public static string SPLIT = string.Empty;

	// Token: 0x04000CA3 RID: 3235
	public static string done = string.Empty;

	// Token: 0x04000CA4 RID: 3236
	public static string opponent = string.Empty;

	// Token: 0x04000CA5 RID: 3237
	public static string you = string.Empty;

	// Token: 0x04000CA6 RID: 3238
	public static string mlock = string.Empty;

	// Token: 0x04000CA7 RID: 3239
	public static string money_trade = string.Empty;

	// Token: 0x04000CA8 RID: 3240
	public static string GETOUT = string.Empty;

	// Token: 0x04000CA9 RID: 3241
	public static string MOVEOUT = string.Empty;

	// Token: 0x04000CAA RID: 3242
	public static string MOVEFORPET = string.Empty;

	// Token: 0x04000CAB RID: 3243
	public static string GETOUTMONEY = string.Empty;

	// Token: 0x04000CAC RID: 3244
	public static string GETINMONEY = string.Empty;

	// Token: 0x04000CAD RID: 3245
	public static string SENDMONEY = string.Empty;

	// Token: 0x04000CAE RID: 3246
	public static string GETIN = string.Empty;

	// Token: 0x04000CAF RID: 3247
	public static string SALE = string.Empty;

	// Token: 0x04000CB0 RID: 3248
	public static string SALES = string.Empty;

	// Token: 0x04000CB1 RID: 3249
	public static string SALEALL = string.Empty;

	// Token: 0x04000CB2 RID: 3250
	public static string BUY = string.Empty;

	// Token: 0x04000CB3 RID: 3251
	public static string BUYS = string.Empty;

	// Token: 0x04000CB4 RID: 3252
	public static string input_money_to_trade = string.Empty;

	// Token: 0x04000CB5 RID: 3253
	public static string input_money = string.Empty;

	// Token: 0x04000CB6 RID: 3254
	public static string input_money_wrong = string.Empty;

	// Token: 0x04000CB7 RID: 3255
	public static string not_enough_money = string.Empty;

	// Token: 0x04000CB8 RID: 3256
	public static string input_quantity_to_trade = string.Empty;

	// Token: 0x04000CB9 RID: 3257
	public static string input_quantity = string.Empty;

	// Token: 0x04000CBA RID: 3258
	public static string input_quantity_wrong = string.Empty;

	// Token: 0x04000CBB RID: 3259
	public static string already_has_item = string.Empty;

	// Token: 0x04000CBC RID: 3260
	public static string unlock_item_to_trade = string.Empty;

	// Token: 0x04000CBD RID: 3261
	public static string root = string.Empty;

	// Token: 0x04000CBE RID: 3262
	public static string need = string.Empty;

	// Token: 0x04000CBF RID: 3263
	public static string need_upper = string.Empty;

	// Token: 0x04000CC0 RID: 3264
	public static string free = string.Empty;

	// Token: 0x04000CC1 RID: 3265
	public static string free1 = string.Empty;

	// Token: 0x04000CC2 RID: 3266
	public static string free2 = string.Empty;

	// Token: 0x04000CC3 RID: 3267
	public static string select_item = string.Empty;

	// Token: 0x04000CC4 RID: 3268
	public static string random = string.Empty;

	// Token: 0x04000CC5 RID: 3269
	public static string say_hello = string.Empty;

	// Token: 0x04000CC6 RID: 3270
	public static string say_wat_do_u_want_to_buy = string.Empty;

	// Token: 0x04000CC7 RID: 3271
	public static string say_wat_do_u_want_to_buy2 = string.Empty;

	// Token: 0x04000CC8 RID: 3272
	public static string do_u_sure_to_trade = string.Empty;

	// Token: 0x04000CC9 RID: 3273
	public static string learn_with = string.Empty;

	// Token: 0x04000CCA RID: 3274
	public static string buy_with = string.Empty;

	// Token: 0x04000CCB RID: 3275
	public static string can_not_do_when_die = string.Empty;

	// Token: 0x04000CCC RID: 3276
	public static string use_for_combine = string.Empty;

	// Token: 0x04000CCD RID: 3277
	public static string use_for_trade = string.Empty;

	// Token: 0x04000CCE RID: 3278
	public static string not_enough_luong_world_channel = string.Empty;

	// Token: 0x04000CCF RID: 3279
	public static string world_channel_5_luong = string.Empty;

	// Token: 0x04000CD0 RID: 3280
	public static string want_to_trade = string.Empty;

	// Token: 0x04000CD1 RID: 3281
	public static string hasJustUpgrade1 = string.Empty;

	// Token: 0x04000CD2 RID: 3282
	public static string hasJustUpgrade2 = string.Empty;

	// Token: 0x04000CD3 RID: 3283
	public static string potential_to_learn = string.Empty;

	// Token: 0x04000CD4 RID: 3284
	public static string potential_point = string.Empty;

	// Token: 0x04000CD5 RID: 3285
	public static string achievement_point = string.Empty;

	// Token: 0x04000CD6 RID: 3286
	public static string increase = string.Empty;

	// Token: 0x04000CD7 RID: 3287
	public static string increase_upper = string.Empty;

	// Token: 0x04000CD8 RID: 3288
	public static string not_enough_potential_point1 = string.Empty;

	// Token: 0x04000CD9 RID: 3289
	public static string not_enough_potential_point2 = string.Empty;

	// Token: 0x04000CDA RID: 3290
	public static string use_potential_point_for1 = string.Empty;

	// Token: 0x04000CDB RID: 3291
	public static string use_potential_point_for2 = string.Empty;

	// Token: 0x04000CDC RID: 3292
	public static string for_HP = string.Empty;

	// Token: 0x04000CDD RID: 3293
	public static string for_KI = string.Empty;

	// Token: 0x04000CDE RID: 3294
	public static string for_hit_point = string.Empty;

	// Token: 0x04000CDF RID: 3295
	public static string for_armor = string.Empty;

	// Token: 0x04000CE0 RID: 3296
	public static string for_crit = string.Empty;

	// Token: 0x04000CE1 RID: 3297
	public static string can_buy_from_Uron1 = string.Empty;

	// Token: 0x04000CE2 RID: 3298
	public static string can_buy_from_Uron2 = string.Empty;

	// Token: 0x04000CE3 RID: 3299
	public static string can_buy_from_Uron3 = string.Empty;

	// Token: 0x04000CE4 RID: 3300
	public static string HP = string.Empty;

	// Token: 0x04000CE5 RID: 3301
	public static string KI = string.Empty;

	// Token: 0x04000CE6 RID: 3302
	public static string hit_point = string.Empty;

	// Token: 0x04000CE7 RID: 3303
	public static string armor = string.Empty;

	// Token: 0x04000CE8 RID: 3304
	public static string vitality = string.Empty;

	// Token: 0x04000CE9 RID: 3305
	public static string critical = string.Empty;

	// Token: 0x04000CEA RID: 3306
	public static string cap_do = string.Empty;

	// Token: 0x04000CEB RID: 3307
	public static string KI_consume = string.Empty;

	// Token: 0x04000CEC RID: 3308
	public static string speed = string.Empty;

	// Token: 0x04000CED RID: 3309
	public static string milisecond = string.Empty;

	// Token: 0x04000CEE RID: 3310
	public static string max_level_reach = string.Empty;

	// Token: 0x04000CEF RID: 3311
	public static string next_level_require = string.Empty;

	// Token: 0x04000CF0 RID: 3312
	public static string potential = string.Empty;

	// Token: 0x04000CF1 RID: 3313
	public static string not_learn = string.Empty;

	// Token: 0x04000CF2 RID: 3314
	public static string learn_require = string.Empty;

	// Token: 0x04000CF3 RID: 3315
	public static string learn = string.Empty;

	// Token: 0x04000CF4 RID: 3316
	public static string to_gain_20hp = string.Empty;

	// Token: 0x04000CF5 RID: 3317
	public static string to_gain_20mp = string.Empty;

	// Token: 0x04000CF6 RID: 3318
	public static string to_gain_1pow = string.Empty;

	// Token: 0x04000CF7 RID: 3319
	public static string[][] hairStyleName = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x04000CF8 RID: 3320
	public static string hp_ki_full = string.Empty;

	// Token: 0x04000CF9 RID: 3321
	public static string quest_place = string.Empty;

	// Token: 0x04000CFA RID: 3322
	public static string no_mission = string.Empty;

	// Token: 0x04000CFB RID: 3323
	public static string reward_mission = string.Empty;

	// Token: 0x04000CFC RID: 3324
	public static string achievement_mission = string.Empty;

	// Token: 0x04000CFD RID: 3325
	public static string trangbi = string.Empty;

	// Token: 0x04000CFE RID: 3326
	public static string wat_do_u_want = string.Empty;

	// Token: 0x04000CFF RID: 3327
	public static string off = string.Empty;

	// Token: 0x04000D00 RID: 3328
	public static string on = string.Empty;

	// Token: 0x04000D01 RID: 3329
	public static string select_map = string.Empty;

	// Token: 0x04000D02 RID: 3330
	public static string offPlease = string.Empty;

	// Token: 0x04000D03 RID: 3331
	public static string onPlease = string.Empty;

	// Token: 0x04000D04 RID: 3332
	public static sbyte language;

	// Token: 0x04000D05 RID: 3333
	public const sbyte VIETNAM = 0;

	// Token: 0x04000D06 RID: 3334
	public const sbyte ENGLISH = 1;

	// Token: 0x04000D07 RID: 3335
	public const sbyte INDONESIA = 2;

	// Token: 0x04000D08 RID: 3336
	public static string choigame;

	// Token: 0x04000D09 RID: 3337
	public static string no_enemy = string.Empty;

	// Token: 0x04000D0A RID: 3338
	public static string kigui;

	// Token: 0x04000D0B RID: 3339
	public static string kiguiXu;

	// Token: 0x04000D0C RID: 3340
	public static string kiguiLuong;

	// Token: 0x04000D0D RID: 3341
	public static string kiguiXuchat;

	// Token: 0x04000D0E RID: 3342
	public static string kiguiLuongchat;

	// Token: 0x04000D0F RID: 3343
	public static string huykigui;

	// Token: 0x04000D10 RID: 3344
	public static string nhantien;

	// Token: 0x04000D11 RID: 3345
	public static string dangban;

	// Token: 0x04000D12 RID: 3346
	public static string daban;

	// Token: 0x04000D13 RID: 3347
	public static string num;

	// Token: 0x04000D14 RID: 3348
	public static string upTop;

	// Token: 0x04000D15 RID: 3349
	public static string page;

	// Token: 0x04000D16 RID: 3350
	public static string getDown;

	// Token: 0x04000D17 RID: 3351
	public static string getUp;

	// Token: 0x04000D18 RID: 3352
	public static string notYetSell;

	// Token: 0x04000D19 RID: 3353
	public static string charger;

	// Token: 0x04000D1A RID: 3354
	public static string finishBomong;

	// Token: 0x04000D1B RID: 3355
	public static string note;

	// Token: 0x04000D1C RID: 3356
	public static string regNote;

	// Token: 0x04000D1D RID: 3357
	public static string remain;

	// Token: 0x04000D1E RID: 3358
	public static string faster;

	// Token: 0x04000D1F RID: 3359
	public static string fasterQuestion;

	// Token: 0x04000D20 RID: 3360
	public static string chuacotaikhoan;

	// Token: 0x04000D21 RID: 3361
	public static string taidulieudechoi;

	// Token: 0x04000D22 RID: 3362
	public static string huy;

	// Token: 0x04000D23 RID: 3363
	public static string taidulieu;

	// Token: 0x04000D24 RID: 3364
	public static string xoadulieu;

	// Token: 0x04000D25 RID: 3365
	public static string deletaDataNote;

	// Token: 0x04000D26 RID: 3366
	public static string playNew;

	// Token: 0x04000D27 RID: 3367
	public static string playAcc;

	// Token: 0x04000D28 RID: 3368
	public static string vuilongnhapduthongtin;

	// Token: 0x04000D29 RID: 3369
	public static string not_register_yet = string.Empty;

	// Token: 0x04000D2A RID: 3370
	public static string nhanngoc;

	// Token: 0x04000D2B RID: 3371
	public static string fusion;

	// Token: 0x04000D2C RID: 3372
	public static string sure_fusion;

	// Token: 0x04000D2D RID: 3373
	public static string fusionForever;

	// Token: 0x04000D2E RID: 3374
	public static string xinchucmung;

	// Token: 0x04000D2F RID: 3375
	public static string den;

	// Token: 0x04000D30 RID: 3376
	public static string nhatvatpham;
}

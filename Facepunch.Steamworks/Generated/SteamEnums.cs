using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks
{
	//
	// ESteamIPType
	//
	internal enum SteamIPType : int
	{
		Type4 = 0,
		Type6 = 1,
	}
	
	//
	// EUniverse
	//
	public enum Universe : int
	{
		Invalid = 0,
		Public = 1,
		Beta = 2,
		Internal = 3,
		Dev = 4,
		Max = 5,
	}
	
	//
	// EResult
	//
	public enum Result : int
	{
		None = 0,
		OK = 1,
		Fail = 2,
		NoConnection = 3,
		InvalidPassword = 5,
		LoggedInElsewhere = 6,
		InvalidProtocolVer = 7,
		InvalidParam = 8,
		FileNotFound = 9,
		Busy = 10,
		InvalidState = 11,
		InvalidName = 12,
		InvalidEmail = 13,
		DuplicateName = 14,
		AccessDenied = 15,
		Timeout = 16,
		Banned = 17,
		AccountNotFound = 18,
		InvalidSteamID = 19,
		ServiceUnavailable = 20,
		NotLoggedOn = 21,
		Pending = 22,
		EncryptionFailure = 23,
		InsufficientPrivilege = 24,
		LimitExceeded = 25,
		Revoked = 26,
		Expired = 27,
		AlreadyRedeemed = 28,
		DuplicateRequest = 29,
		AlreadyOwned = 30,
		IPNotFound = 31,
		PersistFailed = 32,
		LockingFailed = 33,
		LogonSessionReplaced = 34,
		ConnectFailed = 35,
		HandshakeFailed = 36,
		IOFailure = 37,
		RemoteDisconnect = 38,
		ShoppingCartNotFound = 39,
		Blocked = 40,
		Ignored = 41,
		NoMatch = 42,
		AccountDisabled = 43,
		ServiceReadOnly = 44,
		AccountNotFeatured = 45,
		AdministratorOK = 46,
		ContentVersion = 47,
		TryAnotherCM = 48,
		PasswordRequiredToKickSession = 49,
		AlreadyLoggedInElsewhere = 50,
		Suspended = 51,
		Cancelled = 52,
		DataCorruption = 53,
		DiskFull = 54,
		RemoteCallFailed = 55,
		PasswordUnset = 56,
		ExternalAccountUnlinked = 57,
		PSNTicketInvalid = 58,
		ExternalAccountAlreadyLinked = 59,
		RemoteFileConflict = 60,
		IllegalPassword = 61,
		SameAsPreviousValue = 62,
		AccountLogonDenied = 63,
		CannotUseOldPassword = 64,
		InvalidLoginAuthCode = 65,
		AccountLogonDeniedNoMail = 66,
		HardwareNotCapableOfIPT = 67,
		IPTInitError = 68,
		ParentalControlRestricted = 69,
		FacebookQueryError = 70,
		ExpiredLoginAuthCode = 71,
		IPLoginRestrictionFailed = 72,
		AccountLockedDown = 73,
		AccountLogonDeniedVerifiedEmailRequired = 74,
		NoMatchingURL = 75,
		BadResponse = 76,
		RequirePasswordReEntry = 77,
		ValueOutOfRange = 78,
		UnexpectedError = 79,
		Disabled = 80,
		InvalidCEGSubmission = 81,
		RestrictedDevice = 82,
		RegionLocked = 83,
		RateLimitExceeded = 84,
		AccountLoginDeniedNeedTwoFactor = 85,
		ItemDeleted = 86,
		AccountLoginDeniedThrottle = 87,
		TwoFactorCodeMismatch = 88,
		TwoFactorActivationCodeMismatch = 89,
		AccountAssociatedToMultiplePartners = 90,
		NotModified = 91,
		NoMobileDevice = 92,
		TimeNotSynced = 93,
		SmsCodeFailed = 94,
		AccountLimitExceeded = 95,
		AccountActivityLimitExceeded = 96,
		PhoneActivityLimitExceeded = 97,
		RefundToWallet = 98,
		EmailSendFailure = 99,
		NotSettled = 100,
		NeedCaptcha = 101,
		GSLTDenied = 102,
		GSOwnerDenied = 103,
		InvalidItemType = 104,
		IPBanned = 105,
		GSLTExpired = 106,
		InsufficientFunds = 107,
		TooManyPending = 108,
		NoSiteLicensesFound = 109,
		WGNetworkSendExceeded = 110,
		AccountNotFriends = 111,
		LimitedUserAccount = 112,
		CantRemoveItem = 113,
		AccountDeleted = 114,
		ExistingUserCancelledLicense = 115,
	}
	
	//
	// EVoiceResult
	//
	internal enum VoiceResult : int
	{
		OK = 0,
		NotInitialized = 1,
		NotRecording = 2,
		NoData = 3,
		BufferTooSmall = 4,
		DataCorrupted = 5,
		Restricted = 6,
		UnsupportedCodec = 7,
		ReceiverOutOfDate = 8,
		ReceiverDidNotAnswer = 9,
	}
	
	//
	// EDenyReason
	//
	internal enum DenyReason : int
	{
		Invalid = 0,
		InvalidVersion = 1,
		Generic = 2,
		NotLoggedOn = 3,
		NoLicense = 4,
		Cheater = 5,
		LoggedInElseWhere = 6,
		UnknownText = 7,
		IncompatibleAnticheat = 8,
		MemoryCorruption = 9,
		IncompatibleSoftware = 10,
		SteamConnectionLost = 11,
		SteamConnectionError = 12,
		SteamResponseTimedOut = 13,
		SteamValidationStalled = 14,
		SteamOwnerLeftGuestUser = 15,
	}
	
	//
	// EBeginAuthSessionResult
	//
	public enum BeginAuthResult : int
	{
		OK = 0,
		InvalidTicket = 1,
		DuplicateRequest = 2,
		InvalidVersion = 3,
		GameMismatch = 4,
		ExpiredTicket = 5,
	}
	
	//
	// EAuthSessionResponse
	//
	public enum AuthResponse : int
	{
		OK = 0,
		UserNotConnectedToSteam = 1,
		NoLicenseOrExpired = 2,
		VACBanned = 3,
		LoggedInElseWhere = 4,
		VACCheckTimedOut = 5,
		AuthTicketCanceled = 6,
		AuthTicketInvalidAlreadyUsed = 7,
		AuthTicketInvalid = 8,
		PublisherIssuedBan = 9,
	}
	
	//
	// EUserHasLicenseForAppResult
	//
	public enum UserHasLicenseForAppResult : int
	{
		HasLicense = 0,
		DoesNotHaveLicense = 1,
		NoAuth = 2,
	}
	
	//
	// EAccountType
	//
	internal enum AccountType : int
	{
		Invalid = 0,
		Individual = 1,
		Multiseat = 2,
		GameServer = 3,
		AnonGameServer = 4,
		Pending = 5,
		ContentServer = 6,
		Clan = 7,
		Chat = 8,
		ConsoleUser = 9,
		AnonUser = 10,
		Max = 11,
	}
	
	//
	// EAppReleaseState
	//
	internal enum AppReleaseState : int
	{
		Unknown = 0,
		Unavailable = 1,
		Prerelease = 2,
		PreloadOnly = 3,
		Released = 4,
	}
	
	//
	// EAppOwnershipFlags
	//
	internal enum AppOwnershipFlags : int
	{
		None = 0,
		OwnsLicense = 1,
		FreeLicense = 2,
		RegionRestricted = 4,
		LowViolence = 8,
		InvalidPlatform = 16,
		SharedLicense = 32,
		FreeWeekend = 64,
		RetailLicense = 128,
		LicenseLocked = 256,
		LicensePending = 512,
		LicenseExpired = 1024,
		LicensePermanent = 2048,
		LicenseRecurring = 4096,
		LicenseCanceled = 8192,
		AutoGrant = 16384,
		PendingGift = 32768,
		RentalNotActivated = 65536,
		Rental = 131072,
		SiteLicense = 262144,
		LegacyFreeSub = 524288,
		InvalidOSType = 1048576,
	}
	
	//
	// EAppType
	//
	internal enum AppType : int
	{
		Invalid = 0,
		Game = 1,
		Application = 2,
		Tool = 4,
		Demo = 8,
		Media_DEPRECATED = 16,
		DLC = 32,
		Guide = 64,
		Driver = 128,
		Config = 256,
		Hardware = 512,
		Franchise = 1024,
		Video = 2048,
		Plugin = 4096,
		MusicAlbum = 8192,
		Series = 16384,
		Comic_UNUSED = 32768,
		Beta = 65536,
		Shortcut = 1073741824,
		DepotOnly = -2147483648,
	}
	
	//
	// ESteamUserStatType
	//
	internal enum SteamUserStatType : int
	{
		INVALID = 0,
		INT = 1,
		FLOAT = 2,
		AVGRATE = 3,
		ACHIEVEMENTS = 4,
		GROUPACHIEVEMENTS = 5,
		MAX = 6,
	}
	
	//
	// EChatEntryType
	//
	internal enum ChatEntryType : int
	{
		Invalid = 0,
		ChatMsg = 1,
		Typing = 2,
		InviteGame = 3,
		Emote = 4,
		LeftConversation = 6,
		Entered = 7,
		WasKicked = 8,
		WasBanned = 9,
		Disconnected = 10,
		HistoricalChat = 11,
		LinkBlocked = 14,
	}
	
	//
	// EChatRoomEnterResponse
	//
	public enum RoomEnter : int
	{
		Success = 1,
		DoesntExist = 2,
		NotAllowed = 3,
		Full = 4,
		Error = 5,
		Banned = 6,
		Limited = 7,
		ClanDisabled = 8,
		CommunityBan = 9,
		MemberBlockedYou = 10,
		YouBlockedMember = 11,
		RatelimitExceeded = 15,
	}
	
	//
	// EChatSteamIDInstanceFlags
	//
	internal enum ChatSteamIDInstanceFlags : int
	{
		AccountInstanceMask = 4095,
		InstanceFlagClan = 524288,
		InstanceFlagLobby = 262144,
		InstanceFlagMMSLobby = 131072,
	}
	
	//
	// EMarketingMessageFlags
	//
	internal enum MarketingMessageFlags : int
	{
		None = 0,
		HighPriority = 1,
		PlatformWindows = 2,
		PlatformMac = 4,
		PlatformLinux = 8,
		PlatformRestrictions = 14,
	}
	
	//
	// ENotificationPosition
	//
	public enum NotificationPosition : int
	{
		TopLeft = 0,
		TopRight = 1,
		BottomLeft = 2,
		BottomRight = 3,
	}
	
	//
	// EBroadcastUploadResult
	//
	public enum BroadcastUploadResult : int
	{
		None = 0,
		OK = 1,
		InitFailed = 2,
		FrameFailed = 3,
		Timeout = 4,
		BandwidthExceeded = 5,
		LowFPS = 6,
		MissingKeyFrames = 7,
		NoConnection = 8,
		RelayFailed = 9,
		SettingsChanged = 10,
		MissingAudio = 11,
		TooFarBehind = 12,
		TranscodeBehind = 13,
		NotAllowedToPlay = 14,
		Busy = 15,
		Banned = 16,
		AlreadyActive = 17,
		ForcedOff = 18,
		AudioBehind = 19,
		Shutdown = 20,
		Disconnect = 21,
		VideoInitFailed = 22,
		AudioInitFailed = 23,
	}
	
	//
	// ELaunchOptionType
	//
	internal enum LaunchOptionType : int
	{
		None = 0,
		Default = 1,
		SafeMode = 2,
		Multiplayer = 3,
		Config = 4,
		OpenVR = 5,
		Server = 6,
		Editor = 7,
		Manual = 8,
		Benchmark = 9,
		Option1 = 10,
		Option2 = 11,
		Option3 = 12,
		OculusVR = 13,
		OpenVROverlay = 14,
		OSVR = 15,
		Dialog = 1000,
	}
	
	//
	// EVRHMDType
	//
	internal enum VRHMDType : int
	{
		MDType_None = -1,
		MDType_Unknown = 0,
		MDType_HTC_Dev = 1,
		MDType_HTC_VivePre = 2,
		MDType_HTC_Vive = 3,
		MDType_HTC_VivePro = 4,
		MDType_HTC_ViveCosmos = 5,
		MDType_HTC_Unknown = 20,
		MDType_Oculus_DK1 = 21,
		MDType_Oculus_DK2 = 22,
		MDType_Oculus_Rift = 23,
		MDType_Oculus_RiftS = 24,
		MDType_Oculus_Quest = 25,
		MDType_Oculus_Unknown = 40,
		MDType_Acer_Unknown = 50,
		MDType_Acer_WindowsMR = 51,
		MDType_Dell_Unknown = 60,
		MDType_Dell_Visor = 61,
		MDType_Lenovo_Unknown = 70,
		MDType_Lenovo_Explorer = 71,
		MDType_HP_Unknown = 80,
		MDType_HP_WindowsMR = 81,
		MDType_HP_Reverb = 82,
		MDType_Samsung_Unknown = 90,
		MDType_Samsung_Odyssey = 91,
		MDType_Unannounced_Unknown = 100,
		MDType_Unannounced_WindowsMR = 101,
		MDType_vridge = 110,
		MDType_Huawei_Unknown = 120,
		MDType_Huawei_VR2 = 121,
		MDType_Huawei_EndOfRange = 129,
		mdType_Valve_Unknown = 130,
		mdType_Valve_Index = 131,
	}
	
	//
	// EMarketNotAllowedReasonFlags
	//
	internal enum MarketNotAllowedReasonFlags : int
	{
		None = 0,
		TemporaryFailure = 1,
		AccountDisabled = 2,
		AccountLockedDown = 4,
		AccountLimited = 8,
		TradeBanned = 16,
		AccountNotTrusted = 32,
		SteamGuardNotEnabled = 64,
		SteamGuardOnlyRecentlyEnabled = 128,
		RecentPasswordReset = 256,
		NewPaymentMethod = 512,
		InvalidCookie = 1024,
		UsingNewDevice = 2048,
		RecentSelfRefund = 4096,
		NewPaymentMethodCannotBeVerified = 8192,
		NoRecentPurchases = 16384,
		AcceptedWalletGift = 32768,
	}
	
	//
	// EDurationControlProgress
	//
	public enum DurationControlProgress : int
	{
		Progress_Full = 0,
		Progress_Half = 1,
		Progress_None = 2,
		ExitSoon_3h = 3,
		ExitSoon_5h = 4,
		ExitSoon_Night = 5,
	}
	
	//
	// EDurationControlNotification
	//
	internal enum DurationControlNotification : int
	{
		None = 0,
		DurationControlNotification1Hour = 1,
		DurationControlNotification3Hours = 2,
		HalfProgress = 3,
		NoProgress = 4,
		ExitSoon_3h = 5,
		ExitSoon_5h = 6,
		ExitSoon_Night = 7,
	}
	
	//
	// EGameSearchErrorCode_t
	//
	internal enum GameSearchErrorCode_t : int
	{
		OK = 1,
		Failed_Search_Already_In_Progress = 2,
		Failed_No_Search_In_Progress = 3,
		Failed_Not_Lobby_Leader = 4,
		Failed_No_Host_Available = 5,
		Failed_Search_Params_Invalid = 6,
		Failed_Offline = 7,
		Failed_NotAuthorized = 8,
		Failed_Unknown_Error = 9,
	}
	
	//
	// EPlayerResult_t
	//
	internal enum PlayerResult_t : int
	{
		FailedToConnect = 1,
		Abandoned = 2,
		Kicked = 3,
		Incomplete = 4,
		Completed = 5,
	}
	
	//
	// ESteamIPv6ConnectivityProtocol
	//
	internal enum SteamIPv6ConnectivityProtocol : int
	{
		Invalid = 0,
		HTTP = 1,
		UDP = 2,
	}
	
	//
	// ESteamIPv6ConnectivityState
	//
	internal enum SteamIPv6ConnectivityState : int
	{
		Unknown = 0,
		Good = 1,
		Bad = 2,
	}
	
	//
	// EFriendRelationship
	//
	public enum Relationship : int
	{
		None = 0,
		Blocked = 1,
		RequestRecipient = 2,
		Friend = 3,
		RequestInitiator = 4,
		Ignored = 5,
		IgnoredFriend = 6,
		Suggested_DEPRECATED = 7,
		Max = 8,
	}
	
	//
	// EPersonaState
	//
	public enum FriendState : int
	{
		Offline = 0,
		Online = 1,
		Busy = 2,
		Away = 3,
		Snooze = 4,
		LookingToTrade = 5,
		LookingToPlay = 6,
		Invisible = 7,
		Max = 8,
	}
	
	//
	// EFriendFlags
	//
	internal enum FriendFlags : int
	{
		None = 0,
		Blocked = 1,
		FriendshipRequested = 2,
		Immediate = 4,
		ClanMember = 8,
		OnGameServer = 16,
		RequestingFriendship = 128,
		RequestingInfo = 256,
		Ignored = 512,
		IgnoredFriend = 1024,
		ChatMember = 4096,
		All = 65535,
	}
	
	//
	// EUserRestriction
	//
	internal enum UserRestriction : int
	{
		None = 0,
		Unknown = 1,
		AnyChat = 2,
		VoiceChat = 4,
		GroupChat = 8,
		Rating = 16,
		GameInvites = 32,
		Trading = 64,
	}
	
	//
	// EOverlayToStoreFlag
	//
	internal enum OverlayToStoreFlag : int
	{
		None = 0,
		AddToCart = 1,
		AddToCartAndShow = 2,
	}
	
	//
	// EActivateGameOverlayToWebPageMode
	//
	internal enum ActivateGameOverlayToWebPageMode : int
	{
		Default = 0,
		Modal = 1,
	}
	
	//
	// EPersonaChange
	//
	internal enum PersonaChange : int
	{
		Name = 1,
		Status = 2,
		ComeOnline = 4,
		GoneOffline = 8,
		GamePlayed = 16,
		GameServer = 32,
		Avatar = 64,
		JoinedSource = 128,
		LeftSource = 256,
		RelationshipChanged = 512,
		NameFirstSet = 1024,
		Broadcast = 2048,
		Nickname = 4096,
		SteamLevel = 8192,
		RichPresence = 16384,
	}
	
	//
	// ESteamAPICallFailure
	//
	internal enum SteamAPICallFailure : int
	{
		None = -1,
		SteamGone = 0,
		NetworkFailure = 1,
		InvalidHandle = 2,
		MismatchedCallback = 3,
	}
	
	//
	// EGamepadTextInputMode
	//
	public enum GamepadTextInputMode : int
	{
		Normal = 0,
		Password = 1,
	}
	
	//
	// EGamepadTextInputLineMode
	//
	public enum GamepadTextInputLineMode : int
	{
		SingleLine = 0,
		MultipleLines = 1,
	}
	
	//
	// ECheckFileSignature
	//
	public enum CheckFileSignature : int
	{
		InvalidSignature = 0,
		ValidSignature = 1,
		FileNotFound = 2,
		NoSignaturesFoundForThisApp = 3,
		NoSignaturesFoundForThisFile = 4,
	}
	
	//
	// EMatchMakingServerResponse
	//
	internal enum MatchMakingServerResponse : int
	{
		ServerResponded = 0,
		ServerFailedToRespond = 1,
		NoServersListedOnMasterServer = 2,
	}
	
	//
	// ELobbyType
	//
	internal enum LobbyType : int
	{
		Private = 0,
		FriendsOnly = 1,
		Public = 2,
		Invisible = 3,
		PrivateUnique = 4,
	}
	
	//
	// ELobbyComparison
	//
	internal enum LobbyComparison : int
	{
		EqualToOrLessThan = -2,
		LessThan = -1,
		Equal = 0,
		GreaterThan = 1,
		EqualToOrGreaterThan = 2,
		NotEqual = 3,
	}
	
	//
	// ELobbyDistanceFilter
	//
	internal enum LobbyDistanceFilter : int
	{
		Close = 0,
		Default = 1,
		Far = 2,
		Worldwide = 3,
	}
	
	//
	// EChatMemberStateChange
	//
	internal enum ChatMemberStateChange : int
	{
		Entered = 1,
		Left = 2,
		Disconnected = 4,
		Kicked = 8,
		Banned = 16,
	}
	
	//
	// ESteamPartyBeaconLocationType
	//
	internal enum SteamPartyBeaconLocationType : int
	{
		Invalid = 0,
		ChatGroup = 1,
		Max = 2,
	}
	
	//
	// ESteamPartyBeaconLocationData
	//
	internal enum SteamPartyBeaconLocationData : int
	{
		Invalid = 0,
		Name = 1,
		IconURLSmall = 2,
		IconURLMedium = 3,
		IconURLLarge = 4,
	}
	
	//
	// ERemoteStoragePlatform
	//
	internal enum RemoteStoragePlatform : int
	{
		None = 0,
		Windows = 1,
		OSX = 2,
		PS3 = 4,
		Linux = 8,
		Switch = 16,
		Android = 32,
		IOS = 64,
		All = -1,
	}
	
	//
	// ERemoteStoragePublishedFileVisibility
	//
	internal enum RemoteStoragePublishedFileVisibility : int
	{
		Public = 0,
		FriendsOnly = 1,
		Private = 2,
		Unlisted = 3,
	}
	
	//
	// EWorkshopFileType
	//
	internal enum WorkshopFileType : int
	{
		First = 0,
		Community = 0,
		Microtransaction = 1,
		Collection = 2,
		Art = 3,
		Video = 4,
		Screenshot = 5,
		Game = 6,
		Software = 7,
		Concept = 8,
		WebGuide = 9,
		IntegratedGuide = 10,
		Merch = 11,
		ControllerBinding = 12,
		SteamworksAccessInvite = 13,
		SteamVideo = 14,
		GameManagedItem = 15,
		Max = 16,
	}
	
	//
	// EWorkshopVote
	//
	internal enum WorkshopVote : int
	{
		Unvoted = 0,
		For = 1,
		Against = 2,
		Later = 3,
	}
	
	//
	// EWorkshopFileAction
	//
	internal enum WorkshopFileAction : int
	{
		Played = 0,
		Completed = 1,
	}
	
	//
	// EWorkshopEnumerationType
	//
	internal enum WorkshopEnumerationType : int
	{
		RankedByVote = 0,
		Recent = 1,
		Trending = 2,
		FavoritesOfFriends = 3,
		VotedByFriends = 4,
		ContentByFriends = 5,
		RecentFromFollowedUsers = 6,
	}
	
	//
	// EWorkshopVideoProvider
	//
	internal enum WorkshopVideoProvider : int
	{
		None = 0,
		Youtube = 1,
	}
	
	//
	// EUGCReadAction
	//
	internal enum UGCReadAction : int
	{
		ontinueReadingUntilFinished = 0,
		ontinueReading = 1,
		lose = 2,
	}
	
	//
	// ELeaderboardDataRequest
	//
	internal enum LeaderboardDataRequest : int
	{
		Global = 0,
		GlobalAroundUser = 1,
		Friends = 2,
		Users = 3,
	}
	
	//
	// ELeaderboardSortMethod
	//
	//
	// ELeaderboardDisplayType
	//
	//
	// ELeaderboardUploadScoreMethod
	//
	internal enum LeaderboardUploadScoreMethod : int
	{
		None = 0,
		KeepBest = 1,
		ForceUpdate = 2,
	}
	
	//
	// ERegisterActivationCodeResult
	//
	internal enum RegisterActivationCodeResult : int
	{
		ResultOK = 0,
		ResultFail = 1,
		ResultAlreadyRegistered = 2,
		ResultTimeout = 3,
		AlreadyOwned = 4,
	}
	
	//
	// EP2PSessionError
	//
	public enum P2PSessionError : int
	{
		None = 0,
		NotRunningApp = 1,
		NoRightsToApp = 2,
		DestinationNotLoggedIn = 3,
		Timeout = 4,
		Max = 5,
	}
	
	//
	// EP2PSend
	//
	public enum P2PSend : int
	{
		Unreliable = 0,
		UnreliableNoDelay = 1,
		Reliable = 2,
		ReliableWithBuffering = 3,
	}
	
	//
	// ESNetSocketState
	//
	//
	// ESNetSocketConnectionType
	//
	//
	// EVRScreenshotType
	//
	internal enum VRScreenshotType : int
	{
		None = 0,
		Mono = 1,
		Stereo = 2,
		MonoCubemap = 3,
		MonoPanorama = 4,
		StereoPanorama = 5,
	}
	
	//
	// AudioPlayback_Status
	//
	public enum MusicStatus : int
	{
		Undefined = 0,
		Playing = 1,
		Paused = 2,
		Idle = 3,
	}
	
	//
	// EHTTPMethod
	//
	internal enum HTTPMethod : int
	{
		Invalid = 0,
		GET = 1,
		HEAD = 2,
		POST = 3,
		PUT = 4,
		DELETE = 5,
		OPTIONS = 6,
		PATCH = 7,
	}
	
	//
	// EHTTPStatusCode
	//
	internal enum HTTPStatusCode : int
	{
		Invalid = 0,
		Code100Continue = 100,
		Code101SwitchingProtocols = 101,
		Code200OK = 200,
		Code201Created = 201,
		Code202Accepted = 202,
		Code203NonAuthoritative = 203,
		Code204NoContent = 204,
		Code205ResetContent = 205,
		Code206PartialContent = 206,
		Code300MultipleChoices = 300,
		Code301MovedPermanently = 301,
		Code302Found = 302,
		Code303SeeOther = 303,
		Code304NotModified = 304,
		Code305UseProxy = 305,
		Code307TemporaryRedirect = 307,
		Code400BadRequest = 400,
		Code401Unauthorized = 401,
		Code402PaymentRequired = 402,
		Code403Forbidden = 403,
		Code404NotFound = 404,
		Code405MethodNotAllowed = 405,
		Code406NotAcceptable = 406,
		Code407ProxyAuthRequired = 407,
		Code408RequestTimeout = 408,
		Code409Conflict = 409,
		Code410Gone = 410,
		Code411LengthRequired = 411,
		Code412PreconditionFailed = 412,
		Code413RequestEntityTooLarge = 413,
		Code414RequestURITooLong = 414,
		Code415UnsupportedMediaType = 415,
		Code416RequestedRangeNotSatisfiable = 416,
		Code417ExpectationFailed = 417,
		Code4xxUnknown = 418,
		Code429TooManyRequests = 429,
		Code500InternalServerError = 500,
		Code501NotImplemented = 501,
		Code502BadGateway = 502,
		Code503ServiceUnavailable = 503,
		Code504GatewayTimeout = 504,
		Code505HTTPVersionNotSupported = 505,
		Code5xxUnknown = 599,
	}
	
	//
	// EInputSourceMode
	//
	public enum InputSourceMode : int
	{
		None = 0,
		Dpad = 1,
		Buttons = 2,
		FourButtons = 3,
		AbsoluteMouse = 4,
		RelativeMouse = 5,
		JoystickMove = 6,
		JoystickMouse = 7,
		JoystickCamera = 8,
		ScrollWheel = 9,
		Trigger = 10,
		TouchMenu = 11,
		MouseJoystick = 12,
		MouseRegion = 13,
		RadialMenu = 14,
		SingleButton = 15,
		Switches = 16,
	}
	
	//
	// EInputActionOrigin
	//
	internal enum InputActionOrigin : int
	{
		None = 0,
		SteamController_A = 1,
		SteamController_B = 2,
		SteamController_X = 3,
		SteamController_Y = 4,
		SteamController_LeftBumper = 5,
		SteamController_RightBumper = 6,
		SteamController_LeftGrip = 7,
		SteamController_RightGrip = 8,
		SteamController_Start = 9,
		SteamController_Back = 10,
		SteamController_LeftPad_Touch = 11,
		SteamController_LeftPad_Swipe = 12,
		SteamController_LeftPad_Click = 13,
		SteamController_LeftPad_DPadNorth = 14,
		SteamController_LeftPad_DPadSouth = 15,
		SteamController_LeftPad_DPadWest = 16,
		SteamController_LeftPad_DPadEast = 17,
		SteamController_RightPad_Touch = 18,
		SteamController_RightPad_Swipe = 19,
		SteamController_RightPad_Click = 20,
		SteamController_RightPad_DPadNorth = 21,
		SteamController_RightPad_DPadSouth = 22,
		SteamController_RightPad_DPadWest = 23,
		SteamController_RightPad_DPadEast = 24,
		SteamController_LeftTrigger_Pull = 25,
		SteamController_LeftTrigger_Click = 26,
		SteamController_RightTrigger_Pull = 27,
		SteamController_RightTrigger_Click = 28,
		SteamController_LeftStick_Move = 29,
		SteamController_LeftStick_Click = 30,
		SteamController_LeftStick_DPadNorth = 31,
		SteamController_LeftStick_DPadSouth = 32,
		SteamController_LeftStick_DPadWest = 33,
		SteamController_LeftStick_DPadEast = 34,
		SteamController_Gyro_Move = 35,
		SteamController_Gyro_Pitch = 36,
		SteamController_Gyro_Yaw = 37,
		SteamController_Gyro_Roll = 38,
		SteamController_Reserved0 = 39,
		SteamController_Reserved1 = 40,
		SteamController_Reserved2 = 41,
		SteamController_Reserved3 = 42,
		SteamController_Reserved4 = 43,
		SteamController_Reserved5 = 44,
		SteamController_Reserved6 = 45,
		SteamController_Reserved7 = 46,
		SteamController_Reserved8 = 47,
		SteamController_Reserved9 = 48,
		SteamController_Reserved10 = 49,
		PS4_X = 50,
		PS4_Circle = 51,
		PS4_Triangle = 52,
		PS4_Square = 53,
		PS4_LeftBumper = 54,
		PS4_RightBumper = 55,
		PS4_Options = 56,
		PS4_Share = 57,
		PS4_LeftPad_Touch = 58,
		PS4_LeftPad_Swipe = 59,
		PS4_LeftPad_Click = 60,
		PS4_LeftPad_DPadNorth = 61,
		PS4_LeftPad_DPadSouth = 62,
		PS4_LeftPad_DPadWest = 63,
		PS4_LeftPad_DPadEast = 64,
		PS4_RightPad_Touch = 65,
		PS4_RightPad_Swipe = 66,
		PS4_RightPad_Click = 67,
		PS4_RightPad_DPadNorth = 68,
		PS4_RightPad_DPadSouth = 69,
		PS4_RightPad_DPadWest = 70,
		PS4_RightPad_DPadEast = 71,
		PS4_CenterPad_Touch = 72,
		PS4_CenterPad_Swipe = 73,
		PS4_CenterPad_Click = 74,
		PS4_CenterPad_DPadNorth = 75,
		PS4_CenterPad_DPadSouth = 76,
		PS4_CenterPad_DPadWest = 77,
		PS4_CenterPad_DPadEast = 78,
		PS4_LeftTrigger_Pull = 79,
		PS4_LeftTrigger_Click = 80,
		PS4_RightTrigger_Pull = 81,
		PS4_RightTrigger_Click = 82,
		PS4_LeftStick_Move = 83,
		PS4_LeftStick_Click = 84,
		PS4_LeftStick_DPadNorth = 85,
		PS4_LeftStick_DPadSouth = 86,
		PS4_LeftStick_DPadWest = 87,
		PS4_LeftStick_DPadEast = 88,
		PS4_RightStick_Move = 89,
		PS4_RightStick_Click = 90,
		PS4_RightStick_DPadNorth = 91,
		PS4_RightStick_DPadSouth = 92,
		PS4_RightStick_DPadWest = 93,
		PS4_RightStick_DPadEast = 94,
		PS4_DPad_North = 95,
		PS4_DPad_South = 96,
		PS4_DPad_West = 97,
		PS4_DPad_East = 98,
		PS4_Gyro_Move = 99,
		PS4_Gyro_Pitch = 100,
		PS4_Gyro_Yaw = 101,
		PS4_Gyro_Roll = 102,
		PS4_DPad_Move = 103,
		PS4_Reserved1 = 104,
		PS4_Reserved2 = 105,
		PS4_Reserved3 = 106,
		PS4_Reserved4 = 107,
		PS4_Reserved5 = 108,
		PS4_Reserved6 = 109,
		PS4_Reserved7 = 110,
		PS4_Reserved8 = 111,
		PS4_Reserved9 = 112,
		PS4_Reserved10 = 113,
		XBoxOne_A = 114,
		XBoxOne_B = 115,
		XBoxOne_X = 116,
		XBoxOne_Y = 117,
		XBoxOne_LeftBumper = 118,
		XBoxOne_RightBumper = 119,
		XBoxOne_Menu = 120,
		XBoxOne_View = 121,
		XBoxOne_LeftTrigger_Pull = 122,
		XBoxOne_LeftTrigger_Click = 123,
		XBoxOne_RightTrigger_Pull = 124,
		XBoxOne_RightTrigger_Click = 125,
		XBoxOne_LeftStick_Move = 126,
		XBoxOne_LeftStick_Click = 127,
		XBoxOne_LeftStick_DPadNorth = 128,
		XBoxOne_LeftStick_DPadSouth = 129,
		XBoxOne_LeftStick_DPadWest = 130,
		XBoxOne_LeftStick_DPadEast = 131,
		XBoxOne_RightStick_Move = 132,
		XBoxOne_RightStick_Click = 133,
		XBoxOne_RightStick_DPadNorth = 134,
		XBoxOne_RightStick_DPadSouth = 135,
		XBoxOne_RightStick_DPadWest = 136,
		XBoxOne_RightStick_DPadEast = 137,
		XBoxOne_DPad_North = 138,
		XBoxOne_DPad_South = 139,
		XBoxOne_DPad_West = 140,
		XBoxOne_DPad_East = 141,
		XBoxOne_DPad_Move = 142,
		XBoxOne_Reserved1 = 143,
		XBoxOne_Reserved2 = 144,
		XBoxOne_Reserved3 = 145,
		XBoxOne_Reserved4 = 146,
		XBoxOne_Reserved5 = 147,
		XBoxOne_Reserved6 = 148,
		XBoxOne_Reserved7 = 149,
		XBoxOne_Reserved8 = 150,
		XBoxOne_Reserved9 = 151,
		XBoxOne_Reserved10 = 152,
		XBox360_A = 153,
		XBox360_B = 154,
		XBox360_X = 155,
		XBox360_Y = 156,
		XBox360_LeftBumper = 157,
		XBox360_RightBumper = 158,
		XBox360_Start = 159,
		XBox360_Back = 160,
		XBox360_LeftTrigger_Pull = 161,
		XBox360_LeftTrigger_Click = 162,
		XBox360_RightTrigger_Pull = 163,
		XBox360_RightTrigger_Click = 164,
		XBox360_LeftStick_Move = 165,
		XBox360_LeftStick_Click = 166,
		XBox360_LeftStick_DPadNorth = 167,
		XBox360_LeftStick_DPadSouth = 168,
		XBox360_LeftStick_DPadWest = 169,
		XBox360_LeftStick_DPadEast = 170,
		XBox360_RightStick_Move = 171,
		XBox360_RightStick_Click = 172,
		XBox360_RightStick_DPadNorth = 173,
		XBox360_RightStick_DPadSouth = 174,
		XBox360_RightStick_DPadWest = 175,
		XBox360_RightStick_DPadEast = 176,
		XBox360_DPad_North = 177,
		XBox360_DPad_South = 178,
		XBox360_DPad_West = 179,
		XBox360_DPad_East = 180,
		XBox360_DPad_Move = 181,
		XBox360_Reserved1 = 182,
		XBox360_Reserved2 = 183,
		XBox360_Reserved3 = 184,
		XBox360_Reserved4 = 185,
		XBox360_Reserved5 = 186,
		XBox360_Reserved6 = 187,
		XBox360_Reserved7 = 188,
		XBox360_Reserved8 = 189,
		XBox360_Reserved9 = 190,
		XBox360_Reserved10 = 191,
		Switch_A = 192,
		Switch_B = 193,
		Switch_X = 194,
		Switch_Y = 195,
		Switch_LeftBumper = 196,
		Switch_RightBumper = 197,
		Switch_Plus = 198,
		Switch_Minus = 199,
		Switch_Capture = 200,
		Switch_LeftTrigger_Pull = 201,
		Switch_LeftTrigger_Click = 202,
		Switch_RightTrigger_Pull = 203,
		Switch_RightTrigger_Click = 204,
		Switch_LeftStick_Move = 205,
		Switch_LeftStick_Click = 206,
		Switch_LeftStick_DPadNorth = 207,
		Switch_LeftStick_DPadSouth = 208,
		Switch_LeftStick_DPadWest = 209,
		Switch_LeftStick_DPadEast = 210,
		Switch_RightStick_Move = 211,
		Switch_RightStick_Click = 212,
		Switch_RightStick_DPadNorth = 213,
		Switch_RightStick_DPadSouth = 214,
		Switch_RightStick_DPadWest = 215,
		Switch_RightStick_DPadEast = 216,
		Switch_DPad_North = 217,
		Switch_DPad_South = 218,
		Switch_DPad_West = 219,
		Switch_DPad_East = 220,
		Switch_ProGyro_Move = 221,
		Switch_ProGyro_Pitch = 222,
		Switch_ProGyro_Yaw = 223,
		Switch_ProGyro_Roll = 224,
		Switch_DPad_Move = 225,
		Switch_Reserved1 = 226,
		Switch_Reserved2 = 227,
		Switch_Reserved3 = 228,
		Switch_Reserved4 = 229,
		Switch_Reserved5 = 230,
		Switch_Reserved6 = 231,
		Switch_Reserved7 = 232,
		Switch_Reserved8 = 233,
		Switch_Reserved9 = 234,
		Switch_Reserved10 = 235,
		Switch_RightGyro_Move = 236,
		Switch_RightGyro_Pitch = 237,
		Switch_RightGyro_Yaw = 238,
		Switch_RightGyro_Roll = 239,
		Switch_LeftGyro_Move = 240,
		Switch_LeftGyro_Pitch = 241,
		Switch_LeftGyro_Yaw = 242,
		Switch_LeftGyro_Roll = 243,
		Switch_LeftGrip_Lower = 244,
		Switch_LeftGrip_Upper = 245,
		Switch_RightGrip_Lower = 246,
		Switch_RightGrip_Upper = 247,
		Switch_Reserved11 = 248,
		Switch_Reserved12 = 249,
		Switch_Reserved13 = 250,
		Switch_Reserved14 = 251,
		Switch_Reserved15 = 252,
		Switch_Reserved16 = 253,
		Switch_Reserved17 = 254,
		Switch_Reserved18 = 255,
		Switch_Reserved19 = 256,
		Switch_Reserved20 = 257,
		Count = 258,
		MaximumPossibleValue = 32767,
	}
	
	//
	// EXboxOrigin
	//
	internal enum XboxOrigin : int
	{
		A = 0,
		B = 1,
		X = 2,
		Y = 3,
		LeftBumper = 4,
		RightBumper = 5,
		Menu = 6,
		View = 7,
		LeftTrigger_Pull = 8,
		LeftTrigger_Click = 9,
		RightTrigger_Pull = 10,
		RightTrigger_Click = 11,
		LeftStick_Move = 12,
		LeftStick_Click = 13,
		LeftStick_DPadNorth = 14,
		LeftStick_DPadSouth = 15,
		LeftStick_DPadWest = 16,
		LeftStick_DPadEast = 17,
		RightStick_Move = 18,
		RightStick_Click = 19,
		RightStick_DPadNorth = 20,
		RightStick_DPadSouth = 21,
		RightStick_DPadWest = 22,
		RightStick_DPadEast = 23,
		DPad_North = 24,
		DPad_South = 25,
		DPad_West = 26,
		DPad_East = 27,
		Count = 28,
	}
	
	//
	// ESteamControllerPad
	//
	internal enum SteamControllerPad : int
	{
		Left = 0,
		Right = 1,
	}
	
	//
	// ESteamInputType
	//
	public enum InputType : int
	{
		Unknown = 0,
		SteamController = 1,
		XBox360Controller = 2,
		XBoxOneController = 3,
		GenericGamepad = 4,
		PS4Controller = 5,
		AppleMFiController = 6,
		AndroidController = 7,
		SwitchJoyConPair = 8,
		SwitchJoyConSingle = 9,
		SwitchProController = 10,
		MobileTouch = 11,
		PS3Controller = 12,
		Count = 13,
		MaximumPossibleValue = 255,
	}
	
	//
	// ESteamInputLEDFlag
	//
	internal enum SteamInputLEDFlag : int
	{
		SetColor = 0,
		RestoreUserDefault = 1,
	}
	
	//
	// EControllerActionOrigin
	//
	internal enum ControllerActionOrigin : int
	{
		None = 0,
		A = 1,
		B = 2,
		X = 3,
		Y = 4,
		LeftBumper = 5,
		RightBumper = 6,
		LeftGrip = 7,
		RightGrip = 8,
		Start = 9,
		Back = 10,
		LeftPad_Touch = 11,
		LeftPad_Swipe = 12,
		LeftPad_Click = 13,
		LeftPad_DPadNorth = 14,
		LeftPad_DPadSouth = 15,
		LeftPad_DPadWest = 16,
		LeftPad_DPadEast = 17,
		RightPad_Touch = 18,
		RightPad_Swipe = 19,
		RightPad_Click = 20,
		RightPad_DPadNorth = 21,
		RightPad_DPadSouth = 22,
		RightPad_DPadWest = 23,
		RightPad_DPadEast = 24,
		LeftTrigger_Pull = 25,
		LeftTrigger_Click = 26,
		RightTrigger_Pull = 27,
		RightTrigger_Click = 28,
		LeftStick_Move = 29,
		LeftStick_Click = 30,
		LeftStick_DPadNorth = 31,
		LeftStick_DPadSouth = 32,
		LeftStick_DPadWest = 33,
		LeftStick_DPadEast = 34,
		Gyro_Move = 35,
		Gyro_Pitch = 36,
		Gyro_Yaw = 37,
		Gyro_Roll = 38,
		PS4_X = 39,
		PS4_Circle = 40,
		PS4_Triangle = 41,
		PS4_Square = 42,
		PS4_LeftBumper = 43,
		PS4_RightBumper = 44,
		PS4_Options = 45,
		PS4_Share = 46,
		PS4_LeftPad_Touch = 47,
		PS4_LeftPad_Swipe = 48,
		PS4_LeftPad_Click = 49,
		PS4_LeftPad_DPadNorth = 50,
		PS4_LeftPad_DPadSouth = 51,
		PS4_LeftPad_DPadWest = 52,
		PS4_LeftPad_DPadEast = 53,
		PS4_RightPad_Touch = 54,
		PS4_RightPad_Swipe = 55,
		PS4_RightPad_Click = 56,
		PS4_RightPad_DPadNorth = 57,
		PS4_RightPad_DPadSouth = 58,
		PS4_RightPad_DPadWest = 59,
		PS4_RightPad_DPadEast = 60,
		PS4_CenterPad_Touch = 61,
		PS4_CenterPad_Swipe = 62,
		PS4_CenterPad_Click = 63,
		PS4_CenterPad_DPadNorth = 64,
		PS4_CenterPad_DPadSouth = 65,
		PS4_CenterPad_DPadWest = 66,
		PS4_CenterPad_DPadEast = 67,
		PS4_LeftTrigger_Pull = 68,
		PS4_LeftTrigger_Click = 69,
		PS4_RightTrigger_Pull = 70,
		PS4_RightTrigger_Click = 71,
		PS4_LeftStick_Move = 72,
		PS4_LeftStick_Click = 73,
		PS4_LeftStick_DPadNorth = 74,
		PS4_LeftStick_DPadSouth = 75,
		PS4_LeftStick_DPadWest = 76,
		PS4_LeftStick_DPadEast = 77,
		PS4_RightStick_Move = 78,
		PS4_RightStick_Click = 79,
		PS4_RightStick_DPadNorth = 80,
		PS4_RightStick_DPadSouth = 81,
		PS4_RightStick_DPadWest = 82,
		PS4_RightStick_DPadEast = 83,
		PS4_DPad_North = 84,
		PS4_DPad_South = 85,
		PS4_DPad_West = 86,
		PS4_DPad_East = 87,
		PS4_Gyro_Move = 88,
		PS4_Gyro_Pitch = 89,
		PS4_Gyro_Yaw = 90,
		PS4_Gyro_Roll = 91,
		XBoxOne_A = 92,
		XBoxOne_B = 93,
		XBoxOne_X = 94,
		XBoxOne_Y = 95,
		XBoxOne_LeftBumper = 96,
		XBoxOne_RightBumper = 97,
		XBoxOne_Menu = 98,
		XBoxOne_View = 99,
		XBoxOne_LeftTrigger_Pull = 100,
		XBoxOne_LeftTrigger_Click = 101,
		XBoxOne_RightTrigger_Pull = 102,
		XBoxOne_RightTrigger_Click = 103,
		XBoxOne_LeftStick_Move = 104,
		XBoxOne_LeftStick_Click = 105,
		XBoxOne_LeftStick_DPadNorth = 106,
		XBoxOne_LeftStick_DPadSouth = 107,
		XBoxOne_LeftStick_DPadWest = 108,
		XBoxOne_LeftStick_DPadEast = 109,
		XBoxOne_RightStick_Move = 110,
		XBoxOne_RightStick_Click = 111,
		XBoxOne_RightStick_DPadNorth = 112,
		XBoxOne_RightStick_DPadSouth = 113,
		XBoxOne_RightStick_DPadWest = 114,
		XBoxOne_RightStick_DPadEast = 115,
		XBoxOne_DPad_North = 116,
		XBoxOne_DPad_South = 117,
		XBoxOne_DPad_West = 118,
		XBoxOne_DPad_East = 119,
		XBox360_A = 120,
		XBox360_B = 121,
		XBox360_X = 122,
		XBox360_Y = 123,
		XBox360_LeftBumper = 124,
		XBox360_RightBumper = 125,
		XBox360_Start = 126,
		XBox360_Back = 127,
		XBox360_LeftTrigger_Pull = 128,
		XBox360_LeftTrigger_Click = 129,
		XBox360_RightTrigger_Pull = 130,
		XBox360_RightTrigger_Click = 131,
		XBox360_LeftStick_Move = 132,
		XBox360_LeftStick_Click = 133,
		XBox360_LeftStick_DPadNorth = 134,
		XBox360_LeftStick_DPadSouth = 135,
		XBox360_LeftStick_DPadWest = 136,
		XBox360_LeftStick_DPadEast = 137,
		XBox360_RightStick_Move = 138,
		XBox360_RightStick_Click = 139,
		XBox360_RightStick_DPadNorth = 140,
		XBox360_RightStick_DPadSouth = 141,
		XBox360_RightStick_DPadWest = 142,
		XBox360_RightStick_DPadEast = 143,
		XBox360_DPad_North = 144,
		XBox360_DPad_South = 145,
		XBox360_DPad_West = 146,
		XBox360_DPad_East = 147,
		SteamV2_A = 148,
		SteamV2_B = 149,
		SteamV2_X = 150,
		SteamV2_Y = 151,
		SteamV2_LeftBumper = 152,
		SteamV2_RightBumper = 153,
		SteamV2_LeftGrip_Lower = 154,
		SteamV2_LeftGrip_Upper = 155,
		SteamV2_RightGrip_Lower = 156,
		SteamV2_RightGrip_Upper = 157,
		SteamV2_LeftBumper_Pressure = 158,
		SteamV2_RightBumper_Pressure = 159,
		SteamV2_LeftGrip_Pressure = 160,
		SteamV2_RightGrip_Pressure = 161,
		SteamV2_LeftGrip_Upper_Pressure = 162,
		SteamV2_RightGrip_Upper_Pressure = 163,
		SteamV2_Start = 164,
		SteamV2_Back = 165,
		SteamV2_LeftPad_Touch = 166,
		SteamV2_LeftPad_Swipe = 167,
		SteamV2_LeftPad_Click = 168,
		SteamV2_LeftPad_Pressure = 169,
		SteamV2_LeftPad_DPadNorth = 170,
		SteamV2_LeftPad_DPadSouth = 171,
		SteamV2_LeftPad_DPadWest = 172,
		SteamV2_LeftPad_DPadEast = 173,
		SteamV2_RightPad_Touch = 174,
		SteamV2_RightPad_Swipe = 175,
		SteamV2_RightPad_Click = 176,
		SteamV2_RightPad_Pressure = 177,
		SteamV2_RightPad_DPadNorth = 178,
		SteamV2_RightPad_DPadSouth = 179,
		SteamV2_RightPad_DPadWest = 180,
		SteamV2_RightPad_DPadEast = 181,
		SteamV2_LeftTrigger_Pull = 182,
		SteamV2_LeftTrigger_Click = 183,
		SteamV2_RightTrigger_Pull = 184,
		SteamV2_RightTrigger_Click = 185,
		SteamV2_LeftStick_Move = 186,
		SteamV2_LeftStick_Click = 187,
		SteamV2_LeftStick_DPadNorth = 188,
		SteamV2_LeftStick_DPadSouth = 189,
		SteamV2_LeftStick_DPadWest = 190,
		SteamV2_LeftStick_DPadEast = 191,
		SteamV2_Gyro_Move = 192,
		SteamV2_Gyro_Pitch = 193,
		SteamV2_Gyro_Yaw = 194,
		SteamV2_Gyro_Roll = 195,
		Switch_A = 196,
		Switch_B = 197,
		Switch_X = 198,
		Switch_Y = 199,
		Switch_LeftBumper = 200,
		Switch_RightBumper = 201,
		Switch_Plus = 202,
		Switch_Minus = 203,
		Switch_Capture = 204,
		Switch_LeftTrigger_Pull = 205,
		Switch_LeftTrigger_Click = 206,
		Switch_RightTrigger_Pull = 207,
		Switch_RightTrigger_Click = 208,
		Switch_LeftStick_Move = 209,
		Switch_LeftStick_Click = 210,
		Switch_LeftStick_DPadNorth = 211,
		Switch_LeftStick_DPadSouth = 212,
		Switch_LeftStick_DPadWest = 213,
		Switch_LeftStick_DPadEast = 214,
		Switch_RightStick_Move = 215,
		Switch_RightStick_Click = 216,
		Switch_RightStick_DPadNorth = 217,
		Switch_RightStick_DPadSouth = 218,
		Switch_RightStick_DPadWest = 219,
		Switch_RightStick_DPadEast = 220,
		Switch_DPad_North = 221,
		Switch_DPad_South = 222,
		Switch_DPad_West = 223,
		Switch_DPad_East = 224,
		Switch_ProGyro_Move = 225,
		Switch_ProGyro_Pitch = 226,
		Switch_ProGyro_Yaw = 227,
		Switch_ProGyro_Roll = 228,
		Switch_RightGyro_Move = 229,
		Switch_RightGyro_Pitch = 230,
		Switch_RightGyro_Yaw = 231,
		Switch_RightGyro_Roll = 232,
		Switch_LeftGyro_Move = 233,
		Switch_LeftGyro_Pitch = 234,
		Switch_LeftGyro_Yaw = 235,
		Switch_LeftGyro_Roll = 236,
		Switch_LeftGrip_Lower = 237,
		Switch_LeftGrip_Upper = 238,
		Switch_RightGrip_Lower = 239,
		Switch_RightGrip_Upper = 240,
		PS4_DPad_Move = 241,
		XBoxOne_DPad_Move = 242,
		XBox360_DPad_Move = 243,
		Switch_DPad_Move = 244,
		Count = 245,
		MaximumPossibleValue = 32767,
	}
	
	//
	// ESteamControllerLEDFlag
	//
	internal enum SteamControllerLEDFlag : int
	{
		SetColor = 0,
		RestoreUserDefault = 1,
	}
	
	//
	// EUGCMatchingUGCType
	//
	public enum UgcType : int
	{
		Items = 0,
		Items_Mtx = 1,
		Items_ReadyToUse = 2,
		Collections = 3,
		Artwork = 4,
		Videos = 5,
		Screenshots = 6,
		AllGuides = 7,
		WebGuides = 8,
		IntegratedGuides = 9,
		UsableInGame = 10,
		ControllerBindings = 11,
		GameManagedItems = 12,
		All = -1,
	}
	
	//
	// EUserUGCList
	//
	internal enum UserUGCList : int
	{
		Published = 0,
		VotedOn = 1,
		VotedUp = 2,
		VotedDown = 3,
		WillVoteLater = 4,
		Favorited = 5,
		Subscribed = 6,
		UsedOrPlayed = 7,
		Followed = 8,
	}
	
	//
	// EUserUGCListSortOrder
	//
	internal enum UserUGCListSortOrder : int
	{
		CreationOrderDesc = 0,
		CreationOrderAsc = 1,
		TitleAsc = 2,
		LastUpdatedDesc = 3,
		SubscriptionDateDesc = 4,
		VoteScoreDesc = 5,
		ForModeration = 6,
	}
	
	//
	// EUGCQuery
	//
	internal enum UGCQuery : int
	{
		RankedByVote = 0,
		RankedByPublicationDate = 1,
		AcceptedForGameRankedByAcceptanceDate = 2,
		RankedByTrend = 3,
		FavoritedByFriendsRankedByPublicationDate = 4,
		CreatedByFriendsRankedByPublicationDate = 5,
		RankedByNumTimesReported = 6,
		CreatedByFollowedUsersRankedByPublicationDate = 7,
		NotYetRated = 8,
		RankedByTotalVotesAsc = 9,
		RankedByVotesUp = 10,
		RankedByTextSearch = 11,
		RankedByTotalUniqueSubscriptions = 12,
		RankedByPlaytimeTrend = 13,
		RankedByTotalPlaytime = 14,
		RankedByAveragePlaytimeTrend = 15,
		RankedByLifetimeAveragePlaytime = 16,
		RankedByPlaytimeSessionsTrend = 17,
		RankedByLifetimePlaytimeSessions = 18,
	}
	
	//
	// EItemUpdateStatus
	//
	internal enum ItemUpdateStatus : int
	{
		Invalid = 0,
		PreparingConfig = 1,
		PreparingContent = 2,
		UploadingContent = 3,
		UploadingPreviewFile = 4,
		CommittingChanges = 5,
	}
	
	//
	// EItemState
	//
	internal enum ItemState : int
	{
		None = 0,
		Subscribed = 1,
		LegacyItem = 2,
		Installed = 4,
		NeedsUpdate = 8,
		Downloading = 16,
		DownloadPending = 32,
	}
	
	//
	// EItemStatistic
	//
	internal enum ItemStatistic : int
	{
		NumSubscriptions = 0,
		NumFavorites = 1,
		NumFollowers = 2,
		NumUniqueSubscriptions = 3,
		NumUniqueFavorites = 4,
		NumUniqueFollowers = 5,
		NumUniqueWebsiteViews = 6,
		ReportScore = 7,
		NumSecondsPlayed = 8,
		NumPlaytimeSessions = 9,
		NumComments = 10,
		NumSecondsPlayedDuringTimePeriod = 11,
		NumPlaytimeSessionsDuringTimePeriod = 12,
	}
	
	//
	// EItemPreviewType
	//
	internal enum ItemPreviewType : int
	{
		Image = 0,
		YouTubeVideo = 1,
		Sketchfab = 2,
		EnvironmentMap_HorizontalCross = 3,
		EnvironmentMap_LatLong = 4,
		ReservedMax = 255,
	}
	
	//
	// ESteamItemFlags
	//
	internal enum SteamItemFlags : int
	{
		NoTrade = 1,
		Removed = 256,
		Consumed = 512,
	}
	
	//
	// ESteamTVRegionBehavior
	//
	internal enum SteamTVRegionBehavior : int
	{
		Invalid = -1,
		Hover = 0,
		ClickPopup = 1,
		ClickSurroundingRegion = 2,
	}
	
	//
	// EParentalFeature
	//
	public enum ParentalFeature : int
	{
		Invalid = 0,
		Store = 1,
		Community = 2,
		Profile = 3,
		Friends = 4,
		News = 5,
		Trading = 6,
		Settings = 7,
		Console = 8,
		Browser = 9,
		ParentalSetup = 10,
		Library = 11,
		Test = 12,
		SiteLicense = 13,
		Max = 14,
	}
	
	//
	// ESteamDeviceFormFactor
	//
	public enum SteamDeviceFormFactor : int
	{
		Unknown = 0,
		Phone = 1,
		Tablet = 2,
		Computer = 3,
		TV = 4,
	}
	
	//
	// ESteamNetworkingAvailability
	//
	public enum SteamNetworkingAvailability : int
	{
		CannotTry = -102,
		Failed = -101,
		Previously = -100,
		Retrying = -10,
		NeverTried = 1,
		Waiting = 2,
		Attempting = 3,
		Current = 100,
		Unknown = 0,
		Force32bit = 2147483647,
	}
	
	//
	// ESteamNetworkingIdentityType
	//
	internal enum NetIdentityType : int
	{
		Invalid = 0,
		SteamID = 16,
		XboxPairwiseID = 17,
		IPAddress = 1,
		GenericString = 2,
		GenericBytes = 3,
		UnknownType = 4,
		Force32bit = 2147483647,
	}
	
	//
	// ESteamNetworkingConnectionState
	//
	public enum ConnectionState : int
	{
		None = 0,
		Connecting = 1,
		FindingRoute = 2,
		Connected = 3,
		ClosedByPeer = 4,
		ProblemDetectedLocally = 5,
		FinWait = -1,
		Linger = -2,
		Dead = -3,
	}
	
	//
	// ESteamNetConnectionEnd
	//
	public enum NetConnectionEnd : int
	{
		Invalid = 0,
		App_Min = 1000,
		App_Generic = 1000,
		App_Max = 1999,
		AppException_Min = 2000,
		AppException_Generic = 2000,
		AppException_Max = 2999,
		Local_Min = 3000,
		Local_OfflineMode = 3001,
		Local_ManyRelayConnectivity = 3002,
		Local_HostedServerPrimaryRelay = 3003,
		Local_NetworkConfig = 3004,
		Local_Rights = 3005,
		Local_Max = 3999,
		Remote_Min = 4000,
		Remote_Timeout = 4001,
		Remote_BadCrypt = 4002,
		Remote_BadCert = 4003,
		Remote_NotLoggedIn = 4004,
		Remote_NotRunningApp = 4005,
		Remote_BadProtocolVersion = 4006,
		Remote_Max = 4999,
		Misc_Min = 5000,
		Misc_Generic = 5001,
		Misc_InternalError = 5002,
		Misc_Timeout = 5003,
		Misc_RelayConnectivity = 5004,
		Misc_SteamConnectivity = 5005,
		Misc_NoRelaySessionsToClient = 5006,
		Misc_Max = 5999,
	}
	
	//
	// ESteamNetworkingConfigScope
	//
	internal enum NetConfigScope : int
	{
		Global = 1,
		SocketsInterface = 2,
		ListenSocket = 3,
		Connection = 4,
	}
	
	//
	// ESteamNetworkingConfigDataType
	//
	internal enum NetConfigType : int
	{
		Int32 = 1,
		Int64 = 2,
		Float = 3,
		String = 4,
		FunctionPtr = 5,
	}
	
	//
	// ESteamNetworkingConfigValue
	//
	internal enum NetConfig : int
	{
		Invalid = 0,
		FakePacketLoss_Send = 2,
		FakePacketLoss_Recv = 3,
		FakePacketLag_Send = 4,
		FakePacketLag_Recv = 5,
		FakePacketReorder_Send = 6,
		FakePacketReorder_Recv = 7,
		FakePacketReorder_Time = 8,
		FakePacketDup_Send = 26,
		FakePacketDup_Recv = 27,
		FakePacketDup_TimeMax = 28,
		TimeoutInitial = 24,
		TimeoutConnected = 25,
		SendBufferSize = 9,
		SendRateMin = 10,
		SendRateMax = 11,
		NagleTime = 12,
		IP_AllowWithoutAuth = 23,
		MTU_PacketSize = 32,
		MTU_DataSize = 33,
		Unencrypted = 34,
		EnumerateDevVars = 35,
		SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		SDRClient_ConsecutitivePingTimeoutsFail = 20,
		SDRClient_MinPingsBeforePingAccurate = 21,
		SDRClient_SingleSocket = 22,
		SDRClient_ForceRelayCluster = 29,
		SDRClient_DebugTicketAddress = 30,
		SDRClient_ForceProxyAddr = 31,
		SDRClient_FakeClusterPing = 36,
		LogLevel_AckRTT = 13,
		LogLevel_PacketDecode = 14,
		LogLevel_Message = 15,
		LogLevel_PacketGaps = 16,
		LogLevel_P2PRendezvous = 17,
		LogLevel_SDRRelayPings = 18,
	}
	
	//
	// ESteamNetworkingGetConfigValueResult
	//
	internal enum NetConfigResult : int
	{
		BadValue = -1,
		BadScopeObj = -2,
		BufferTooSmall = -3,
		OK = 1,
		OKInherited = 2,
	}
	
	//
	// ESteamNetworkingSocketsDebugOutputType
	//
	public enum NetDebugOutput : int
	{
		None = 0,
		Bug = 1,
		Error = 2,
		Important = 3,
		Warning = 4,
		Msg = 5,
		Verbose = 6,
		Debug = 7,
		Everything = 8,
	}
	
	//
	// EServerMode
	//
	internal enum ServerMode : int
	{
		Invalid = 0,
		NoAuthentication = 1,
		Authentication = 2,
		AuthenticationAndSecure = 3,
	}
	
}

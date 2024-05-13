; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [393 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [780 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 6259146, ; 1: Nito.Cancellation => 0x5f81ca => 242
	i32 10166715, ; 2: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 3: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 26230656, ; 4: Microsoft.Extensions.DependencyModel => 0x1903f80 => 202
	i32 32687329, ; 5: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 298
	i32 34715100, ; 6: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 332
	i32 34839235, ; 7: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 38948123, ; 8: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 340
	i32 39109920, ; 9: Newtonsoft.Json.dll => 0x254c520 => 235
	i32 39485524, ; 10: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 39908443, ; 11: Domain.WordModels.dll => 0x260f45b => 378
	i32 42244203, ; 12: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 349
	i32 42639949, ; 13: System.Threading.Thread => 0x28aa24d => 145
	i32 52239042, ; 14: HtmlAgilityPack => 0x31d1ac2 => 184
	i32 66541672, ; 15: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 16: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 373
	i32 68219467, ; 17: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 18: Microsoft.Maui.Graphics.dll => 0x44bb714 => 217
	i32 82292897, ; 19: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 83839681, ; 20: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 357
	i32 98325684, ; 21: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 204
	i32 101534019, ; 22: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 316
	i32 117431740, ; 23: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120482276, ; 24: Microsoft.Recognizers.Text.DateTime.dll => 0x72e69e4 => 231
	i32 120558881, ; 25: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 316
	i32 122350210, ; 26: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 127363243, ; 27: ICSharpCode.SharpZipLib => 0x79768ab => 249
	i32 134690465, ; 28: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 336
	i32 136584136, ; 29: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 372
	i32 140062828, ; 30: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 365
	i32 142721839, ; 31: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 32: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 33: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 34: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 272
	i32 176265551, ; 35: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 36: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 318
	i32 184328833, ; 37: System.ValueTuple.dll => 0xafca281 => 151
	i32 205061960, ; 38: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 39: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 270
	i32 217737068, ; 40: WebSources.CambridgeDictionary => 0xcfa676c => 385
	i32 220171995, ; 41: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 221958352, ; 42: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 203
	i32 230216969, ; 43: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 292
	i32 230752869, ; 44: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 45: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 46: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 47: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 48: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 275
	i32 276479776, ; 49: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 50: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 294
	i32 280482487, ; 51: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 291
	i32 281658484, ; 52: Microsoft.Recognizers.Text.dll => 0x10c9c474 => 229
	i32 291076382, ; 53: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 291275502, ; 54: Microsoft.Extensions.Http.dll => 0x115c82ee => 205
	i32 298918909, ; 55: System.Net.Ping.dll => 0x11d123fd => 69
	i32 305710573, ; 56: Microsoft.ML.CpuMath.dll => 0x1238c5ed => 225
	i32 317674968, ; 57: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 370
	i32 318968648, ; 58: Xamarin.AndroidX.Activity.dll => 0x13031348 => 261
	i32 321597661, ; 59: System.Numerics => 0x132b30dd => 83
	i32 321963286, ; 60: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 348
	i32 342366114, ; 61: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 293
	i32 347068432, ; 62: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 252
	i32 350420333, ; 63: FluentValidation.DependencyInjectionExtensions => 0x14e2fd6d => 182
	i32 360082299, ; 64: System.ServiceModel.Web => 0x15766b7b => 131
	i32 366258070, ; 65: Tools.Common => 0x15d4a796 => 381
	i32 367780167, ; 66: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 67: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 68: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 69: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 70: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 71: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395382782, ; 72: Nito.AsyncEx.Tasks => 0x17910ffe => 241
	i32 395744057, ; 73: _Microsoft.Android.Resource.Designer => 0x17969339 => 389
	i32 403441872, ; 74: WindowsBase => 0x180c08d0 => 165
	i32 403493213, ; 75: MediatR.Contracts => 0x180cd15d => 186
	i32 409257351, ; 76: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 368
	i32 410091291, ; 77: WebSources.CambridgeDictionary.dll => 0x18717f1b => 385
	i32 437487038, ; 78: Microsoft.Recognizers.Definitions => 0x1a1385be => 228
	i32 441335492, ; 79: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 276
	i32 442565967, ; 80: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 81: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 289
	i32 451504562, ; 82: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 83: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 84: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 85: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 86: System.dll => 0x1bff388e => 164
	i32 476646585, ; 87: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 291
	i32 483909602, ; 88: Catalyst.dll => 0x1cd7dfe2 => 174
	i32 486930444, ; 89: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 304
	i32 489220957, ; 90: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 346
	i32 498788369, ; 91: System.ObjectModel => 0x1dbae811 => 84
	i32 513247710, ; 92: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 211
	i32 520409316, ; 93: Domain.Common.dll => 0x1f04d0e4 => 375
	i32 526420162, ; 94: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 95: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 336
	i32 530272170, ; 96: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 538707440, ; 97: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 367
	i32 539058512, ; 98: Microsoft.Extensions.Logging => 0x20216150 => 206
	i32 540030774, ; 99: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 100: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 101: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 548916678, ; 102: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 189
	i32 549171840, ; 103: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 104: Jsr305Binding => 0x213954e7 => 329
	i32 566397485, ; 105: Nito.AsyncEx.Interop.WaitHandles.dll => 0x21c28a2d => 239
	i32 567788613, ; 106: NickBuhro.Translit => 0x21d7c445 => 236
	i32 569601784, ; 107: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 327
	i32 577335427, ; 108: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 591152530, ; 109: Catalyst.Models.English => 0x233c4592 => 175
	i32 597488923, ; 110: CommunityToolkit.Maui => 0x239cf51b => 177
	i32 601371474, ; 111: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 112: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 113: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 622817381, ; 114: System.Numerics.Tensors => 0x251f7065 => 256
	i32 627609679, ; 115: Xamarin.AndroidX.CustomView => 0x2568904f => 281
	i32 627931235, ; 116: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 359
	i32 639457795, ; 117: Tools.Http => 0x261d5a03 => 382
	i32 639843206, ; 118: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 287
	i32 643868501, ; 119: System.Net => 0x2660a755 => 81
	i32 662205335, ; 120: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 121: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 323
	i32 666292255, ; 122: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 268
	i32 672442732, ; 123: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 124: System.Net.Security => 0x28bdabca => 73
	i32 690569205, ; 125: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 126: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 338
	i32 693804605, ; 127: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 128: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 129: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 333
	i32 700358131, ; 130: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 711994786, ; 131: MessagePack.Annotations.dll => 0x2a702da2 => 188
	i32 719641779, ; 132: Application.Common.dll => 0x2ae4dcb3 => 374
	i32 720511267, ; 133: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 337
	i32 722857257, ; 134: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 731701662, ; 135: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 210
	i32 735137430, ; 136: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 746488404, ; 137: FluentValidation.DependencyInjectionExtensions.dll => 0x2c7e8254 => 182
	i32 748832960, ; 138: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 250
	i32 752232764, ; 139: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 140: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 258
	i32 759454413, ; 141: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 142: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 772964186, ; 143: Microsoft.NET.StringTools => 0x2e127f5a => 227
	i32 775507847, ; 144: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 145: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 365
	i32 781953439, ; 146: Mosaik.Core.dll => 0x2e9ba99f => 234
	i32 789151979, ; 147: Microsoft.Extensions.Options => 0x2f0980eb => 209
	i32 790371945, ; 148: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 282
	i32 790760699, ; 149: Catalyst.Models.English.dll => 0x2f220cfb => 175
	i32 804715423, ; 150: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 151: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 296
	i32 821261248, ; 152: Domain.WordModels => 0x30f373c0 => 378
	i32 823281589, ; 153: System.Private.Uri.dll => 0x311247b5 => 86
	i32 828617472, ; 154: MessagePack => 0x3163b300 => 187
	i32 830298997, ; 155: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 156: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 157: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 158: Xamarin.AndroidX.Print => 0x3246f6cd => 309
	i32 848419076, ; 159: Microsoft.ML.DataView => 0x3291d904 => 226
	i32 869139383, ; 160: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 350
	i32 873119928, ; 161: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 162: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 163: System.Net.Http.Json => 0x3463c971 => 63
	i32 880668424, ; 164: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 364
	i32 883672651, ; 165: Domain.Localization.dll => 0x34abc64b => 376
	i32 898440345, ; 166: CsvHelper => 0x358d1c99 => 180
	i32 904024072, ; 167: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 168: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 918734561, ; 169: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 361
	i32 928116545, ; 170: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 332
	i32 933403136, ; 171: Microsoft.ML.KMeansClustering => 0x37a29a00 => 220
	i32 952186615, ; 172: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 955402788, ; 173: Newtonsoft.Json => 0x38f24a24 => 235
	i32 956575887, ; 174: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 337
	i32 961460050, ; 175: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 354
	i32 966729478, ; 176: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 330
	i32 967690846, ; 177: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 293
	i32 975236339, ; 178: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 179: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 180: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 181: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 990727110, ; 182: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 363
	i32 992768348, ; 183: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 184: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 185: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1011129461, ; 186: Tools.Http.dll => 0x3c449c75 => 382
	i32 1012816738, ; 187: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 313
	i32 1014992539, ; 188: Domain.WebSources.dll => 0x3c7f8e9b => 377
	i32 1019214401, ; 189: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 190: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 201
	i32 1031528504, ; 191: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 331
	i32 1035644815, ; 192: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 266
	i32 1036536393, ; 193: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1043796917, ; 194: Nito.Disposables => 0x3e3713b5 => 244
	i32 1043950537, ; 195: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 344
	i32 1044663988, ; 196: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1048992957, ; 197: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 204
	i32 1052210849, ; 198: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 300
	i32 1059984159, ; 199: Microsoft.ML.dll => 0x3f2e131f => 224
	i32 1067306892, ; 200: GoogleGson => 0x3f9dcf8c => 183
	i32 1082857460, ; 201: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 202: Xamarin.Kotlin.StdLib => 0x409e66d8 => 334
	i32 1098259244, ; 203: System => 0x41761b2c => 164
	i32 1108272742, ; 204: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 366
	i32 1117529484, ; 205: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 360
	i32 1118262833, ; 206: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 356
	i32 1121599056, ; 207: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 299
	i32 1127624469, ; 208: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 208
	i32 1145085672, ; 209: System.Linq.Async.dll => 0x44409ee8 => 255
	i32 1149092582, ; 210: Xamarin.AndroidX.Window => 0x447dc2e6 => 326
	i32 1157931901, ; 211: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 192
	i32 1168523401, ; 212: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 362
	i32 1170634674, ; 213: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 214: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 322
	i32 1178241025, ; 215: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 307
	i32 1186543882, ; 216: Infrastructure.Data => 0x46b9390a => 379
	i32 1199680934, ; 217: Microsoft.ML.PCA.dll => 0x4781ada6 => 221
	i32 1202000627, ; 218: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 192
	i32 1204270330, ; 219: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 268
	i32 1204575371, ; 220: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 196
	i32 1208641965, ; 221: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1214827643, ; 222: CommunityToolkit.Mvvm => 0x4868cc7b => 179
	i32 1219128291, ; 223: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1243150071, ; 224: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 327
	i32 1253011324, ; 225: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 226: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 342
	i32 1264511973, ; 227: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 317
	i32 1267360935, ; 228: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 321
	i32 1273260888, ; 229: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 273
	i32 1275534314, ; 230: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 338
	i32 1278448581, ; 231: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 265
	i32 1292207520, ; 232: SQLitePCLRaw.core.dll => 0x4d0585a0 => 251
	i32 1293217323, ; 233: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 284
	i32 1301589138, ; 234: Catalyst.Spacy => 0x4d94ac92 => 176
	i32 1308624726, ; 235: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 351
	i32 1309188875, ; 236: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 237: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 326
	i32 1324164729, ; 238: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 239: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1336711579, ; 240: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 371
	i32 1347751866, ; 241: Plugin.Maui.Audio => 0x50550fba => 245
	i32 1364015309, ; 242: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 243: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 372
	i32 1376866003, ; 244: Xamarin.AndroidX.SavedState => 0x52114ed3 => 313
	i32 1379779777, ; 245: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1385717816, ; 246: Microsoft.NET.StringTools.dll => 0x52986038 => 227
	i32 1402170036, ; 247: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 248: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 277
	i32 1408764838, ; 249: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 250: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1413052328, ; 251: Nito.AsyncEx.Context => 0x543977a8 => 237
	i32 1422545099, ; 252: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 253: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 340
	i32 1434145427, ; 254: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 255: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 330
	i32 1439761251, ; 256: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1445117038, ; 257: Microsoft.ML.StandardTrainers => 0x5622bc6e => 222
	i32 1452070440, ; 258: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 259: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 260: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 261: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461004990, ; 262: es\Microsoft.Maui.Controls.resources => 0x57152abe => 346
	i32 1461234159, ; 263: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 264: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 265: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 266: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 267
	i32 1470490898, ; 267: Microsoft.Extensions.Primitives => 0x57a5e912 => 211
	i32 1472315147, ; 268: Microsoft.ML.KMeansClustering.dll => 0x57c1bf0b => 220
	i32 1479771757, ; 269: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 270: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 271: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 272: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 314
	i32 1490351284, ; 273: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 190
	i32 1505131794, ; 274: Microsoft.Extensions.Http => 0x59b67d12 => 205
	i32 1525031578, ; 275: Python.Runtime.dll => 0x5ae6229a => 248
	i32 1526286932, ; 276: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 370
	i32 1536373174, ; 277: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 278: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 279: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 280: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1555438164, ; 281: Microsoft.ML.StandardTrainers.dll => 0x5cb61a54 => 222
	i32 1565862583, ; 282: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 283: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 284: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 285: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582372066, ; 286: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 283
	i32 1592978981, ; 287: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 288: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 331
	i32 1601112923, ; 289: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 290: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 291: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 292: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 303
	i32 1622358360, ; 293: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 294: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 325
	i32 1634654947, ; 295: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 178
	i32 1635184631, ; 296: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 287
	i32 1636350590, ; 297: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 280
	i32 1639515021, ; 298: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 299: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 300: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 301: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 302: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 319
	i32 1658251792, ; 303: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 328
	i32 1670060433, ; 304: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 275
	i32 1675553242, ; 305: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 306: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 307: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 308: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1688112883, ; 309: Microsoft.Data.Sqlite => 0x649e8ef3 => 190
	i32 1689493916, ; 310: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 191
	i32 1691477237, ; 311: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 312: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 313: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 335
	i32 1701541528, ; 314: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1706079114, ; 315: WebSources.ReversoContext => 0x65b0b38a => 388
	i32 1711441057, ; 316: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 252
	i32 1720223769, ; 317: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 296
	i32 1726116996, ; 318: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 319: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1728929640, ; 320: Microsoft.ML.Data.dll => 0x670d5f68 => 219
	i32 1729485958, ; 321: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 271
	i32 1743415430, ; 322: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 341
	i32 1744735666, ; 323: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 324: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 325: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 326: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 327: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 328: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 329: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 318
	i32 1770582343, ; 330: Microsoft.Extensions.Logging.dll => 0x6988f147 => 206
	i32 1776026572, ; 331: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 332: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 333: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1781418309, ; 334: AngleSharp => 0x6a2e4945 => 173
	i32 1782862114, ; 335: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 357
	i32 1788241197, ; 336: Xamarin.AndroidX.Fragment => 0x6a96652d => 289
	i32 1793755602, ; 337: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 349
	i32 1796167890, ; 338: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 189
	i32 1808609942, ; 339: Xamarin.AndroidX.Loader => 0x6bcd3296 => 303
	i32 1813058853, ; 340: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 334
	i32 1813201214, ; 341: Xamarin.Google.Android.Material => 0x6c13413e => 328
	i32 1818569960, ; 342: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 308
	i32 1818787751, ; 343: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 344: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 345: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 346: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 207
	i32 1832997650, ; 347: WebSources.Linker => 0x6d415312 => 387
	i32 1847515442, ; 348: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 258
	i32 1853025655, ; 349: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 366
	i32 1858542181, ; 350: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 351: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1875935024, ; 352: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 348
	i32 1879696579, ; 353: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1883079902, ; 354: MessagePack.dll => 0x703d84de => 187
	i32 1885316902, ; 355: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 269
	i32 1886040351, ; 356: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 194
	i32 1888955245, ; 357: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 358: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1893218855, ; 359: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 342
	i32 1898237753, ; 360: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 361: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1906351395, ; 362: Tools.Validations => 0x71a09d23 => 384
	i32 1910275211, ; 363: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1927897671, ; 364: System.CodeDom.dll => 0x72e96247 => 254
	i32 1939592360, ; 365: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1953182387, ; 366: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 353
	i32 1956758971, ; 367: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 368: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 315
	i32 1968388702, ; 369: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 197
	i32 1983156543, ; 370: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 335
	i32 1985761444, ; 371: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 260
	i32 1987243864, ; 372: FluentValidation => 0x7672ef58 => 181
	i32 2003115576, ; 373: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 345
	i32 2011961780, ; 374: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014489277, ; 375: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 194
	i32 2019465201, ; 376: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 300
	i32 2031763787, ; 377: Xamarin.Android.Glide => 0x791a414b => 257
	i32 2045470958, ; 378: System.Private.Xml => 0x79eb68ee => 88
	i32 2048278909, ; 379: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 199
	i32 2055239286, ; 380: Nito.Disposables.dll => 0x7a807676 => 244
	i32 2055257422, ; 381: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 295
	i32 2060060697, ; 382: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 383: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 344
	i32 2070888862, ; 384: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2078458103, ; 385: Nito.AsyncEx.Oop => 0x7be2c0f7 => 240
	i32 2079903147, ; 386: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 387: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2094405699, ; 388: System.Numerics.Tensors.dll => 0x7cd61843 => 256
	i32 2098472657, ; 389: Microsoft.Recognizers.Text.DataTypes.TimexExpression => 0x7d1426d1 => 230
	i32 2103459038, ; 390: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 253
	i32 2123549063, ; 391: Nito.AsyncEx.Coordination.dll => 0x7e92c987 => 238
	i32 2127167465, ; 392: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 393: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 394: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 395: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2148940882, ; 396: Apps.MauiRunner.dll => 0x80163c52 => 0
	i32 2149884392, ; 397: Python.Runtime => 0x8024a1e8 => 248
	i32 2159891885, ; 398: Microsoft.Maui => 0x80bd55ad => 215
	i32 2169148018, ; 399: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 352
	i32 2178612968, ; 400: System.CodeDom => 0x81dafee8 => 254
	i32 2181898931, ; 401: Microsoft.Extensions.Options.dll => 0x820d22b3 => 209
	i32 2192057212, ; 402: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 207
	i32 2193016926, ; 403: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2197979891, ; 404: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 202
	i32 2201107256, ; 405: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 339
	i32 2201231467, ; 406: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 407: it\Microsoft.Maui.Controls.resources => 0x839595db => 354
	i32 2217644978, ; 408: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 322
	i32 2222056684, ; 409: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 410: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 304
	i32 2252106437, ; 411: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2252897993, ; 412: Microsoft.EntityFrameworkCore => 0x86487ec9 => 191
	i32 2256313426, ; 413: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 414: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 415: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 198
	i32 2267693617, ; 416: Microsoft.ML.Transforms.dll => 0x872a4231 => 223
	i32 2267999099, ; 417: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 259
	i32 2275458144, ; 418: AngleSharp.dll => 0x87a0bc60 => 173
	i32 2276592402, ; 419: Microsoft.ML.Core => 0x87b20b12 => 218
	i32 2279755925, ; 420: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 311
	i32 2280430125, ; 421: Microsoft.ML.Transforms => 0x87ec9a2d => 223
	i32 2293034957, ; 422: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 423: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 424: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 425: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 358
	i32 2305521784, ; 426: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 427: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 263
	i32 2320631194, ; 428: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2329808537, ; 429: Nito.AsyncEx.Coordination => 0x8ade0e99 => 238
	i32 2331555068, ; 430: Nito.Cancellation.dll => 0x8af8b4fc => 242
	i32 2340441535, ; 431: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 432: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 433: System.Net.Primitives => 0x8c40e0db => 70
	i32 2366048013, ; 434: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 352
	i32 2368005991, ; 435: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2371007202, ; 436: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 197
	i32 2378619854, ; 437: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 438: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 439: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 353
	i32 2401565422, ; 440: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 441: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 286
	i32 2421380589, ; 442: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 443: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 273
	i32 2427813419, ; 444: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 350
	i32 2435356389, ; 445: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 446: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 447: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 448: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 449: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 450: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 250
	i32 2465532216, ; 451: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 276
	i32 2471841756, ; 452: netstandard.dll => 0x93554fdc => 167
	i32 2473157462, ; 453: Microsoft.ML.CpuMath => 0x93696356 => 225
	i32 2475788418, ; 454: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 455: Microsoft.Maui.Controls => 0x93dba8a1 => 213
	i32 2483903535, ; 456: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 457: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 458: System.AppContext.dll => 0x94798bc5 => 6
	i32 2495665806, ; 459: Application.Common => 0x94c0d68e => 374
	i32 2501346920, ; 460: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2503351294, ; 461: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 356
	i32 2505896520, ; 462: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 298
	i32 2514967922, ; 463: Domain.Localization => 0x95e75d72 => 376
	i32 2522472828, ; 464: Xamarin.Android.Glide.dll => 0x9659e17c => 257
	i32 2530453544, ; 465: Nito.AsyncEx.Context.dll => 0x96d3a828 => 237
	i32 2538310050, ; 466: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2549538471, ; 467: Microsoft.ML.Core.dll => 0x97f6dea7 => 218
	i32 2550873716, ; 468: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 351
	i32 2562349572, ; 469: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 470: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2576534780, ; 471: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 355
	i32 2579795415, ; 472: Domain.Common => 0x99c48dd7 => 375
	i32 2581783588, ; 473: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 299
	i32 2581819634, ; 474: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 321
	i32 2585220780, ; 475: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 476: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 477: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2593496499, ; 478: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 360
	i32 2605712449, ; 479: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 339
	i32 2615233544, ; 480: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 290
	i32 2615519321, ; 481: MediatR => 0x9be5a859 => 185
	i32 2616218305, ; 482: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 208
	i32 2617129537, ; 483: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 484: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 485: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 280
	i32 2624644809, ; 486: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 285
	i32 2626831493, ; 487: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 355
	i32 2627185994, ; 488: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 489: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 490: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 294
	i32 2634653062, ; 491: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 193
	i32 2643034788, ; 492: Microsoft.ML.Data => 0x9d8982a4 => 219
	i32 2663391936, ; 493: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 259
	i32 2663698177, ; 494: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 495: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 496: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 497: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2677943566, ; 498: MediatR.Contracts.dll => 0x9f9e2d0e => 186
	i32 2681855494, ; 499: Infrastructure.Data.dll => 0x9fd9de06 => 379
	i32 2686887180, ; 500: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 501: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 502: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 319
	i32 2715334215, ; 503: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 504: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 505: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2720107201, ; 506: Tools.Parsers.dll => 0xa2218ac1 => 383
	i32 2724373263, ; 507: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 508: Xamarin.AndroidX.Activity => 0xa2e0939b => 261
	i32 2733918439, ; 509: Microsoft.Recognizers.Text => 0xa2f448e7 => 229
	i32 2735172069, ; 510: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 511: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 267
	i32 2740698338, ; 512: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 341
	i32 2740948882, ; 513: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 514: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 515: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 361
	i32 2758225723, ; 516: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 214
	i32 2764765095, ; 517: Microsoft.Maui.dll => 0xa4caf7a7 => 215
	i32 2765824710, ; 518: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 519: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 333
	i32 2778768386, ; 520: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 324
	i32 2779977773, ; 521: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 312
	i32 2785988530, ; 522: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 367
	i32 2788224221, ; 523: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 290
	i32 2790245304, ; 524: Catalyst => 0xa64fc3b8 => 174
	i32 2800623833, ; 525: Microsoft.Recognizers.Definitions.dll => 0xa6ee20d9 => 228
	i32 2801831435, ; 526: Microsoft.Maui.Graphics => 0xa7008e0b => 217
	i32 2803228030, ; 527: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2810250172, ; 528: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 277
	i32 2818335264, ; 529: System.Linq.Async => 0xa7fc6220 => 255
	i32 2819470561, ; 530: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 531: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 532: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 312
	i32 2824502124, ; 533: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2838993487, ; 534: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 301
	i32 2847789619, ; 535: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 193
	i32 2849599387, ; 536: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 537: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 324
	i32 2855708567, ; 538: Xamarin.AndroidX.Transition => 0xaa36a797 => 320
	i32 2861098320, ; 539: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 540: Microsoft.Maui.Essentials => 0xaa8a4878 => 216
	i32 2868488919, ; 541: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 178
	i32 2870099610, ; 542: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 262
	i32 2875164099, ; 543: Jsr305Binding.dll => 0xab5f85c3 => 329
	i32 2875220617, ; 544: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 545: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 288
	i32 2887636118, ; 546: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 547: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 548: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 549: System.Reflection => 0xacf080de => 97
	i32 2904521939, ; 550: NickBuhro.Translit.dll => 0xad1f7cd3 => 236
	i32 2905242038, ; 551: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 552: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2916838712, ; 553: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 325
	i32 2919462931, ; 554: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 555: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 264
	i32 2923418970, ; 556: Nito.Collections.Deque.dll => 0xae3fd55a => 243
	i32 2933086635, ; 557: Nito.Collections.Deque => 0xaed359ab => 243
	i32 2936416060, ; 558: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 559: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 560: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 561: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2965891262, ; 562: WebSources.Common.dll => 0xb0c7e8be => 386
	i32 2968338931, ; 563: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2971004615, ; 564: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 210
	i32 2972252294, ; 565: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 566: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 284
	i32 2987532451, ; 567: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 315
	i32 2992773137, ; 568: Tools.Validations.dll => 0xb2621811 => 384
	i32 2996846495, ; 569: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 297
	i32 3015645789, ; 570: Domain.WebSources => 0xb3bf1a5d => 377
	i32 3016983068, ; 571: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 317
	i32 3020703001, ; 572: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 203
	i32 3023353419, ; 573: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 574: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 292
	i32 3038032645, ; 575: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 389
	i32 3053864966, ; 576: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 347
	i32 3056245963, ; 577: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 314
	i32 3057625584, ; 578: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 305
	i32 3059408633, ; 579: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 580: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3069363400, ; 581: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 195
	i32 3075834255, ; 582: System.Threading.Tasks => 0xb755818f => 144
	i32 3090735792, ; 583: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 584: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 585: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 586: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3114461270, ; 587: Python.Deployment => 0xb9a2e856 => 246
	i32 3121463068, ; 588: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 589: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 590: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3135029042, ; 591: ICSharpCode.SharpZipLib.dll => 0xbadcbf32 => 249
	i32 3147165239, ; 592: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 593: GoogleGson.dll => 0xbba64c02 => 183
	i32 3159123045, ; 594: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 595: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 596: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 306
	i32 3192346100, ; 597: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 598: System.Web => 0xbe592c0c => 153
	i32 3195844289, ; 599: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 195
	i32 3204380047, ; 600: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 601: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 602: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 283
	i32 3218227740, ; 603: Microsoft.ML => 0xbfd2421c => 224
	i32 3218469051, ; 604: Python.Included.dll => 0xbfd5f0bb => 247
	i32 3220365878, ; 605: System.Threading => 0xbff2e236 => 148
	i32 3222350070, ; 606: MessagePack.Annotations => 0xc01128f6 => 188
	i32 3226221578, ; 607: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 608: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 609: Xamarin.AndroidX.CardView => 0xc235e84d => 271
	i32 3265493905, ; 610: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 611: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 612: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 613: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 614: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3283523061, ; 615: ML.Predictor.dll => 0xc3b695f5 => 380
	i32 3290767353, ; 616: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 617: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 618: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 619: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 347
	i32 3316085879, ; 620: Microsoft.Recognizers.Text.Number.dll => 0xc5a77477 => 232
	i32 3316684772, ; 621: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 622: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 281
	i32 3317144872, ; 623: System.Data => 0xc5b79d28 => 24
	i32 3331047956, ; 624: Tools.Parsers => 0xc68bc214 => 383
	i32 3340431453, ; 625: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 269
	i32 3345895724, ; 626: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 310
	i32 3346324047, ; 627: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 307
	i32 3354404874, ; 628: Python.Deployment.dll => 0xc7f0280a => 246
	i32 3357674450, ; 629: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 364
	i32 3358260929, ; 630: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 631: SQLitePCLRaw.core => 0xc849ca45 => 251
	i32 3362336904, ; 632: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 262
	i32 3362522851, ; 633: Xamarin.AndroidX.Core => 0xc86c06e3 => 278
	i32 3366347497, ; 634: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 635: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 311
	i32 3381016424, ; 636: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 343
	i32 3389287332, ; 637: Microsoft.Recognizers.Text.DateTime => 0xca046ba4 => 231
	i32 3395150330, ; 638: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 639: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 640: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 282
	i32 3421170118, ; 641: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 199
	i32 3428513518, ; 642: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 200
	i32 3429136800, ; 643: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 644: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 645: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 285
	i32 3445260447, ; 646: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 647: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 212
	i32 3458724246, ; 648: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 362
	i32 3471940407, ; 649: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 650: Mono.Android => 0xcf3163e6 => 171
	i32 3478383276, ; 651: Mosaik.Core => 0xcf53eaac => 234
	i32 3484440000, ; 652: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 363
	i32 3485117614, ; 653: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3485328213, ; 654: Nito.AsyncEx.Tasks.dll => 0xcfbde355 => 241
	i32 3486566296, ; 655: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 656: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 274
	i32 3509114376, ; 657: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 658: System.Security.dll => 0xd1854eb4 => 130
	i32 3522696746, ; 659: Microsoft.ML.PCA => 0xd1f8162a => 221
	i32 3530912306, ; 660: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 661: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3560100363, ; 662: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 663: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3580758918, ; 664: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 371
	i32 3592228221, ; 665: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 373
	i32 3597029428, ; 666: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 260
	i32 3598340787, ; 667: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 668: System.Linq.dll => 0xd715a361 => 61
	i32 3623953038, ; 669: FluentValidation.dll => 0xd801228e => 181
	i32 3624195450, ; 670: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 671: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 309
	i32 3633644679, ; 672: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 264
	i32 3638274909, ; 673: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 674: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 295
	i32 3643446276, ; 675: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 368
	i32 3643854240, ; 676: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 306
	i32 3645089577, ; 677: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3656837941, ; 678: WebSources.Linker.dll => 0xd9f6eb35 => 387
	i32 3657292374, ; 679: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 198
	i32 3660523487, ; 680: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3660726404, ; 681: Plugin.Maui.Audio.dll => 0xda324084 => 245
	i32 3672681054, ; 682: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 683: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 270
	i32 3684561358, ; 684: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 274
	i32 3700866549, ; 685: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 686: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 279
	i32 3711244101, ; 687: Microsoft.ML.DataView.dll => 0xdd351745 => 226
	i32 3716563718, ; 688: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 689: Xamarin.AndroidX.Annotation => 0xdda814c6 => 263
	i32 3724971120, ; 690: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 305
	i32 3732100267, ; 691: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 692: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 693: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3750787767, ; 694: Microsoft.Recognizers.Text.NumberWithUnit.dll => 0xdf907ab7 => 233
	i32 3751444290, ; 695: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3751619990, ; 696: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 343
	i32 3754567612, ; 697: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 253
	i32 3786282454, ; 698: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 272
	i32 3792276235, ; 699: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3793403393, ; 700: Nito.AsyncEx.Oop.dll => 0xe21abe01 => 240
	i32 3800979733, ; 701: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 212
	i32 3802395368, ; 702: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3810220126, ; 703: HtmlAgilityPack.dll => 0xe31b585e => 184
	i32 3817368567, ; 704: CommunityToolkit.Maui.dll => 0xe3886bf7 => 177
	i32 3818918118, ; 705: CsvHelper.dll => 0xe3a010e6 => 180
	i32 3819260425, ; 706: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 707: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 708: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 709: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 201
	i32 3844307129, ; 710: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 711: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3865950343, ; 712: Microsoft.Recognizers.Text.DataTypes.TimexExpression.dll => 0xe66db887 => 230
	i32 3870376305, ; 713: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 714: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 715: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3882883416, ; 716: Apps.MauiRunner => 0xe7701958 => 0
	i32 3885497537, ; 717: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 718: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 320
	i32 3888767677, ; 719: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 310
	i32 3896106733, ; 720: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 721: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 278
	i32 3900306149, ; 722: Microsoft.Recognizers.Text.Number => 0xe879f2e5 => 232
	i32 3901907137, ; 723: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3917289289, ; 724: Microsoft.Recognizers.Text.NumberWithUnit => 0xe97d1749 => 233
	i32 3920221145, ; 725: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 359
	i32 3920810846, ; 726: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 727: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 323
	i32 3928044579, ; 728: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 729: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 730: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 308
	i32 3945713374, ; 731: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 732: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 733: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 266
	i32 3959773229, ; 734: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 297
	i32 3967175044, ; 735: Nito.AsyncEx.Interop.WaitHandles => 0xec764984 => 239
	i32 3992811056, ; 736: WebSources.ReversoContext.dll => 0xedfd7630 => 388
	i32 3995133918, ; 737: Python.Included => 0xee20e7de => 247
	i32 3997659347, ; 738: WebSources.Common => 0xee4770d3 => 386
	i32 4003436829, ; 739: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 740: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 265
	i32 4025784931, ; 741: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 742: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 214
	i32 4054681211, ; 743: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 744: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 745: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4091086043, ; 746: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 345
	i32 4094352644, ; 747: Microsoft.Maui.Essentials.dll => 0xf40add04 => 216
	i32 4099507663, ; 748: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 749: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 750: Xamarin.AndroidX.Emoji2 => 0xf479582c => 286
	i32 4101842092, ; 751: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 196
	i32 4103439459, ; 752: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 369
	i32 4112916512, ; 753: Catalyst.Spacy.dll => 0xf5262020 => 176
	i32 4126470640, ; 754: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 200
	i32 4127667938, ; 755: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 756: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 757: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 758: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 369
	i32 4151237749, ; 759: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 760: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 761: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 762: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4172475964, ; 763: ML.Predictor => 0xf8b2ee3c => 380
	i32 4181436372, ; 764: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 765: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 302
	i32 4185676441, ; 766: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 767: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 768: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4249188766, ; 769: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 358
	i32 4251825377, ; 770: Tools.Common.dll => 0xfd6db4e1 => 381
	i32 4256097574, ; 771: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 279
	i32 4258378803, ; 772: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 301
	i32 4260525087, ; 773: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 774: Microsoft.Maui.Controls.dll => 0xfea12dee => 213
	i32 4274623895, ; 775: CommunityToolkit.Mvvm.dll => 0xfec99597 => 179
	i32 4274976490, ; 776: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 777: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 302
	i32 4292200793, ; 778: MediatR.dll => 0xffd5c959 => 185
	i32 4294763496 ; 779: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 288
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [780 x i32] [
	i32 68, ; 0
	i32 242, ; 1
	i32 67, ; 2
	i32 108, ; 3
	i32 202, ; 4
	i32 298, ; 5
	i32 332, ; 6
	i32 48, ; 7
	i32 340, ; 8
	i32 235, ; 9
	i32 80, ; 10
	i32 378, ; 11
	i32 349, ; 12
	i32 145, ; 13
	i32 184, ; 14
	i32 30, ; 15
	i32 373, ; 16
	i32 124, ; 17
	i32 217, ; 18
	i32 102, ; 19
	i32 357, ; 20
	i32 204, ; 21
	i32 316, ; 22
	i32 107, ; 23
	i32 231, ; 24
	i32 316, ; 25
	i32 139, ; 26
	i32 249, ; 27
	i32 336, ; 28
	i32 372, ; 29
	i32 365, ; 30
	i32 77, ; 31
	i32 124, ; 32
	i32 13, ; 33
	i32 272, ; 34
	i32 132, ; 35
	i32 318, ; 36
	i32 151, ; 37
	i32 18, ; 38
	i32 270, ; 39
	i32 385, ; 40
	i32 26, ; 41
	i32 203, ; 42
	i32 292, ; 43
	i32 1, ; 44
	i32 59, ; 45
	i32 42, ; 46
	i32 91, ; 47
	i32 275, ; 48
	i32 147, ; 49
	i32 294, ; 50
	i32 291, ; 51
	i32 229, ; 52
	i32 54, ; 53
	i32 205, ; 54
	i32 69, ; 55
	i32 225, ; 56
	i32 370, ; 57
	i32 261, ; 58
	i32 83, ; 59
	i32 348, ; 60
	i32 293, ; 61
	i32 252, ; 62
	i32 182, ; 63
	i32 131, ; 64
	i32 381, ; 65
	i32 55, ; 66
	i32 149, ; 67
	i32 74, ; 68
	i32 145, ; 69
	i32 62, ; 70
	i32 146, ; 71
	i32 241, ; 72
	i32 389, ; 73
	i32 165, ; 74
	i32 186, ; 75
	i32 368, ; 76
	i32 385, ; 77
	i32 228, ; 78
	i32 276, ; 79
	i32 12, ; 80
	i32 289, ; 81
	i32 125, ; 82
	i32 152, ; 83
	i32 113, ; 84
	i32 166, ; 85
	i32 164, ; 86
	i32 291, ; 87
	i32 174, ; 88
	i32 304, ; 89
	i32 346, ; 90
	i32 84, ; 91
	i32 211, ; 92
	i32 375, ; 93
	i32 150, ; 94
	i32 336, ; 95
	i32 60, ; 96
	i32 367, ; 97
	i32 206, ; 98
	i32 51, ; 99
	i32 103, ; 100
	i32 114, ; 101
	i32 189, ; 102
	i32 40, ; 103
	i32 329, ; 104
	i32 239, ; 105
	i32 236, ; 106
	i32 327, ; 107
	i32 120, ; 108
	i32 175, ; 109
	i32 177, ; 110
	i32 52, ; 111
	i32 44, ; 112
	i32 119, ; 113
	i32 256, ; 114
	i32 281, ; 115
	i32 359, ; 116
	i32 382, ; 117
	i32 287, ; 118
	i32 81, ; 119
	i32 136, ; 120
	i32 323, ; 121
	i32 268, ; 122
	i32 8, ; 123
	i32 73, ; 124
	i32 155, ; 125
	i32 338, ; 126
	i32 154, ; 127
	i32 92, ; 128
	i32 333, ; 129
	i32 45, ; 130
	i32 188, ; 131
	i32 374, ; 132
	i32 337, ; 133
	i32 109, ; 134
	i32 210, ; 135
	i32 129, ; 136
	i32 182, ; 137
	i32 250, ; 138
	i32 25, ; 139
	i32 258, ; 140
	i32 72, ; 141
	i32 55, ; 142
	i32 227, ; 143
	i32 46, ; 144
	i32 365, ; 145
	i32 234, ; 146
	i32 209, ; 147
	i32 282, ; 148
	i32 175, ; 149
	i32 22, ; 150
	i32 296, ; 151
	i32 378, ; 152
	i32 86, ; 153
	i32 187, ; 154
	i32 43, ; 155
	i32 160, ; 156
	i32 71, ; 157
	i32 309, ; 158
	i32 226, ; 159
	i32 350, ; 160
	i32 3, ; 161
	i32 42, ; 162
	i32 63, ; 163
	i32 364, ; 164
	i32 376, ; 165
	i32 180, ; 166
	i32 16, ; 167
	i32 53, ; 168
	i32 361, ; 169
	i32 332, ; 170
	i32 220, ; 171
	i32 105, ; 172
	i32 235, ; 173
	i32 337, ; 174
	i32 354, ; 175
	i32 330, ; 176
	i32 293, ; 177
	i32 34, ; 178
	i32 158, ; 179
	i32 85, ; 180
	i32 32, ; 181
	i32 363, ; 182
	i32 12, ; 183
	i32 51, ; 184
	i32 56, ; 185
	i32 382, ; 186
	i32 313, ; 187
	i32 377, ; 188
	i32 36, ; 189
	i32 201, ; 190
	i32 331, ; 191
	i32 266, ; 192
	i32 35, ; 193
	i32 244, ; 194
	i32 344, ; 195
	i32 58, ; 196
	i32 204, ; 197
	i32 300, ; 198
	i32 224, ; 199
	i32 183, ; 200
	i32 17, ; 201
	i32 334, ; 202
	i32 164, ; 203
	i32 366, ; 204
	i32 360, ; 205
	i32 356, ; 206
	i32 299, ; 207
	i32 208, ; 208
	i32 255, ; 209
	i32 326, ; 210
	i32 192, ; 211
	i32 362, ; 212
	i32 153, ; 213
	i32 322, ; 214
	i32 307, ; 215
	i32 379, ; 216
	i32 221, ; 217
	i32 192, ; 218
	i32 268, ; 219
	i32 196, ; 220
	i32 29, ; 221
	i32 179, ; 222
	i32 52, ; 223
	i32 327, ; 224
	i32 5, ; 225
	i32 342, ; 226
	i32 317, ; 227
	i32 321, ; 228
	i32 273, ; 229
	i32 338, ; 230
	i32 265, ; 231
	i32 251, ; 232
	i32 284, ; 233
	i32 176, ; 234
	i32 351, ; 235
	i32 85, ; 236
	i32 326, ; 237
	i32 61, ; 238
	i32 112, ; 239
	i32 371, ; 240
	i32 245, ; 241
	i32 57, ; 242
	i32 372, ; 243
	i32 313, ; 244
	i32 99, ; 245
	i32 227, ; 246
	i32 19, ; 247
	i32 277, ; 248
	i32 111, ; 249
	i32 101, ; 250
	i32 237, ; 251
	i32 102, ; 252
	i32 340, ; 253
	i32 104, ; 254
	i32 330, ; 255
	i32 71, ; 256
	i32 222, ; 257
	i32 38, ; 258
	i32 32, ; 259
	i32 103, ; 260
	i32 73, ; 261
	i32 346, ; 262
	i32 9, ; 263
	i32 123, ; 264
	i32 46, ; 265
	i32 267, ; 266
	i32 211, ; 267
	i32 220, ; 268
	i32 9, ; 269
	i32 43, ; 270
	i32 4, ; 271
	i32 314, ; 272
	i32 190, ; 273
	i32 205, ; 274
	i32 248, ; 275
	i32 370, ; 276
	i32 31, ; 277
	i32 138, ; 278
	i32 92, ; 279
	i32 93, ; 280
	i32 222, ; 281
	i32 49, ; 282
	i32 141, ; 283
	i32 112, ; 284
	i32 140, ; 285
	i32 283, ; 286
	i32 115, ; 287
	i32 331, ; 288
	i32 157, ; 289
	i32 76, ; 290
	i32 79, ; 291
	i32 303, ; 292
	i32 37, ; 293
	i32 325, ; 294
	i32 178, ; 295
	i32 287, ; 296
	i32 280, ; 297
	i32 64, ; 298
	i32 138, ; 299
	i32 15, ; 300
	i32 116, ; 301
	i32 319, ; 302
	i32 328, ; 303
	i32 275, ; 304
	i32 48, ; 305
	i32 70, ; 306
	i32 80, ; 307
	i32 126, ; 308
	i32 190, ; 309
	i32 191, ; 310
	i32 94, ; 311
	i32 121, ; 312
	i32 335, ; 313
	i32 26, ; 314
	i32 388, ; 315
	i32 252, ; 316
	i32 296, ; 317
	i32 97, ; 318
	i32 28, ; 319
	i32 219, ; 320
	i32 271, ; 321
	i32 341, ; 322
	i32 149, ; 323
	i32 169, ; 324
	i32 4, ; 325
	i32 98, ; 326
	i32 33, ; 327
	i32 93, ; 328
	i32 318, ; 329
	i32 206, ; 330
	i32 21, ; 331
	i32 41, ; 332
	i32 170, ; 333
	i32 173, ; 334
	i32 357, ; 335
	i32 289, ; 336
	i32 349, ; 337
	i32 189, ; 338
	i32 303, ; 339
	i32 334, ; 340
	i32 328, ; 341
	i32 308, ; 342
	i32 2, ; 343
	i32 134, ; 344
	i32 111, ; 345
	i32 207, ; 346
	i32 387, ; 347
	i32 258, ; 348
	i32 366, ; 349
	i32 58, ; 350
	i32 95, ; 351
	i32 348, ; 352
	i32 39, ; 353
	i32 187, ; 354
	i32 269, ; 355
	i32 194, ; 356
	i32 25, ; 357
	i32 94, ; 358
	i32 342, ; 359
	i32 89, ; 360
	i32 99, ; 361
	i32 384, ; 362
	i32 10, ; 363
	i32 254, ; 364
	i32 87, ; 365
	i32 353, ; 366
	i32 100, ; 367
	i32 315, ; 368
	i32 197, ; 369
	i32 335, ; 370
	i32 260, ; 371
	i32 181, ; 372
	i32 345, ; 373
	i32 7, ; 374
	i32 194, ; 375
	i32 300, ; 376
	i32 257, ; 377
	i32 88, ; 378
	i32 199, ; 379
	i32 244, ; 380
	i32 295, ; 381
	i32 154, ; 382
	i32 344, ; 383
	i32 33, ; 384
	i32 240, ; 385
	i32 116, ; 386
	i32 82, ; 387
	i32 256, ; 388
	i32 230, ; 389
	i32 253, ; 390
	i32 238, ; 391
	i32 20, ; 392
	i32 11, ; 393
	i32 162, ; 394
	i32 3, ; 395
	i32 0, ; 396
	i32 248, ; 397
	i32 215, ; 398
	i32 352, ; 399
	i32 254, ; 400
	i32 209, ; 401
	i32 207, ; 402
	i32 84, ; 403
	i32 202, ; 404
	i32 339, ; 405
	i32 64, ; 406
	i32 354, ; 407
	i32 322, ; 408
	i32 143, ; 409
	i32 304, ; 410
	i32 157, ; 411
	i32 191, ; 412
	i32 41, ; 413
	i32 117, ; 414
	i32 198, ; 415
	i32 223, ; 416
	i32 259, ; 417
	i32 173, ; 418
	i32 218, ; 419
	i32 311, ; 420
	i32 223, ; 421
	i32 131, ; 422
	i32 75, ; 423
	i32 66, ; 424
	i32 358, ; 425
	i32 172, ; 426
	i32 263, ; 427
	i32 143, ; 428
	i32 238, ; 429
	i32 242, ; 430
	i32 106, ; 431
	i32 151, ; 432
	i32 70, ; 433
	i32 352, ; 434
	i32 156, ; 435
	i32 197, ; 436
	i32 121, ; 437
	i32 127, ; 438
	i32 353, ; 439
	i32 152, ; 440
	i32 286, ; 441
	i32 141, ; 442
	i32 273, ; 443
	i32 350, ; 444
	i32 20, ; 445
	i32 14, ; 446
	i32 135, ; 447
	i32 75, ; 448
	i32 59, ; 449
	i32 250, ; 450
	i32 276, ; 451
	i32 167, ; 452
	i32 225, ; 453
	i32 168, ; 454
	i32 213, ; 455
	i32 15, ; 456
	i32 74, ; 457
	i32 6, ; 458
	i32 374, ; 459
	i32 23, ; 460
	i32 356, ; 461
	i32 298, ; 462
	i32 376, ; 463
	i32 257, ; 464
	i32 237, ; 465
	i32 91, ; 466
	i32 218, ; 467
	i32 351, ; 468
	i32 1, ; 469
	i32 136, ; 470
	i32 355, ; 471
	i32 375, ; 472
	i32 299, ; 473
	i32 321, ; 474
	i32 134, ; 475
	i32 69, ; 476
	i32 146, ; 477
	i32 360, ; 478
	i32 339, ; 479
	i32 290, ; 480
	i32 185, ; 481
	i32 208, ; 482
	i32 88, ; 483
	i32 96, ; 484
	i32 280, ; 485
	i32 285, ; 486
	i32 355, ; 487
	i32 31, ; 488
	i32 45, ; 489
	i32 294, ; 490
	i32 193, ; 491
	i32 219, ; 492
	i32 259, ; 493
	i32 109, ; 494
	i32 158, ; 495
	i32 35, ; 496
	i32 22, ; 497
	i32 186, ; 498
	i32 379, ; 499
	i32 114, ; 500
	i32 57, ; 501
	i32 319, ; 502
	i32 144, ; 503
	i32 118, ; 504
	i32 120, ; 505
	i32 383, ; 506
	i32 110, ; 507
	i32 261, ; 508
	i32 229, ; 509
	i32 139, ; 510
	i32 267, ; 511
	i32 341, ; 512
	i32 54, ; 513
	i32 105, ; 514
	i32 361, ; 515
	i32 214, ; 516
	i32 215, ; 517
	i32 133, ; 518
	i32 333, ; 519
	i32 324, ; 520
	i32 312, ; 521
	i32 367, ; 522
	i32 290, ; 523
	i32 174, ; 524
	i32 228, ; 525
	i32 217, ; 526
	i32 159, ; 527
	i32 277, ; 528
	i32 255, ; 529
	i32 163, ; 530
	i32 132, ; 531
	i32 312, ; 532
	i32 161, ; 533
	i32 301, ; 534
	i32 193, ; 535
	i32 140, ; 536
	i32 324, ; 537
	i32 320, ; 538
	i32 169, ; 539
	i32 216, ; 540
	i32 178, ; 541
	i32 262, ; 542
	i32 329, ; 543
	i32 40, ; 544
	i32 288, ; 545
	i32 81, ; 546
	i32 56, ; 547
	i32 37, ; 548
	i32 97, ; 549
	i32 236, ; 550
	i32 166, ; 551
	i32 172, ; 552
	i32 325, ; 553
	i32 82, ; 554
	i32 264, ; 555
	i32 243, ; 556
	i32 243, ; 557
	i32 98, ; 558
	i32 30, ; 559
	i32 159, ; 560
	i32 18, ; 561
	i32 386, ; 562
	i32 127, ; 563
	i32 210, ; 564
	i32 119, ; 565
	i32 284, ; 566
	i32 315, ; 567
	i32 384, ; 568
	i32 297, ; 569
	i32 377, ; 570
	i32 317, ; 571
	i32 203, ; 572
	i32 165, ; 573
	i32 292, ; 574
	i32 389, ; 575
	i32 347, ; 576
	i32 314, ; 577
	i32 305, ; 578
	i32 170, ; 579
	i32 16, ; 580
	i32 195, ; 581
	i32 144, ; 582
	i32 125, ; 583
	i32 118, ; 584
	i32 38, ; 585
	i32 115, ; 586
	i32 246, ; 587
	i32 47, ; 588
	i32 142, ; 589
	i32 117, ; 590
	i32 249, ; 591
	i32 34, ; 592
	i32 183, ; 593
	i32 95, ; 594
	i32 53, ; 595
	i32 306, ; 596
	i32 129, ; 597
	i32 153, ; 598
	i32 195, ; 599
	i32 24, ; 600
	i32 161, ; 601
	i32 283, ; 602
	i32 224, ; 603
	i32 247, ; 604
	i32 148, ; 605
	i32 188, ; 606
	i32 104, ; 607
	i32 89, ; 608
	i32 271, ; 609
	i32 60, ; 610
	i32 142, ; 611
	i32 100, ; 612
	i32 5, ; 613
	i32 13, ; 614
	i32 380, ; 615
	i32 122, ; 616
	i32 135, ; 617
	i32 28, ; 618
	i32 347, ; 619
	i32 232, ; 620
	i32 72, ; 621
	i32 281, ; 622
	i32 24, ; 623
	i32 383, ; 624
	i32 269, ; 625
	i32 310, ; 626
	i32 307, ; 627
	i32 246, ; 628
	i32 364, ; 629
	i32 137, ; 630
	i32 251, ; 631
	i32 262, ; 632
	i32 278, ; 633
	i32 168, ; 634
	i32 311, ; 635
	i32 343, ; 636
	i32 231, ; 637
	i32 101, ; 638
	i32 123, ; 639
	i32 282, ; 640
	i32 199, ; 641
	i32 200, ; 642
	i32 163, ; 643
	i32 167, ; 644
	i32 285, ; 645
	i32 39, ; 646
	i32 212, ; 647
	i32 362, ; 648
	i32 17, ; 649
	i32 171, ; 650
	i32 234, ; 651
	i32 363, ; 652
	i32 137, ; 653
	i32 241, ; 654
	i32 150, ; 655
	i32 274, ; 656
	i32 155, ; 657
	i32 130, ; 658
	i32 221, ; 659
	i32 19, ; 660
	i32 65, ; 661
	i32 147, ; 662
	i32 47, ; 663
	i32 371, ; 664
	i32 373, ; 665
	i32 260, ; 666
	i32 79, ; 667
	i32 61, ; 668
	i32 181, ; 669
	i32 106, ; 670
	i32 309, ; 671
	i32 264, ; 672
	i32 49, ; 673
	i32 295, ; 674
	i32 368, ; 675
	i32 306, ; 676
	i32 14, ; 677
	i32 387, ; 678
	i32 198, ; 679
	i32 68, ; 680
	i32 245, ; 681
	i32 171, ; 682
	i32 270, ; 683
	i32 274, ; 684
	i32 78, ; 685
	i32 279, ; 686
	i32 226, ; 687
	i32 108, ; 688
	i32 263, ; 689
	i32 305, ; 690
	i32 67, ; 691
	i32 63, ; 692
	i32 27, ; 693
	i32 233, ; 694
	i32 160, ; 695
	i32 343, ; 696
	i32 253, ; 697
	i32 272, ; 698
	i32 10, ; 699
	i32 240, ; 700
	i32 212, ; 701
	i32 11, ; 702
	i32 184, ; 703
	i32 177, ; 704
	i32 180, ; 705
	i32 78, ; 706
	i32 126, ; 707
	i32 83, ; 708
	i32 201, ; 709
	i32 66, ; 710
	i32 107, ; 711
	i32 230, ; 712
	i32 65, ; 713
	i32 128, ; 714
	i32 122, ; 715
	i32 0, ; 716
	i32 77, ; 717
	i32 320, ; 718
	i32 310, ; 719
	i32 8, ; 720
	i32 278, ; 721
	i32 232, ; 722
	i32 2, ; 723
	i32 233, ; 724
	i32 359, ; 725
	i32 44, ; 726
	i32 323, ; 727
	i32 156, ; 728
	i32 128, ; 729
	i32 308, ; 730
	i32 23, ; 731
	i32 133, ; 732
	i32 266, ; 733
	i32 297, ; 734
	i32 239, ; 735
	i32 388, ; 736
	i32 247, ; 737
	i32 386, ; 738
	i32 29, ; 739
	i32 265, ; 740
	i32 62, ; 741
	i32 214, ; 742
	i32 90, ; 743
	i32 87, ; 744
	i32 148, ; 745
	i32 345, ; 746
	i32 216, ; 747
	i32 36, ; 748
	i32 86, ; 749
	i32 286, ; 750
	i32 196, ; 751
	i32 369, ; 752
	i32 176, ; 753
	i32 200, ; 754
	i32 50, ; 755
	i32 6, ; 756
	i32 90, ; 757
	i32 369, ; 758
	i32 21, ; 759
	i32 162, ; 760
	i32 96, ; 761
	i32 50, ; 762
	i32 380, ; 763
	i32 113, ; 764
	i32 302, ; 765
	i32 130, ; 766
	i32 76, ; 767
	i32 27, ; 768
	i32 358, ; 769
	i32 381, ; 770
	i32 279, ; 771
	i32 301, ; 772
	i32 7, ; 773
	i32 213, ; 774
	i32 179, ; 775
	i32 110, ; 776
	i32 302, ; 777
	i32 185, ; 778
	i32 288 ; 779
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ af27162bee43b7fecdca59b4f67aa8c175cbc875"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}

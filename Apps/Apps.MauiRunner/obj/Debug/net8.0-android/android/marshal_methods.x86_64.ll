; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [393 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [780 x i64] [
	i64 14005105541811523, ; 0: System.Numerics.Tensors => 0x31c191bcdc8d43 => 256
	i64 24362543149721218, ; 1: Xamarin.AndroidX.DynamicAnimation => 0x568d9a9a43a682 => 285
	i64 41048320473317273, ; 2: NickBuhro.Translit.dll => 0x91d53ae0703f99 => 236
	i64 98382396393917666, ; 3: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 211
	i64 120698629574877762, ; 4: Mono.Android => 0x1accec39cafe242 => 171
	i64 125226092537430946, ; 5: Domain.WebSources.dll => 0x1bce477ba4dc3a2 => 377
	i64 131669012237370309, ; 6: Microsoft.Maui.Essentials.dll => 0x1d3c844de55c3c5 => 216
	i64 196720943101637631, ; 7: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 58
	i64 197751585713159992, ; 8: ICSharpCode.SharpZipLib.dll => 0x2be8e04fc33ff38 => 249
	i64 210515253464952879, ; 9: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 272
	i64 229794953483747371, ; 10: System.ValueTuple.dll => 0x330654aed93802b => 151
	i64 232391251801502327, ; 11: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 313
	i64 295915112840604065, ; 12: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 316
	i64 316157742385208084, ; 13: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 279
	i64 350667413455104241, ; 14: System.ServiceProcess.dll => 0x4ddd227954be8f1 => 132
	i64 354178770117062970, ; 15: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0x4ea4bb703cff13a => 210
	i64 373588702732756378, ; 16: Microsoft.Recognizers.Text.Number.dll => 0x52f40f21e7c259a => 232
	i64 376821212072123757, ; 17: AngleSharp.dll => 0x53abce559641d6d => 173
	i64 422779754995088667, ; 18: System.IO.UnmanagedMemoryStream => 0x5de03f27ab57d1b => 56
	i64 435118502366263740, ; 19: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x609d9f8f8bdb9bc => 315
	i64 560278790331054453, ; 20: System.Reflection.Primitives => 0x7c6829760de3975 => 95
	i64 634308326490598313, ; 21: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 298
	i64 649145001856603771, ; 22: System.Security.SecureString => 0x90239f09b62167b => 129
	i64 668723562677762733, ; 23: Microsoft.Extensions.Configuration.Binder.dll => 0x947c88986577aad => 199
	i64 750875890346172408, ; 24: System.Threading.Thread => 0xa6ba5a4da7d1ff8 => 145
	i64 798450721097591769, ; 25: Xamarin.AndroidX.Collection.Ktx.dll => 0xb14aab351ad2bd9 => 273
	i64 799765834175365804, ; 26: System.ComponentModel.dll => 0xb1956c9f18442ac => 18
	i64 805302231655005164, ; 27: hu\Microsoft.Maui.Controls.resources.dll => 0xb2d021ceea03bec => 352
	i64 845617881323029723, ; 28: Domain.Common.dll => 0xbbc3cfb97fa44db => 375
	i64 870603111519317375, ; 29: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 252
	i64 872800313462103108, ; 30: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 284
	i64 895210737996778430, ; 31: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0xc6c6d6c5569cbbe => 299
	i64 936521732570528272, ; 32: Microsoft.ML.DataView.dll => 0xcff318dca4cba10 => 226
	i64 940822596282819491, ; 33: System.Transactions => 0xd0e792aa81923a3 => 150
	i64 960778385402502048, ; 34: System.Runtime.Handles.dll => 0xd555ed9e1ca1ba0 => 104
	i64 964003131647442271, ; 35: HtmlAgilityPack => 0xd60d3bda035bd5f => 184
	i64 1010599046655515943, ; 36: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 95
	i64 1084271602438139705, ; 37: Python.Runtime.dll => 0xf0c1b47175e6f39 => 248
	i64 1120440138749646132, ; 38: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 328
	i64 1268860745194512059, ; 39: System.Drawing.dll => 0x119be62002c19ebb => 36
	i64 1301485588176585670, ; 40: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 251
	i64 1301626418029409250, ; 41: System.Diagnostics.FileVersionInfo => 0x12104e54b4e833e2 => 28
	i64 1315114680217950157, ; 42: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 268
	i64 1332369539331056316, ; 43: MediatR.dll => 0x127d87096d60e2bc => 185
	i64 1369545283391376210, ; 44: Xamarin.AndroidX.Navigation.Fragment.dll => 0x13019a2dd85acb52 => 306
	i64 1404195534211153682, ; 45: System.IO.FileSystem.Watcher.dll => 0x137cb4660bd87f12 => 50
	i64 1425944114962822056, ; 46: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 115
	i64 1476839205573959279, ; 47: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 70
	i64 1486715745332614827, ; 48: Microsoft.Maui.Controls.dll => 0x14a1e017ea87d6ab => 213
	i64 1492954217099365037, ; 49: System.Net.HttpListener => 0x14b809f350210aad => 65
	i64 1510093918963340424, ; 50: Nito.AsyncEx.Interop.WaitHandles.dll => 0x14f4ee6b21499488 => 239
	i64 1513467482682125403, ; 51: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 170
	i64 1518315023656898250, ; 52: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 253
	i64 1537168428375924959, ; 53: System.Threading.Thread.dll => 0x15551e8a954ae0df => 145
	i64 1576750169145655260, ; 54: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x15e1bdecc376bfdc => 327
	i64 1624659445732251991, ; 55: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 267
	i64 1628611045998245443, ; 56: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 302
	i64 1636321030536304333, ; 57: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 292
	i64 1651782184287836205, ; 58: System.Globalization.Calendars => 0x16ec4f2524cb982d => 40
	i64 1659332977923810219, ; 59: System.Reflection.DispatchProxy => 0x1707228d493d63ab => 89
	i64 1672383392659050004, ; 60: Microsoft.Data.Sqlite.dll => 0x17357fd5bfb48e14 => 190
	i64 1682513316613008342, ; 61: System.Net.dll => 0x17597cf276952bd6 => 81
	i64 1731380447121279447, ; 62: Newtonsoft.Json => 0x18071957e9b889d7 => 235
	i64 1735388228521408345, ; 63: System.Net.Mail.dll => 0x181556663c69b759 => 66
	i64 1743969030606105336, ; 64: System.Memory.dll => 0x1833d297e88f2af8 => 62
	i64 1767386781656293639, ; 65: System.Private.Uri.dll => 0x188704e9f5582107 => 86
	i64 1795316252682057001, ; 66: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 266
	i64 1825687700144851180, ; 67: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 106
	i64 1835311033149317475, ; 68: es\Microsoft.Maui.Controls.resources => 0x197855a927386163 => 346
	i64 1836611346387731153, ; 69: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 313
	i64 1854145951182283680, ; 70: System.Runtime.CompilerServices.VisualC => 0x19bb3feb3df2e3a0 => 102
	i64 1865037103900624886, ; 71: Microsoft.Bcl.AsyncInterfaces => 0x19e1f15d56eb87f6 => 189
	i64 1875417405349196092, ; 72: System.Drawing.Primitives => 0x1a06d2319b6c713c => 35
	i64 1875917498431009007, ; 73: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 263
	i64 1881198190668717030, ; 74: tr\Microsoft.Maui.Controls.resources => 0x1a1b5bc992ea9be6 => 368
	i64 1897575647115118287, ; 75: Xamarin.AndroidX.Security.SecurityCrypto => 0x1a558aff4cba86cf => 315
	i64 1920760634179481754, ; 76: Microsoft.Maui.Controls.Xaml => 0x1aa7e99ec2d2709a => 214
	i64 1930726298510463061, ; 77: CommunityToolkit.Mvvm.dll => 0x1acb5156cd389055 => 179
	i64 1933498180790871425, ; 78: WebSources.Common => 0x1ad52a59efe76581 => 386
	i64 1972385128188460614, ; 79: System.Security.Cryptography.Algorithms => 0x1b5f51d2edefbe46 => 119
	i64 1981742497975770890, ; 80: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 300
	i64 2040001226662520565, ; 81: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 142
	i64 2062890601515140263, ; 82: System.Threading.Tasks.Dataflow => 0x1ca0dc1289cd44a7 => 141
	i64 2064708342624596306, ; 83: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 336
	i64 2070433629898762414, ; 84: Microsoft.Recognizers.Definitions => 0x1cbba86ab12368ae => 228
	i64 2080945842184875448, ; 85: System.IO.MemoryMappedFiles => 0x1ce10137d8416db8 => 53
	i64 2102659300918482391, ; 86: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 35
	i64 2106033277907880740, ; 87: System.Threading.Tasks.Dataflow.dll => 0x1d3a221ba6d9cb24 => 141
	i64 2133195048986300728, ; 88: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 235
	i64 2165310824878145998, ; 89: Xamarin.Android.Glide.GifDecoder => 0x1e0cbab9112b81ce => 260
	i64 2165725771938924357, ; 90: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 270
	i64 2192948757939169934, ; 91: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x1e6eeb46cf992a8e => 192
	i64 2200176636225660136, ; 92: Microsoft.Extensions.Logging.Debug.dll => 0x1e8898fe5d5824e8 => 208
	i64 2225893729833803749, ; 93: Catalyst.Models.English => 0x1ee3f68dd2835fe5 => 175
	i64 2262844636196693701, ; 94: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 284
	i64 2287834202362508563, ; 95: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 8
	i64 2287887973817120656, ; 96: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 14
	i64 2304837677853103545, ; 97: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 312
	i64 2315304989185124968, ; 98: System.IO.FileSystem.dll => 0x20219d9ee311aa68 => 51
	i64 2329709569556905518, ; 99: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 295
	i64 2335503487726329082, ; 100: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 136
	i64 2337758774805907496, ; 101: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 101
	i64 2470498323731680442, ; 102: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 277
	i64 2479423007379663237, ; 103: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 322
	i64 2497223385847772520, ; 104: System.Runtime => 0x22a7eb7046413568 => 116
	i64 2547086958574651984, ; 105: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 261
	i64 2592350477072141967, ; 106: System.Xml.dll => 0x23f9e10627330e8f => 163
	i64 2602673633151553063, ; 107: th\Microsoft.Maui.Controls.resources => 0x241e8de13a460e27 => 367
	i64 2624866290265602282, ; 108: mscorlib.dll => 0x246d65fbde2db8ea => 166
	i64 2632269733008246987, ; 109: System.Net.NameResolution => 0x2487b36034f808cb => 67
	i64 2656907746661064104, ; 110: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 200
	i64 2662981627730767622, ; 111: cs\Microsoft.Maui.Controls.resources => 0x24f4cfae6c48af06 => 342
	i64 2706075432581334785, ; 112: System.Net.WebSockets => 0x258de944be6c0701 => 80
	i64 2749208280645304866, ; 113: Tools.Common => 0x2627265d856d0222 => 381
	i64 2783046991838674048, ; 114: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 101
	i64 2787234703088983483, ; 115: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 317
	i64 2815524396660695947, ; 116: System.Security.AccessControl => 0x2712c0857f68238b => 117
	i64 2855371091389124434, ; 117: Microsoft.Recognizers.Text.NumberWithUnit => 0x27a050e1142b0f52 => 233
	i64 2895129759130297543, ; 118: fi\Microsoft.Maui.Controls.resources => 0x282d912d479fa4c7 => 347
	i64 2923871038697555247, ; 119: Jsr305Binding => 0x2893ad37e69ec52f => 329
	i64 2951810402710157670, ; 120: Microsoft.ML => 0x28f6efec01d77966 => 224
	i64 3017136373564924869, ; 121: System.Net.WebProxy => 0x29df058bd93f63c5 => 78
	i64 3017704767998173186, ; 122: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 328
	i64 3106852385031680087, ; 123: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 114
	i64 3110390492489056344, ; 124: System.Security.Cryptography.Csp.dll => 0x2b2a53ac61900058 => 121
	i64 3135773902340015556, ; 125: System.IO.FileSystem.DriveInfo.dll => 0x2b8481c008eac5c4 => 48
	i64 3253959448134772007, ; 126: Nito.Disposables => 0x2d2862e0bb996527 => 244
	i64 3281594302220646930, ; 127: System.Security.Principal => 0x2d8a90a198ceba12 => 128
	i64 3289520064315143713, ; 128: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 293
	i64 3303437397778967116, ; 129: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 264
	i64 3311221304742556517, ; 130: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 82
	i64 3325875462027654285, ; 131: System.Runtime.Numerics => 0x2e27e21c8958b48d => 110
	i64 3328853167529574890, ; 132: System.Net.Sockets.dll => 0x2e327651a008c1ea => 75
	i64 3344514922410554693, ; 133: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 339
	i64 3368876280519495760, ; 134: Microsoft.ML.Core => 0x2ec0a720c89a9050 => 218
	i64 3386864410740794978, ; 135: Nito.Collections.Deque => 0x2f008f3cb89e2662 => 243
	i64 3397747728761535915, ; 136: HtmlAgilityPack.dll => 0x2f27398ea938bdab => 184
	i64 3429672777697402584, ; 137: Microsoft.Maui.Essentials => 0x2f98a5385a7b1ed8 => 216
	i64 3437845325506641314, ; 138: System.IO.MemoryMappedFiles.dll => 0x2fb5ae1beb8f7da2 => 53
	i64 3493805808809882663, ; 139: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 319
	i64 3494946837667399002, ; 140: Microsoft.Extensions.Configuration => 0x30808ba1c00a455a => 197
	i64 3508450208084372758, ; 141: System.Net.Ping => 0x30b084e02d03ad16 => 69
	i64 3522470458906976663, ; 142: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 318
	i64 3523004241079211829, ; 143: Microsoft.Extensions.Caching.Memory.dll => 0x30e439b10bb89735 => 196
	i64 3531994851595924923, ; 144: System.Numerics => 0x31042a9aade235bb => 83
	i64 3551103847008531295, ; 145: System.Private.CoreLib.dll => 0x31480e226177735f => 172
	i64 3567343442040498961, ; 146: pt\Microsoft.Maui.Controls.resources => 0x3181bff5bea4ab11 => 362
	i64 3571415421602489686, ; 147: System.Runtime.dll => 0x319037675df7e556 => 116
	i64 3638003163729360188, ; 148: Microsoft.Extensions.Configuration.Abstractions => 0x327cc89a39d5f53c => 198
	i64 3647754201059316852, ; 149: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 156
	i64 3655542548057982301, ; 150: Microsoft.Extensions.Configuration.dll => 0x32bb18945e52855d => 197
	i64 3659371656528649588, ; 151: Xamarin.Android.Glide.Annotations => 0x32c8b3222885dd74 => 258
	i64 3692385509564015696, ; 152: Microsoft.ML.DataView => 0x333dfd0ecf5ce450 => 226
	i64 3716579019761409177, ; 153: netstandard.dll => 0x3393f0ed5c8c5c99 => 167
	i64 3727469159507183293, ; 154: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 311
	i64 3772598417116884899, ; 155: Xamarin.AndroidX.DynamicAnimation.dll => 0x345af645b473efa3 => 285
	i64 3788163066378099170, ; 156: Nito.Collections.Deque.dll => 0x3492423d02b899e2 => 243
	i64 3869221888984012293, ; 157: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 206
	i64 3869649043256705283, ; 158: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 32
	i64 3875180953283865480, ; 159: MessagePack.dll => 0x35c7688ba0d82b88 => 187
	i64 3890352374528606784, ; 160: Microsoft.Maui.Controls.Xaml.dll => 0x35fd4edf66e00240 => 214
	i64 3919223565570527920, ; 161: System.Security.Cryptography.Encoding => 0x3663e111652bd2b0 => 122
	i64 3933965368022646939, ; 162: System.Net.Requests => 0x369840a8bfadc09b => 72
	i64 3966267475168208030, ; 163: System.Memory => 0x370b03412596249e => 62
	i64 4004395173082023280, ; 164: Python.Included.dll => 0x3792783197d7c570 => 247
	i64 4006972109285359177, ; 165: System.Xml.XmlDocument => 0x379b9fe74ed9fe49 => 161
	i64 4009997192427317104, ; 166: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 113
	i64 4020380517496724220, ; 167: MessagePack.Annotations.dll => 0x37cb42c79f4b1afc => 188
	i64 4070326265757318011, ; 168: da\Microsoft.Maui.Controls.resources.dll => 0x387cb42c56683b7b => 343
	i64 4073500526318903918, ; 169: System.Private.Xml.dll => 0x3887fb25779ae26e => 88
	i64 4073631083018132676, ; 170: Microsoft.Maui.Controls.Compatibility.dll => 0x388871e311491cc4 => 212
	i64 4120493066591692148, ; 171: zh-Hant\Microsoft.Maui.Controls.resources => 0x392eee9cdda86574 => 373
	i64 4148881117810174540, ; 172: System.Runtime.InteropServices.JavaScript.dll => 0x3993c9651a66aa4c => 105
	i64 4154383907710350974, ; 173: System.ComponentModel => 0x39a7562737acb67e => 18
	i64 4167269041631776580, ; 174: System.Threading.ThreadPool => 0x39d51d1d3df1cf44 => 146
	i64 4168469861834746866, ; 175: System.Security.Claims.dll => 0x39d96140fb94ebf2 => 118
	i64 4187479170553454871, ; 176: System.Linq.Expressions => 0x3a1cea1e912fa117 => 58
	i64 4201423742386704971, ; 177: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 279
	i64 4205801962323029395, ; 178: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 17
	i64 4235503420553921860, ; 179: System.IO.IsolatedStorage.dll => 0x3ac787eb9b118544 => 52
	i64 4282138915307457788, ; 180: System.Reflection.Emit => 0x3b6d36a7ddc70cfc => 92
	i64 4337444564132831293, ; 181: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 250
	i64 4360412976914417659, ; 182: tr\Microsoft.Maui.Controls.resources.dll => 0x3c834c8002fcc7fb => 368
	i64 4373617458794931033, ; 183: System.IO.Pipes.dll => 0x3cb235e806eb2359 => 55
	i64 4397634830160618470, ; 184: System.Security.SecureString.dll => 0x3d0789940f9be3e6 => 129
	i64 4477672992252076438, ; 185: System.Web.HttpUtility.dll => 0x3e23e3dcdb8ba196 => 152
	i64 4484706122338676047, ; 186: System.Globalization.Extensions.dll => 0x3e3ce07510042d4f => 41
	i64 4513320955448359355, ; 187: Microsoft.EntityFrameworkCore.Relational => 0x3ea2897f12d379bb => 193
	i64 4533124835995628778, ; 188: System.Reflection.Emit.dll => 0x3ee8e505540534ea => 92
	i64 4612482779465751747, ; 189: Microsoft.EntityFrameworkCore.Abstractions => 0x4002d4a662a99cc3 => 192
	i64 4636684751163556186, ; 190: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 323
	i64 4657212095206026001, ; 191: Microsoft.Extensions.Http.dll => 0x40a1bdb9c2686b11 => 205
	i64 4672453897036726049, ; 192: System.IO.FileSystem.Watcher => 0x40d7e4104a437f21 => 50
	i64 4716677666592453464, ; 193: System.Xml.XmlSerializer => 0x417501590542f358 => 162
	i64 4743821336939966868, ; 194: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 13
	i64 4748433834360566865, ; 195: Microsoft.Recognizers.Text => 0x41e5d36a0081f451 => 229
	i64 4759461199762736555, ; 196: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 297
	i64 4776553240714709859, ; 197: Infrastructure.Data => 0x4249b9dd7b8a8b63 => 379
	i64 4794310189461587505, ; 198: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 261
	i64 4795410492532947900, ; 199: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 318
	i64 4809057822547766521, ; 200: System.Drawing => 0x42bd349c3145ecf9 => 36
	i64 4814660307502931973, ; 201: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 67
	i64 4853321196694829351, ; 202: System.Runtime.Loader.dll => 0x435a75ea15de7927 => 109
	i64 4871824391508510238, ; 203: nb\Microsoft.Maui.Controls.resources.dll => 0x439c3278d7f2c61e => 358
	i64 4894585304434094697, ; 204: Apps.MauiRunner.dll => 0x43ed0f66d9a43269 => 0
	i64 4953714692329509532, ; 205: sv\Microsoft.Maui.Controls.resources.dll => 0x44bf21444aef129c => 366
	i64 4984589564328971462, ; 206: FluentValidation => 0x452cd1cc9cf28cc6 => 181
	i64 5050175356009799755, ; 207: Microsoft.ML.CpuMath.dll => 0x4615d3bab465604b => 225
	i64 5055365687667823624, ; 208: Xamarin.AndroidX.Activity.Ktx.dll => 0x4628444ef7239408 => 262
	i64 5081566143765835342, ; 209: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 99
	i64 5099468265966638712, ; 210: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 99
	i64 5103417709280584325, ; 211: System.Collections.Specialized => 0x46d2fb5e161b6285 => 11
	i64 5129462924058778861, ; 212: Microsoft.Data.Sqlite => 0x472f835a350f5ced => 190
	i64 5182934613077526976, ; 213: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 11
	i64 5205316157927637098, ; 214: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 304
	i64 5244375036463807528, ; 215: System.Diagnostics.Contracts.dll => 0x48c7c34f4d59fc28 => 25
	i64 5262971552273843408, ; 216: System.Security.Principal.dll => 0x4909d4be0c44c4d0 => 128
	i64 5278787618751394462, ; 217: System.Net.WebClient.dll => 0x4942055efc68329e => 76
	i64 5280980186044710147, ; 218: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x4949cf7fd7123d03 => 296
	i64 5290786973231294105, ; 219: System.Runtime.Loader => 0x496ca6b869b72699 => 109
	i64 5301302169971007484, ; 220: WebSources.Common.dll => 0x4992023c70211ffc => 386
	i64 5376510917114486089, ; 221: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 322
	i64 5404556524862317797, ; 222: FluentValidation.dll => 0x4b00d78658f024e5 => 181
	i64 5408338804355907810, ; 223: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 320
	i64 5423376490970181369, ; 224: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 106
	i64 5440320908473006344, ; 225: Microsoft.VisualBasic.Core => 0x4b7fe70acda9f908 => 2
	i64 5446034149219586269, ; 226: System.Diagnostics.Debug => 0x4b94333452e150dd => 26
	i64 5451019430259338467, ; 227: Xamarin.AndroidX.ConstraintLayout.dll => 0x4ba5e94a845c2ce3 => 275
	i64 5457765010617926378, ; 228: System.Xml.Serialization => 0x4bbde05c557002ea => 157
	i64 5471532531798518949, ; 229: sv\Microsoft.Maui.Controls.resources => 0x4beec9d926d82ca5 => 366
	i64 5507995362134886206, ; 230: System.Core.dll => 0x4c705499688c873e => 21
	i64 5513385544875721796, ; 231: Python.Included => 0x4c837af120174444 => 247
	i64 5522859530602327440, ; 232: uk\Microsoft.Maui.Controls.resources => 0x4ca5237b51eead90 => 369
	i64 5527431512186326818, ; 233: System.IO.FileSystem.Primitives.dll => 0x4cb561acbc2a8f22 => 49
	i64 5570799893513421663, ; 234: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 43
	i64 5573260873512690141, ; 235: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 126
	i64 5574231584441077149, ; 236: Xamarin.AndroidX.Annotation.Jvm => 0x4d5ba617ae5f8d9d => 265
	i64 5591791169662171124, ; 237: System.Linq.Parallel => 0x4d9a087135e137f4 => 59
	i64 5638251088794709185, ; 238: Catalyst.Spacy => 0x4e3f177e16116cc1 => 176
	i64 5650097808083101034, ; 239: System.Security.Cryptography.Algorithms.dll => 0x4e692e055d01a56a => 119
	i64 5692067934154308417, ; 240: Xamarin.AndroidX.ViewPager2.dll => 0x4efe49a0d4a8bb41 => 325
	i64 5724799082821825042, ; 241: Xamarin.AndroidX.ExifInterface => 0x4f72926f3e13b212 => 288
	i64 5757522595884336624, ; 242: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 274
	i64 5763738884890217630, ; 243: Nito.AsyncEx.Coordination.dll => 0x4ffce9fa6bfccc9e => 238
	i64 5783556987928984683, ; 244: Microsoft.VisualBasic => 0x504352701bbc3c6b => 3
	i64 5896680224035167651, ; 245: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 294
	i64 5959344983920014087, ; 246: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x52b3d8b05c8ef307 => 314
	i64 5979151488806146654, ; 247: System.Formats.Asn1 => 0x52fa3699a489d25e => 38
	i64 5979606371666529271, ; 248: NickBuhro.Translit => 0x52fbd4504fd88bf7 => 236
	i64 5984759512290286505, ; 249: System.Security.Cryptography.Primitives => 0x530e23115c33dba9 => 124
	i64 5986721769312442665, ; 250: WebSources.ReversoContext.dll => 0x53151bbaecf75929 => 388
	i64 6010974535988770325, ; 251: Microsoft.Extensions.Diagnostics.dll => 0x536b457e33877615 => 203
	i64 6102788177522843259, ; 252: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0x54b1758374b3de7b => 314
	i64 6156085254003611741, ; 253: Domain.WordModels => 0x556eceec13e7d85d => 378
	i64 6183170893902868313, ; 254: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 250
	i64 6200764641006662125, ; 255: ro\Microsoft.Maui.Controls.resources => 0x560d8a96830131ed => 363
	i64 6222399776351216807, ; 256: System.Text.Json.dll => 0x565a67a0ffe264a7 => 137
	i64 6251069312384999852, ; 257: System.Transactions.Local => 0x56c0426b870da1ac => 149
	i64 6278736998281604212, ; 258: System.Private.DataContractSerialization => 0x57228e08a4ad6c74 => 85
	i64 6284145129771520194, ; 259: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 90
	i64 6300676701234028427, ; 260: es\Microsoft.Maui.Controls.resources.dll => 0x57708013cda25f8b => 346
	i64 6303175102741384284, ; 261: Domain.Common => 0x5779605c3bfc8c5c => 375
	i64 6319713645133255417, ; 262: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 298
	i64 6357457916754632952, ; 263: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 389
	i64 6358467586697636532, ; 264: Tools.Validations => 0x583dd094dcf2aab4 => 384
	i64 6401687960814735282, ; 265: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 295
	i64 6471714727257221498, ; 266: fi\Microsoft.Maui.Controls.resources.dll => 0x59d026417dd4a97a => 347
	i64 6478287442656530074, ; 267: hr\Microsoft.Maui.Controls.resources => 0x59e7801b0c6a8e9a => 351
	i64 6504860066809920875, ; 268: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 270
	i64 6548213210057960872, ; 269: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 281
	i64 6557084851308642443, ; 270: Xamarin.AndroidX.Window.dll => 0x5aff71ee6c58c08b => 326
	i64 6557387789652226423, ; 271: Nito.AsyncEx.Oop.dll => 0x5b008573c0b47177 => 240
	i64 6560151584539558821, ; 272: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 209
	i64 6589202984700901502, ; 273: Xamarin.Google.ErrorProne.Annotations.dll => 0x5b718d34180a787e => 331
	i64 6591971792923354531, ; 274: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x5b7b636b7e9765a3 => 296
	i64 6617685658146568858, ; 275: System.Text.Encoding.CodePages => 0x5bd6be0b4905fa9a => 133
	i64 6713440830605852118, ; 276: System.Reflection.TypeExtensions.dll => 0x5d2aeeddb8dd7dd6 => 96
	i64 6739853162153639747, ; 277: Microsoft.VisualBasic.dll => 0x5d88c4bde075ff43 => 3
	i64 6743165466166707109, ; 278: nl\Microsoft.Maui.Controls.resources => 0x5d948943c08c43a5 => 359
	i64 6772837112740759457, ; 279: System.Runtime.InteropServices.JavaScript => 0x5dfdf378527ec7a1 => 105
	i64 6786606130239981554, ; 280: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 33
	i64 6798329586179154312, ; 281: System.Windows => 0x5e5884bd523ca188 => 154
	i64 6814185388980153342, ; 282: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 158
	i64 6876862101832370452, ; 283: System.Xml.Linq => 0x5f6f85a57d108914 => 155
	i64 6894844156784520562, ; 284: System.Numerics.Vectors => 0x5faf683aead1ad72 => 82
	i64 7011053663211085209, ; 285: Xamarin.AndroidX.Fragment.Ktx => 0x614c442918e5dd99 => 290
	i64 7060896174307865760, ; 286: System.Threading.Tasks.Parallel.dll => 0x61fd57a90988f4a0 => 143
	i64 7083547580668757502, ; 287: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 87
	i64 7101497697220435230, ; 288: System.Configuration => 0x628d9687c0141d1e => 19
	i64 7103753931438454322, ; 289: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 291
	i64 7112547816752919026, ; 290: System.IO.FileSystem => 0x62b4d88e3189b1f2 => 51
	i64 7192745174564810625, ; 291: Xamarin.Android.Glide.GifDecoder.dll => 0x63d1c3a0a1d72f81 => 260
	i64 7270811800166795866, ; 292: System.Linq => 0x64e71ccf51a90a5a => 61
	i64 7299370801165188114, ; 293: System.IO.Pipes.AccessControl.dll => 0x654c9311e74f3c12 => 54
	i64 7316205155833392065, ; 294: Microsoft.Win32.Primitives => 0x658861d38954abc1 => 4
	i64 7338192458477945005, ; 295: System.Reflection => 0x65d67f295d0740ad => 97
	i64 7349431895026339542, ; 296: Xamarin.Android.Glide.DiskLruCache => 0x65fe6d5e9bf88ed6 => 259
	i64 7377312882064240630, ; 297: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 17
	i64 7394319280655904474, ; 298: Microsoft.ML.KMeansClustering.dll => 0x669de6357f512eda => 220
	i64 7488575175965059935, ; 299: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 155
	i64 7489048572193775167, ; 300: System.ObjectModel => 0x67ee71ff6b419e3f => 84
	i64 7592577537120840276, ; 301: System.Diagnostics.Process => 0x695e410af5b2aa54 => 29
	i64 7637303409920963731, ; 302: System.IO.Compression.ZipFile.dll => 0x69fd26fcb637f493 => 45
	i64 7654504624184590948, ; 303: System.Net.Http => 0x6a3a4366801b8264 => 64
	i64 7661539998969416453, ; 304: Microsoft.ML.Transforms.dll => 0x6a5342095704cb05 => 223
	i64 7694700312542370399, ; 305: System.Net.Mail => 0x6ac9112a7e2cda5f => 66
	i64 7713917983711940402, ; 306: Microsoft.ML.Core.dll => 0x6b0d57893da5cf32 => 218
	i64 7714652370974252055, ; 307: System.Private.CoreLib => 0x6b0ff375198b9c17 => 172
	i64 7724393568250365724, ; 308: Microsoft.ML.PCA => 0x6b328f0654e0071c => 221
	i64 7725404731275645577, ; 309: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x6b3626ac11ce9289 => 299
	i64 7735176074855944702, ; 310: Microsoft.CSharp => 0x6b58dda848e391fe => 1
	i64 7735352534559001595, ; 311: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 334
	i64 7742555799473854801, ; 312: it\Microsoft.Maui.Controls.resources.dll => 0x6b73157a51479951 => 354
	i64 7791074099216502080, ; 313: System.IO.FileSystem.AccessControl.dll => 0x6c1f749d468bcd40 => 47
	i64 7820441508502274321, ; 314: System.Data => 0x6c87ca1e14ff8111 => 24
	i64 7836164640616011524, ; 315: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 267
	i64 7919757340696389605, ; 316: Microsoft.Extensions.Diagnostics.Abstractions => 0x6de8a157378027e5 => 204
	i64 7972383140441761405, ; 317: Microsoft.Extensions.Caching.Abstractions.dll => 0x6ea3983a0b58267d => 195
	i64 7975724199463739455, ; 318: sk\Microsoft.Maui.Controls.resources.dll => 0x6eaf76e6f785e03f => 365
	i64 7996969948337405685, ; 319: WebSources.Linker => 0x6efaf1cc5a1ffef5 => 387
	i64 8025517457475554965, ; 320: WindowsBase => 0x6f605d9b4786ce95 => 165
	i64 8031450141206250471, ; 321: System.Runtime.Intrinsics.dll => 0x6f757159d9dc03e7 => 108
	i64 8047356596405340068, ; 322: Tools.Parsers.dll => 0x6fadf4300da307a4 => 383
	i64 8064050204834738623, ; 323: System.Collections.dll => 0x6fe942efa61731bf => 12
	i64 8083354569033831015, ; 324: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 293
	i64 8085230611270010360, ; 325: System.Net.Http.Json.dll => 0x703482674fdd05f8 => 63
	i64 8087206902342787202, ; 326: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 27
	i64 8103644804370223335, ; 327: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 23
	i64 8108129031893776750, ; 328: ko\Microsoft.Maui.Controls.resources.dll => 0x7085dc65530f796e => 356
	i64 8113615946733131500, ; 329: System.Reflection.Extensions => 0x70995ab73cf916ec => 93
	i64 8118302431275239495, ; 330: Nito.AsyncEx.Oop => 0x70aa010c7359c847 => 240
	i64 8167236081217502503, ; 331: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 168
	i64 8185542183669246576, ; 332: System.Collections => 0x7198e33f4794aa70 => 12
	i64 8187640529827139739, ; 333: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 338
	i64 8246048515196606205, ; 334: Microsoft.Maui.Graphics.dll => 0x726fd96f64ee56fd => 217
	i64 8264926008854159966, ; 335: System.Diagnostics.Process.dll => 0x72b2ea6a64a3a25e => 29
	i64 8290740647658429042, ; 336: System.Runtime.Extensions => 0x730ea0b15c929a72 => 103
	i64 8293702073711834350, ; 337: System.Linq.Async => 0x731926181883b4ee => 255
	i64 8318905602908530212, ; 338: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 14
	i64 8329843434826495442, ; 339: ICSharpCode.SharpZipLib => 0x73998c787773f5d2 => 249
	i64 8368701292315763008, ; 340: System.Security.Cryptography => 0x7423997c6fd56140 => 126
	i64 8386351099740279196, ; 341: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x74624de475b9d19c => 371
	i64 8398329775253868912, ; 342: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x748cdc6f3097d170 => 276
	i64 8400357532724379117, ; 343: Xamarin.AndroidX.Navigation.UI.dll => 0x749410ab44503ded => 308
	i64 8402288248351260170, ; 344: Catalyst.Spacy.dll => 0x749aeca5076a7e0a => 176
	i64 8410671156615598628, ; 345: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 91
	i64 8426919725312979251, ; 346: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 297
	i64 8518412311883997971, ; 347: System.Collections.Immutable => 0x76377add7c28e313 => 9
	i64 8541187067250985012, ; 348: Tools.Http.dll => 0x768864626fa7c834 => 382
	i64 8563666267364444763, ; 349: System.Private.Uri => 0x76d841191140ca5b => 86
	i64 8598790081731763592, ; 350: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 287
	i64 8599632406834268464, ; 351: CommunityToolkit.Maui => 0x7758081c784b4930 => 177
	i64 8601935802264776013, ; 352: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 320
	i64 8623059219396073920, ; 353: System.Net.Quic.dll => 0x77ab42ac514299c0 => 71
	i64 8626175481042262068, ; 354: Java.Interop => 0x77b654e585b55834 => 168
	i64 8638972117149407195, ; 355: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 1
	i64 8639588376636138208, ; 356: Xamarin.AndroidX.Navigation.Runtime => 0x77e5fbdaa2fda2e0 => 307
	i64 8643856503331104902, ; 357: Microsoft.Recognizers.Text.dll => 0x77f525b152802886 => 229
	i64 8648495978913578441, ; 358: Microsoft.Win32.Registry.dll => 0x7805a1456889bdc9 => 5
	i64 8677882282824630478, ; 359: pt-BR\Microsoft.Maui.Controls.resources => 0x786e07f5766b00ce => 361
	i64 8684531736582871431, ; 360: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 44
	i64 8708565345795569651, ; 361: MediatR.Contracts => 0x78db0a0ac36bf7f3 => 186
	i64 8725526185868997716, ; 362: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 27
	i64 8816904670177563593, ; 363: Microsoft.Extensions.Diagnostics => 0x7a5bf015646a93c9 => 203
	i64 8853378295825400934, ; 364: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 335
	i64 8888008862345921665, ; 365: Nito.AsyncEx.Tasks.dll => 0x7b588cf838a70481 => 241
	i64 8914455469365692833, ; 366: WebSources.CambridgeDictionary => 0x7bb68204c19489a1 => 385
	i64 8941376889969657626, ; 367: System.Xml.XDocument => 0x7c1626e87187471a => 158
	i64 8951477988056063522, ; 368: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 310
	i64 8954753533646919997, ; 369: System.Runtime.Serialization.Json => 0x7c45ace50032d93d => 112
	i64 9045785047181495996, ; 370: zh-HK\Microsoft.Maui.Controls.resources => 0x7d891592e3cb0ebc => 371
	i64 9111603110219107042, ; 371: Microsoft.Extensions.Caching.Memory => 0x7e72eac0def44ae2 => 196
	i64 9138683372487561558, ; 372: System.Security.Cryptography.Csp => 0x7ed3201bc3e3d156 => 121
	i64 9250544137016314866, ; 373: Microsoft.EntityFrameworkCore => 0x806088e191ee0bf2 => 191
	i64 9287535527870586490, ; 374: Tools.Http => 0x80e3f45ae98bde7a => 382
	i64 9312692141327339315, ; 375: Xamarin.AndroidX.ViewPager2 => 0x813d54296a634f33 => 325
	i64 9324707631942237306, ; 376: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 266
	i64 9363564275759486853, ; 377: el\Microsoft.Maui.Controls.resources.dll => 0x81f21019382e9785 => 345
	i64 9450774115493428928, ; 378: Microsoft.Recognizers.Definitions.dll => 0x8327e4fc56713ec0 => 228
	i64 9460088545788012197, ; 379: Mosaik.Core => 0x8348fc69236fa6a5 => 234
	i64 9468215723722196442, ; 380: System.Xml.XPath.XDocument.dll => 0x8365dc09353ac5da => 159
	i64 9551379474083066910, ; 381: uk\Microsoft.Maui.Controls.resources.dll => 0x848d5106bbadb41e => 369
	i64 9554839972845591462, ; 382: System.ServiceModel.Web => 0x84999c54e32a1ba6 => 131
	i64 9575902398040817096, ; 383: Xamarin.Google.Crypto.Tink.Android.dll => 0x84e4707ee708bdc8 => 330
	i64 9584643793929893533, ; 384: System.IO.dll => 0x85037ebfbbd7f69d => 57
	i64 9659729154652888475, ; 385: System.Text.RegularExpressions => 0x860e407c9991dd9b => 138
	i64 9662334977499516867, ; 386: System.Numerics.dll => 0x8617827802b0cfc3 => 83
	i64 9667360217193089419, ; 387: System.Diagnostics.StackTrace => 0x86295ce5cd89898b => 30
	i64 9678050649315576968, ; 388: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 277
	i64 9702891218465930390, ; 389: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 10
	i64 9773637193738963987, ; 390: ca\Microsoft.Maui.Controls.resources.dll => 0x87a2ef3ea85b4c13 => 341
	i64 9780093022148426479, ; 391: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x87b9dec9576efaef => 327
	i64 9808709177481450983, ; 392: Mono.Android.dll => 0x881f890734e555e7 => 171
	i64 9825649861376906464, ; 393: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 274
	i64 9834056768316610435, ; 394: System.Transactions.dll => 0x8879968718899783 => 150
	i64 9836529246295212050, ; 395: System.Reflection.Metadata => 0x88825f3bbc2ac012 => 94
	i64 9864956466380592553, ; 396: Microsoft.EntityFrameworkCore.Sqlite => 0x88e75da3af4ed5a9 => 194
	i64 9874244970929996684, ; 397: Mosaik.Core.dll => 0x89085d7c2d78438c => 234
	i64 9875831699105201959, ; 398: Nito.AsyncEx.Tasks => 0x890e009b1eb53f27 => 241
	i64 9907349773706910547, ; 399: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 287
	i64 9933555792566666578, ; 400: System.Linq.Queryable.dll => 0x89db145cf475c552 => 60
	i64 9956195530459977388, ; 401: Microsoft.Maui => 0x8a2b8315b36616ac => 215
	i64 9959489431142554298, ; 402: System.CodeDom => 0x8a3736deb7825aba => 254
	i64 9974604633896246661, ; 403: System.Xml.Serialization.dll => 0x8a6cea111a59dd85 => 157
	i64 10017511394021241210, ; 404: Microsoft.Extensions.Logging.Debug => 0x8b055989ae10717a => 208
	i64 10038780035334861115, ; 405: System.Net.Http.dll => 0x8b50e941206af13b => 64
	i64 10041592016144926793, ; 406: Microsoft.ML.KMeansClustering => 0x8b5ae6bc6db78849 => 220
	i64 10051358222726253779, ; 407: System.Private.Xml => 0x8b7d990c97ccccd3 => 88
	i64 10078727084704864206, ; 408: System.Net.WebSockets.Client => 0x8bded4e257f117ce => 79
	i64 10089571585547156312, ; 409: System.IO.FileSystem.AccessControl => 0x8c055be67469bb58 => 47
	i64 10092835686693276772, ; 410: Microsoft.Maui.Controls => 0x8c10f49539bd0c64 => 213
	i64 10105485790837105934, ; 411: System.Threading.Tasks.Parallel => 0x8c3de5c91d9a650e => 143
	i64 10138029046405158360, ; 412: Microsoft.Recognizers.Text.DataTypes.TimexExpression.dll => 0x8cb183b4419255d8 => 230
	i64 10143853363526200146, ; 413: da\Microsoft.Maui.Controls.resources => 0x8cc634e3c2a16b52 => 343
	i64 10205853378024263619, ; 414: Microsoft.Extensions.Configuration.Binder => 0x8da279930adb4fc3 => 199
	i64 10209869394718195525, ; 415: nl\Microsoft.Maui.Controls.resources.dll => 0x8db0be1ecb4f7f45 => 359
	i64 10223714027811491717, ; 416: Tools.Common.dll => 0x8de1edbda0e1cf85 => 381
	i64 10226222362177979215, ; 417: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 336
	i64 10229024438826829339, ; 418: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 281
	i64 10236703004850800690, ; 419: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 74
	i64 10245369515835430794, ; 420: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 91
	i64 10321854143672141184, ; 421: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 333
	i64 10360651442923773544, ; 422: System.Text.Encoding => 0x8fc86d98211c1e68 => 135
	i64 10364469296367737616, ; 423: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 90
	i64 10374097620777714788, ; 424: Application.Common => 0x8ff832d25fae4464 => 374
	i64 10376576884623852283, ; 425: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 319
	i64 10406448008575299332, ; 426: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 339
	i64 10430153318873392755, ; 427: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 278
	i64 10447083246144586668, ; 428: Microsoft.Bcl.AsyncInterfaces.dll => 0x90fb7edc816203ac => 189
	i64 10506226065143327199, ; 429: ca\Microsoft.Maui.Controls.resources => 0x91cd9cf11ed169df => 341
	i64 10546663366131771576, ; 430: System.Runtime.Serialization.Json.dll => 0x925d4673efe8e8b8 => 112
	i64 10566960649245365243, ; 431: System.Globalization.dll => 0x92a562b96dcd13fb => 42
	i64 10577027937037808856, ; 432: AngleSharp => 0x92c926de868574d8 => 173
	i64 10586492127710950038, ; 433: Python.Runtime => 0x92eac68021ae3296 => 248
	i64 10595762989148858956, ; 434: System.Xml.XPath.XDocument => 0x930bb64cc472ea4c => 159
	i64 10670374202010151210, ; 435: Microsoft.Win32.Primitives.dll => 0x9414c8cd7b4ea92a => 4
	i64 10681343289177776264, ; 436: ML.Predictor.dll => 0x943bc1211b71a088 => 380
	i64 10700352648151603995, ; 437: Microsoft.Recognizers.Text.DateTime => 0x947f4a0a647c9b1b => 231
	i64 10714184849103829812, ; 438: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 103
	i64 10761706286639228993, ; 439: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0x955942d988382841 => 373
	i64 10785150219063592792, ; 440: System.Net.Primitives => 0x95ac8cfb68830758 => 70
	i64 10809043855025277762, ; 441: Microsoft.Extensions.Options.ConfigurationExtensions => 0x9601701e0c668b42 => 210
	i64 10810768014613037474, ; 442: Microsoft.Recognizers.Text.DataTypes.TimexExpression => 0x9607903b3c2a41a2 => 230
	i64 10811915265162633087, ; 443: Microsoft.EntityFrameworkCore.Relational.dll => 0x960ba3a651a45f7f => 193
	i64 10822644899632537592, ; 444: System.Linq.Queryable => 0x9631c23204ca5ff8 => 60
	i64 10830817578243619689, ; 445: System.Formats.Tar => 0x964ecb340a447b69 => 39
	i64 10847732767863316357, ; 446: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 268
	i64 10880838204485145808, ; 447: CommunityToolkit.Maui.dll => 0x970080b2a4d614d0 => 177
	i64 10899834349646441345, ; 448: System.Web => 0x9743fd975946eb81 => 153
	i64 10943875058216066601, ; 449: System.IO.UnmanagedMemoryStream.dll => 0x97e07461df39de29 => 56
	i64 10953751836886437922, ; 450: System.Linq.Async.dll => 0x98038b429b661022 => 255
	i64 10964653383833615866, ; 451: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 34
	i64 11002576679268595294, ; 452: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 207
	i64 11009005086950030778, ; 453: Microsoft.Maui.dll => 0x98c7d7cc621ffdba => 215
	i64 11019817191295005410, ; 454: Xamarin.AndroidX.Annotation.Jvm.dll => 0x98ee415998e1b2e2 => 265
	i64 11023048688141570732, ; 455: System.Core => 0x98f9bc61168392ac => 21
	i64 11037814507248023548, ; 456: System.Xml => 0x992e31d0412bf7fc => 163
	i64 11071824625609515081, ; 457: Xamarin.Google.ErrorProne.Annotations => 0x99a705d600e0a049 => 331
	i64 11078422477095414952, ; 458: MessagePack.Annotations => 0x99be768c02f9aca8 => 188
	i64 11103970607964515343, ; 459: hu\Microsoft.Maui.Controls.resources => 0x9a193a6fc41a6c0f => 352
	i64 11136029745144976707, ; 460: Jsr305Binding.dll => 0x9a8b200d4f8cd543 => 329
	i64 11156122287428000958, ; 461: th\Microsoft.Maui.Controls.resources.dll => 0x9ad2821cdcf6dcbe => 367
	i64 11157797727133427779, ; 462: fr\Microsoft.Maui.Controls.resources.dll => 0x9ad875ea9172e843 => 348
	i64 11162124722117608902, ; 463: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 324
	i64 11188319605227840848, ; 464: System.Threading.Overlapped => 0x9b44e5671724e550 => 140
	i64 11220793807500858938, ; 465: ja\Microsoft.Maui.Controls.resources => 0x9bb8448481fdd63a => 355
	i64 11226290749488709958, ; 466: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 209
	i64 11235648312900863002, ; 467: System.Reflection.DispatchProxy.dll => 0x9bed0a9c8fac441a => 89
	i64 11329751333533450475, ; 468: System.Threading.Timer.dll => 0x9d3b5ccf6cc500eb => 147
	i64 11340910727871153756, ; 469: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 280
	i64 11347436699239206956, ; 470: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 162
	i64 11392833485892708388, ; 471: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 309
	i64 11398376662953476300, ; 472: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x9e2f2b2f0b71c0cc => 194
	i64 11432101114902388181, ; 473: System.AppContext => 0x9ea6fb64e61a9dd5 => 6
	i64 11446671985764974897, ; 474: Mono.Android.Export => 0x9edabf8623efc131 => 169
	i64 11448276831755070604, ; 475: System.Diagnostics.TextWriterTraceListener => 0x9ee0731f77186c8c => 31
	i64 11485890710487134646, ; 476: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 107
	i64 11508496261504176197, ; 477: Xamarin.AndroidX.Fragment.Ktx.dll => 0x9fb664600dde1045 => 290
	i64 11518296021396496455, ; 478: id\Microsoft.Maui.Controls.resources => 0x9fd9353475222047 => 353
	i64 11529969570048099689, ; 479: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 324
	i64 11530571088791430846, ; 480: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 206
	i64 11580057168383206117, ; 481: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 263
	i64 11591352189662810718, ; 482: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 317
	i64 11597940890313164233, ; 483: netstandard => 0xa0f429ca8d1805c9 => 167
	i64 11618208124946770737, ; 484: WebSources.Linker.dll => 0xa13c2abbd6026b31 => 387
	i64 11672361001936329215, ; 485: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 291
	i64 11692977985522001935, ; 486: System.Threading.Overlapped.dll => 0xa245cd869980680f => 140
	i64 11707554492040141440, ; 487: System.Linq.Parallel.dll => 0xa27996c7fe94da80 => 59
	i64 11743665907891708234, ; 488: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 144
	i64 11845411120810355536, ; 489: FluentValidation.DependencyInjectionExtensions => 0xa4635aa79a5ceb50 => 182
	i64 11855031688536399763, ; 490: vi\Microsoft.Maui.Controls.resources.dll => 0xa485888294361f93 => 370
	i64 11873143858965261388, ; 491: Microsoft.ML.Data.dll => 0xa4c5e16ee0bc2c4c => 219
	i64 11991047634523762324, ; 492: System.Net => 0xa668c24ad493ae94 => 81
	i64 12036415867504728778, ; 493: CsvHelper => 0xa709f075b77e9aca => 180
	i64 12040886584167504988, ; 494: System.Net.ServicePoint => 0xa719d28d8e121c5c => 74
	i64 12063623837170009990, ; 495: System.Security => 0xa76a99f6ce740786 => 130
	i64 12075303808054746781, ; 496: Plugin.Maui.Audio.dll => 0xa79418d5f2065e9d => 245
	i64 12087937285434132119, ; 497: Nito.AsyncEx.Coordination => 0xa7c0faea9d804697 => 238
	i64 12090529733743980508, ; 498: Microsoft.ML.StandardTrainers.dll => 0xa7ca30bc061bafdc => 222
	i64 12096697103934194533, ; 499: System.Diagnostics.Contracts => 0xa7e019eccb7e8365 => 25
	i64 12102847907131387746, ; 500: System.Buffers => 0xa7f5f40c43256f62 => 7
	i64 12123043025855404482, ; 501: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 93
	i64 12137774235383566651, ; 502: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 321
	i64 12145679461940342714, ; 503: System.Text.Json => 0xa88e1f1ebcb62fba => 137
	i64 12191646537372739477, ; 504: Xamarin.Android.Glide.dll => 0xa9316dee7f392795 => 257
	i64 12201331334810686224, ; 505: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 113
	i64 12269460666702402136, ; 506: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 9
	i64 12279246230491828964, ; 507: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 253
	i64 12285418208979896853, ; 508: ML.Predictor => 0xaa7e92c5275cca15 => 380
	i64 12312677680500400930, ; 509: WebSources.ReversoContext => 0xaadf6b1d741b6322 => 388
	i64 12332222936682028543, ; 510: System.Runtime.Handles => 0xab24db6c07db5dff => 104
	i64 12341818387765915815, ; 511: CommunityToolkit.Maui.Core.dll => 0xab46f26f152bf0a7 => 178
	i64 12375446203996702057, ; 512: System.Configuration.dll => 0xabbe6ac12e2e0569 => 19
	i64 12451044538927396471, ; 513: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 289
	i64 12466513435562512481, ; 514: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 303
	i64 12475113361194491050, ; 515: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 389
	i64 12487638416075308985, ; 516: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 283
	i64 12517810545449516888, ; 517: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 33
	i64 12538491095302438457, ; 518: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 271
	i64 12550732019250633519, ; 519: System.IO.Compression => 0xae2d28465e8e1b2f => 46
	i64 12680623572785838193, ; 520: Catalyst.Models.English.dll => 0xaffa9ff462583871 => 175
	i64 12691405489416028621, ; 521: Microsoft.ML.Data => 0xb020ee0cf915c1cd => 219
	i64 12699999919562409296, ; 522: System.Diagnostics.StackTrace.dll => 0xb03f76a3ad01c550 => 30
	i64 12700543734426720211, ; 523: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 272
	i64 12708238894395270091, ; 524: System.IO => 0xb05cbbf17d3ba3cb => 57
	i64 12708922737231849740, ; 525: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 134
	i64 12717050818822477433, ; 526: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 114
	i64 12726057104018228727, ; 527: Microsoft.NET.StringTools => 0xb09c0982b457c5f7 => 227
	i64 12753841065332862057, ; 528: Xamarin.AndroidX.Window => 0xb0febee04cf46c69 => 326
	i64 12811461558951005639, ; 529: Microsoft.Recognizers.Text.NumberWithUnit.dll => 0xb1cb7468ead8cdc7 => 233
	i64 12828192437253469131, ; 530: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 337
	i64 12835242264250840079, ; 531: System.IO.Pipes => 0xb21ff0d5d6c0740f => 55
	i64 12843321153144804894, ; 532: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 211
	i64 12843770487262409629, ; 533: System.AppContext.dll => 0xb23e3d357debf39d => 6
	i64 12859557719246324186, ; 534: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 77
	i64 12947507347511303976, ; 535: Microsoft.Recognizers.Text.DateTime.dll => 0xb3aec9556f2bdf28 => 231
	i64 12982280885948128408, ; 536: Xamarin.AndroidX.CustomView.PoolingContainer => 0xb42a53aec5481c98 => 282
	i64 12989346753972519995, ; 537: ar\Microsoft.Maui.Controls.resources.dll => 0xb4436e0d5ee7c43b => 340
	i64 13005833372463390146, ; 538: pt-BR\Microsoft.Maui.Controls.resources.dll => 0xb47e008b5d99ddc2 => 361
	i64 13041731019321118825, ; 539: Plugin.Maui.Audio => 0xb4fd894396d41069 => 245
	i64 13068258254871114833, ; 540: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 111
	i64 13129914918964716986, ; 541: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 286
	i64 13173818576982874404, ; 542: System.Runtime.CompilerServices.VisualC.dll => 0xb6d2ce32a8819924 => 102
	i64 13343850469010654401, ; 543: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 170
	i64 13370592475155966277, ; 544: System.Runtime.Serialization => 0xb98de304062ea945 => 115
	i64 13381594904270902445, ; 545: he\Microsoft.Maui.Controls.resources => 0xb9b4f9aaad3e94ad => 349
	i64 13401370062847626945, ; 546: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 321
	i64 13404347523447273790, ; 547: Xamarin.AndroidX.ConstraintLayout.Core => 0xba05cf0da4f6393e => 276
	i64 13431476299110033919, ; 548: System.Net.WebClient => 0xba663087f18829ff => 76
	i64 13454009404024712428, ; 549: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 332
	i64 13463706743370286408, ; 550: System.Private.DataContractSerialization.dll => 0xbad8b1f3069e0548 => 85
	i64 13465488254036897740, ; 551: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 334
	i64 13472593393856384934, ; 552: Nito.AsyncEx.Interop.WaitHandles => 0xbaf8444f94158fa6 => 239
	i64 13491513212026656886, ; 553: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 269
	i64 13540124433173649601, ; 554: vi\Microsoft.Maui.Controls.resources => 0xbbe82f6eede718c1 => 370
	i64 13572454107664307259, ; 555: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 311
	i64 13578472628727169633, ; 556: System.Xml.XPath => 0xbc706ce9fba5c261 => 160
	i64 13580399111273692417, ; 557: Microsoft.VisualBasic.Core.dll => 0xbc77450a277fbd01 => 2
	i64 13621154251410165619, ; 558: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0xbd080f9faa1acf73 => 282
	i64 13647894001087880694, ; 559: System.Data.dll => 0xbd670f48cb071df6 => 24
	i64 13675589307506966157, ; 560: Xamarin.AndroidX.Activity.Ktx => 0xbdc97404d0153e8d => 262
	i64 13702626353344114072, ; 561: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 32
	i64 13710614125866346983, ; 562: System.Security.AccessControl.dll => 0xbe45e2e7d0b769e7 => 117
	i64 13713329104121190199, ; 563: System.Dynamic.Runtime => 0xbe4f8829f32b5737 => 37
	i64 13717397318615465333, ; 564: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 16
	i64 13768883594457632599, ; 565: System.IO.IsolatedStorage => 0xbf14e6adb159cf57 => 52
	i64 13828521679616088467, ; 566: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 335
	i64 13881769479078963060, ; 567: System.Console.dll => 0xc0a5f3cade5c6774 => 20
	i64 13911222732217019342, ; 568: System.Security.Cryptography.OpenSsl.dll => 0xc10e975ec1226bce => 123
	i64 13928444506500929300, ; 569: System.Windows.dll => 0xc14bc67b8bba9714 => 154
	i64 13955418299340266673, ; 570: Microsoft.Extensions.DependencyModel.dll => 0xc1ab9b0118299cb1 => 202
	i64 13959074834287824816, ; 571: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 289
	i64 13966086452609593515, ; 572: Nito.Cancellation.dll => 0xc1d181a21630b0ab => 242
	i64 14075334701871371868, ; 573: System.ServiceModel.Web.dll => 0xc355a25647c5965c => 131
	i64 14113375261243792383, ; 574: System.Numerics.Tensors.dll => 0xc3dcc8063438dbff => 256
	i64 14124974489674258913, ; 575: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 271
	i64 14125464355221830302, ; 576: System.Threading.dll => 0xc407bafdbc707a9e => 148
	i64 14133832980772275001, ; 577: Microsoft.EntityFrameworkCore.dll => 0xc425763635a1c339 => 191
	i64 14138690778401619511, ; 578: Catalyst => 0xc436b85a5bb83e37 => 174
	i64 14178052285788134900, ; 579: Xamarin.Android.Glide.Annotations.dll => 0xc4c28f6f75511df4 => 258
	i64 14212104595480609394, ; 580: System.Security.Cryptography.Cng.dll => 0xc53b89d4a4518272 => 120
	i64 14220608275227875801, ; 581: System.Diagnostics.FileVersionInfo.dll => 0xc559bfe1def019d9 => 28
	i64 14226382999226559092, ; 582: System.ServiceProcess => 0xc56e43f6938e2a74 => 132
	i64 14232023429000439693, ; 583: System.Resources.Writer.dll => 0xc5824de7789ba78d => 100
	i64 14254574811015963973, ; 584: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 134
	i64 14261073672896646636, ; 585: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 309
	i64 14271787347307718904, ; 586: Domain.Localization => 0xc60f92fa2a30e8f8 => 376
	i64 14298246716367104064, ; 587: System.Web.dll => 0xc66d93a217f4e840 => 153
	i64 14327695147300244862, ; 588: System.Reflection.dll => 0xc6d632d338eb4d7e => 97
	i64 14327709162229390963, ; 589: System.Security.Cryptography.X509Certificates => 0xc6d63f9253cade73 => 125
	i64 14331727281556788554, ; 590: Xamarin.Android.Glide.DiskLruCache.dll => 0xc6e48607a2f7954a => 259
	i64 14346402571976470310, ; 591: System.Net.Ping.dll => 0xc718a920f3686f26 => 69
	i64 14349907877893689639, ; 592: ro\Microsoft.Maui.Controls.resources.dll => 0xc7251d2f956ed127 => 363
	i64 14405190046395267921, ; 593: Nito.Disposables.dll => 0xc7e984067561fb51 => 244
	i64 14461014870687870182, ; 594: System.Net.Requests.dll => 0xc8afd8683afdece6 => 72
	i64 14464374589798375073, ; 595: ru\Microsoft.Maui.Controls.resources => 0xc8bbc80dcb1e5ea1 => 364
	i64 14486659737292545672, ; 596: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 294
	i64 14491877563792607819, ; 597: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0xc91d7ddcee4fca4b => 372
	i64 14495724990987328804, ; 598: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 312
	i64 14551742072151931844, ; 599: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 136
	i64 14556034074661724008, ; 600: CommunityToolkit.Maui.Core => 0xca016bdea6b19f68 => 178
	i64 14561513370130550166, ; 601: System.Security.Cryptography.Primitives.dll => 0xca14e3428abb8d96 => 124
	i64 14574160591280636898, ; 602: System.Net.Quic => 0xca41d1d72ec783e2 => 71
	i64 14610046442689856844, ; 603: cs\Microsoft.Maui.Controls.resources.dll => 0xcac14fd5107d054c => 342
	i64 14622043554576106986, ; 604: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 111
	i64 14644440854989303794, ; 605: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 304
	i64 14647451993120199252, ; 606: Apps.MauiRunner => 0xcb4633fb72845254 => 0
	i64 14669215534098758659, ; 607: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 200
	i64 14690985099581930927, ; 608: System.Web.HttpUtility => 0xcbe0dd1ca5233daf => 152
	i64 14705122255218365489, ; 609: ko\Microsoft.Maui.Controls.resources => 0xcc1316c7b0fb5431 => 356
	i64 14735017007120366644, ; 610: ja\Microsoft.Maui.Controls.resources.dll => 0xcc7d4be604bfbc34 => 355
	i64 14744092281598614090, ; 611: zh-Hans\Microsoft.Maui.Controls.resources => 0xcc9d89d004439a4a => 372
	i64 14792063746108907174, ; 612: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 332
	i64 14832630590065248058, ; 613: System.Security.Claims => 0xcdd816ef5d6e873a => 118
	i64 14852515768018889994, ; 614: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 280
	i64 14889905118082851278, ; 615: GoogleGson.dll => 0xcea391d0969961ce => 183
	i64 14904040806490515477, ; 616: ar\Microsoft.Maui.Controls.resources => 0xced5ca2604cb2815 => 340
	i64 14912225920358050525, ; 617: System.Security.Principal.Windows => 0xcef2de7759506add => 127
	i64 14935719434541007538, ; 618: System.Text.Encoding.CodePages.dll => 0xcf4655b160b702b2 => 133
	i64 14954917835170835695, ; 619: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 201
	i64 14984936317414011727, ; 620: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 77
	i64 14987728460634540364, ; 621: System.IO.Compression.dll => 0xcfff1ba06622494c => 46
	i64 14988210264188246988, ; 622: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 283
	i64 15015154896917945444, ; 623: System.Net.Security.dll => 0xd0608bd33642dc64 => 73
	i64 15024878362326791334, ; 624: System.Net.Http.Json => 0xd0831743ebf0f4a6 => 63
	i64 15026606801040537416, ; 625: Domain.Localization.dll => 0xd0893b456b7d7748 => 376
	i64 15051741671811457419, ; 626: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0xd0e2874d8f44218b => 204
	i64 15071021337266399595, ; 627: System.Resources.Reader.dll => 0xd127060e7a18a96b => 98
	i64 15076659072870671916, ; 628: System.ObjectModel.dll => 0xd13b0d8c1620662c => 84
	i64 15111608613780139878, ; 629: ms\Microsoft.Maui.Controls.resources => 0xd1b737f831192f66 => 357
	i64 15115185479366240210, ; 630: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 43
	i64 15133485256822086103, ; 631: System.Linq.dll => 0xd204f0a9127dd9d7 => 61
	i64 15150743910298169673, ; 632: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 310
	i64 15203009853192377507, ; 633: pt\Microsoft.Maui.Controls.resources.dll => 0xd2fbf0e9984b94a3 => 362
	i64 15227001540531775957, ; 634: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd3512d3999b8e9d5 => 198
	i64 15234786388537674379, ; 635: System.Dynamic.Runtime.dll => 0xd36cd580c5be8a8b => 37
	i64 15250465174479574862, ; 636: System.Globalization.Calendars.dll => 0xd3a489469852174e => 40
	i64 15255918880244313232, ; 637: Microsoft.ML.dll => 0xd3b7e9646b232c90 => 224
	i64 15272359115529052076, ; 638: Xamarin.AndroidX.Collection.Ktx => 0xd3f251b2fb4edfac => 273
	i64 15273235828284483249, ; 639: Microsoft.ML.Transforms => 0xd3f56f1093aa66b1 => 223
	i64 15279429628684179188, ; 640: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 338
	i64 15299439993936780255, ; 641: System.Xml.XPath.dll => 0xd452879d55019bdf => 160
	i64 15325339488831891965, ; 642: Tools.Validations.dll => 0xd4ae8b11661769fd => 384
	i64 15338463749992804988, ; 643: System.Resources.Reader => 0xd4dd2b839286f27c => 98
	i64 15370334346939861994, ; 644: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 278
	i64 15391712275433856905, ; 645: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 201
	i64 15526743539506359484, ; 646: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 135
	i64 15527772828719725935, ; 647: System.Console => 0xd77dbb1e38cd3d6f => 20
	i64 15530465045505749832, ; 648: System.Net.HttpListener.dll => 0xd7874bacc9fdb348 => 65
	i64 15536481058354060254, ; 649: de\Microsoft.Maui.Controls.resources => 0xd79cab34eec75bde => 344
	i64 15541854775306130054, ; 650: System.Security.Cryptography.X509Certificates.dll => 0xd7afc292e8d49286 => 125
	i64 15557562860424774966, ; 651: System.Net.Sockets => 0xd7e790fe7a6dc536 => 75
	i64 15582737692548360875, ; 652: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 302
	i64 15609085926864131306, ; 653: System.dll => 0xd89e9cf3334914ea => 164
	i64 15620595871140898079, ; 654: Microsoft.Extensions.DependencyModel => 0xd8c7812eef49651f => 202
	i64 15661133872274321916, ; 655: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 156
	i64 15665897052703636989, ; 656: Python.Deployment.dll => 0xd968725e323d3dfd => 246
	i64 15710114879900314733, ; 657: Microsoft.Win32.Registry => 0xda058a3f5d096c6d => 5
	i64 15755368083429170162, ; 658: System.IO.FileSystem.Primitives => 0xdaa64fcbde529bf2 => 49
	i64 15777549416145007739, ; 659: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 316
	i64 15783653065526199428, ; 660: el\Microsoft.Maui.Controls.resources => 0xdb0accd674b1c484 => 345
	i64 15817206913877585035, ; 661: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 144
	i64 15847085070278954535, ; 662: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 139
	i64 15885744048853936810, ; 663: System.Resources.Writer => 0xdc75800bd0b6eaaa => 100
	i64 15928521404965645318, ; 664: Microsoft.Maui.Controls.Compatibility => 0xdd0d79d32c2eec06 => 212
	i64 15934062614519587357, ; 665: System.Security.Cryptography.OpenSsl => 0xdd2129868f45a21d => 123
	i64 15937190497610202713, ; 666: System.Security.Cryptography.Cng => 0xdd2c465197c97e59 => 120
	i64 15963349826457351533, ; 667: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 142
	i64 15971679995444160383, ; 668: System.Formats.Tar.dll => 0xdda6ce5592a9677f => 39
	i64 16009073600704591924, ; 669: Nito.AsyncEx.Context => 0xde2ba79ec114b834 => 237
	i64 16018552496348375205, ; 670: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 68
	i64 16054465462676478687, ; 671: System.Globalization.Extensions => 0xdecceb47319bdadf => 41
	i64 16056281320585338352, ; 672: ru\Microsoft.Maui.Controls.resources.dll => 0xded35eca8f3a9df0 => 364
	i64 16154507427712707110, ; 673: System => 0xe03056ea4e39aa26 => 164
	i64 16219561732052121626, ; 674: System.Net.Security => 0xe1177575db7c781a => 73
	i64 16288847719894691167, ; 675: nb\Microsoft.Maui.Controls.resources => 0xe20d9cb300c12d5f => 358
	i64 16315482530584035869, ; 676: WindowsBase.dll => 0xe26c3ceb1e8d821d => 165
	i64 16321164108206115771, ; 677: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 207
	i64 16337011941688632206, ; 678: System.Security.Principal.Windows.dll => 0xe2b8b9cdc3aa638e => 127
	i64 16361933716545543812, ; 679: Xamarin.AndroidX.ExifInterface.dll => 0xe3114406a52f1e84 => 288
	i64 16423015068819898779, ; 680: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 337
	i64 16441649841863217159, ; 681: Microsoft.Recognizers.Text.Number => 0xe42c796b699d5007 => 232
	i64 16454459195343277943, ; 682: System.Net.NetworkInformation => 0xe459fb756d988f77 => 68
	i64 16479351772512606635, ; 683: Infrastructure.Data.dll => 0xe4b26b20315159ab => 379
	i64 16495551891359062599, ; 684: MediatR.Contracts.dll => 0xe4ebf90c4a7ada47 => 186
	i64 16496768397145114574, ; 685: Mono.Android.Export.dll => 0xe4f04b741db987ce => 169
	i64 16507452361048536309, ; 686: Catalyst.dll => 0xe51640764efb34f5 => 174
	i64 16510773408599260707, ; 687: Nito.AsyncEx.Context.dll => 0xe5220ceff2863e23 => 237
	i64 16540950613692298325, ; 688: Microsoft.ML.StandardTrainers => 0xe58d42f20868b455 => 222
	i64 16558262036769511634, ; 689: Microsoft.Extensions.Http => 0xe5cac397cf7b98d2 => 205
	i64 16571160433513028530, ; 690: Microsoft.ML.CpuMath => 0xe5f8969dd38d5bb2 => 225
	i64 16582434033142728747, ; 691: Microsoft.NET.StringTools.dll => 0xe620a3e548d2082b => 227
	i64 16589693266713801121, ; 692: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xe63a6e214f2a71a1 => 301
	i64 16621146507174665210, ; 693: Xamarin.AndroidX.ConstraintLayout => 0xe6aa2caf87dedbfa => 275
	i64 16631798820069378354, ; 694: FluentValidation.DependencyInjectionExtensions.dll => 0xe6d004e865ff0932 => 182
	i64 16648892297579399389, ; 695: CommunityToolkit.Mvvm => 0xe70cbf55c4f508dd => 179
	i64 16649148416072044166, ; 696: Microsoft.Maui.Graphics => 0xe70da84600bb4e86 => 217
	i64 16677317093839702854, ; 697: Xamarin.AndroidX.Navigation.UI => 0xe771bb8960dd8b46 => 308
	i64 16702652415771857902, ; 698: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 151
	i64 16709499819875633724, ; 699: System.IO.Compression.ZipFile => 0xe7e4118e32240a3c => 45
	i64 16737807731308835127, ; 700: System.Runtime.Intrinsics => 0xe848a3736f733137 => 108
	i64 16746942327847962830, ; 701: Application.Common.dll => 0xe86917516d54c8ce => 374
	i64 16755018182064898362, ; 702: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 251
	i64 16758309481308491337, ; 703: System.IO.FileSystem.DriveInfo => 0xe89179af15740e49 => 48
	i64 16762783179241323229, ; 704: System.Reflection.TypeExtensions => 0xe8a15e7d0d927add => 96
	i64 16765015072123548030, ; 705: System.Diagnostics.TextWriterTraceListener.dll => 0xe8a94c621bfe717e => 31
	i64 16803648858859583026, ; 706: ms\Microsoft.Maui.Controls.resources.dll => 0xe9328d9b8ab93632 => 357
	i64 16822611501064131242, ; 707: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 23
	i64 16833383113903931215, ; 708: mscorlib => 0xe99c30c1484d7f4f => 166
	i64 16848182392821183571, ; 709: CsvHelper.dll => 0xe9d0c49eeb18f453 => 180
	i64 16856067890322379635, ; 710: System.Data.Common.dll => 0xe9ecc87060889373 => 22
	i64 16890310621557459193, ; 711: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 138
	i64 16933958494752847024, ; 712: System.Net.WebProxy.dll => 0xeb018187f0f3b4b0 => 78
	i64 16942731696432749159, ; 713: sk\Microsoft.Maui.Controls.resources => 0xeb20acb622a01a67 => 365
	i64 16974217814034580509, ; 714: Python.Deployment => 0xeb90892b29a3841d => 246
	i64 16977952268158210142, ; 715: System.IO.Pipes.AccessControl => 0xeb9dcda2851b905e => 54
	i64 16989020923549080504, ; 716: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xebc52084add25bb8 => 301
	i64 16998075588627545693, ; 717: Xamarin.AndroidX.Navigation.Fragment => 0xebe54bb02d623e5d => 306
	i64 17008137082415910100, ; 718: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 10
	i64 17024911836938395553, ; 719: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 264
	i64 17031351772568316411, ; 720: Xamarin.AndroidX.Navigation.Common.dll => 0xec5b843380a769fb => 305
	i64 17037200463775726619, ; 721: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 292
	i64 17062143951396181894, ; 722: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 16
	i64 17118171214553292978, ; 723: System.Threading.Channels => 0xed8ff6060fc420b2 => 139
	i64 17187273293601214786, ; 724: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 13
	i64 17201328579425343169, ; 725: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 15
	i64 17202182880784296190, ; 726: System.Security.Cryptography.Encoding.dll => 0xeeba6e30627428fe => 122
	i64 17203614576212522419, ; 727: pl\Microsoft.Maui.Controls.resources.dll => 0xeebf844ef3e135b3 => 360
	i64 17230721278011714856, ; 728: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 87
	i64 17234219099804750107, ; 729: System.Transactions.Local.dll => 0xef2c3ef5e11d511b => 149
	i64 17260702271250283638, ; 730: System.Data.Common => 0xef8a5543bba6bc76 => 22
	i64 17310278548634113468, ; 731: hi\Microsoft.Maui.Controls.resources.dll => 0xf03a76a04e6695bc => 350
	i64 17313014331733301023, ; 732: Domain.WordModels.dll => 0xf0442ece7235d71f => 378
	i64 17317573276585426033, ; 733: WebSources.CambridgeDictionary.dll => 0xf054612482abfc71 => 385
	i64 17333249706306540043, ; 734: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 34
	i64 17338386382517543202, ; 735: System.Net.WebSockets.Client.dll => 0xf09e528d5c6da122 => 79
	i64 17342750010158924305, ; 736: hi\Microsoft.Maui.Controls.resources => 0xf0add33f97ecc211 => 350
	i64 17360349973592121190, ; 737: Xamarin.Google.Crypto.Tink.Android => 0xf0ec5a52686b9f66 => 330
	i64 17369617474300715483, ; 738: Tools.Parsers => 0xf10d4710901ca1db => 383
	i64 17470386307322966175, ; 739: System.Threading.Timer => 0xf27347c8d0d5709f => 147
	i64 17509662556995089465, ; 740: System.Net.WebSockets.dll => 0xf2fed1534ea67439 => 80
	i64 17514990004910432069, ; 741: fr\Microsoft.Maui.Controls.resources => 0xf311be9c6f341f45 => 348
	i64 17522591619082469157, ; 742: GoogleGson => 0xf32cc03d27a5bf25 => 183
	i64 17574451528562143913, ; 743: Microsoft.ML.PCA.dll => 0xf3e4fe8d424472a9 => 221
	i64 17590473451926037903, ; 744: Xamarin.Android.Glide => 0xf41dea67fcfda58f => 257
	i64 17623389608345532001, ; 745: pl\Microsoft.Maui.Controls.resources => 0xf492db79dfbef661 => 360
	i64 17627500474728259406, ; 746: System.Globalization => 0xf4a176498a351f4e => 42
	i64 17685921127322830888, ; 747: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 26
	i64 17703422902511870100, ; 748: Domain.WebSources => 0xf5af315544bc2094 => 377
	i64 17704177640604968747, ; 749: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 303
	i64 17710060891934109755, ; 750: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 300
	i64 17712670374920797664, ; 751: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 107
	i64 17743407583038752114, ; 752: System.CodeDom.dll => 0xf63d3f302bff4572 => 254
	i64 17777860260071588075, ; 753: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 110
	i64 17827813215687577648, ; 754: hr\Microsoft.Maui.Controls.resources.dll => 0xf7691da9f3129030 => 351
	i64 17838668724098252521, ; 755: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 7
	i64 17891337867145587222, ; 756: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 333
	i64 17928294245072900555, ; 757: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 44
	i64 17942426894774770628, ; 758: de\Microsoft.Maui.Controls.resources.dll => 0xf9004e329f771bc4 => 344
	i64 17992315986609351877, ; 759: System.Xml.XmlDocument.dll => 0xf9b18c0ffc6eacc5 => 161
	i64 18017743553296241350, ; 760: Microsoft.Extensions.Caching.Abstractions => 0xfa0be24cb44e92c6 => 195
	i64 18025913125965088385, ; 761: System.Threading => 0xfa28e87b91334681 => 148
	i64 18116111925905154859, ; 762: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 269
	i64 18121036031235206392, ; 763: Xamarin.AndroidX.Navigation.Common => 0xfb7ada42d3d42cf8 => 305
	i64 18125049892544899879, ; 764: MediatR => 0xfb891cd8bd5eeb27 => 185
	i64 18146411883821974900, ; 765: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 38
	i64 18146811631844267958, ; 766: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 15
	i64 18171003490741864161, ; 767: MessagePack => 0xfc2c5f66960d46e1 => 187
	i64 18192621550514078819, ; 768: Nito.Cancellation => 0xfc792ce95b820463 => 242
	i64 18225059387460068507, ; 769: System.Threading.ThreadPool.dll => 0xfcec6af3cff4a49b => 146
	i64 18245806341561545090, ; 770: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 8
	i64 18260797123374478311, ; 771: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 286
	i64 18305135509493619199, ; 772: Xamarin.AndroidX.Navigation.Runtime.dll => 0xfe08e7c2d8c199ff => 307
	i64 18318849532986632368, ; 773: System.Security.dll => 0xfe39a097c37fa8b0 => 130
	i64 18324163916253801303, ; 774: it\Microsoft.Maui.Controls.resources => 0xfe4c81ff0a56ab57 => 354
	i64 18342408478508122430, ; 775: id\Microsoft.Maui.Controls.resources.dll => 0xfe8d53543697013e => 353
	i64 18358161419737137786, ; 776: he\Microsoft.Maui.Controls.resources.dll => 0xfec54a8ba8b6827a => 349
	i64 18370042311372477656, ; 777: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 252
	i64 18380184030268848184, ; 778: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 323
	i64 18439108438687598470 ; 779: System.Reflection.Metadata.dll => 0xffe4df6e2ee1c786 => 94
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [780 x i32] [
	i32 256, ; 0
	i32 285, ; 1
	i32 236, ; 2
	i32 211, ; 3
	i32 171, ; 4
	i32 377, ; 5
	i32 216, ; 6
	i32 58, ; 7
	i32 249, ; 8
	i32 272, ; 9
	i32 151, ; 10
	i32 313, ; 11
	i32 316, ; 12
	i32 279, ; 13
	i32 132, ; 14
	i32 210, ; 15
	i32 232, ; 16
	i32 173, ; 17
	i32 56, ; 18
	i32 315, ; 19
	i32 95, ; 20
	i32 298, ; 21
	i32 129, ; 22
	i32 199, ; 23
	i32 145, ; 24
	i32 273, ; 25
	i32 18, ; 26
	i32 352, ; 27
	i32 375, ; 28
	i32 252, ; 29
	i32 284, ; 30
	i32 299, ; 31
	i32 226, ; 32
	i32 150, ; 33
	i32 104, ; 34
	i32 184, ; 35
	i32 95, ; 36
	i32 248, ; 37
	i32 328, ; 38
	i32 36, ; 39
	i32 251, ; 40
	i32 28, ; 41
	i32 268, ; 42
	i32 185, ; 43
	i32 306, ; 44
	i32 50, ; 45
	i32 115, ; 46
	i32 70, ; 47
	i32 213, ; 48
	i32 65, ; 49
	i32 239, ; 50
	i32 170, ; 51
	i32 253, ; 52
	i32 145, ; 53
	i32 327, ; 54
	i32 267, ; 55
	i32 302, ; 56
	i32 292, ; 57
	i32 40, ; 58
	i32 89, ; 59
	i32 190, ; 60
	i32 81, ; 61
	i32 235, ; 62
	i32 66, ; 63
	i32 62, ; 64
	i32 86, ; 65
	i32 266, ; 66
	i32 106, ; 67
	i32 346, ; 68
	i32 313, ; 69
	i32 102, ; 70
	i32 189, ; 71
	i32 35, ; 72
	i32 263, ; 73
	i32 368, ; 74
	i32 315, ; 75
	i32 214, ; 76
	i32 179, ; 77
	i32 386, ; 78
	i32 119, ; 79
	i32 300, ; 80
	i32 142, ; 81
	i32 141, ; 82
	i32 336, ; 83
	i32 228, ; 84
	i32 53, ; 85
	i32 35, ; 86
	i32 141, ; 87
	i32 235, ; 88
	i32 260, ; 89
	i32 270, ; 90
	i32 192, ; 91
	i32 208, ; 92
	i32 175, ; 93
	i32 284, ; 94
	i32 8, ; 95
	i32 14, ; 96
	i32 312, ; 97
	i32 51, ; 98
	i32 295, ; 99
	i32 136, ; 100
	i32 101, ; 101
	i32 277, ; 102
	i32 322, ; 103
	i32 116, ; 104
	i32 261, ; 105
	i32 163, ; 106
	i32 367, ; 107
	i32 166, ; 108
	i32 67, ; 109
	i32 200, ; 110
	i32 342, ; 111
	i32 80, ; 112
	i32 381, ; 113
	i32 101, ; 114
	i32 317, ; 115
	i32 117, ; 116
	i32 233, ; 117
	i32 347, ; 118
	i32 329, ; 119
	i32 224, ; 120
	i32 78, ; 121
	i32 328, ; 122
	i32 114, ; 123
	i32 121, ; 124
	i32 48, ; 125
	i32 244, ; 126
	i32 128, ; 127
	i32 293, ; 128
	i32 264, ; 129
	i32 82, ; 130
	i32 110, ; 131
	i32 75, ; 132
	i32 339, ; 133
	i32 218, ; 134
	i32 243, ; 135
	i32 184, ; 136
	i32 216, ; 137
	i32 53, ; 138
	i32 319, ; 139
	i32 197, ; 140
	i32 69, ; 141
	i32 318, ; 142
	i32 196, ; 143
	i32 83, ; 144
	i32 172, ; 145
	i32 362, ; 146
	i32 116, ; 147
	i32 198, ; 148
	i32 156, ; 149
	i32 197, ; 150
	i32 258, ; 151
	i32 226, ; 152
	i32 167, ; 153
	i32 311, ; 154
	i32 285, ; 155
	i32 243, ; 156
	i32 206, ; 157
	i32 32, ; 158
	i32 187, ; 159
	i32 214, ; 160
	i32 122, ; 161
	i32 72, ; 162
	i32 62, ; 163
	i32 247, ; 164
	i32 161, ; 165
	i32 113, ; 166
	i32 188, ; 167
	i32 343, ; 168
	i32 88, ; 169
	i32 212, ; 170
	i32 373, ; 171
	i32 105, ; 172
	i32 18, ; 173
	i32 146, ; 174
	i32 118, ; 175
	i32 58, ; 176
	i32 279, ; 177
	i32 17, ; 178
	i32 52, ; 179
	i32 92, ; 180
	i32 250, ; 181
	i32 368, ; 182
	i32 55, ; 183
	i32 129, ; 184
	i32 152, ; 185
	i32 41, ; 186
	i32 193, ; 187
	i32 92, ; 188
	i32 192, ; 189
	i32 323, ; 190
	i32 205, ; 191
	i32 50, ; 192
	i32 162, ; 193
	i32 13, ; 194
	i32 229, ; 195
	i32 297, ; 196
	i32 379, ; 197
	i32 261, ; 198
	i32 318, ; 199
	i32 36, ; 200
	i32 67, ; 201
	i32 109, ; 202
	i32 358, ; 203
	i32 0, ; 204
	i32 366, ; 205
	i32 181, ; 206
	i32 225, ; 207
	i32 262, ; 208
	i32 99, ; 209
	i32 99, ; 210
	i32 11, ; 211
	i32 190, ; 212
	i32 11, ; 213
	i32 304, ; 214
	i32 25, ; 215
	i32 128, ; 216
	i32 76, ; 217
	i32 296, ; 218
	i32 109, ; 219
	i32 386, ; 220
	i32 322, ; 221
	i32 181, ; 222
	i32 320, ; 223
	i32 106, ; 224
	i32 2, ; 225
	i32 26, ; 226
	i32 275, ; 227
	i32 157, ; 228
	i32 366, ; 229
	i32 21, ; 230
	i32 247, ; 231
	i32 369, ; 232
	i32 49, ; 233
	i32 43, ; 234
	i32 126, ; 235
	i32 265, ; 236
	i32 59, ; 237
	i32 176, ; 238
	i32 119, ; 239
	i32 325, ; 240
	i32 288, ; 241
	i32 274, ; 242
	i32 238, ; 243
	i32 3, ; 244
	i32 294, ; 245
	i32 314, ; 246
	i32 38, ; 247
	i32 236, ; 248
	i32 124, ; 249
	i32 388, ; 250
	i32 203, ; 251
	i32 314, ; 252
	i32 378, ; 253
	i32 250, ; 254
	i32 363, ; 255
	i32 137, ; 256
	i32 149, ; 257
	i32 85, ; 258
	i32 90, ; 259
	i32 346, ; 260
	i32 375, ; 261
	i32 298, ; 262
	i32 389, ; 263
	i32 384, ; 264
	i32 295, ; 265
	i32 347, ; 266
	i32 351, ; 267
	i32 270, ; 268
	i32 281, ; 269
	i32 326, ; 270
	i32 240, ; 271
	i32 209, ; 272
	i32 331, ; 273
	i32 296, ; 274
	i32 133, ; 275
	i32 96, ; 276
	i32 3, ; 277
	i32 359, ; 278
	i32 105, ; 279
	i32 33, ; 280
	i32 154, ; 281
	i32 158, ; 282
	i32 155, ; 283
	i32 82, ; 284
	i32 290, ; 285
	i32 143, ; 286
	i32 87, ; 287
	i32 19, ; 288
	i32 291, ; 289
	i32 51, ; 290
	i32 260, ; 291
	i32 61, ; 292
	i32 54, ; 293
	i32 4, ; 294
	i32 97, ; 295
	i32 259, ; 296
	i32 17, ; 297
	i32 220, ; 298
	i32 155, ; 299
	i32 84, ; 300
	i32 29, ; 301
	i32 45, ; 302
	i32 64, ; 303
	i32 223, ; 304
	i32 66, ; 305
	i32 218, ; 306
	i32 172, ; 307
	i32 221, ; 308
	i32 299, ; 309
	i32 1, ; 310
	i32 334, ; 311
	i32 354, ; 312
	i32 47, ; 313
	i32 24, ; 314
	i32 267, ; 315
	i32 204, ; 316
	i32 195, ; 317
	i32 365, ; 318
	i32 387, ; 319
	i32 165, ; 320
	i32 108, ; 321
	i32 383, ; 322
	i32 12, ; 323
	i32 293, ; 324
	i32 63, ; 325
	i32 27, ; 326
	i32 23, ; 327
	i32 356, ; 328
	i32 93, ; 329
	i32 240, ; 330
	i32 168, ; 331
	i32 12, ; 332
	i32 338, ; 333
	i32 217, ; 334
	i32 29, ; 335
	i32 103, ; 336
	i32 255, ; 337
	i32 14, ; 338
	i32 249, ; 339
	i32 126, ; 340
	i32 371, ; 341
	i32 276, ; 342
	i32 308, ; 343
	i32 176, ; 344
	i32 91, ; 345
	i32 297, ; 346
	i32 9, ; 347
	i32 382, ; 348
	i32 86, ; 349
	i32 287, ; 350
	i32 177, ; 351
	i32 320, ; 352
	i32 71, ; 353
	i32 168, ; 354
	i32 1, ; 355
	i32 307, ; 356
	i32 229, ; 357
	i32 5, ; 358
	i32 361, ; 359
	i32 44, ; 360
	i32 186, ; 361
	i32 27, ; 362
	i32 203, ; 363
	i32 335, ; 364
	i32 241, ; 365
	i32 385, ; 366
	i32 158, ; 367
	i32 310, ; 368
	i32 112, ; 369
	i32 371, ; 370
	i32 196, ; 371
	i32 121, ; 372
	i32 191, ; 373
	i32 382, ; 374
	i32 325, ; 375
	i32 266, ; 376
	i32 345, ; 377
	i32 228, ; 378
	i32 234, ; 379
	i32 159, ; 380
	i32 369, ; 381
	i32 131, ; 382
	i32 330, ; 383
	i32 57, ; 384
	i32 138, ; 385
	i32 83, ; 386
	i32 30, ; 387
	i32 277, ; 388
	i32 10, ; 389
	i32 341, ; 390
	i32 327, ; 391
	i32 171, ; 392
	i32 274, ; 393
	i32 150, ; 394
	i32 94, ; 395
	i32 194, ; 396
	i32 234, ; 397
	i32 241, ; 398
	i32 287, ; 399
	i32 60, ; 400
	i32 215, ; 401
	i32 254, ; 402
	i32 157, ; 403
	i32 208, ; 404
	i32 64, ; 405
	i32 220, ; 406
	i32 88, ; 407
	i32 79, ; 408
	i32 47, ; 409
	i32 213, ; 410
	i32 143, ; 411
	i32 230, ; 412
	i32 343, ; 413
	i32 199, ; 414
	i32 359, ; 415
	i32 381, ; 416
	i32 336, ; 417
	i32 281, ; 418
	i32 74, ; 419
	i32 91, ; 420
	i32 333, ; 421
	i32 135, ; 422
	i32 90, ; 423
	i32 374, ; 424
	i32 319, ; 425
	i32 339, ; 426
	i32 278, ; 427
	i32 189, ; 428
	i32 341, ; 429
	i32 112, ; 430
	i32 42, ; 431
	i32 173, ; 432
	i32 248, ; 433
	i32 159, ; 434
	i32 4, ; 435
	i32 380, ; 436
	i32 231, ; 437
	i32 103, ; 438
	i32 373, ; 439
	i32 70, ; 440
	i32 210, ; 441
	i32 230, ; 442
	i32 193, ; 443
	i32 60, ; 444
	i32 39, ; 445
	i32 268, ; 446
	i32 177, ; 447
	i32 153, ; 448
	i32 56, ; 449
	i32 255, ; 450
	i32 34, ; 451
	i32 207, ; 452
	i32 215, ; 453
	i32 265, ; 454
	i32 21, ; 455
	i32 163, ; 456
	i32 331, ; 457
	i32 188, ; 458
	i32 352, ; 459
	i32 329, ; 460
	i32 367, ; 461
	i32 348, ; 462
	i32 324, ; 463
	i32 140, ; 464
	i32 355, ; 465
	i32 209, ; 466
	i32 89, ; 467
	i32 147, ; 468
	i32 280, ; 469
	i32 162, ; 470
	i32 309, ; 471
	i32 194, ; 472
	i32 6, ; 473
	i32 169, ; 474
	i32 31, ; 475
	i32 107, ; 476
	i32 290, ; 477
	i32 353, ; 478
	i32 324, ; 479
	i32 206, ; 480
	i32 263, ; 481
	i32 317, ; 482
	i32 167, ; 483
	i32 387, ; 484
	i32 291, ; 485
	i32 140, ; 486
	i32 59, ; 487
	i32 144, ; 488
	i32 182, ; 489
	i32 370, ; 490
	i32 219, ; 491
	i32 81, ; 492
	i32 180, ; 493
	i32 74, ; 494
	i32 130, ; 495
	i32 245, ; 496
	i32 238, ; 497
	i32 222, ; 498
	i32 25, ; 499
	i32 7, ; 500
	i32 93, ; 501
	i32 321, ; 502
	i32 137, ; 503
	i32 257, ; 504
	i32 113, ; 505
	i32 9, ; 506
	i32 253, ; 507
	i32 380, ; 508
	i32 388, ; 509
	i32 104, ; 510
	i32 178, ; 511
	i32 19, ; 512
	i32 289, ; 513
	i32 303, ; 514
	i32 389, ; 515
	i32 283, ; 516
	i32 33, ; 517
	i32 271, ; 518
	i32 46, ; 519
	i32 175, ; 520
	i32 219, ; 521
	i32 30, ; 522
	i32 272, ; 523
	i32 57, ; 524
	i32 134, ; 525
	i32 114, ; 526
	i32 227, ; 527
	i32 326, ; 528
	i32 233, ; 529
	i32 337, ; 530
	i32 55, ; 531
	i32 211, ; 532
	i32 6, ; 533
	i32 77, ; 534
	i32 231, ; 535
	i32 282, ; 536
	i32 340, ; 537
	i32 361, ; 538
	i32 245, ; 539
	i32 111, ; 540
	i32 286, ; 541
	i32 102, ; 542
	i32 170, ; 543
	i32 115, ; 544
	i32 349, ; 545
	i32 321, ; 546
	i32 276, ; 547
	i32 76, ; 548
	i32 332, ; 549
	i32 85, ; 550
	i32 334, ; 551
	i32 239, ; 552
	i32 269, ; 553
	i32 370, ; 554
	i32 311, ; 555
	i32 160, ; 556
	i32 2, ; 557
	i32 282, ; 558
	i32 24, ; 559
	i32 262, ; 560
	i32 32, ; 561
	i32 117, ; 562
	i32 37, ; 563
	i32 16, ; 564
	i32 52, ; 565
	i32 335, ; 566
	i32 20, ; 567
	i32 123, ; 568
	i32 154, ; 569
	i32 202, ; 570
	i32 289, ; 571
	i32 242, ; 572
	i32 131, ; 573
	i32 256, ; 574
	i32 271, ; 575
	i32 148, ; 576
	i32 191, ; 577
	i32 174, ; 578
	i32 258, ; 579
	i32 120, ; 580
	i32 28, ; 581
	i32 132, ; 582
	i32 100, ; 583
	i32 134, ; 584
	i32 309, ; 585
	i32 376, ; 586
	i32 153, ; 587
	i32 97, ; 588
	i32 125, ; 589
	i32 259, ; 590
	i32 69, ; 591
	i32 363, ; 592
	i32 244, ; 593
	i32 72, ; 594
	i32 364, ; 595
	i32 294, ; 596
	i32 372, ; 597
	i32 312, ; 598
	i32 136, ; 599
	i32 178, ; 600
	i32 124, ; 601
	i32 71, ; 602
	i32 342, ; 603
	i32 111, ; 604
	i32 304, ; 605
	i32 0, ; 606
	i32 200, ; 607
	i32 152, ; 608
	i32 356, ; 609
	i32 355, ; 610
	i32 372, ; 611
	i32 332, ; 612
	i32 118, ; 613
	i32 280, ; 614
	i32 183, ; 615
	i32 340, ; 616
	i32 127, ; 617
	i32 133, ; 618
	i32 201, ; 619
	i32 77, ; 620
	i32 46, ; 621
	i32 283, ; 622
	i32 73, ; 623
	i32 63, ; 624
	i32 376, ; 625
	i32 204, ; 626
	i32 98, ; 627
	i32 84, ; 628
	i32 357, ; 629
	i32 43, ; 630
	i32 61, ; 631
	i32 310, ; 632
	i32 362, ; 633
	i32 198, ; 634
	i32 37, ; 635
	i32 40, ; 636
	i32 224, ; 637
	i32 273, ; 638
	i32 223, ; 639
	i32 338, ; 640
	i32 160, ; 641
	i32 384, ; 642
	i32 98, ; 643
	i32 278, ; 644
	i32 201, ; 645
	i32 135, ; 646
	i32 20, ; 647
	i32 65, ; 648
	i32 344, ; 649
	i32 125, ; 650
	i32 75, ; 651
	i32 302, ; 652
	i32 164, ; 653
	i32 202, ; 654
	i32 156, ; 655
	i32 246, ; 656
	i32 5, ; 657
	i32 49, ; 658
	i32 316, ; 659
	i32 345, ; 660
	i32 144, ; 661
	i32 139, ; 662
	i32 100, ; 663
	i32 212, ; 664
	i32 123, ; 665
	i32 120, ; 666
	i32 142, ; 667
	i32 39, ; 668
	i32 237, ; 669
	i32 68, ; 670
	i32 41, ; 671
	i32 364, ; 672
	i32 164, ; 673
	i32 73, ; 674
	i32 358, ; 675
	i32 165, ; 676
	i32 207, ; 677
	i32 127, ; 678
	i32 288, ; 679
	i32 337, ; 680
	i32 232, ; 681
	i32 68, ; 682
	i32 379, ; 683
	i32 186, ; 684
	i32 169, ; 685
	i32 174, ; 686
	i32 237, ; 687
	i32 222, ; 688
	i32 205, ; 689
	i32 225, ; 690
	i32 227, ; 691
	i32 301, ; 692
	i32 275, ; 693
	i32 182, ; 694
	i32 179, ; 695
	i32 217, ; 696
	i32 308, ; 697
	i32 151, ; 698
	i32 45, ; 699
	i32 108, ; 700
	i32 374, ; 701
	i32 251, ; 702
	i32 48, ; 703
	i32 96, ; 704
	i32 31, ; 705
	i32 357, ; 706
	i32 23, ; 707
	i32 166, ; 708
	i32 180, ; 709
	i32 22, ; 710
	i32 138, ; 711
	i32 78, ; 712
	i32 365, ; 713
	i32 246, ; 714
	i32 54, ; 715
	i32 301, ; 716
	i32 306, ; 717
	i32 10, ; 718
	i32 264, ; 719
	i32 305, ; 720
	i32 292, ; 721
	i32 16, ; 722
	i32 139, ; 723
	i32 13, ; 724
	i32 15, ; 725
	i32 122, ; 726
	i32 360, ; 727
	i32 87, ; 728
	i32 149, ; 729
	i32 22, ; 730
	i32 350, ; 731
	i32 378, ; 732
	i32 385, ; 733
	i32 34, ; 734
	i32 79, ; 735
	i32 350, ; 736
	i32 330, ; 737
	i32 383, ; 738
	i32 147, ; 739
	i32 80, ; 740
	i32 348, ; 741
	i32 183, ; 742
	i32 221, ; 743
	i32 257, ; 744
	i32 360, ; 745
	i32 42, ; 746
	i32 26, ; 747
	i32 377, ; 748
	i32 303, ; 749
	i32 300, ; 750
	i32 107, ; 751
	i32 254, ; 752
	i32 110, ; 753
	i32 351, ; 754
	i32 7, ; 755
	i32 333, ; 756
	i32 44, ; 757
	i32 344, ; 758
	i32 161, ; 759
	i32 195, ; 760
	i32 148, ; 761
	i32 269, ; 762
	i32 305, ; 763
	i32 185, ; 764
	i32 38, ; 765
	i32 15, ; 766
	i32 187, ; 767
	i32 242, ; 768
	i32 146, ; 769
	i32 8, ; 770
	i32 286, ; 771
	i32 307, ; 772
	i32 130, ; 773
	i32 354, ; 774
	i32 353, ; 775
	i32 349, ; 776
	i32 252, ; 777
	i32 323, ; 778
	i32 94 ; 779
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

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
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ af27162bee43b7fecdca59b4f67aa8c175cbc875"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}

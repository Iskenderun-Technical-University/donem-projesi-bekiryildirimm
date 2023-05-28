; ModuleID = 'obj\Debug\120\android\marshal_methods.x86.ll'
source_filename = "obj\Debug\120\android\marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [184 x i32] [
	i32 32687329, ; 0: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 50
	i32 34715100, ; 1: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 71
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 5
	i32 52239042, ; 3: HtmlAgilityPack => 0x31d1ac2 => 1
	i32 134690465, ; 4: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 75
	i32 165246403, ; 5: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 32
	i32 209399409, ; 6: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 30
	i32 230216969, ; 7: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 47
	i32 232815796, ; 8: System.Web.Services => 0xde07cb4 => 89
	i32 261689757, ; 9: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 35
	i32 280482487, ; 10: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 46
	i32 318968648, ; 11: Xamarin.AndroidX.Activity.dll => 0x13031348 => 23
	i32 321597661, ; 12: System.Numerics => 0x132b30dd => 18
	i32 342366114, ; 13: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 48
	i32 347068432, ; 14: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 9
	i32 385762202, ; 15: System.Memory.dll => 0x16fe439a => 15
	i32 441335492, ; 16: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 34
	i32 442521989, ; 17: Xamarin.Essentials => 0x1a605985 => 68
	i32 450948140, ; 18: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 45
	i32 465846621, ; 19: mscorlib => 0x1bc4415d => 4
	i32 469710990, ; 20: System.dll => 0x1bff388e => 13
	i32 476646585, ; 21: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 46
	i32 486930444, ; 22: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 54
	i32 526420162, ; 23: System.Transactions.dll => 0x1f6088c2 => 81
	i32 527452488, ; 24: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 75
	i32 605376203, ; 25: System.IO.Compression.FileSystem => 0x24154ecb => 85
	i32 627609679, ; 26: Xamarin.AndroidX.CustomView => 0x2568904f => 40
	i32 643868501, ; 27: System.Net => 0x2660a755 => 16
	i32 663517072, ; 28: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 65
	i32 666292255, ; 29: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 28
	i32 690569205, ; 30: System.Xml.Linq.dll => 0x29293ff5 => 90
	i32 691348768, ; 31: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 77
	i32 700284507, ; 32: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 72
	i32 720511267, ; 33: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 76
	i32 748832960, ; 34: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 7
	i32 775507847, ; 35: System.IO.Compression => 0x2e394f87 => 84
	i32 790371945, ; 36: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 41
	i32 809851609, ; 37: System.Drawing.Common.dll => 0x30455ad9 => 83
	i32 843511501, ; 38: Xamarin.AndroidX.Print => 0x3246f6cd => 56
	i32 928116545, ; 39: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 71
	i32 955402788, ; 40: Newtonsoft.Json => 0x38f24a24 => 5
	i32 956575887, ; 41: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 76
	i32 967690846, ; 42: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 48
	i32 1012816738, ; 43: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 59
	i32 1031528504, ; 44: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 70
	i32 1035644815, ; 45: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 27
	i32 1052210849, ; 46: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 51
	i32 1084122840, ; 47: Xamarin.Kotlin.StdLib => 0x409e66d8 => 74
	i32 1098259244, ; 48: System => 0x41761b2c => 13
	i32 1175144683, ; 49: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 63
	i32 1204270330, ; 50: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 28
	i32 1264511973, ; 51: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 60
	i32 1267360935, ; 52: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 64
	i32 1275534314, ; 53: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 77
	i32 1292207520, ; 54: SQLitePCLRaw.core.dll => 0x4d0585a0 => 8
	i32 1293217323, ; 55: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 43
	i32 1365406463, ; 56: System.ServiceModel.Internals.dll => 0x516272ff => 88
	i32 1376866003, ; 57: Xamarin.AndroidX.SavedState => 0x52114ed3 => 59
	i32 1406073936, ; 58: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 36
	i32 1411638395, ; 59: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 20
	i32 1462112819, ; 60: System.IO.Compression.dll => 0x57261233 => 84
	i32 1469204771, ; 61: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 26
	i32 1582372066, ; 62: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 42
	i32 1592978981, ; 63: System.Runtime.Serialization.dll => 0x5ef2ee25 => 87
	i32 1597949149, ; 64: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 70
	i32 1622152042, ; 65: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 53
	i32 1624863272, ; 66: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 67
	i32 1636350590, ; 67: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 39
	i32 1639515021, ; 68: System.Net.Http.dll => 0x61b9038d => 17
	i32 1657153582, ; 69: System.Runtime => 0x62c6282e => 21
	i32 1658241508, ; 70: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 61
	i32 1658251792, ; 71: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 69
	i32 1670060433, ; 72: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 35
	i32 1698840827, ; 73: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 73
	i32 1711441057, ; 74: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 9
	i32 1729485958, ; 75: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 31
	i32 1776026572, ; 76: System.Core.dll => 0x69dc03cc => 12
	i32 1788241197, ; 77: Xamarin.AndroidX.Fragment => 0x6a96652d => 45
	i32 1808609942, ; 78: Xamarin.AndroidX.Loader => 0x6bcd3296 => 53
	i32 1813058853, ; 79: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 74
	i32 1813201214, ; 80: Xamarin.Google.Android.Material => 0x6c13413e => 69
	i32 1867746548, ; 81: Xamarin.Essentials.dll => 0x6f538cf4 => 68
	i32 1885316902, ; 82: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 29
	i32 1919157823, ; 83: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 55
	i32 1983156543, ; 84: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 73
	i32 2011961780, ; 85: System.Buffers.dll => 0x77ec19b4 => 11
	i32 2019465201, ; 86: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 51
	i32 2055257422, ; 87: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 49
	i32 2079903147, ; 88: System.Runtime.dll => 0x7bf8cdab => 21
	i32 2090596640, ; 89: System.Numerics.Vectors => 0x7c9bf920 => 19
	i32 2103459038, ; 90: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 10
	i32 2162456754, ; 91: EzanVakti_Mobil.dll => 0x80e478b2 => 0
	i32 2188064486, ; 92: System.Json.dll => 0x826b36e6 => 14
	i32 2201107256, ; 93: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 78
	i32 2201231467, ; 94: System.Net.Http => 0x8334206b => 17
	i32 2217644978, ; 95: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 63
	i32 2244775296, ; 96: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 54
	i32 2256548716, ; 97: Xamarin.AndroidX.MultiDex => 0x8680336c => 55
	i32 2279755925, ; 98: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 58
	i32 2315684594, ; 99: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 24
	i32 2465273461, ; 100: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 7
	i32 2465532216, ; 101: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 34
	i32 2471841756, ; 102: netstandard.dll => 0x93554fdc => 79
	i32 2475788418, ; 103: Java.Interop.dll => 0x93918882 => 2
	i32 2501346920, ; 104: System.Data.DataSetExtensions => 0x95178668 => 82
	i32 2505896520, ; 105: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 50
	i32 2581819634, ; 106: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 64
	i32 2605712449, ; 107: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 78
	i32 2620871830, ; 108: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 39
	i32 2624644809, ; 109: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 44
	i32 2701096212, ; 110: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 61
	i32 2732626843, ; 111: Xamarin.AndroidX.Activity => 0xa2e0939b => 23
	i32 2737747696, ; 112: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 26
	i32 2770495804, ; 113: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 72
	i32 2778768386, ; 114: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 66
	i32 2810250172, ; 115: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 36
	i32 2819470561, ; 116: System.Xml.dll => 0xa80db4e1 => 22
	i32 2853208004, ; 117: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 66
	i32 2855708567, ; 118: Xamarin.AndroidX.Transition => 0xaa36a797 => 62
	i32 2887636118, ; 119: System.Net.dll => 0xac1dd496 => 16
	i32 2903344695, ; 120: System.ComponentModel.Composition => 0xad0d8637 => 86
	i32 2905242038, ; 121: mscorlib.dll => 0xad2a79b6 => 4
	i32 2916838712, ; 122: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 67
	i32 2919462931, ; 123: System.Numerics.Vectors.dll => 0xae037813 => 19
	i32 2921128767, ; 124: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 25
	i32 2978675010, ; 125: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 43
	i32 3016983068, ; 126: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 60
	i32 3024354802, ; 127: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 47
	i32 3111772706, ; 128: System.Runtime.Serialization => 0xb979e222 => 87
	i32 3201217166, ; 129: System.Json => 0xbeceb28e => 14
	i32 3204380047, ; 130: System.Data.dll => 0xbefef58f => 80
	i32 3211777861, ; 131: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 42
	i32 3247949154, ; 132: Mono.Security => 0xc197c562 => 91
	i32 3258312781, ; 133: Xamarin.AndroidX.CardView => 0xc235e84d => 31
	i32 3286872994, ; 134: SQLite-net.dll => 0xc3e9b3a2 => 6
	i32 3317135071, ; 135: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 40
	i32 3317144872, ; 136: System.Data => 0xc5b79d28 => 80
	i32 3340431453, ; 137: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 29
	i32 3345895724, ; 138: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 57
	i32 3360279109, ; 139: SQLitePCLRaw.core => 0xc849ca45 => 8
	i32 3362522851, ; 140: Xamarin.AndroidX.Core => 0xc86c06e3 => 38
	i32 3366347497, ; 141: Java.Interop => 0xc8a662e9 => 2
	i32 3374999561, ; 142: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 58
	i32 3395150330, ; 143: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 20
	i32 3404865022, ; 144: System.ServiceModel.Internals => 0xcaf21dfe => 88
	i32 3405233483, ; 145: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 41
	i32 3429136800, ; 146: System.Xml => 0xcc6479a0 => 22
	i32 3430777524, ; 147: netstandard => 0xcc7d82b4 => 79
	i32 3441283291, ; 148: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 44
	i32 3476120550, ; 149: Mono.Android => 0xcf3163e6 => 3
	i32 3486566296, ; 150: System.Transactions => 0xcfd0c798 => 81
	i32 3493954962, ; 151: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 33
	i32 3509114376, ; 152: System.Xml.Linq => 0xd128d608 => 90
	i32 3567349600, ; 153: System.ComponentModel.Composition.dll => 0xd4a16f60 => 86
	i32 3627220390, ; 154: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 56
	i32 3633644679, ; 155: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 25
	i32 3641597786, ; 156: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 49
	i32 3672681054, ; 157: Mono.Android.dll => 0xdae8aa5e => 3
	i32 3676310014, ; 158: System.Web.Services.dll => 0xdb2009fe => 89
	i32 3682565725, ; 159: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 30
	i32 3684561358, ; 160: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 33
	i32 3689375977, ; 161: System.Drawing.Common => 0xdbe768e9 => 83
	i32 3706696989, ; 162: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 37
	i32 3718780102, ; 163: Xamarin.AndroidX.Annotation => 0xdda814c6 => 24
	i32 3754567612, ; 164: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 10
	i32 3786282454, ; 165: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 32
	i32 3810220126, ; 166: HtmlAgilityPack.dll => 0xe31b585e => 1
	i32 3829621856, ; 167: System.Numerics.dll => 0xe4436460 => 18
	i32 3876362041, ; 168: SQLite-net => 0xe70c9739 => 6
	i32 3885922214, ; 169: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 62
	i32 3888767677, ; 170: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 57
	i32 3896760992, ; 171: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 38
	i32 3920810846, ; 172: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 85
	i32 3921031405, ; 173: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 65
	i32 3945713374, ; 174: System.Data.DataSetExtensions.dll => 0xeb2ecede => 82
	i32 3955647286, ; 175: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 27
	i32 3977364195, ; 176: EzanVakti_Mobil => 0xed11c2e3 => 0
	i32 4025784931, ; 177: System.Memory => 0xeff49a63 => 15
	i32 4105002889, ; 178: Mono.Security.dll => 0xf4ad5f89 => 91
	i32 4151237749, ; 179: System.Core => 0xf76edc75 => 12
	i32 4182413190, ; 180: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 52
	i32 4256097574, ; 181: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 37
	i32 4260525087, ; 182: System.Buffers => 0xfdf2741f => 11
	i32 4292120959 ; 183: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 52
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [184 x i32] [
	i32 50, i32 71, i32 5, i32 1, i32 75, i32 32, i32 30, i32 47, ; 0..7
	i32 89, i32 35, i32 46, i32 23, i32 18, i32 48, i32 9, i32 15, ; 8..15
	i32 34, i32 68, i32 45, i32 4, i32 13, i32 46, i32 54, i32 81, ; 16..23
	i32 75, i32 85, i32 40, i32 16, i32 65, i32 28, i32 90, i32 77, ; 24..31
	i32 72, i32 76, i32 7, i32 84, i32 41, i32 83, i32 56, i32 71, ; 32..39
	i32 5, i32 76, i32 48, i32 59, i32 70, i32 27, i32 51, i32 74, ; 40..47
	i32 13, i32 63, i32 28, i32 60, i32 64, i32 77, i32 8, i32 43, ; 48..55
	i32 88, i32 59, i32 36, i32 20, i32 84, i32 26, i32 42, i32 87, ; 56..63
	i32 70, i32 53, i32 67, i32 39, i32 17, i32 21, i32 61, i32 69, ; 64..71
	i32 35, i32 73, i32 9, i32 31, i32 12, i32 45, i32 53, i32 74, ; 72..79
	i32 69, i32 68, i32 29, i32 55, i32 73, i32 11, i32 51, i32 49, ; 80..87
	i32 21, i32 19, i32 10, i32 0, i32 14, i32 78, i32 17, i32 63, ; 88..95
	i32 54, i32 55, i32 58, i32 24, i32 7, i32 34, i32 79, i32 2, ; 96..103
	i32 82, i32 50, i32 64, i32 78, i32 39, i32 44, i32 61, i32 23, ; 104..111
	i32 26, i32 72, i32 66, i32 36, i32 22, i32 66, i32 62, i32 16, ; 112..119
	i32 86, i32 4, i32 67, i32 19, i32 25, i32 43, i32 60, i32 47, ; 120..127
	i32 87, i32 14, i32 80, i32 42, i32 91, i32 31, i32 6, i32 40, ; 128..135
	i32 80, i32 29, i32 57, i32 8, i32 38, i32 2, i32 58, i32 20, ; 136..143
	i32 88, i32 41, i32 22, i32 79, i32 44, i32 3, i32 81, i32 33, ; 144..151
	i32 90, i32 86, i32 56, i32 25, i32 49, i32 3, i32 89, i32 30, ; 152..159
	i32 33, i32 83, i32 37, i32 24, i32 10, i32 32, i32 1, i32 18, ; 160..167
	i32 6, i32 62, i32 57, i32 38, i32 85, i32 65, i32 82, i32 27, ; 168..175
	i32 0, i32 15, i32 91, i32 12, i32 52, i32 37, i32 11, i32 52 ; 184..183
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"NumRegisterParameters", i32 0}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 797e2e13d1706ace607da43703769c5a55c4de60"}
!llvm.linker.options = !{}

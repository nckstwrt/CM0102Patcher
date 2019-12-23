using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class ProcessPatch : IDisposable
    {
		Win32.STARTUPINFO si;
		Win32.PROCESS_INFORMATION pi;
		int size = 8*1024*1024;//0x00966FFF - 0x400000;
		byte[] buffer;
		MemoryStream ms = null;

		public bool LoadProcess(string exeFile)
		{
			si = new Win32.STARTUPINFO();
			pi = new Win32.PROCESS_INFORMATION();

			Directory.SetCurrentDirectory(Path.GetDirectoryName(exeFile));

			return Win32.CreateProcess(exeFile, null, IntPtr.Zero, IntPtr.Zero, false, Win32.ProcessCreationFlags.CREATE_SUSPENDED, IntPtr.Zero, null, ref si, out pi);
		}

		public MemoryStream ReadIn()
		{
			// Read size to be read
			uint addr = 0x400000;
			for (;addr <= 0x7fffffff;)
			{
				Win32.MEMORY_BASIC_INFORMATION mbi;
				int result = Win32.VirtualQueryEx(pi.hProcess, (IntPtr)addr, out mbi, (uint)Marshal.SizeOf(typeof(Win32.MEMORY_BASIC_INFORMATION)));
				if (((uint)mbi.AllocationBase) == 0 || mbi.Type == 0)
					break;
				addr += (uint)mbi.RegionSize;
			}
			size = (int)(addr - 0x400000);

			uint old;
			buffer = new byte[size];
			uint bytesRead;
			Win32.VirtualProtectEx(pi.hProcess, (IntPtr)0x400000, size, Win32.PAGE_EXECUTE_READWRITE, out old);
			Win32.ReadProcessMemory(pi.hProcess, (IntPtr)0x400000, buffer, size, out bytesRead);
			ms = new MemoryStream(buffer);
			ms.Seek(0, SeekOrigin.Begin);
			return ms;
		}
		
		public void Write()
		{
			uint bytesWritten;
			Win32.WriteProcessMemory(pi.hProcess, (IntPtr)0x400000, buffer, size, out bytesWritten);
		}

		public void Start()
		{
			Win32.ResumeThread(pi.hThread);
		}

		public void Dispose()
		{
			if (ms != null)
			{
				ms.Dispose();
				ms = null;
			}
		}
    }

    public class Win32
    {
		public static UInt32 MEM_COMMIT = 0x1000;

		public static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
		public static UInt32 PAGE_READWRITE = 0x04;
		public static UInt32 PAGE_EXECUTE_READ = 0x20;
		
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VirtualMemoryOperation = 0x00000008,
			VirtualMemoryRead = 0x00000010,
			VirtualMemoryWrite = 0x00000020,
			DuplicateHandle = 0x00000040,
			CreateProcess = 0x000000080,
			SetQuota = 0x00000100,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			QueryLimitedInformation = 0x00001000,
			Synchronize = 0x00100000
		}

		[Flags]
		public enum ProcessCreationFlags : uint
		{
			ZERO_FLAG = 0x00000000,
			CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
			CREATE_DEFAULT_ERROR_MODE = 0x04000000,
			CREATE_NEW_CONSOLE = 0x00000010,
			CREATE_NEW_PROCESS_GROUP = 0x00000200,
			CREATE_NO_WINDOW = 0x08000000,
			CREATE_PROTECTED_PROCESS = 0x00040000,
			CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
			CREATE_SEPARATE_WOW_VDM = 0x00001000,
			CREATE_SHARED_WOW_VDM = 0x00001000,
			CREATE_SUSPENDED = 0x00000004,
			CREATE_UNICODE_ENVIRONMENT = 0x00000400,
			DEBUG_ONLY_THIS_PROCESS = 0x00000002,
			DEBUG_PROCESS = 0x00000001,
			DETACHED_PROCESS = 0x00000008,
			EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
			INHERIT_PARENT_AFFINITY = 0x00010000
		}
		public struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public uint dwProcessId;
			public uint dwThreadId;
		}
		public struct STARTUPINFO
		{
			public uint cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public uint dwX;
			public uint dwY;
			public uint dwXSize;
			public uint dwYSize;
			public uint dwXCountChars;
			public uint dwYCountChars;
			public uint dwFillAttribute;
			public uint dwFlags;
			public short wShowWindow;
			public short cbReserved2;
			public IntPtr lpReserved2;
			public IntPtr hStdInput;
			public IntPtr hStdOutput;
			public IntPtr hStdError;
		}

		[Flags]
		public enum ThreadAccess : int
		{
			TERMINATE = (0x0001),
			SUSPEND_RESUME = (0x0002),
			GET_CONTEXT = (0x0008),
			SET_CONTEXT = (0x0010),
			SET_INFORMATION = (0x0020),
			QUERY_INFORMATION = (0x0040),
			SET_THREAD_TOKEN = (0x0080),
			IMPERSONATE = (0x0100),
			DIRECT_IMPERSONATION = (0x0200)
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MEMORY_BASIC_INFORMATION
		{
			public IntPtr BaseAddress;
			public IntPtr AllocationBase;
			public uint AllocationProtect;
			public IntPtr RegionSize;
			public uint State;
			public uint Protect;
			public uint Type;
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle,
			int dwThreadId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(
		   IntPtr hProcess,
		   IntPtr lpBaseAddress,
		   byte[] lpBuffer,
		   Int32 nSize,
		   out uint lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(
			IntPtr hProcess,
			IntPtr lpBaseAddress,
			byte[] lpBuffer,
			int nSize,
			out uint lpNumberOfBytesWritten);

		[DllImport("kernel32.dll")]
		public static extern IntPtr QueueUserAPC(IntPtr pfnAPC, IntPtr hThread, IntPtr dwData);

		[DllImport("kernel32")]
		public static extern IntPtr VirtualAlloc(UInt32 lpStartAddr,
			 Int32 size, UInt32 flAllocationType, UInt32 flProtect);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
		Int32 dwSize, UInt32 flAllocationType, UInt32 flProtect);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(
		 ProcessAccessFlags processAccess,
		 bool bInheritHandle,
		 int processId
		);

		[DllImport("kernel32.dll")]
		public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, ProcessCreationFlags dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

		[DllImport("kernel32.dll")]
		public static extern uint ResumeThread(IntPtr hThread);

		[DllImport("kernel32.dll")]
		public static extern uint SuspendThread(IntPtr hThread);

		[DllImport("kernel32.dll")]
		public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,
		int dwSize, uint flNewProtect, out uint lpflOldProtect);

		[DllImport("kernel32.dll")]
		public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
	}
}

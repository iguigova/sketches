using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Logging
{
    public enum LogMsgPriority {lmpNone, lmpLow, lmpMedium, lmpHigh }
    public enum LogMsgSeverity {lmsNone, lmsMinor, lmsMajor, lmsCritical, lmsFatal }
    public enum LogMsgGroup {lmgNone, lmgHint, lmgWarning, lmgError, lmgException }
    public class LogMsgCategory
    {
        // http://en.wikipedia.org/wiki/Set_(computer_science); Consider implementing a Bloom map ; ) 

        private const int LOGMSG_MAXCATEGORYID = 100;
        private int[] categoryIDs = new int[LOGMSG_MAXCATEGORYID];

        // Set operations: union, intersection, difference, complement
        // Associative array operations: Add, Reassign, Remove, Lookup
        // Logging operations: Clear, Include, Contains, IsEmpty
        public void Clear()
        {
            int id;
            for (id = 0; id < categoryIDs.Length; id++)
            {
                categoryIDs[id] = 0;
            }
        }

        public void Include(int id)
        {
            if ((id >= 0) && (id < LOGMSG_MAXCATEGORYID))
            {
                categoryIDs[id] = 1; 
            }
        }

        public bool Contains(int id)
        {
            if ((id >= 0) && (id < LOGMSG_MAXCATEGORYID))
            {
                return (categoryIDs[id] == 1);
            }
            return false;
        }

        public bool IsEmpty()
        {            
            int id = 0;
            bool categoryIDFound = false; 
            while ((id < categoryIDs.Length) && (!categoryIDFound))
            {
                categoryIDFound = (categoryIDs[id] == 1);
            }
            return !categoryIDFound; 
        }
    }

    public class LogMessage
    {
        private DateTime timestamp;
        private string source;
        private string userMsg;
        private LogMsgPriority priority;
        private LogMsgSeverity severity;
        private LogMsgGroup group;
        private LogMsgCategory category;
        private string systemMsg;
        private string callStack;
        private Dictionary<string, string> details;

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }
        public string Source
        {
            get
            {
                return source; 
            }
            set
            {
                source = value; 
            }
        }
        public string UserMsg
        {
            get
            {
                return userMsg;
            }
            set
            {
                userMsg = value;
            }
        }
        public LogMsgPriority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }
        public LogMsgSeverity Severity
        {
            get
            {
                return severity;
            }
            set
            {
                severity = value;
            }
        }
        public LogMsgGroup Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }
        public LogMsgCategory Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        public string SystemMsg
        {
            get
            {
                return systemMsg;
            }
            set
            {
                systemMsg = value;
            }
        }
        public string CallStack
        {
            get
            {
                return callStack;
            }
            set
            {
                callStack = value;
            }
        }
        public Dictionary<string, string> Details
        {
            get
            {
                return details;
            }
            set
            {
                details = value;
            }
        }

        public LogMessage(string src, string usrMsg, LogMsgGroup g): this(src, usrMsg, g, LogMsgPriority.lmpLow, LogMsgSeverity.lmsMinor, "", new LogMsgCategory(), "", null) 
        {
 
        }

        public LogMessage(string src, string usrMsg, LogMsgGroup g, LogMsgPriority p, LogMsgSeverity s, string sysMsg, LogMsgCategory cat, string stack, Dictionary<string, string> ds)
        {
            timestamp = DateTime.Now;
            source = src;
            userMsg = usrMsg;
            group = g;
            priority = p;
            severity = s;
            systemMsg = sysMsg;
            category = cat;
            callStack = stack;
            details = ds;
        }

        ~LogMessage()
        {
            // do we need to free / destroy explicitely anything here?
        }

        public bool Match(LogMessage msg)
        {
            return false; 
        }

        public int WM_LOG_MESSAGE = 0;

        private const int HWND_BROADCAST = 0xffff;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int PostMessage(int hwnd, int wMsg, int wParam, int lParam);

        public void Post()
        {
            if (WM_LOG_MESSAGE == 0)
            {
                WM_LOG_MESSAGE = (int)RegisterWindowMessage("LogMsg");
            }
            // http://blogs.msdn.com/oldnewthing/archive/2004/05/05/126427.aspx
            PostMessage(HWND_BROADCAST, WM_LOG_MESSAGE, (int)&this, 0);
        }

    //constructor Create; overload;
    //constructor Create(Src: string; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    //destructor Destroy; override;
    //function Match(Msg: TLogMessage): boolean; 
    //procedure Post; overload;
    //class procedure Post(Src: TObject; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    //class procedure Post(Src: TClass; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    //class procedure Post(Src: string; UsrMsg: string; G: TLogMsgGroup; P: TLogMsgPriority = lmpLow; S:TLogMsgSeverity = lmsMinor; SysMsg: string = ''); overload;
    }
}

<%@ Application Language="C#" %>
<%@ Import Namespace="wojilu.Web" %>
<%@ Import Namespace="wojilu.Web.Mvc" %>
<%@ Import Namespace="wojilu.Web.Jobs" %>
<%@ Import Namespace="wojilu.Web.GlobalApp" %>

<script RunAt="server">

    /// <summary>
    /// Class is called only on the first request
    /// </summary>
    private class AppStart
    {
        static bool _init = false;
        private static Object _lock = new Object();

        /// <summary>
        /// Does nothing after first request
        /// </summary>
        /// <param name="context"></param>
        public static void Start(HttpContext context)
        {
            if (_init)
            {
                return;
            }
            //create class level lock in case multiple sessions start simultaneously
            lock (_lock)
            {
                if (!_init)
                {
                    SystemInfo.Init();
                    MvcFilterLoader.Init();
                }
            }
        }
    }

    protected void Session_Start(object sender, EventArgs e)
    {
        //initializes Cache on first request
        AppStart.Start(HttpContext.Current);
    }
    
    void Application_Start( object sender, EventArgs e ) {
        
        //SystemInfo.Init();
        //MvcFilterLoader.Init();
        //WebJobStarter.Init();
        
    }

    void Application_Error( object sender, EventArgs e ) {
        AppGlobalHelper gh = AppGlobalHelper.New( sender );
        gh.LogError( true );
        gh.MailError();
        gh.ClearError();
    }

    void Application_BeginRequest( object sender, EventArgs e ) {
        SystemInfo.UpdateSessionId();
    }
    
</script>


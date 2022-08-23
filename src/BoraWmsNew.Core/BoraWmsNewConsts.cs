using BoraWmsNew.Debugging;

namespace BoraWmsNew
{
    public class BoraWmsNewConsts
    {
        public const string LocalizationSourceName = "BoraWmsNew";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "806a0b9aa6b64ee5b7e5bd8e52e88cf5";
    }
}
